FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReportingTeamWebApiService/ReportingTeamWebApiService.csproj", "ReportingTeamWebApiService/"]
RUN dotnet restore "ReportingTeamWebApiService/ReportingTeamWebApiService.csproj"
COPY . .
WORKDIR "/src/ReportingTeamWebApiService"
RUN dotnet build "ReportingTeamWebApiService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportingTeamWebApiService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportingTeamWebApiService.dll"]
