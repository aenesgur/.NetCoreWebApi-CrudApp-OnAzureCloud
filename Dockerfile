FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CrudApp.API/*.csproj", "CrudApp.API/"]
COPY ["CrudApp.Data/*.csproj", "CrudApp.Data/"]
COPY ["CrudApp.Cache/*.csproj", "CrudApp.Cache/"]
COPY ["CrudApp.Entities/*.csproj", "CrudApp.Entities/"]
RUN dotnet restore "CrudApp.API/CrudApp.API.csproj"
COPY . .
WORKDIR "/src/CrudApp.API"
RUN dotnet build "CrudApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrudApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrudApp.API.dll"]