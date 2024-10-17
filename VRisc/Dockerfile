FROM rustlang/rust:nightly-slim AS rust
WORKDIR /app

COPY /native /app
RUN cargo build --package ffi --release 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY */*.csproj .
RUN find *.csproj -exec sh -c \
     'dir=$(echo "$1" | sed -r "s/.*\.(.*)\..*/\1/") && mkdir -p $dir && cp "$1" $dir' _ {} \;
RUN rm *.csproj

RUN dotnet restore Api/VRisc.Api.csproj

FROM build AS publish

COPY . .
RUN dotnet test Api/VRisc.Api.csproj && dotnet publish Api/VRisc.Api.csproj -c release -o publish

COPY Api/appsettings.json .
COPY --from=rust /app/target/release/* publish/

ENTRYPOINT ["dotnet", "publish/VRisc.Api.dll"]