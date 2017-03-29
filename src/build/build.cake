#tool "nuget:?package=gitlink"

// Arguments
var target = Argument("target", "Default");
var version = "1.8.0.0";
var configGitLink = new GitLinkSettings {
  RepositoryUrl = "https://github.com/MahApps/MahApps.Metro.IconPacks",
  Branch        = "master",
  Configuration = "Release",
  SolutionFileName = "src\\MahApps.Metro.IconPacks.sln"
};

// Tasks
Task("GitLink")
  .Does(() =>
{
  configGitLink.Configuration = "Release";
  GitLink("..\\..\\", configGitLink);

  configGitLink.Configuration = "Release_NET45";
  GitLink("..\\..\\", configGitLink);

  configGitLink.Configuration = "Release_NET46";
  GitLink("..\\..\\", configGitLink);
});

Task("GitLink_dev")
  .Does(() =>
{
  configGitLink.Branch = "dev";
});

Task("UpdateAssemblyInfo")
  .Does(() =>
{
  var assemblyInfo = ParseAssemblyInfo("../GlobalAssemblyInfo.cs");
  var newAssemblyInfoSettings = new AssemblyInfoSettings {
    Product = string.Format("MahApps.Metro.IconPacks {0}", version),
    Version = version,
    FileVersion = version,
    InformationalVersion = version,
    Copyright = string.Format("Copyright © MahApps.Metro {0}", DateTime.Now.Year)
  };
  CreateAssemblyInfo("../GlobalAssemblyInfo.cs", newAssemblyInfoSettings);
});

Task("Build")
  .Does(() =>
{
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET45").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET46").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.Browser.sln", settings => settings.SetConfiguration("Release").UseToolVersion(MSBuildToolVersion.VS2015));
});

Task("BuildAll")
  .Does(() =>
{
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Debug").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.Browser.sln", settings => settings.SetConfiguration("Debug").UseToolVersion(MSBuildToolVersion.VS2015));

  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET45").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET46").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.Browser.sln", settings => settings.SetConfiguration("Release").UseToolVersion(MSBuildToolVersion.VS2015));
});

Task("ZipDebug")
  .Does(() =>
{
  Zip("..\\bin\\MahApps.Metro.IconPacks.Browser\\Debug\\", "IconPacks.Browser.Debug.NET45.zip");
});

Task("ZipRelease")
  .Does(() =>
{
  Zip("..\\bin\\MahApps.Metro.IconPacks.Browser\\Release\\", "IconPacks.Browser.NET45.zip");
});

Task("NuGetPack")
  .Does(() =>
{
  var iconPacksNuGet = "MahApps.Metro.IconPacks";
  var nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "..\\bin",
    Id                      = iconPacksNuGet,
    Version                 = version,
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
    Version                 = version,
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
    Version                 = version,
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
    Version                 = version,
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
    Version                 = version,
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
    Version                 = version,
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
    Version                 = version,
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
               .IsDependentOn("GitLink")
               .IsDependentOn("ZipRelease")
               .IsDependentOn("NuGetPack");
Task("dev").IsDependentOn("CleanOutput")
           .IsDependentOn("UpdateAssemblyInfo")
           .IsDependentOn("BuildAll")
           .IsDependentOn("GitLink_dev").IsDependentOn("GitLink")
           .IsDependentOn("ZipDebug").IsDependentOn("ZipRelease")
           .IsDependentOn("NuGetPack");

// Execution
RunTarget(target);