#!/bin/sh


VERSION="0.1.1"
echo "Create Nuget V $VERSION"

cd ..
dotnet pack ./StructuredLogNet/StructuredLogNet.csproj --configuration Release -p:VersionPrefix=$VERSION
