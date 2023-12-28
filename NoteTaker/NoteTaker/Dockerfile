# Base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY NoteTaker.Api.csproj .

# Restore
RUN dotnet restore

# Copy the rest of the backend source code
COPY . .

# Expose ports
EXPOSE 7892

# Publish the ASP.NET backend
CMD ["dotnet", "run", "--urls", "http://0.0.0.0:7892"]