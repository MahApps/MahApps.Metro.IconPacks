
//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////

#tool "nuget:?package=GitVersion.CommandLine&version=3.6.5"

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

// Build configuration
var msbuildtoolversion = MSBuildToolVersion.VS2017;

var local = BuildSystem.IsLocalBuild;
var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;
var isDevelopBranch = StringComparer.OrdinalIgnoreCase.Equals("dev", AppVeyor.Environment.Repository.Branch);
var isReleaseBranch = StringComparer.OrdinalIgnoreCase.Equals("master", AppVeyor.Environment.Repository.Branch);
var isTagged = AppVeyor.Environment.Repository.Tag?.IsTag;

// Version
GitVersion(new GitVersionSettings { OutputType = GitVersionOutput.BuildServer });
var gitVersion = GitVersion(new GitVersionSettings { UpdateAssemblyInfo = true,  UpdateAssemblyInfoFilePath = "../GlobalAssemblyInfo.cs" });
var majorMinorPatch = gitVersion.MajorMinorPatch;
var informationalVersion = gitVersion.InformationalVersion;
var nugetVersion = isReleaseBranch ? gitVersion.AssemblySemVer : gitVersion.NuGetVersion;
var buildVersion = gitVersion.FullBuildMetaData;

var browserVersion = "1.5.0";

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

    Information("Building version {0} of MahApps.Metro.IconPacks. (isTagged: {1})", informationalVersion, isTagged);
});

Teardown(context =>
{
    // Executed AFTER the last task.
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

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

Task("Build")
  .Does(() =>
{
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release").UseToolVersion(msbuildtoolversion));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET45").UseToolVersion(msbuildtoolversion));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET46").UseToolVersion(msbuildtoolversion));
  MSBuild("../MahApps.Metro.IconPacks.Browser.sln", settings => settings.SetConfiguration("Release").UseToolVersion(msbuildtoolversion));
});

Task("BuildAll")
  .Does(() =>
{
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Debug").UseToolVersion(msbuildtoolversion));
  MSBuild("../MahApps.Metro.IconPacks.Browser.sln", settings => settings.SetConfiguration("Debug").UseToolVersion(msbuildtoolversion));

  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release").UseToolVersion(msbuildtoolversion));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET45").UseToolVersion(msbuildtoolversion));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET46").UseToolVersion(msbuildtoolversion));
  MSBuild("../MahApps.Metro.IconPacks.Browser.sln", settings => settings.SetConfiguration("Release").UseToolVersion(msbuildtoolversion));
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

Task("CleanOutput")
  .ContinueOnError()
  .Does(() =>
{
  CleanDirectories("../bin");
  DeleteFiles("./IconPacks.Browser*.zip");
  DeleteFiles("./MahApps.Metro.IconPacks*.nupkg");
});

// Task Targets
Task("Default").IsDependentOn("CleanOutput")
               .IsDependentOn("UpdateAssemblyInfo")
               .IsDependentOn("Build")
               .IsDependentOn("ZipRelease")
               .IsDependentOn("NuGetPack");
Task("dev").IsDependentOn("CleanOutput")
           .IsDependentOn("UpdateAssemblyInfo")
           .IsDependentOn("BuildAll")
           .IsDependentOn("ZipDebug").IsDependentOn("ZipRelease")
           .IsDependentOn("NuGetPack");

// Execution
RunTarget(target);