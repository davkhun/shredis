#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:latest AS base
WORKDIR /app
EXPOSE 32145

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS http://+:32145
ENV CACHE_SIZE_LIMIT 512

FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /src
COPY ["shredis.csproj", "shredis/"]
RUN dotnet restore "shredis/shredis.csproj"

WORKDIR "/src/shredis"
COPY . .
RUN dotnet build "shredis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "shredis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shredis.dll"]