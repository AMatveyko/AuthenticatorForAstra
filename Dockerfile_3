#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AccountManager/AccountManager.csproj", "AccountManager/"]
COPY ["Common.Db/Common.Db.csproj", "Common.Db/"]
COPY ["Common.Configurations/Common.Configurations.csproj", "Common.Configurations/"]
COPY ["Authorization/Authorization.csproj", "Authorization/"]
COPY ["UserServiceInterface/UserServiceInterface.csproj", "UserServiceInterface/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["AccountManagerData/AccountManagerData.csproj", "AccountManagerData/"]
COPY ["Irbis64Plus/Irbis64Plus.csproj", "Irbis64Plus/"]
RUN dotnet restore "AccountManager/AccountManager.csproj"
COPY . .
WORKDIR "/src/AccountManager"
RUN dotnet build "AccountManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountManager.dll"]