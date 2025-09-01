# MicroservicesSolution
Microservicios con API Gateway - Guía de Ejecución
📋 Requisitos Previos
.NET 9.0 SDK

Docker Desktop (opcional)

Postman o similar para pruebas, pero cada microservicio tiene su swagger en http://localhost:{port}/api-docs

MySQL (puede usarse en Docker)

🚀 Ejecución Local
1. Clonar el repositorio
bash
git clone [URL_DEL_REPOSITORIO]
cd MicroservicesSolution
2. Configurar bases de datos
Opción A: Con Docker (recomendado)
bash
docker-compose -f docker-compose.db.yml up -d
Opción B: Manual
Crear bases de datos:

userdb

productdb

Configurar conexiones en:

UserService/appsettings.json

ProductService/appsettings.json

Ejecutar migraciones con: dotnet ef database update

3. Ejecutar servicios
A. Sin Docker
En terminales separadas:

bash
# User Service
dotnet run --project src/UserService

# Product Service
dotnet run --project src/ProductService

# API Gateway (puerto 5000)
dotnet run --project src/ApiGateway
B. Con Docker
bash
docker-compose up --build
🔌 Endpoints Disponibles
API Gateway (http://localhost:5000)
Servicio	Endpoint	Métodos	Auth Required
Auth	/api/auth/*	POST	No
Users	/api/users/*	GET,POST,PUT	Sí (JWT)
Products	/api/products/*	Todos	Sí (JWT)
🔐 Autenticación
Registro:

bash
POST /api/auth/register
{
  "username": "string",
  "email": "user@example.com",
  "password": "Admin123!!",
  "firstName": "string",
  "lastName": "string",
  "role": "string"
}
Login (obtener JWT):

bash
POST /api/auth/login
{
  "username": "admin",
  "password": "Admin123!!"
}
Respuesta:

json
{ "token": "eyJhbGciOiJIUzI1NiIs..." }
🐳 Docker Compose
Archivo docker-compose.yml incluye:

Servicios:

user-service (puerto 5149)

product-service (puerto 5008)

api-gateway (puerto 5000)

Bases de datos:

MySQL para UserService

MySQL para ProductService

bash
# Comandos útiles
docker-compose up --build  # Iniciar todo
docker-compose logs -f     # Ver logs
🛠 Troubleshooting
Error: Puerto en uso
bash
# Windows
netstat -ano | findstr :5000
taskkill /PID [ID_PUERTO] /F

# Linux/Mac
lsof -i :5000
kill -9 [PID]
Error: Conexión a DB
Verificar:

Credenciales en appsettings.json

Que MySQL esté corriendo

Las migraciones fueron aplicadas:

bash
dotnet ef database update --project src/UserService
**📊 Estructura del Proyecto
text
MicroservicesSolution/
├── src/
│   ├── ApiGateway/         # (Puerto 5000)
│   ├── UserService/        # (Puerto 5149)
│   └── ProductService/     # (Puerto 5008)
├── docker-compose.yml      # Configuración Docker
└── README.md               # Este archivo
💡 Ejemplo de Uso
bash
# Obtener productos (después de login)
curl -X GET http://localhost:5000/api/products \
  -H "Authorization: Bearer [TOKEN_JWT]"

Configuración de Ocelot en ApiGateway/ocelot.json

Variables de entorno para producción en appsettings.Production.json
