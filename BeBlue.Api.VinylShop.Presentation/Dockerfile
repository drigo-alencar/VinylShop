FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine3.8 AS base 
WORKDIR /app
EXPOSE 43608

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.8 AS build
WORKDIR /src
COPY ["BeBlue.Api.VinylShop.Presentation/BeBlue.Api.VinylShop.Presentation.csproj", "BeBlue.Api.VinylShop.Presentation/"]
COPY ["BeBlue.Api.VinylShop.DomainModel/BeBlue.Api.VinylShop.DomainModel.csproj", "BeBlue.Api.VinylShop.DomainModel/"]
COPY ["BeBlue.Api.VinylShop.LogicLayer/BeBlue.Api.VinylShop.LogicLayer.csproj", "BeBlue.Api.VinylShop.LogicLayer/"]
COPY ["BeBlue.Api.VinylShop.DataLayer/BeBlue.Api.VinylShop.DataLayer.csproj", "BeBlue.Api.VinylShop.DataLayer/"]
COPY ["BeBlue.Api.VinylShop.ExternalServices/BeBlue.Api.VinylShop.ExternalServices.csproj", "BeBlue.Api.VinylShop.ExternalServices/"]
RUN dotnet restore "BeBlue.Api.VinylShop.Presentation/BeBlue.Api.VinylShop.Presentation.csproj"

COPY . .

WORKDIR "/src/BeBlue.Api.VinylShop.Presentation"
RUN dotnet build "BeBlue.Api.VinylShop.Presentation.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BeBlue.Api.VinylShop.Presentation.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BeBlue.Api.VinylShop.Presentation.dll"]