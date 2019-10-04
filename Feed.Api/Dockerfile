FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Feed.Api/Feed.Api.csproj", "Feed.Api/"]
RUN dotnet restore "Feed.Api/Feed.Api.csproj"
COPY . .
WORKDIR "/src/Feed.Api"
RUN dotnet build "Feed.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Feed.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Feed.Api.dll"]