# WasmNeat

This repository utilizes submodules for [SharpNeat](https://github.com/colgreen/sharpneat) and [Redzen](https://github.com/colgreen/Redzen) in order to keep up-to-date with the upstream branch. There are some patches that are needed in order to compile correctly for Wasm.

## Prerequisites

.NET 7.0 SDK

## Installation and Building

Note that these instructions are for a Linux or MacOS system and have not been tested on Windows. I suspect that the `patch` command is the only thing that would be different for Windows.

```
# Clone the repo and submodules
git clone --recurse-submodules https://github.com/Up-North-Coding/WasmNeat
cd WasmNeat

# Install wasm-tools
sudo dotnet workload install wasm-tools

# Add references for SharpNeat and Redzen so that everything gets built correctly
(cd sharpneat/src/SharpNeat && dotnet add SharpNeat.csproj reference ../../../Redzen/Redzen/Redzen.csproj)
(cd WasmNeat/src && dotnet add WasmNeat.csproj reference ../../sharpneat/src/SharpNeat/SharpNeat.csproj)

# Patch the ComplexityRegulationStrategyConfig so that the JSON serialization doesn't break
patch sharpneat/src/SharpNeat/Experiments/ConfigModels/ComplexityRegulationStrategyConfig.cs < patches/ComplexityRegulationStrategyConfig.cs.patch

# Compile a release build
cd WasmNeat/src
dotnet publish -c Release

# Install dotnet-serve to serve the example application
dotnet tool install --global dotnet-serve

# Now serve the wasm on port 9000
dotnet serve --mime .wasm=application/wasm --mime .js=text/javascript --mime .json=application/json --directory bin/Release/net7.0/browser-wasm/AppBundle --port 9000
```
