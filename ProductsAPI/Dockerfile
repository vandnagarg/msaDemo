#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProductsAPI/ProductsAPI.csproj", "ProductsAPI/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "ProductsAPI/ProductsAPI.csproj"
COPY . .
WORKDIR "/src/ProductsAPI"
RUN dotnet build "ProductsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductsAPI.dll"]