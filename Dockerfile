FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["TvMaze/TvMaze.csproj", "TvMaze/"]
COPY ["Infrastructure/TvMaze.Infrastructure.DependencyBuilder/TvMaze.Infrastructure.DependencyBuilder.csproj", "Infrastructure/TvMaze.Infrastructure.DependencyBuilder/"]
COPY ["TvMaze.Core/TvMaze.Core.csproj", "TvMaze.Core/"]
COPY ["Infrastructure/TvMaze.Infrastructure.Scraper/TvMaze.Infrastructure.Scraper.csproj", "Infrastructure/TvMaze.Infrastructure.Scraper/"]
COPY ["Infrastructure/TvMaze.Infrastructure/TvMaze.Infrastructure.csproj", "Infrastructure/TvMaze.Infrastructure/"]
COPY ["Infrastructure/TvMaze.Infrastructure.Data/TvMaze.Infrastructure.Data.csproj", "Infrastructure/TvMaze.Infrastructure.Data/"]
COPY ["Infrastructure/TvMaze.Infrastructure.HostedServices/TvMaze.Infrastructure.HostedServices.csproj", "Infrastructure/TvMaze.Infrastructure.HostedServices/"]
COPY ["Infrastructure/TvMaze.Infrastructure.Scheduler/TvMaze.Infrastructure.Scheduler.csproj", "Infrastructure/TvMaze.Infrastructure.Scheduler/"]
RUN dotnet restore "TvMaze/TvMaze.csproj"
COPY . .
WORKDIR "/src/TvMaze"
RUN dotnet build "TvMaze.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TvMaze.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TvMaze.dll"]