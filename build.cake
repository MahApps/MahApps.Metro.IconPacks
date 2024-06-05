///////////////////////////////////////////////////////////////////////////////
// TOOLS / ADDINS
///////////////////////////////////////////////////////////////////////////////

#tool dotnet:?package=NuGetKeyVaultSignTool&version=3.2.3
#tool dotnet:?package=AzureSignTool&version=4.0.1
#tool dotnet:?package=GitReleaseManager.Tool&version=0.17.0
#tool dotnet:?package=XamlStyler.Console&version=3.2206.4

#tool vswhere&version=2.8.4

#tool nuget:?package=GitVersion.CommandLine&version=5.12.0
#addin nuget:?package=Cake.Figlet&version=2.0.1

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var verbosity = Argument("verbosity", Verbosity.Minimal);

var PROJECT_DIR = Context.Environment.WorkingDirectory.FullPath + "/";
var PACKAGE_DIR = Directory(Argument("artifact-dir", PROJECT_DIR + "Publish") + "/");

///////////////////////////////////////////////////////////////////////////////
// PREPARATION
///////////////////////////////////////////////////////////////////////////////

var repoName = "MahApps.Metro.IconPacks";
var baseDir = MakeAbsolute(Directory(".")).ToString();
var srcDir = baseDir + "/src";
var solution = srcDir + "/MahApps.Metro.IconPacks.sln";

var styler = Context.Tools.Resolve("xstyler.exe");
var stylerFile = baseDir + "/Settings.XAMLStyler";

var isLocal = BuildSystem.IsLocalBuild;
var isAppVeyorBuild = AppVeyor.IsRunningOnAppVeyor;
var isGitHubActionsBuild = GitHubActions.IsRunningOnGitHubActions;

// Set build version
if (isLocal == false || verbosity == Verbosity.Verbose)
{
    GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.BuildServer });
}

GitVersion gitVersion = GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.Json });

var isPullRequest = (isAppVeyorBuild && AppVeyor.Environment.PullRequest.IsPullRequest) || (isGitHubActionsBuild && GitHubActions.Environment.PullRequest.IsPullRequest);

var branchName = gitVersion.BranchName;
var isDevelopBranch = StringComparer.OrdinalIgnoreCase.Equals("develop", branchName);
var isReleaseBranch = StringComparer.OrdinalIgnoreCase.Equals("main", branchName);

var latestInstallationPath = VSWhereLatest(new VSWhereLatestSettings { IncludePrerelease = false });
var msBuildPath = latestInstallationPath.Combine("./MSBuild/Current/Bin");
var msBuildPathExe = msBuildPath.CombineWithFilePath("./MSBuild.exe");

if (FileExists(msBuildPathExe) == false)
{
    throw new NotImplementedException("You need at least Visual Studio 2019 to build this project.");
}

// Define global marcos.
Action Abort = () => { throw new Exception("a non-recoverable fatal error occurred."); };

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
    if (!IsRunningOnWindows())
    {
        throw new NotImplementedException($"{repoName} will only build on Windows because it's not possible to target WPF and Windows Forms from UNIX.");
    }

    Information(Figlet("MahApps.Metro"));
    Information(Figlet("IconPacks"));

    Information("Informational   Version: {0}", gitVersion.InformationalVersion);
    Information("SemVer          Version: {0}", gitVersion.SemVer);
    Information("AssemblySemVer  Version: {0}", gitVersion.AssemblySemVer);
    Information("MajorMinorPatch Version: {0}", gitVersion.MajorMinorPatch);
    Information("NuGet           Version: {0}", gitVersion.NuGetVersion);
    Information("IsLocalBuild           : {0}", isLocal);
    Information("Branch                 : {0}", branchName);
    Information("Configuration          : {0}", configuration);
    Information("MSBuildPath            : {0}", msBuildPath);
    Information("Publish to             : {0}", PACKAGE_DIR);
});

Teardown(ctx =>
{
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

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
    NuGetRestore(solution, new NuGetRestoreSettings { MSBuildPath = msBuildPath.ToString() });
    //DotNetCoreRestore(solution);
});

Task("Build")
    .Does(() =>
{
    EnsureDirectoryExists(PACKAGE_DIR);

    var msBuildSettings = new MSBuildSettings {
        Verbosity = verbosity
        , ToolPath = msBuildPathExe
        , Configuration = configuration
        , ArgumentCustomization = args => args.Append("/m").Append("/nr:false") // The /nr switch tells msbuild to quite once it�s done
        , BinaryLogger = new MSBuildBinaryLogSettings() { Enabled = isLocal }
    };
    MSBuild(solution, msBuildSettings
            .SetMaxCpuCount(0)
            .WithProperty("GeneratePackageOnBuild", "true")
            .WithProperty("PackageOutputPath", MakeAbsolute(PACKAGE_DIR).ToString())
            .WithProperty("RepositoryBranch", branchName)
            .WithProperty("RepositoryCommit", gitVersion.Sha)
            .WithProperty("Version", isReleaseBranch ? gitVersion.MajorMinorPatch : gitVersion.NuGetVersion)
            .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
            .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
            .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
            .WithProperty("ContinuousIntegrationBuild", isReleaseBranch ? "true" : "false")
            );
});

void SignFiles(IEnumerable<FilePath> files, string description, string repoUrl)
{
    var vurl = EnvironmentVariable("azure-key-vault-url");
    if(string.IsNullOrWhiteSpace(vurl)) {
        Error("Could not resolve signing url.");
        return;
    }

    var vcid = EnvironmentVariable("azure-key-vault-client-id");
    if(string.IsNullOrWhiteSpace(vcid)) {
        Error("Could not resolve signing client id.");
        return;
    }

    var vctid = EnvironmentVariable("azure-key-vault-tenant-id");
    if(string.IsNullOrWhiteSpace(vctid)) {
        Error("Could not resolve signing client tenant id.");
        return;
    }

    var vcs = EnvironmentVariable("azure-key-vault-client-secret");
    if(string.IsNullOrWhiteSpace(vcs)) {
        Error("Could not resolve signing client secret.");
        return;
    }

    var vc = EnvironmentVariable("azure-key-vault-certificate");
    if(string.IsNullOrWhiteSpace(vc)) {
        Error("Could not resolve signing certificate.");
        return;
    }

    var filesToSign = string.Join(" ", files.Select(f => MakeAbsolute(f).FullPath));

    var processSettings = new ProcessSettings {
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        Arguments = new ProcessArgumentBuilder()
            .Append("sign")
            .Append(filesToSign)
            .AppendSwitchQuoted("--file-digest", "sha256")
            .AppendSwitchQuoted("--description", description)
            .AppendSwitchQuoted("--description-url", repoUrl)
            .Append("--no-page-hashing")
            .AppendSwitchQuoted("--timestamp-rfc3161", "http://timestamp.digicert.com")
            .AppendSwitchQuoted("--timestamp-digest", "sha256")
            .AppendSwitchQuoted("--azure-key-vault-url", vurl)
            .AppendSwitchQuotedSecret("--azure-key-vault-client-id", vcid)
            .AppendSwitchQuotedSecret("--azure-key-vault-tenant-id", vctid)
            .AppendSwitchQuotedSecret("--azure-key-vault-client-secret", vcs)
            .AppendSwitchQuotedSecret("--azure-key-vault-certificate", vc)
    };

    using(var process = StartAndReturnProcess("tools/AzureSignTool", processSettings))
    {
        process.WaitForExit();

        if (process.GetStandardOutput().Any())
        {
            Information($"Output:{Environment.NewLine}{string.Join(Environment.NewLine, process.GetStandardOutput())}");
        }

        if (process.GetStandardError().Any())
        {
            Information($"Errors occurred:{Environment.NewLine}{string.Join(Environment.NewLine, process.GetStandardError())}");
        }

        // This should output 0 as valid arguments supplied
        Information("Exit code: {0}", process.GetExitCode());
    }
}

void SignNuGet(string publishDir)
{
    if (!DirectoryExists(Directory(publishDir)))
    {
        return;
    }

    var vurl = EnvironmentVariable("azure-key-vault-url");
    if(string.IsNullOrWhiteSpace(vurl)) {
        Error("Could not resolve signing url.");
        return;
    }

    var vcid = EnvironmentVariable("azure-key-vault-client-id");
    if(string.IsNullOrWhiteSpace(vcid)) {
        Error("Could not resolve signing client id.");
        return;
    }

    var vcs = EnvironmentVariable("azure-key-vault-client-secret");
    if(string.IsNullOrWhiteSpace(vcs)) {
        Error("Could not resolve signing client secret.");
        return;
    }

    var vc = EnvironmentVariable("azure-key-vault-certificate");
    if(string.IsNullOrWhiteSpace(vc)) {
        Error("Could not resolve signing certificate.");
        return;
    }

    var nugetFiles = GetFiles(publishDir + "/*.nupkg");
    foreach(var file in nugetFiles)
    {
        Information($"Sign file: {file}");
        var processSettings = new ProcessSettings {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            Arguments = new ProcessArgumentBuilder()
                .Append("sign")
                .Append(MakeAbsolute(file).FullPath)
                .Append("--force")
                .AppendSwitchQuoted("--file-digest", "sha256")
                .AppendSwitchQuoted("--timestamp-rfc3161", "http://timestamp.digicert.com")
                .AppendSwitchQuoted("--timestamp-digest", "sha256")
                .AppendSwitchQuoted("--azure-key-vault-url", vurl)
                .AppendSwitchQuotedSecret("--azure-key-vault-client-id", vcid)
                .AppendSwitchQuotedSecret("--azure-key-vault-client-secret", vcs)
                .AppendSwitchQuotedSecret("--azure-key-vault-certificate", vc)
        };

        using(var process = StartAndReturnProcess("tools/NuGetKeyVaultSignTool", processSettings))
        {
            process.WaitForExit();

            if (process.GetStandardOutput().Any())
            {
                Information($"Output:{Environment.NewLine}{string.Join(Environment.NewLine, process.GetStandardOutput())}");
            }

            if (process.GetStandardError().Any())
            {
                Information($"Errors occurred:{Environment.NewLine}{string.Join(Environment.NewLine, process.GetStandardError())}");
            }

            // This should output 0 as valid arguments supplied
            Information("Exit code: {0}", process.GetExitCode());
        }
    }
}

Task("Sign")
    .WithCriteria(() => !isPullRequest)
    .ContinueOnError()
    .Does(() =>
{
    SignNuGet(MakeAbsolute(PACKAGE_DIR).ToString());
});

Task("StyleXaml")
    .Description("Ensures XAML Formatting is Clean")
    .Does(() =>
{
    Func<IFileSystemInfo, bool> exclude_Dir =
        fileSystemInfo => !fileSystemInfo.Path.Segments.Contains("obj");

    var files = GetFiles(srcDir + "/**/*.xaml", new GlobberSettings { Predicate = exclude_Dir });
    Information("\nChecking " + files.Count() + " file(s) for XAML Structure");
    StartProcess(styler, "-f \"" + string.Join(",", files.Select(f => f.ToString())) + "\" -c \"" + stylerFile + "\"");
});

Task("CreateRelease")
    .WithCriteria(() => !isPullRequest)
    .Does(() =>
{
    var token = EnvironmentVariable("GITHUB_TOKEN");
    if (string.IsNullOrEmpty(token))
    {
        throw new Exception("The GITHUB_TOKEN environment variable is not defined.");
    }

    GitReleaseManagerCreate(token, "MahApps", repoName, new GitReleaseManagerCreateSettings {
        Milestone         = gitVersion.MajorMinorPatch,
        Name              = gitVersion.AssemblySemFileVer,
        Prerelease        = isDevelopBranch,
        TargetCommitish   = branchName,
        WorkingDirectory  = "."
    });
});

///////////////////////////////////////////////////////////////////////////////
// TASK TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
//    .IsDependentOn("StyleXaml")
    .IsDependentOn("Build")
    ;

Task("ci")
    .IsDependentOn("Default")
    .IsDependentOn("Sign")
    ;

Task("azure")
    .IsDependentOn("Default")
    ;

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
