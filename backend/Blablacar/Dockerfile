FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5201

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Blablacar.WEB/Blablacar.WEB.csproj", "Blablacar.WEB/"]
COPY ["Blablacar.Core/Blablacar.Core.csproj", "Blablacar.Core/"]
COPY ["Blablacar.Contracts/Blablacar.Contracts.csproj", "Blablacar.Contracts/"]
COPY ["Blablacar.Data.PostgreSQL/Blablacar.Data.PostgreSQL.csproj", "Blablacar.Data.PostgreSQL/"]
COPY ["Blablacar.Worker/Blablacar.Worker.csproj", "Blablacar.Worker/"]
COPY ["Blablacar.SFTP/Blablacar.SFTP.csproj", "Blablacar.SFTP/"]
RUN dotnet restore "Blablacar.WEB/Blablacar.WEB.csproj"
COPY . .
WORKDIR "/src/Blablacar.WEB"
RUN dotnet build "Blablacar.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Blablacar.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blablacar.WEB.dll"]
