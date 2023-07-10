FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Toy.OpenApi/Toy.OpenApi.csproj", "Toy.OpenApi/"]
RUN dotnet restore "Toy.OpenApi/Toy.OpenApi.csproj"
COPY . .
WORKDIR "/src/Toy.OpenApi"
RUN dotnet build "Toy.OpenApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Toy.OpenApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Toy.OpenApi.dll"]
