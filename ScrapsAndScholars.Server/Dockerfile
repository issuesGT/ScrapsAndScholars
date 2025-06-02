# Use the official .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy csproj and restore dependencies
COPY *.sln .
COPY ScrapsAndScholars.Server/*.csproj ./ScrapsAndScholars.Server/
RUN dotnet restore

# Copy the rest of the code
COPY ScrapsAndScholars.Server/. ./ScrapsAndScholars.Server/

WORKDIR /app/ScrapsAndScholars.Server

# Build the app
RUN dotnet publish -c Release -o out

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/ScrapsAndScholars.Server/out .

# Set up server to listen on port 5000
ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "ScrapsAndScholars.Server.dll"]
