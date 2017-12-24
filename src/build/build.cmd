@echo off
tools\nuget.exe update -self
tools\nuget.exe install Cake -OutputDirectory tools -ExcludeVersion

IF "%1" == "" ( tools\Cake\Cake.exe build.cake ) ELSE ( tools\Cake\Cake.exe build.cake --target=%1 )

exit /b %errorlevel%
