#!/bin/sh


VERSION="0.1.1"
echo "Create Nuget V $VERSION"

cd ..
dotnet pack ./StructuredLogNet.Web/StructuredLogNet.Web.csproj --configuration Release -p:VersionPrefix=$VERSION
