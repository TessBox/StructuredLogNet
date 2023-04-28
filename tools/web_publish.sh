#!/bin/sh


VERSION="1.0.0"
echo "Create Nuget V $VERSION"

cd ..
dotnet pack ./StructuredLogNet.Web/StructuredLogNet.Web.csproj --configuration Release -p:VersionPrefix=$VERSION
