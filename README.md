# Lab14-C

Proyecto .NET 8 Web API/MVC para un sistema de biblioteca conectado a Neon DB.

## Estructura principal

```text
Biblioteca.Mvc/
  Controllers/
  Data/
  Dtos/
  Models/
  Repositories/
  Services/
```

## Ejecutar localmente

Configurar la conexion a Neon como user-secret:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "TU_CONNECTION_STRING" --project Biblioteca.Mvc\Biblioteca.Mvc.csproj
```

Ejecutar:

```powershell
dotnet run --project Biblioteca.Mvc
```

Abrir Swagger:

```text
http://localhost:PUERTO/swagger
```

## Endpoints

```text
GET /api/health
GET /api/libros
GET /api/usuarios
GET /api/roles
```

## Despliegues

Ver:

```text
DEPLOYMENT.md
```
