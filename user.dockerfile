# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY ./ .
RUN dotnet restore "./ms.infrastructure/ms.infrastructure.csproj"
RUN dotnet restore "./ms.userapi/ms.user.csproj"

RUN dotnet publish ./ms.userapi/ms.user.csproj  -c debug -o out --no-restore

# Use the official .NET 8 runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ms.user.dll", "--urls=http://0.0.0.0:5000"]