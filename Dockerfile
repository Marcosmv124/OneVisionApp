# Imagen base para producción
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagen base para compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar y restaurar dependencias
COPY ["One Vision.csproj", "./"]
RUN dotnet restore "One Vision.csproj"

# Copiar el resto del proyecto y compilar
COPY . .
RUN dotnet publish "One Vision.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "One Vision.dll"]
