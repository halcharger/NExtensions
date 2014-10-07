How to publish latest nuget packages to nuget.org

Get the latest nuget and symbol packages from the build server (see link on github readme page)

execute: 

nuget push <path to nupkg file>

The above command will also push the symbols package if it is available