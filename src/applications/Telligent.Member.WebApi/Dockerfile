#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/applications/Telligent.Member.WebApi/Telligent.Member.WebApi.csproj", "src/applications/Telligent.Member.WebApi/"]
COPY ["src/modules/Telligent.Member.Application/Telligent.Member.Application.csproj", "src/modules/Telligent.Member.Application/"]
COPY ["src/modules/Telligent.Member.Database/Telligent.Member.Database.csproj", "src/modules/Telligent.Member.Database/"]
COPY ["src/modules/Telligent.Member.Domain/Telligent.Member.Domain.csproj", "src/modules/Telligent.Member.Domain/"]

RUN dotnet nuget add source http://teola.3rdchannel.com.tw/Nuget/nuget/ -n Telexpress
RUN dotnet restore "src/applications/Telligent.Member.WebApi/Telligent.Member.WebApi.csproj"
COPY . .
WORKDIR "/src/src/applications/Telligent.Member.WebApi"
RUN dotnet build "Telligent.Member.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Telligent.Member.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Telligent.Member.WebApi.dll"]
