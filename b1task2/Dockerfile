FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["b1task2/b1task2.csproj", "b1task2/"]
RUN dotnet restore "b1task2/b1task2.csproj"
COPY . .
WORKDIR "/src/b1task2"
RUN dotnet build "b1task2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "b1task2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "b1task2.dll"]
