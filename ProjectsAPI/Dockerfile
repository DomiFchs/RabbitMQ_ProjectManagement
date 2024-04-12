FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProjectManagement_MOM/ProjectManagement_MOM.csproj", "ProjectManagement_MOM/"]
RUN dotnet restore "ProjectManagement_MOM/ProjectManagement_MOM.csproj"
COPY . .
WORKDIR "/src/ProjectManagement_MOM"
RUN dotnet build "ProjectManagement_MOM.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProjectManagement_MOM.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectManagement_MOM.dll"]
