FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY */*.csproj .
RUN find *.csproj -exec sh -c \
     'dir=$(echo "$1" | sed -r "s/.*\.(.*)\..*/\1/") && mkdir -p $dir && cp "$1" $dir' _ {} \;
RUN rm *.csproj

RUN dotnet restore Api/Identity.Api.csproj

FROM build AS publish

COPY . .
RUN dotnet publish Api/Identity.Api.csproj -c release -o publish

COPY Api/appsettings.json .

ENTRYPOINT ["dotnet", "publish/Identity.Api.dll"]