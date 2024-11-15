#Etapa base, puse el directorio en /app y expuse el puerto 8080 y 8081
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# En esta hice el restore de las dependencias y compile la app en Release 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY . .
RUN dotnet restore "./DevOpsPalermo/DevOpsPalermo.csproj"
WORKDIR "/src/DevOpsPalermo"

RUN dotnet build "./DevOpsPalermo.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Aca solo hago la publicacion en /app/publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevOpsPalermo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# aca copio lo que hice en el publish y defino que la entrada para ejecutar la app es DevOpsPalermo.dll
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevOpsPalermo.dll"]