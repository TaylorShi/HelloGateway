#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TeslaOrder.Mobile.Gateway/TeslaOrder.Mobile.Gateway.csproj", "TeslaOrder.Mobile.Gateway/"]
RUN dotnet restore "TeslaOrder.Mobile.Gateway/TeslaOrder.Mobile.Gateway.csproj"
COPY . .
WORKDIR "/src/TeslaOrder.Mobile.Gateway"
RUN dotnet build "TeslaOrder.Mobile.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeslaOrder.Mobile.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeslaOrder.Mobile.Gateway.dll"]