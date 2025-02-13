# Etapa 1: Construcción de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos del proyecto y restaurar dependencias
COPY *.sln ./
COPY Application/*.csproj ./Application/
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY WebUI/*.csproj ./WebUI/
RUN dotnet restore

# Copiar el código restante y compilar la aplicación
COPY . .
WORKDIR /app/WebUI
RUN dotnet publish -c Release -o /out

# Etapa 2: Ejecución de la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /out .

# Exponer el puerto en el que corre la API
EXPOSE 5078

# Configurar la variable de entorno para producción
ENV ASPNETCORE_URLS=http://+:5078
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ["dotnet", "WebUI.dll"]
