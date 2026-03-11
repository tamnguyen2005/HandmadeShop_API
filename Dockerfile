FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["HandmadeShop.API/HandmadeShop.API.csproj","HandmadeShop.API/"]
COPY ["HandmadeShop.Application/HandmadeShop.Application.csproj","HandmadeShop.Application/"]
COPY ["HandmadeShop.Domain/HandmadeShop.Domain.csproj","HandmadeShop.Domain/"]
COPY ["HandmadeShop.Infrastructure/HandmadeShop.Infrastructure.csproj","HandmadeShop.Infrastructure/"]

RUN dotnet restore "HandmadeShop.API/HandmadeShop.API.csproj"

COPY . .
WORKDIR "/src/HandmadeShop.API"
RUN dotnet build "HandmadeShop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HandmadeShop.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","HandmadeShop.API.dll"]