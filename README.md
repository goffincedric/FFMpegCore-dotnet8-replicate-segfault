# FFMpegCore-dotnet8-replicate-segfault

To reproduce:
Build image from dockerfile and start a container.
A fail message will be printed + the container will stay open until a key is pressed.
If you open a terminal to the container and check 'dmesg', some segfaults will pop up.

Working:
Change the project to .NET 7
- ConsoleApp1/ConsoleApp1.csproj:
```diff
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
-       <TargetFramework>net8.0</TargetFramework>
+       <TargetFramework>net7.0</TargetFramework> 
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        <LangVersion>default</LangVersion>
        <DockerDefaultTargetOS>Linux</DockerDefaultTargetOS>
    </PropertyGroup>
    
    ...
```
- Dockerfile:

```diff
- FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
+ FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
USER $APP_UID
WORKDIR /app

- FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
+ FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

...
```
After changing the .NET version, rebuild the image and run the container. It will succeed in transcoding and create a new mp3 file in the 'ConsoleApp1/media' media folder.
