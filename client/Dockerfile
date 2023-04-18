# Base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy frontend project files
COPY  . ./client/client.csproj

# Restore frontend project NuGet packages
WORKDIR ./client

RUN dotnet restore client.csproj

# Copy backend project files
COPY . ./server/server.csproj

# Restore backend project NuGet packages
WORKDIR ./server

RUN dotnet restore server.csproj

# Copy all other frontend project files
COPY . ./client

WORKDIR ./client

# Build frontend project
RUN dotnet build -c Release -o /app

# Publish frontend project
RUN dotnet publish -c Release -o /app

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MyFrontendProject.dll"]