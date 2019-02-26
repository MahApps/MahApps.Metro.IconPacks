
//////////////////////////////////////////////////////////////////////
// TOOLS / ADDINS
//////////////////////////////////////////////////////////////////////

#tool nuget:?package=GitVersion.CommandLine&prerelease
#tool nuget:?package=gitreleasemanager&version=0.8
#tool nuget:?package=vswhere&version=2.6.7
#addin nuget:?package=Cake.Figlet&version=1.2

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var verbosity = Argument("verbosity", Verbosity.Minimal);

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var repoName = "MahApps.Metro.IconPacks";
var isLocal = BuildSystem.IsLocalBuild;

// Set build version
if (isLocal == false || verbosity == Verbosity.Verbose)
{
    GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.BuildServer });
}
GitVersion gitVersion = GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.Json });

// var latestInstallationPath = VSWhereLatest(new VSWhereLatestSettings { IncludePrerelease = true });
// var msBuildPath = latestInstallationPath.Combine("./MSBuild/Current/Bin");
// var msBuildPathExe = msBuildPath.CombineWithFilePath("./MSBuild.exe");

var latestInstallationPath = VSWhereLatest(new VSWhereLatestSettings { IncludePrerelease = true });
var msBuildPath = latestInstallationPath.CombineWithFilePath("./MSBuild/Current/Bin/MSBuild.exe");

// var latestInstallationPath = VSWhereLatest();
// var msBuildPath = latestInstallationPath.CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");

var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;
var branchName = gitVersion.BranchName;
var isDevelopBranch = StringComparer.OrdinalIgnoreCase.Equals("dev", branchName);
var isReleaseBranch = StringComparer.OrdinalIgnoreCase.Equals("master", branchName);
var isTagged = AppVeyor.Environment.Repository.Tag.IsTag;

// Directories and Paths
var solution = "./MahApps.Metro.IconPacks.sln";
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
    Information("IsLocalBuild           : {0}", isLocal);
    Information("Branch                 : {0}", branchName);
    Information("Configuration          : {0}", configuration);
    Information("MSBuild                : {0}", msBuildPath);
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
    var directoriesToDelete = GetDirectories("./**/obj")
        .Concat(GetDirectories("./**/bin"))
        .Concat(GetDirectories("./**/Publish"))
        .Concat(GetDirectories("./**/output"));
    DeleteDirectories(directoriesToDelete, new DeleteDirectorySettings { Recursive = true, Force = true });
});

Task("Restore")
    .Does(() =>
{
    var msBuildSettings = new MSBuildSettings {
        ToolPath = msBuildPath
        , ArgumentCustomization = args => args.Append("/m")
        , BinaryLogger = new MSBuildBinaryLogSettings() { Enabled = isLocal }
        };
    MSBuild(solution, msBuildSettings
            .SetConfiguration(configuration)
            .SetVerbosity(verbosity)
            .WithTarget("restore")
            );
});

Task("Build")
  .Does(() =>
{
    var msBuildSettings = new MSBuildSettings {
        ToolPath = msBuildPath
        , ArgumentCustomization = args => args.Append("/m")
        , BinaryLogger = new MSBuildBinaryLogSettings() { Enabled = isLocal }
        };
    MSBuild(solution, msBuildSettings
            .SetMaxCpuCount(0)
            .SetConfiguration(configuration)
            .SetVerbosity(verbosity)
            .WithProperty("Version", isReleaseBranch ? gitVersion.MajorMinorPatch : gitVersion.NuGetVersion)
            .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
            .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
            .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
            );
});

Task("Zip")
  .Does(() =>
{
  EnsureDirectoryExists(Directory(publishDir));
  Zip($"./MahApps.Metro.IconPacks.Browser/bin/{configuration}/", $"{publishDir}/IconPacks.Browser.{configuration}-v" + gitVersion.NuGetVersion + ".zip");
});

Task("Pack")
  .WithCriteria(() => !isPullRequest)
    .Does(() =>
{
  EnsureDirectoryExists(Directory(publishDir));

  var msBuildSettings = new MSBuildSettings { ToolPath = msBuildPath };
 
  var projects = GetFiles("./MahApps.Metro.IconPacks/*.csproj");

  foreach(var project in projects)
  {
    Information("Packing {0}...", project);
    
    MSBuild(project, msBuildSettings
        .SetConfiguration(configuration)
        .SetVerbosity(Verbosity.Normal)
        .WithTarget("pack")
        .WithProperty("PackageOutputPath", "../" + publishDir)
        .WithProperty("RepositoryBranch", branchName)
        .WithProperty("RepositoryCommit", gitVersion.Sha)
        .WithProperty("Version", isReleaseBranch ? gitVersion.MajorMinorPatch : gitVersion.NuGetVersion)
        .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
        .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
        .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
    );

    var nuspecFiles = GetFiles("./MahApps.Metro.IconPacks/obj/**/*.nuspec");
    if (isLocal)
    {
        CopyFiles(nuspecFiles, publishDir);
    }
    DeleteFiles(nuspecFiles);
  }

});

Task("CreateRelease")
    .WithCriteria(() => !isTagged)
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
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Zip");

Task("appveyor")
    .IsDependentOn("Default")
    .IsDependentOn("Pack");

// Execution
RunTarget(target);