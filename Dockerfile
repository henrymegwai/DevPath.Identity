#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BlinkCash.Identity/BlinkCash.Identity.csproj", "BlinkCash.Identity/"]
COPY ["BlinkCash.Core/BlinkCash.Core.csproj", "BlinkCash.Core/"]
COPY ["BlinkCash.Infrastructure/BlinkCash.Infrastructure.csproj", "BlinkCash.Infrastructure/"]
COPY ["BlinkCash.Data/BlinkCash.Data.csproj", "BlinkCash.Data/"]
RUN dotnet restore "BlinkCash.Identity/BlinkCash.Identity.csproj"
COPY . .
WORKDIR "/src/BlinkCash.Identity"
RUN dotnet build "BlinkCash.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlinkCash.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlinkCash.Identity.dll"]