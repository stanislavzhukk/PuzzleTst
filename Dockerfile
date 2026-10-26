# Этап 1: Сборка приложения с использованием тяжелого SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["/PuzzleTst/PuzzleTst.csproj", "./"]
RUN dotnet restore "./PuzzleTst.csproj"

COPY . .
RUN dotnet publish "./PuzzleTst.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PuzzleTst.dll"]