FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PlaylistCleaner.Api/PlaylistCleaner.Api.csproj", "PlaylistCleaner.Api/"]
RUN dotnet restore "PlaylistCleaner.Api/PlaylistCleaner.Api.csproj"
COPY . .
WORKDIR "/src/PlaylistCleaner.Api"
RUN dotnet build "PlaylistCleaner.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlaylistCleaner.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlaylistCleaner.Api.dll"]