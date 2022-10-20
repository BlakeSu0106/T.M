#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["src/applications/Telligent.Member.AuthService/Telligent.Member.AuthService.csproj", "applications/Telligent.Member.AuthService/"]
COPY ["src/modules/Telligent.Member.Application/Telligent.Member.Application.csproj", "modules/Telligent.Member.Application/"]
COPY ["src/modules/Telligent.Member.Database/Telligent.Member.Database.csproj", "modules/Telligent.Member.Database/"]
COPY ["src/modules/Telligent.Member.Domain/Telligent.Member.Domain.csproj", "modules/Telligent.Member.Domain/"]

RUN dotnet nuget add source http://teola.3rdchannel.com.tw/Nuget/nuget/ -n Telexpress
RUN dotnet restore "applications/Telligent.Member.AuthService/Telligent.Member.AuthService.csproj"
COPY . .
WORKDIR "/src/src/applications/Telligent.Member.AuthService"
RUN dotnet build "Telligent.Member.AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Telligent.Member.AuthService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Telligent.Member.AuthService.dll"]