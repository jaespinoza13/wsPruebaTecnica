# Etapa 1: Construcci�n de la aplicaci�n
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos del proyecto y restaurar dependencias
COPY *.sln ./
COPY Application/*.csproj ./Application/
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY WebUI/*.csproj ./WebUI/
RUN dotnet restore

# Copiar el c�digo restante y compilar la aplicaci�n
COPY . .
WORKDIR /app/WebUI
RUN dotnet publish -c Release -o /out

# Etapa 2: Ejecuci�n de la aplicaci�n
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /out .

# Exponer el puerto en el que corre la API
EXPOSE 5078

# Configurar la variable de entorno para producci�n
ENV ASPNETCORE_URLS=http://+:5078
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ["dotnet", "WebUI.dll"]
