FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SignalR.Testing.WebApi/SignalR.Testing.WebApi.csproj", "SignalR.Testing.WebApi/"]
RUN dotnet restore "SignalR.Testing.WebApi/SignalR.Testing.WebApi.csproj"
COPY . .
WORKDIR "/src/SignalR.Testing.WebApi"
RUN dotnet build "SignalR.Testing.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SignalR.Testing.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignalR.Testing.WebApi.dll"]
