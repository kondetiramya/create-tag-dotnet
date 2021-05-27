#!/bin/sh -l

dotnet restore
dotnet build
dotnet run --project create-or-replace-tag-dotnet/ -- $1 $2