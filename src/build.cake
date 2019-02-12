//////////////////////////////////////////////////////////////////////
// TOOLS / ADDINS
//////////////////////////////////////////////////////////////////////

#tool GitVersion.CommandLine
#tool gitreleasemanager
#tool vswhere
#addin Cake.Figlet

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
if (string.IsNullOrWhiteSpace(target))
{
    target = "Default";
}

var configuration = Argument("configuration", "Release");
if (string.IsNullOrWhiteSpace(configuration))
{
    configuration = "Release";
}

var verbosity = Argument("verbosity", Verbosity.Normal);
if (string.IsNullOrWhiteSpace(configuration))
{
    verbosity = Verbosity.Minimal;
}

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var repoName = "MahApps.Metro.IconPacks";
var local = BuildSystem.IsLocalBuild;

// Set build version
if (local == false
    || verbosity == Verbosity.Verbose)
{
    GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.BuildServer });
}
GitVersion gitVersion = GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.Json });

var msBuildPath = VSWhereLatest(new VSWhereLatestSettings { IncludePrerelease = true }).Combine("./MSBuild/Current/Bin");
var msBuildPathExe = msBuildPath.CombineWithFilePath("./MSBuild.exe");

var msBuild2017Path = VSWhereLatest(new VSWhereLatestSettings { IncludePrerelease = false }).Combine("./MSBuild/15.0/Bin");
var msBuild2017PathExe = msBuild2017Path.CombineWithFilePath("./MSBuild.exe");

if (FileExists(msBuildPathExe) == false)
{
    throw new NotImplementedException("You need at least Visual Studio 2019 to build this project.");
}

// Should MSBuild treat any errors as warnings?
var treatWarningsAsErrors = false;

var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;
var branchName = gitVersion.BranchName;
var isDevelopBranch = StringComparer.OrdinalIgnoreCase.Equals("dev", branchName);
var isReleaseBranch = StringComparer.OrdinalIgnoreCase.Equals("master", branchName);
var isTagged = AppVeyor.Environment.Repository.Tag.IsTag;

// Directories and Paths
var solution = "./MahApps.Metro.IconPacks.sln";
var coreSolution = "./MahApps.Metro.IconPacks.Wpf.sln";
var uwpSolution = "./MahApps.Metro.IconPacks.Windows10.sln";
var publishDir = "./Publish";

// Define global marcos.
Action Abort = () => { throw new Exception("a non-recoverable fatal error occurred."); };

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
    if (!IsRunningOnWindows())
    {
        throw new NotImplementedException($"{repoName} will only build on Windows because it's not possible to target WPF and Windows Forms from UNIX.");
    }

    Information(Figlet(repoName));

    Information("Informational Version  : {0}", gitVersion.InformationalVersion);
    Information("SemVer Version         : {0}", gitVersion.SemVer);
    Information("AssemblySemVer Version : {0}", gitVersion.AssemblySemVer);
    Information("MajorMinorPatch Version: {0}", gitVersion.MajorMinorPatch);
    Information("NuGet Version          : {0}", gitVersion.NuGetVersion);
    Information("IsLocalBuild           : {0}", local);
    Information("Branch                 : {0}", branchName);
    Information("Configuration          : {0}", configuration);
    Information("MSBuildPath            : {0}", msBuildPath);
});

Teardown(context =>
{
    // Executed AFTER the last task.
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .ContinueOnError()
    .Does(() =>
{
    var directoriesToDelete = GetDirectories("./**/obj").Concat(GetDirectories("./**/bin")).Concat(GetDirectories("./**/Publish"));
    DeleteDirectories(directoriesToDelete, new DeleteDirectorySettings { Recursive = true, Force = true });
});

Task("Restore")
    .Does(() =>
{
    var msBuildSettings = new MSBuildSettings {
        Verbosity = verbosity
        , ToolPath = msBuild2017PathExe
        , ToolVersion = MSBuildToolVersion.VS2017
        , Configuration = configuration
        , PlatformTarget = PlatformTarget.MSIL
        , ArgumentCustomization = args => args.Append("/m")
    };
    MSBuild(uwpSolution, msBuildSettings.WithTarget("restore"));
    // DotNetCoreRestore(uwpSolution);
    // NuGetRestore(uwpSolution, new NuGetRestoreSettings {
    //     NoCache = true
    //     });
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    var msBuildSettings = new MSBuildSettings {
        Verbosity = verbosity
        , ToolPath = msBuild2017PathExe
        , ToolVersion = MSBuildToolVersion.VS2017
        , Configuration = configuration
        , PlatformTarget = PlatformTarget.MSIL
        , ArgumentCustomization = args => args.Append("/m")
    };
    MSBuild(uwpSolution, msBuildSettings
            .SetMaxCpuCount(0)
            .WithProperty("Version", isReleaseBranch ? gitVersion.MajorMinorPatch : gitVersion.NuGetVersion)
            .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
            .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
            .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
            );
});

Task("CoreBuild")
    .Does(() =>
{
    var msBuildSettings = new DotNetCoreMSBuildSettings
        {
            MaxCpuCount = -1
        };

    DotNetCoreBuild(coreSolution, new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            MSBuildSettings = msBuildSettings
                .WithProperty("Version", isReleaseBranch ? gitVersion.MajorMinorPatch : gitVersion.NuGetVersion)
                .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
                .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
                .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
        });
});

Task("Zip")
    .Does(() =>
{
    EnsureDirectoryExists(Directory(publishDir));
    Zip($"./MahApps.Metro.IconPacks.Browser/bin/{configuration}/", $"{publishDir}/IconPacks.Browser.{configuration}-v" + gitVersion.NuGetVersion + ".zip");
});

Task("Pack")
    .ContinueOnError()
    .Does(() =>
{
    EnsureDirectoryExists(Directory(publishDir));

    var msBuildSettings = new MSBuildSettings {
        Verbosity = verbosity,
        ToolPath = msBuildPathExe,
        ToolVersion = MSBuildToolVersion.VS2019,
        Configuration = configuration
    };

    var projects = GetFiles("./MahApps.Metro.IconPacks/*.csproj");

    foreach(var project in projects)
    {
        Information("Packing {0}", project);

        DeleteFiles(GetFiles("./MahApps.Metro.IconPacks/obj/**/*.nuspec"));

        MSBuild(project, msBuildSettings
            .WithTarget("pack")
            .WithProperty("PackageOutputPath", "../" + publishDir)
            .WithProperty("RepositoryBranch", branchName)
            .WithProperty("RepositoryCommit", gitVersion.Sha)
            .WithProperty("Version", isReleaseBranch ? gitVersion.MajorMinorPatch : gitVersion.NuGetVersion)
            .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
            .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
            .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
        );
    }
});

Task("CreateRelease")
    .WithCriteria(() => !isTagged)
    .WithCriteria(() => !isPullRequest)
    .Does(() =>
{
    var username = EnvironmentVariable("GITHUB_USERNAME");
    if (string.IsNullOrEmpty(username))
    {
        throw new Exception("The GITHUB_USERNAME environment variable is not defined.");
    }

    var token = EnvironmentVariable("GITHUB_TOKEN");
    if (string.IsNullOrEmpty(token))
    {
        throw new Exception("The GITHUB_TOKEN environment variable is not defined.");
    }

    GitReleaseManagerCreate(username, token, "MahApps", repoName, new GitReleaseManagerCreateSettings {
        Milestone         = gitVersion.MajorMinorPatch,
        Name              = gitVersion.AssemblySemFileVer,
        Prerelease        = isDevelopBranch,
        TargetCommitish   = branchName,
        WorkingDirectory  = "../"
    });
});

// Task Targets
Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Build")
    .IsDependentOn("CoreBuild")
    .IsDependentOn("Zip");

Task("appveyor")
    .IsDependentOn("Clean")
    .IsDependentOn("Build")
    .IsDependentOn("CoreBuild")
    .IsDependentOn("Zip")
    .IsDependentOn("Pack");

// Execution
RunTarget(target);