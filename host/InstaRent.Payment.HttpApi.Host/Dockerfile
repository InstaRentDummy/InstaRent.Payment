#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["host/InstaRent.Payment.HttpApi.Host/InstaRent.Payment.HttpApi.Host.csproj", "host/InstaRent.Payment.HttpApi.Host/"]
COPY ["src/InstaRent.Payment.HttpApi/InstaRent.Payment.HttpApi.csproj", "src/InstaRent.Payment.HttpApi/"]
COPY ["src/InstaRent.Payment.Application.Contracts/InstaRent.Payment.Application.Contracts.csproj", "src/InstaRent.Payment.Application.Contracts/"]
COPY ["src/InstaRent.Payment.Domain.Shared/InstaRent.Payment.Domain.Shared.csproj", "src/InstaRent.Payment.Domain.Shared/"]
COPY ["src/InstaRent.Payment.Application/InstaRent.Payment.Application.csproj", "src/InstaRent.Payment.Application/"]
COPY ["src/InstaRent.Payment.Domain/InstaRent.Payment.Domain.csproj", "src/InstaRent.Payment.Domain/"]
COPY ["src/InstaRent.Payment.MongoDB/InstaRent.Payment.MongoDB.csproj", "src/InstaRent.Payment.MongoDB/"]
COPY ["host/InstaRent.Payment.Host.Shared/InstaRent.Payment.Host.Shared.csproj", "host/InstaRent.Payment.Host.Shared/"]
RUN dotnet restore "host/InstaRent.Payment.HttpApi.Host/InstaRent.Payment.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/host/InstaRent.Payment.HttpApi.Host"
RUN dotnet build "InstaRent.Payment.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaRent.Payment.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstaRent.Payment.HttpApi.Host.dll"]