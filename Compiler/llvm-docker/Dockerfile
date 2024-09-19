ARG UBUNTU_VERSION=22.04
ARG LLVM_URL="https://github.com/llvm/llvm-project"
ARG LLVM_TAG="19.1.0"
ARG BUILD_TYPE=MinSizeRel
ARG PROJECTS="llvm"
ARG TARGETS=RISCV

FROM ubuntu:22.04 AS builder

ARG UBUNTU_VERSION
ARG LLVM_URL
ARG LLVM_TAG
ARG BUILD_TYPE
ARG PROJECTS
ARG TARGETS

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update -q && apt-get install -y mold curl tar libssl-dev cmake ninja-build python3 python3-distutils build-essential gcc g++ gcc-multilib \
    && apt-get autoremove -y --purge \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/*

RUN cd /tmp/ && curl -o src.tar.xz https://github.com/llvm/llvm-project/releases/download/llvmorg-$LLVM_TAG/llvm-project-$LLVM_TAG.src.tar.xz -L \
    && tar xf src.tar.xz && rm src.tar.xz && mv llvm-project-$LLVM_TAG.src llvm-project

RUN mkdir /tmp/llvm-project/build && mkdir /tmp/llvm && cd /tmp/llvm-project/build \
    && cmake -G Ninja ../llvm -Wno-dev \
    -DCMAKE_BUILD_TYPE=$BUILD_TYPE -DLLVM_ENABLE_ASSERTIONS=ON -DCMAKE_INSTALL_PREFIX=/tmp/llvm \
    -DCMAKE_C_COMPILER="gcc" -DCMAKE_CXX_COMPILER="g++" -DLLVM_USE_LINKER="mold" -DLLVM_TARGETS_TO_BUILD="$TARGETS" \
    -DLLVM_ENABLE_PROJECTS=$PROJECTS -DLLVM_USE_SPLIT_DWARF=ON \
    -DLLVM_INCLUDE_EXAMPLES=OFF -DLLVM_INCLUDE_TESTS=OFF -DLLVM_INCLUDE_BENCHMARKS=OFF \
    -DLLVM_PARALLEL_LINK_JOBS=2 -DLLVM_ENABLE_ZLIB=OFF \
    && cmake --build . && cmake --build . --target install

FROM ubuntu:$UBUNTU_VERSION

COPY --from=builder /tmp/llvm/ /usr/local/

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update -q && apt-get install -y cmake ninja-build build-essential python3 python3-distutils git unzip gcc g++ libeigen3-dev libboost-dev \
    && apt-get autoremove -y --purge \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/* \
