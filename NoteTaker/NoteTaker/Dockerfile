# Base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install the Entity Framework tools
RUN dotnet tool install --global dotnet-ef

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY NoteTaker.Api.csproj .

# Restore
RUN dotnet restore

# Copy the rest of the backend source code
COPY . .

# Build the application
RUN dotnet build -c Release -o /app

# Waiting script (for migration)
RUN git clone https://github.com/vishnubob/wait-for-it.git

# Publish the ASP.NET backend
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 7892
ENTRYPOINT ["dotnet", "NoteTaker.Api.dll", "--urls", "http://+:7892"]