#tool "nuget:?package=gitlink"

// Arguments
var target = Argument("target", "Default");

var version = "1.0.0.0";

var configGitLink = new GitLinkSettings {
  RepositoryUrl = "https://github.com/MahApps/MahApps.Metro.IconPacks",
  Branch        = "master",
  Configuration = "Release"
};

// Tasks
Task("GitLink")
  .Does(() =>
{
  GitLink("../../", configGitLink);
  configGitLink.Configuration = "Release_NET45";
  GitLink("../../", configGitLink);
  configGitLink.Configuration = "Release_NET451";
  GitLink("../../", configGitLink);
  configGitLink.Configuration = "Release_NET452";
  GitLink("../../", configGitLink);
  configGitLink.Configuration = "Release_NET46";
  GitLink("../../", configGitLink);
  configGitLink.Configuration = "Release_NET461";
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
    Copyright = string.Format("Copyright © MahApps.Metro 2011 - {0}", DateTime.Now.Year)
  };
  CreateAssemblyInfo("../GlobalAssemblyInfo.cs", newAssemblyInfoSettings);
});

Task("Build")
  .Does(() =>
{
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET45").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET451").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET452").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET46").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET461").UseToolVersion(MSBuildToolVersion.VS2015));
});

// Task Targets
Task("Default").IsDependentOn("UpdateAssemblyInfo").IsDependentOn("Build").IsDependentOn("GitLink");

Task("dev").IsDependentOn("UpdateAssemblyInfo").IsDependentOn("Build").IsDependentOn("GitLink_dev").IsDependentOn("GitLink");

// Execution
RunTarget(target);