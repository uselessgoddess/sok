FROM rustlang/rust:nightly-slim AS rust
WORKDIR /app

COPY /native /app
RUN cargo build --package ffi --release 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY */*.csproj .
RUN find *.csproj -exec sh -c 'mkdir -p "${1%.*}" && cp "$1" "${1%.*}/"' _ {} \;
RUN rm *.csproj

RUN dotnet restore Api/Api.csproj

FROM build AS publish

COPY . .
RUN dotnet publish Api/Api.csproj -c release -o publish

COPY Api/appsettings.json .
COPY --from=rust /app/target/release/* publish/

ENTRYPOINT ["dotnet", "publish/Api.dll"]