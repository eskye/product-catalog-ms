#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

#The COPY lines must be same across all dockerfiles so as to take advantage of Docker Cache for fast build
COPY ["src/Services/Product/Product.Api/Product.Api.csproj", "src/Services/Product/Product.Api/"]
COPY ["src/Services/Product/Product.Domain/Product.Domain.csproj", "src/Services/Product/Product.Domain/"]
COPY ["src/Services/Product/Product.Application/Product.Application.csproj", "src/Services/Product/Product.Application/"]
COPY ["src/Services/Product/Product.Infrastructure/Product.Infrastructure.csproj", "src/Services/Product/Product.Infrastructure/"]

COPY ["src/Services/Inventory/Inventory.Api/Inventory.Api.csproj", "src/Services/Inventory/Inventory.Api/"]
COPY ["src/Services/Inventory/Inventory.Domain/Inventory.Domain.csproj", "src/Services/Inventory/Inventory.Domain/"]
COPY ["src/Services/Inventory/Inventory.Infrastructure/Inventory.Infrastructure.csproj", "src/Services/Inventory/Inventory.Infrastructure/"]

RUN dotnet restore "src/Services/Product/Product.Api/Product.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Product/Product.Api"
RUN dotnet build "Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Api.dll"]
