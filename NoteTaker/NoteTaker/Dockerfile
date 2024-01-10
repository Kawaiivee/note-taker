# # Base image
# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# # Set the working directory
# WORKDIR /src

# # Copy the project file and restore dependencies
# COPY NoteTaker.Api.csproj .

# # Restore
# RUN dotnet restore

# # Copy the rest of the backend source code
# COPY . .

# # Expose ports
# EXPOSE 7892

# # Publish the ASP.NET backend
# ENTRYPOINT ["dotnet", "run", "--urls", "http://localhost:7892"]

### ChatGPT Solution
# Base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

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

# Install dotnet-ef and migrate DB
WORKDIR /app
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef && dotnet ef migrations add InitialCreate

# Publish the ASP.NET backend
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 7892
ENTRYPOINT ["dotnet", "NoteTaker.Api.dll", "--urls", "http://+:7892"]