
//////////////////////////////////////////////////////////////////////
// TOOLS / ADDINS
//////////////////////////////////////////////////////////////////////

#tool "nuget:?package=GitVersion.CommandLine&prerelease"
#tool "nuget:?package=gitreleasemanager"
#tool "nuget:?package=vswhere"
#addin "nuget:?package=Cake.Figlet"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
if (string.IsNullOrWhiteSpace(target))
{
    target = "Default";
}

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var msBuildPath = VSWhereLatest().CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");

// Should MSBuild treat any errors as warnings?
var treatWarningsAsErrors = false;

// Build configuration
var local = BuildSystem.IsLocalBuild;
var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;
var isDevelopBranch = StringComparer.OrdinalIgnoreCase.Equals("dev", AppVeyor.Environment.Repository.Branch);
var isReleaseBranch = StringComparer.OrdinalIgnoreCase.Equals("master", AppVeyor.Environment.Repository.Branch);
var isTagged = AppVeyor.Environment.Repository.Tag.IsTag;

// Version
GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.BuildServer });
var gitVersion = GitVersion(new GitVersionSettings { UpdateAssemblyInfo = true,  UpdateAssemblyInfoFilePath = "../GlobalAssemblyInfo.cs" });
var majorMinorPatch = gitVersion.MajorMinorPatch;
var informationalVersion = gitVersion.InformationalVersion;
var nugetVersion = isReleaseBranch ? gitVersion.MajorMinorPatch : gitVersion.NuGetVersion;
var buildVersion = gitVersion.FullBuildMetaData;

var browserVersion = "1.5.0";

// Directories and Paths
var buildDir = "../bin";
var iconPacksSolution = "../MahApps.Metro.IconPacks.sln";
var browserSolution = "../MahApps.Metro.IconPacks.Browser.sln";

// Define global marcos.
Action Abort = () => { throw new Exception("a non-recoverable fatal error occurred."); };

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
    if (!IsRunningOnWindows())
    {
        throw new NotImplementedException("MahApps.Metro.IconPacks will only build on Windows because it's not possible to target WPF and Windows Forms from UNIX.");
    }

    Information("Informational Version  : {0}", gitVersion.InformationalVersion);
    Information("SemVer Version         : {0}", gitVersion.SemVer);
    Information("AssemblySemVer Version : {0}", gitVersion.AssemblySemVer);
    Information("MajorMinorPatch Version: {0}", gitVersion.MajorMinorPatch);
    Information("NuGet Version          : {0}", gitVersion.NuGetVersion);
    Information("IsLocalBuild           : {0}", local);

    Information(Figlet("MahApps.Metro.IconPacks"));
});

Teardown(context =>
{
    // Executed AFTER the last task.
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("CleanOutput")
  .ContinueOnError()
  .Does(() =>
{
  CleanDirectory(Directory(buildDir));
  DeleteFiles("./IconPacks.Browser*.zip");
  DeleteFiles("./MahApps.Metro.IconPacks*.nupkg");
});

Task("UpdateAssemblyInfo")
  .Does(() =>
{
  var assemblyInfo = ParseAssemblyInfo("../MahApps.Metro.IconPacks.Browser/Properties/GlobalAssemblyInfo.cs");
  var newAssemblyInfoSettings = new AssemblyInfoSettings {
    Version = browserVersion,
    FileVersion = browserVersion,
    InformationalVersion = browserVersion
  };
  CreateAssemblyInfo("../MahApps.Metro.IconPacks.Browser/Properties/GlobalAssemblyInfo.cs", newAssemblyInfoSettings);
});

Task("Restore")
  .Does(() =>
{
  var msBuildSettings = new MSBuildSettings() { ToolPath = msBuildPath };
  MSBuild(iconPacksSolution, msBuildSettings.WithTarget("restore").SetVerbosity(Verbosity.Minimal));
});

Task("Build")
  .Does(() =>
{
  var msBuildSettings = new MSBuildSettings() { ToolPath = msBuildPath };
  Information("Build: Release");
  MSBuild(iconPacksSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Release"));
  Information("Build: Release_NET45");
  MSBuild(iconPacksSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Release_NET45"));
  Information("Build: Release_NET46");
  MSBuild(iconPacksSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Release_NET46"));
  Information("Build: Browser Release");
  MSBuild(browserSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Release"));
});

Task("BuildAll")
  .Does(() =>
{
  var msBuildSettings = new MSBuildSettings() { ToolPath = msBuildPath };
  Information("Build: Debug");
  MSBuild(iconPacksSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Debug"));
  Information("Build: Browser Debug");
  MSBuild(browserSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Debug"));

  Information("Build: Release");
  MSBuild(iconPacksSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Release"));
  Information("Build: Release_NET45");
  MSBuild(iconPacksSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Release_NET45"));
  Information("Build: Release_NET46");
  MSBuild(iconPacksSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Release_NET46"));
  Information("Build: Browser Release");
  MSBuild(browserSolution, msBuildSettings.SetMaxCpuCount(0).SetVerbosity(Verbosity.Minimal).SetConfiguration("Release"));
});

Task("ZipDebug")
  .Does(() =>
{
  Zip("..\\bin\\MahApps.Metro.IconPacks.Browser\\Debug\\", "IconPacks.Browser.Debug.v" + nugetVersion + ".zip");
});

Task("ZipRelease")
  .Does(() =>
{
  Zip("..\\bin\\MahApps.Metro.IconPacks.Browser\\Release\\", "IconPacks.Browser.v" + nugetVersion + ".zip");
});

Task("NuGetPack")
  .WithCriteria(() => !isPullRequest)
  .Does(() =>
{
  var iconPacksNuGet = "MahApps.Metro.IconPacks";
  var nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "..\\bin",
    Id                      = iconPacksNuGet,
    Version                 = nugetVersion,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro {0}", DateTime.Now.Year),
    Files                   = new [] {
       new NuSpecContent {Source = string.Format("Release\\{0}.dll", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.pdb", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.XML", iconPacksNuGet), Target = "lib\\net40\\"},
       
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.dll", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.pdb", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.XML", iconPacksNuGet), Target = "lib\\net45\\"},

       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.dll", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.pdb", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.XML", iconPacksNuGet), Target = "lib\\net46\\"},

       new NuSpecContent {Source = "Release_UWP\\IconPacks\\**\\*.*", Target = "lib\\uap10.0\\"},
    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.Entypo";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "..\\bin",
    Id                      = iconPacksNuGet,
    Version                 = nugetVersion,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro {0}", DateTime.Now.Year),
    Files                   = new [] {
       new NuSpecContent {Source = string.Format("Release\\{0}.dll", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.pdb", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.XML", iconPacksNuGet), Target = "lib\\net40\\"},
       
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.dll", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.pdb", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.XML", iconPacksNuGet), Target = "lib\\net45\\"},

       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.dll", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.pdb", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.XML", iconPacksNuGet), Target = "lib\\net46\\"},

       new NuSpecContent {Source = "Release_UWP\\IconPacksEntypo\\**\\*.*", Target = "lib\\uap10.0\\"},
	}
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.FontAwesome";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "..\\bin",
    Id                      = iconPacksNuGet,
    Version                 = nugetVersion,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro {0}", DateTime.Now.Year),
    Files                   = new [] {
       new NuSpecContent {Source = string.Format("Release\\{0}.dll", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.pdb", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.XML", iconPacksNuGet), Target = "lib\\net40\\"},
       
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.dll", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.pdb", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.XML", iconPacksNuGet), Target = "lib\\net45\\"},

       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.dll", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.pdb", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.XML", iconPacksNuGet), Target = "lib\\net46\\"},

       new NuSpecContent {Source = "Release_UWP\\IconPacksFontAwesome\\**\\*.*", Target = "lib\\uap10.0\\"},
    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.Material";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "..\\bin",
    Id                      = iconPacksNuGet,
    Version                 = nugetVersion,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro {0}", DateTime.Now.Year),
    Files                   = new [] {
       new NuSpecContent {Source = string.Format("Release\\{0}.dll", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.pdb", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.XML", iconPacksNuGet), Target = "lib\\net40\\"},
       
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.dll", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.pdb", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.XML", iconPacksNuGet), Target = "lib\\net45\\"},

       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.dll", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.pdb", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.XML", iconPacksNuGet), Target = "lib\\net46\\"},

       new NuSpecContent {Source = "Release_UWP\\IconPacksMaterial\\**\\*.*", Target = "lib\\uap10.0\\"},
    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.MaterialLight";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "..\\bin",
    Id                      = iconPacksNuGet,
    Version                 = nugetVersion,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro {0}", DateTime.Now.Year),
    Files                   = new [] {
       new NuSpecContent {Source = string.Format("Release\\{0}.dll", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.pdb", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.XML", iconPacksNuGet), Target = "lib\\net40\\"},
       
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.dll", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.pdb", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.XML", iconPacksNuGet), Target = "lib\\net45\\"},

       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.dll", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.pdb", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.XML", iconPacksNuGet), Target = "lib\\net46\\"},

       new NuSpecContent {Source = "Release_UWP\\IconPacksMaterialLight\\**\\*.*", Target = "lib\\uap10.0\\"},
    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.Modern";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "..\\bin",
    Id                      = iconPacksNuGet,
    Version                 = nugetVersion,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro {0}", DateTime.Now.Year),
    Files                   = new [] {
       new NuSpecContent {Source = string.Format("Release\\{0}.dll", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.pdb", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.XML", iconPacksNuGet), Target = "lib\\net40\\"},
       
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.dll", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.pdb", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.XML", iconPacksNuGet), Target = "lib\\net45\\"},

       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.dll", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.pdb", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.XML", iconPacksNuGet), Target = "lib\\net46\\"},

       new NuSpecContent {Source = "Release_UWP\\IconPacksModern\\**\\*.*", Target = "lib\\uap10.0\\"},
    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.Octicons";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "..\\bin",
    Id                      = iconPacksNuGet,
    Version                 = nugetVersion,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro {0}", DateTime.Now.Year),
    Files                   = new [] {
       new NuSpecContent {Source = string.Format("Release\\{0}.dll", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.pdb", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.XML", iconPacksNuGet), Target = "lib\\net40\\"},
       
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.dll", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.pdb", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.XML", iconPacksNuGet), Target = "lib\\net45\\"},

       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.dll", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.pdb", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.XML", iconPacksNuGet), Target = "lib\\net46\\"},

       new NuSpecContent {Source = "Release_UWP\\IconPacksOcticons\\**\\*.*", Target = "lib\\uap10.0\\"},
    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.SimpleIcons";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "..\\bin",
    Id                      = iconPacksNuGet,
    Version                 = nugetVersion,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro {0}", DateTime.Now.Year),
    Files                   = new [] {
       new NuSpecContent {Source = string.Format("Release\\{0}.dll", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.pdb", iconPacksNuGet), Target = "lib\\net40\\"},
       new NuSpecContent {Source = string.Format("Release\\{0}.XML", iconPacksNuGet), Target = "lib\\net40\\"},
       
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.dll", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.pdb", iconPacksNuGet), Target = "lib\\net45\\"},
       new NuSpecContent {Source = string.Format("Release_NET45\\{0}.XML", iconPacksNuGet), Target = "lib\\net45\\"},

       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.dll", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.pdb", iconPacksNuGet), Target = "lib\\net46\\"},
       new NuSpecContent {Source = string.Format("Release_NET46\\{0}.XML", iconPacksNuGet), Target = "lib\\net46\\"},

       new NuSpecContent {Source = "Release_UWP\\IconPacksSimpleIcons\\**\\*.*", Target = "lib\\uap10.0\\"},
    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);
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

    GitReleaseManagerCreate(username, token, "MahApps", "MahApps.Metro.IconPacks", new GitReleaseManagerCreateSettings {
        Milestone         = gitVersion.MajorMinorPatch,
        Name              = gitVersion.MajorMinorPatch,
        Prerelease        = false,
        TargetCommitish   = "master",
        WorkingDirectory  = "../../"
    });
});

// Task Targets
Task("Default").IsDependentOn("CleanOutput")
               .IsDependentOn("UpdateAssemblyInfo")
               .IsDependentOn("Restore")
               .IsDependentOn("Build")
               .IsDependentOn("ZipRelease")
               .IsDependentOn("NuGetPack");
Task("dev").IsDependentOn("CleanOutput")
           .IsDependentOn("UpdateAssemblyInfo")
           .IsDependentOn("Restore")
           .IsDependentOn("BuildAll")
           .IsDependentOn("ZipDebug").IsDependentOn("ZipRelease")
           .IsDependentOn("NuGetPack");

// Execution
RunTarget(target);