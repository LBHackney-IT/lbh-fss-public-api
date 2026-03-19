FROM mcr.microsoft.com/dotnet/sdk:8.0

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

# OpenCover report for SonarCloud (written to mounted /app/coverage in CI)
RUN mkdir -p /app/coverage
CMD dotnet test LBHFSSPublicAPI.Tests/LBHFSSPublicAPI.Tests.csproj -c Debug --no-build \
    /p:CollectCoverage=true \
    /p:CoverletOutput=/app/coverage/ \
    /p:CoverletOutputFormat=opencover \
    /p:CoverletOutputFile=coverage.opencover.xml
