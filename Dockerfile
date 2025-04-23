FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the projects we need
COPY ["dotnet-ipcom-pim-console/*.csproj", "dotnet-ipcom-pim-console/"]
COPY ["dotnet-ipcom-pim-domain/*.csproj", "dotnet-ipcom-pim-domain/"]
COPY ["dotnet-ipcom-pim-infrastructure/*.csproj", "dotnet-ipcom-pim-infrastructure/"]
COPY ["dotnet-ipcom-pim-shared/*.csproj", "dotnet-ipcom-pim-shared/"]
COPY ["dotnet-ipcom-pim-application/*.csproj", "dotnet-ipcom-pim-application/"]

# Create a temporary solution file
RUN dotnet new sln --name TempSolution
RUN dotnet sln TempSolution.sln add $(find . -name "*.csproj")

# Restore packages
RUN dotnet restore "TempSolution.sln"

# Copy the full source code
COPY ["dotnet-ipcom-pim-console/", "dotnet-ipcom-pim-console/"]
COPY ["dotnet-ipcom-pim-domain/", "dotnet-ipcom-pim-domain/"]
COPY ["dotnet-ipcom-pim-infrastructure/", "dotnet-ipcom-pim-infrastructure/"]
COPY ["dotnet-ipcom-pim-shared/", "dotnet-ipcom-pim-shared/"]
COPY ["dotnet-ipcom-pim-application/", "dotnet-ipcom-pim-application/"]

# Build the console project
WORKDIR "/src/dotnet-ipcom-pim-console"
RUN dotnet build "dotnet-ipcom-pim-console.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
WORKDIR "/src/dotnet-ipcom-pim-console"
RUN dotnet publish "dotnet-ipcom-pim-console.csproj" -c Release -o /app/publish

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy the app settings
COPY ["dotnet-ipcom-pim-console/appsettings.json", "."]

# Copy the published app
COPY --from=publish /app/publish .

# Create a volume for logs
VOLUME /app/logs

# Set the entrypoint
ENTRYPOINT ["dotnet", "dotnet-ipcom-pim-console.dll"]