#!/bin/sh


VERSION="0.1.0"
echo "Create Nuget V $VERSION"

cd ..
dotnet pack ./StructuredLogNet.Web/StructuredLogNet.Web.csproj --configuration Release -p:VersionPrefix=$VERSION
dotnet nuget push "StructuredLogNet.Web/bin/Release/StructuredLogNet.Web.$VERSION.nupkg" --source "davil74"
