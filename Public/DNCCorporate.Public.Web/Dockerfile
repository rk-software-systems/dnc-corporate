#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Public/DNCCorporate.Public.Web/DNCCorporate.Public.Web.csproj", "Public/DNCCorporate.Public.Web/"]
RUN dotnet restore "Public/DNCCorporate.Public.Web/DNCCorporate.Public.Web.csproj"
COPY . .
WORKDIR "/src/Public/DNCCorporate.Public.Web"
RUN dotnet build "DNCCorporate.Public.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DNCCorporate.Public.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DNCCorporate.Public.Web.dll"]