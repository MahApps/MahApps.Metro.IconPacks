#tool "nuget:?package=gitlink"

// Arguments
var target = Argument("target", "Default");
var version = "1.2.0.0";
var configGitLink = new GitLinkSettings {
  RepositoryUrl = "https://github.com/MahApps/MahApps.Metro.IconPacks",
  Branch        = "master",
  Configuration = "Release",
  SolutionFileName = "src/MahApps.Metro.IconPacks.sln"
};

// Tasks
Task("GitLink")
  .Does(() =>
{
  GitLink("../../", configGitLink);
  configGitLink.Configuration = "Release";
  GitLink("../../", configGitLink);
  configGitLink.Configuration = "Release_NET45";
  GitLink("../../", configGitLink);
  configGitLink.Configuration = "Release_NET46";
  GitLink("../../", configGitLink);
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

Task("NuGetPack")
  .Does(() =>
{
  var iconPacksNuGet = "MahApps.Metro.IconPacks";
  var nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "../bin/",
    Id                      = iconPacksNuGet,
    Version                 = version,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro 2011 - {0}", DateTime.Now.Year),
    Files                   = new [] {
                                       new NuSpecContent {Source = string.Format("Release/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net40/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net40/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net40/{0}.XML", iconPacksNuGet)},
                                       
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net45/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net45/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net45/{0}.XML", iconPacksNuGet)},

                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net46/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net46/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net46/{0}.XML", iconPacksNuGet)}
                                    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.Entypo";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "../bin/",
    Id                      = iconPacksNuGet,
    Version                 = version,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro 2011 - {0}", DateTime.Now.Year),
    Files                   = new [] {
                                       new NuSpecContent {Source = string.Format("Release/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net40/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net40/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net40/{0}.XML", iconPacksNuGet)},
                                       
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net45/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net45/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net45/{0}.XML", iconPacksNuGet)},

                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net46/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net46/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net46/{0}.XML", iconPacksNuGet)}
                                    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.FontAwesome";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "../bin/",
    Id                      = iconPacksNuGet,
    Version                 = version,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro 2011 - {0}", DateTime.Now.Year),
    Files                   = new [] {
                                       new NuSpecContent {Source = string.Format("Release/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net40/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net40/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net40/{0}.XML", iconPacksNuGet)},
                                       
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net45/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net45/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net45/{0}.XML", iconPacksNuGet)},

                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net46/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net46/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net46/{0}.XML", iconPacksNuGet)}
                                    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.Material";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "../bin/",
    Id                      = iconPacksNuGet,
    Version                 = version,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro 2011 - {0}", DateTime.Now.Year),
    Files                   = new [] {
                                       new NuSpecContent {Source = string.Format("Release/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net40/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net40/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net40/{0}.XML", iconPacksNuGet)},
                                       
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net45/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net45/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net45/{0}.XML", iconPacksNuGet)},

                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net46/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net46/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net46/{0}.XML", iconPacksNuGet)}
                                    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);

  iconPacksNuGet = "MahApps.Metro.IconPacks.Modern";
  nuGetPackSettings   = new NuGetPackSettings {
    BasePath                = "../bin/",
    Id                      = iconPacksNuGet,
    Version                 = version,
    Title                   = iconPacksNuGet,
    Copyright               = string.Format("Copyright © MahApps.Metro 2011 - {0}", DateTime.Now.Year),
    Files                   = new [] {
                                       new NuSpecContent {Source = string.Format("Release/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net40/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net40/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net40/{0}.XML", iconPacksNuGet)},
                                       
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net45/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net45/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET45/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net45/{0}.XML", iconPacksNuGet)},

                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.dll", iconPacksNuGet), Target = string.Format("lib/net46/{0}.dll", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.pdb", iconPacksNuGet), Target = string.Format("lib/net46/{0}.pdb", iconPacksNuGet)},
                                       new NuSpecContent {Source = string.Format("Release_NET46/{0}.XML", iconPacksNuGet), Target = string.Format("lib/net46/{0}.XML", iconPacksNuGet)}
                                    }
  };
  NuGetPack("MahApps.Metro.IconPacks.nuspec", nuGetPackSettings);
});

// Task Targets
Task("Default").IsDependentOn("UpdateAssemblyInfo").IsDependentOn("Build").IsDependentOn("GitLink").IsDependentOn("NuGetPack");

Task("dev").IsDependentOn("UpdateAssemblyInfo").IsDependentOn("Build").IsDependentOn("GitLink_dev").IsDependentOn("GitLink");

Task("pack").IsDependentOn("NuGetPack");

// Execution
RunTarget(target);