# MicroservicesSolution
Microservicio Productos - Guía de Ejecución
📋 Requisitos Previos
.NET 9.0 SDK

Postman o similar para pruebas, pero el microservicio tiene su swagger en http://localhost:5008/api-docs

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

productdb

Configurar conexiones en:

ProductService/appsettings.json

Ejecutar migraciones con: dotnet ef database update

3. Ejecutar servicios
A. Sin Docker
En terminales separadas:

bash
# Product Service
dotnet run --project src/ProductService

🐳 Docker Compose (Para este apartado no es necesario, solo enfocarse en levantar ProductService)
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
# Obtener productos
curl -X GET http://localhost:5000/api/products 

Configuración de Ocelot en ApiGateway/ocelot.json

Variables de entorno para producción en appsettings.Production.json
