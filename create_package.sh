#!/bin/bash
VERSION="$(cat VERSION)"
./nuget.exe pack "Gpg.NET/Gpg.NET.csproj" version "$VERSION" -Prop Configuration=Release