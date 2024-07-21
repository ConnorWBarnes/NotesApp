#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file into the container and restore its dependencies (i.e. NuGet packages)
COPY ["backend/src/Services/Access/NotesApp.Backend.Services.Access.Api/NotesApp.Backend.Services.Access.Api.csproj", "backend/src/Services/Access/NotesApp.Backend.Services.Access.Api/"]
RUN dotnet restore "./backend/src/Services/Access/NotesApp.Backend.Services.Access.Api/NotesApp.Backend.Services.Access.Api.csproj"

# Copy everything in the (project or solution?) folder into the container and then build the project
COPY . .
WORKDIR "/src/backend/src/Services/Access/NotesApp.Backend.Services.Access.Api"
RUN dotnet build "./NotesApp.Backend.Services.Access.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotesApp.Backend.Services.Access.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesApp.Backend.Services.Access.Api.dll"]