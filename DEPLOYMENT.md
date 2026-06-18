# Despliegues - Biblioteca .NET 8 + Neon DB

La base de datos ya esta en Neon. Para cualquier despliegue se usa esta variable de entorno:

```text
ConnectionStrings__DefaultConnection
```

Valor recomendado:

```text
Host=ep-broad-lake-ah5em06k-pooler.c-3.us-east-1.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=TU_PASSWORD;SSL Mode=Require;Channel Binding=Require;Timeout=120;Command Timeout=120;Server Compatibility Mode=NoTypeLoading
```

## 1. Despliegue local con .NET

Sirve para demostrar que el proyecto corre directamente con el SDK.

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "TU_CONNECTION_STRING" --project Biblioteca.Mvc\Biblioteca.Mvc.csproj
dotnet run --project Biblioteca.Mvc
```

Abrir:

```text
http://localhost:PUERTO/swagger
```

## 2. Despliegue con Docker

Crear un archivo `.env` en la raiz del proyecto con:

```text
ConnectionStrings__DefaultConnection=TU_CONNECTION_STRING
```

Ejecutar:

```powershell
docker compose up --build
```

Abrir:

```text
http://localhost:8080/swagger
```

## 3. Despliegue cloud con Render usando Docker

1. Subir el proyecto a GitHub.
2. Crear un nuevo Web Service en Render.
3. Elegir el repositorio.
4. Usar Docker.
5. Root Directory: `Biblioteca.Mvc`.
6. Agregar variable de entorno:

```text
ConnectionStrings__DefaultConnection=TU_CONNECTION_STRING
```

7. Deploy.

La URL final sera parecida a:

```text
https://biblioteca-net8.onrender.com/swagger
```

## Alternativa: Azure App Service

Publicar carpeta Release:

```powershell
dotnet publish Biblioteca.Mvc\Biblioteca.Mvc.csproj -c Release -o publish
```

En Azure App Service agregar en Configuration:

```text
ConnectionStrings__DefaultConnection=TU_CONNECTION_STRING
ASPNETCORE_ENVIRONMENT=Production
```

Luego subir el contenido de `publish`.
