
//////////////////////////////////////////////////////////////////////
// TOOLS / ADDINS
//////////////////////////////////////////////////////////////////////

#tool paket:?package=GitVersion.CommandLine
#tool paket:?package=gitreleasemanager
#tool paket:?package=vswhere
#addin paket:?package=Cake.Figlet
#addin paket:?package=Cake.Paket

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
    verbosity = Verbosity.Normal;
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

var latestInstallationPath = VSWhereProducts("*", new VSWhereProductSettings { Version = "[\"15.0\",\"16.0\"]" }).FirstOrDefault();
var msBuildPath = latestInstallationPath.CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");

// Should MSBuild treat any errors as warnings?
var treatWarningsAsErrors = false;

var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;
var branchName = gitVersion.BranchName;
var isDevelopBranch = StringComparer.OrdinalIgnoreCase.Equals("dev", branchName);
var isReleaseBranch = StringComparer.OrdinalIgnoreCase.Equals("master", branchName);
var isTagged = AppVeyor.Environment.Repository.Tag.IsTag;

// Version
var nugetVersion = isReleaseBranch ? gitVersion.MajorMinorPatch : gitVersion.NuGetVersion;
var browserVersion = "1.5.0";

// Directories and Paths
var iconPacksSolution = "./MahApps.Metro.IconPacks.sln";
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

Task("CleanOutput")
  .ContinueOnError()
  .Does(() =>
{
    var directoriesToDelete = GetDirectories("./**/obj").Concat(GetDirectories("./**/bin")).Concat(GetDirectories("./**/Publish"));
    DeleteDirectories(directoriesToDelete, new DeleteDirectorySettings { Recursive = true, Force = true });
});

Task("Restore")
    .Does(() =>
{
    //PaketRestore();

    var msBuildSettings = new MSBuildSettings { ToolPath = msBuildPath, ArgumentCustomization = args => args.Append("/m") };

    MSBuild(iconPacksSolution, msBuildSettings
            .SetConfiguration("Debug") //.SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Normal)
            .WithTarget("restore")
            );
    MSBuild(iconPacksSolution, msBuildSettings
            .SetConfiguration("Release") //.SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Normal)
            .WithTarget("restore")
            );
});

Task("Build")
  .Does(() =>
{
  var msBuildSettings = new MSBuildSettings { ToolPath = msBuildPath, ArgumentCustomization = args => args.Append("/m") };

  Information("Build: Release");
  MSBuild(iconPacksSolution, msBuildSettings
            .SetMaxCpuCount(0)
            .SetConfiguration("Release") //.SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Normal)
            //.WithRestore() only with cake 0.28.x            
            .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
            .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
            .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
            );
});

Task("BuildAll")
  .Does(() =>
{
  var msBuildSettings = new MSBuildSettings { ToolPath = msBuildPath, ArgumentCustomization = args => args.Append("/m") };

  MSBuild(iconPacksSolution, msBuildSettings
            .SetMaxCpuCount(0)
            .SetConfiguration("Debug") //.SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Normal)
            //.WithRestore() only with cake 0.28.x            
            .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
            .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
            .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
            );
  MSBuild(iconPacksSolution, msBuildSettings
            .SetMaxCpuCount(0)
            .SetConfiguration("Release") //.SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Normal)
            //.WithRestore() only with cake 0.28.x            
            .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
            .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
            .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
            );
});

Task("ZipConfig")
  .Does(() =>
{
  EnsureDirectoryExists(Directory(publishDir));
  Zip($"./MahApps.Metro.IconPacks.Browser/bin/{configuration}/MahApps.Metro.IconPacks.Browser/", $"{publishDir}/IconPacks.Browser.{configuration}-v" + gitVersion.NuGetVersion + ".zip");
});

Task("ZipAll")
  .Does(() =>
{
  EnsureDirectoryExists(Directory(publishDir));
  configuration = "Debug";
  Zip($"./MahApps.Metro.IconPacks.Browser/bin/{configuration}/MahApps.Metro.IconPacks.Browser/", $"{publishDir}/IconPacks.Browser.{configuration}-v" + gitVersion.NuGetVersion + ".zip");
  configuration = "Release";
  Zip($"./MahApps.Metro.IconPacks.Browser/bin/{configuration}/MahApps.Metro.IconPacks.Browser/", $"{publishDir}/IconPacks.Browser.{configuration}-v" + gitVersion.NuGetVersion + ".zip");
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
    Information("Packing {0}", project);

    DeleteFiles(GetFiles("./MahApps.Metro.IconPacks/obj/**/*.nuspec"));

    MSBuild(project, msBuildSettings
      .SetConfiguration(configuration)
      .SetVerbosity(Verbosity.Normal)
      .WithTarget("pack")
      .WithProperty("PackageOutputPath", publishDir)
      .WithProperty("Version", isReleaseBranch ? gitVersion.MajorMinorPatch : gitVersion.NuGetVersion)
      .WithProperty("AssemblyVersion", gitVersion.AssemblySemVer)
      .WithProperty("FileVersion", gitVersion.AssemblySemFileVer)
      .WithProperty("InformationalVersion", gitVersion.InformationalVersion)
    );
  }

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
    .IsDependentOn("CleanOutput")
    .IsDependentOn("Restore")
    .IsDependentOn("BuildAll")
    .IsDependentOn("ZipAll");

Task("appveyor")
    .IsDependentOn("CleanOutput")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("ZipConfig")
    .IsDependentOn("Pack");

// Execution
RunTarget(target);