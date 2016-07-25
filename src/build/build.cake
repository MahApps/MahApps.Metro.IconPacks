// Arguments
var target = Argument("target", "Default");

// Tasks
Task("Build")
  .Does(() =>
{
  Information("Now build this awesome IconPacks!");
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET45").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET451").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET452").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET46").UseToolVersion(MSBuildToolVersion.VS2015));
  MSBuild("../MahApps.Metro.IconPacks.sln", settings => settings.SetConfiguration("Release_NET461").UseToolVersion(MSBuildToolVersion.VS2015));
});

// Task Targets
Task("Default").IsDependentOn("Build");

// Execution
RunTarget(target);