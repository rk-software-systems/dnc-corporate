FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Public/DNCCorporate.Public.Web/DNCCorporate.Public.Web.csproj", "Public/DNCCorporate.Public.Web/"]
COPY ["Libraries/DNCCorporate.Contracts/DNCCorporate.Contracts.csproj", "Libraries/DNCCorporate.Contracts/"]
COPY ["Libraries/DNCCorporate.ViewModels/DNCCorporate.ViewModels.csproj", "Libraries/DNCCorporate.ViewModels/"]
COPY ["Libraries/DNCCorporate.Services/DNCCorporate.Services.csproj", "Libraries/DNCCorporate.Services/"]
COPY ["Public/DNCCorporate.Public.Web.Framework/DNCCorporate.Public.Web.Framework.csproj", "Public/DNCCorporate.Public.Web.Framework/"]
RUN dotnet restore "./Public/DNCCorporate.Public.Web/./DNCCorporate.Public.Web.csproj"
COPY . .
WORKDIR "/src/Public/DNCCorporate.Public.Web"
RUN dotnet build "./DNCCorporate.Public.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build -p:BuildOS=lin

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DNCCorporate.Public.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish -p:UseAppHost=false -p:BuildOS=lin

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DNCCorporate.Public.Web.dll"]