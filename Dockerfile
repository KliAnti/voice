FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY voice/voice.csproj voice/
RUN dotnet restore voice/voice.csproj

COPY voice/ voice/
RUN dotnet publish voice/voice.csproj --configuration Release --output /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_Kestrel__Endpoints__Http__Url=http://+:8080 \
    ASPNETCORE_Kestrel__Endpoints__Http__Protocols=Http1 \
    ASPNETCORE_Kestrel__Endpoints__Grpc__Url=http://+:8081 \
    ASPNETCORE_Kestrel__Endpoints__Grpc__Protocols=Http2

EXPOSE 8080 8081

COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "voice.dll"]