FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.csproj .
RUN dotnet restore ApiGateway.csproj

FROM build AS publish

COPY . .
RUN dotnet publish ApiGateway.csproj -c release -o publish

COPY appsettings.json .

ENTRYPOINT ["dotnet", "publish/ApiGateway.dll"]
