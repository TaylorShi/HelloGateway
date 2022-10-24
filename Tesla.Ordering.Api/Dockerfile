#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Tesla.Ordering.Api/Tesla.Ordering.Api.csproj", "Tesla.Ordering.Api/"]
RUN dotnet restore "Tesla.Ordering.Api/Tesla.Ordering.Api.csproj"
COPY . .
WORKDIR "/src/Tesla.Ordering.Api"
RUN dotnet build "Tesla.Ordering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tesla.Ordering.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tesla.Ordering.Api.dll"]