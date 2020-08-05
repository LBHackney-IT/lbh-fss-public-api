FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./LBHFSSPublicAPI.sln ./
COPY ./LBHFSSPublicAPI/LBHFSSPublicAPI.csproj ./LBHFSSPublicAPI/
COPY ./LBHFSSPublicAPI.Tests/LBHFSSPublicAPI.Tests.csproj ./LBHFSSPublicAPI.Tests/

RUN dotnet restore ./LBHFSSPublicAPI/LBHFSSPublicAPI.csproj
RUN dotnet restore ./LBHFSSPublicAPI.Tests/LBHFSSPublicAPI.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out LBHFSSPublicAPI.Tests/LBHFSSPublicAPI.Tests.csproj

CMD dotnet test
