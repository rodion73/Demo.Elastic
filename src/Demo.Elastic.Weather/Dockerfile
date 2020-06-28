FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY src/**/*.csproj ./
RUN for p in *.csproj; do dotnet restore $p --no-dependencies; done

COPY . .
RUN dotnet publish src/Demo.Elastic.Weather/Demo.Elastic.Weather.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Demo.Elastic.Weather.dll"]
