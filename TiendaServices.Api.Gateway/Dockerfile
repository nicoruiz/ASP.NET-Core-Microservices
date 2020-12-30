#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TiendaServices.Api.Gateway/TiendaServices.Api.Gateway.csproj", "TiendaServices.Api.Gateway/"]
RUN dotnet restore "TiendaServices.Api.Gateway/TiendaServices.Api.Gateway.csproj"
COPY . .
WORKDIR "/src/TiendaServices.Api.Gateway"
RUN dotnet build "TiendaServices.Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServices.Api.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServices.Api.Gateway.dll"]
