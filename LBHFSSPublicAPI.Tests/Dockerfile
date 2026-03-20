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

RUN dotnet build -c Debug LBHFSSPublicAPI.Tests/LBHFSSPublicAPI.Tests.csproj

RUN mkdir -p /app/coverage /app/test_results
CMD dotnet test LBHFSSPublicAPI.Tests/LBHFSSPublicAPI.Tests.csproj -c Debug --no-build \
    --logger "console;verbosity=minimal" \
    --logger "junit;LogFilePath=/app/test_results/junit.xml" \
    /p:CollectCoverage=true \
    /p:CoverletOutput=/app/coverage/ \
    /p:CoverletOutputFormat=opencover \
    /p:CoverletOutputFile=coverage.opencover.xml
