#!/bin/sh -l

cd /app

dotnet restore
dotnet build
dotnet run --project src/create-or-replace-tag-dotnet -- \
    --token "$TOKEN" --tag-name "&TAG_NAME"