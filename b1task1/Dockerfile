FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["b1/b1.csproj", "b1/"]
RUN dotnet restore "b1/b1.csproj"
COPY . .
WORKDIR "/src/b1"
RUN dotnet build "b1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "b1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "b1.dll"]
