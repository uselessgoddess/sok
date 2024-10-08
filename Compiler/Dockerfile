FROM uselessgoddess/llvm AS rust

RUN apt-get update && apt-get install -y curl \
    && apt-get autoremove -y --purge \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/*

RUN curl --proto '=https' --tlsv1.2 -sSf https://sh.rustup.rs | sh -s -- -y --default-toolchain=nightly

WORKDIR /app
COPY /zxc /app

ENV PATH="/root/.cargo/bin:${PATH}"

# TODO: later build llvm with X86 target 
RUN cargo build --release && cargo test --lib --release

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN apt-get update -q && apt-get install -y curl cmake build-essential gcc g++ gcc-multilib \
    && apt-get autoremove -y --purge \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/* 

RUN curl --proto '=https' --tlsv1.2 -sSf https://sh.rustup.rs | sh -s -- -y --default-toolchain=nightly
ENV PATH="/root/.cargo/bin:${PATH}"
ENV PATH="/root/.rustup/toolchains/nightly-x86_64-unknown-linux-gnu/lib/rustlib/x86_64-unknown-linux-gnu/bin:${PATH}"

COPY */*.csproj .
RUN find *.csproj -exec sh -c \
     'dir=$(echo "$1" | sed -r "s/.*\.(.*)\..*/\1/") && mkdir -p $dir && cp "$1" $dir' _ {} \;
RUN rm *.csproj

RUN dotnet restore Api/Compiler.Api.csproj

FROM build AS publish

COPY . .
RUN dotnet test Api/Compiler.Api.csproj && dotnet publish Api/Compiler.Api.csproj -c release -o publish

COPY Api/appsettings.json .
COPY --from=rust /app/target/release/driver ./
COPY --from=rust /usr/local/bin/llvm-objcopy /usr/local/bin/

ENTRYPOINT ["dotnet", "publish/Compiler.Api.dll"]
