#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
copy *.sln .
COPY ["create-or-replace-tag-dotnet/create-or-replace-tag-dotnet.csproj", "create-or-replace-tag-dotnet/"]

ADD entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

ENTRYPOINT["/entrypoint.sh"]