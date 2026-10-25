FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY voice.csproj ./
RUN dotnet restore voice.csproj

COPY . ./
RUN dotnet publish voice.csproj --configuration Release --output /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_Kestrel__EndpointDefaults__Protocols=Http2

EXPOSE 8080

COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "voice.dll"]