FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Public/DNCCorporate.Public.Web/DNCCorporate.Public.Web.csproj", "Public/DNCCorporate.Public.Web/"]
COPY ["Libraries/DNCCorporate.Server.Contract/DNCCorporate.Server.Contract.csproj", "Libraries/DNCCorporate.Server.Contract/"]
COPY ["Libraries/DNCCorporate.ViewModel/DNCCorporate.ViewModel.csproj", "Libraries/DNCCorporate.ViewModel/"]
COPY ["Libraries/DNCCorporate.Server.Services/DNCCorporate.Server.Services.csproj", "Libraries/DNCCorporate.Server.Services/"]
COPY ["Public/DNCCorporate.Public.Web.Framework/DNCCorporate.Public.Web.Framework.csproj", "Public/DNCCorporate.Public.Web.Framework/"]
RUN dotnet restore "./Public/DNCCorporate.Public.Web/./DNCCorporate.Public.Web.csproj"
COPY . .
WORKDIR "/src/Public/DNCCorporate.Public.Web"
RUN dotnet build "./DNCCorporate.Public.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DNCCorporate.Public.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DNCCorporate.Public.Web.dll"]