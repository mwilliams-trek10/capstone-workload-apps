FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BackendTeamWebApiService/BackendTeamWebApiService.csproj", "BackendTeamWebApiService/"]
RUN dotnet restore "BackendTeamWebApiService/BackendTeamWebApiService.csproj"
COPY . .
WORKDIR "/src/BackendTeamWebApiService"
RUN dotnet build "BackendTeamWebApiService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackendTeamWebApiService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendTeamWebApiService.dll"]
