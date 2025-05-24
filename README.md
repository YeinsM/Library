````markdown
# 📚 Prueba Técnica DEXTRA - Library API

API RESTful desarrollada en .NET 8 para la gestión de Autores, Libros y Préstamos. Incluye autenticación JWT, autorización por roles, consultas optimizadas, pruebas unitarias y documentación Swagger.


## 🚀 Tecnologías Utilizadas

- .NET 8
- Entity Framework Core (Code First)
- SQL Server
- JWT (JSON Web Tokens)
- Swagger / Swashbuckle
- xUnit + NSubstitute
- Clean Architecture (Domain, Application, Infrastructure, WebAPI)



## 🛠️ Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- Visual Studio 2022+ o VS Code



## 🧪 Ejecución del Proyecto

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/YeinsM/library.git
   cd library
```

2. **Configurar cadena de conexión en `appsettings.json`:**
   Ubicado en `src/WebAPI/appsettings.json`

   ```No es necesario configurar, como es un proyecto para ustedes
Dejare los datos del appsettings para que ustedes eviten esta configuración
   ```

3. **Aplicar migraciones y crear la base de datos:**

   ```bash
   dotnet ef database update --project Library.Infrastructure --startup-project Library.WebAPI
   ```

4. **Ejecutar la API:**

   ```bash
   dotnet run --project Library.WebAPI
   ```

5. **Abrir Swagger:**
   Navegar a `https://localhost:7215/swagger` o `http://localhost:5026/swagger`


## 🔑 Usuarios Simulados

Para autenticación, puedes usar estos usuarios simulados del `UserStore.cs`:

| Usuario | Contraseña | Rol   |
| ------- | ---------- | ----- |
| admin   | admin123   | Admin |
| user    | user123    | User  |



## 🧪 Ejecutar las Pruebas Unitarias

1. Navegar al directorio del proyecto de pruebas:

   ```bash
   cd Library.Tests
   ```

2. Ejecutar las pruebas:

   ```bash
   dotnet test
   ```



## 📋 Endpoints Destacados

* `POST /api/auth/login` - Login y generación de JWT
* `GET /api/authors` - Listar autores
* `GET /api/books/antes-de-2000` - Libros publicados antes del 2000
* `POST /api/loans` - Registrar préstamo de libro
* `GET /api/loans/populares` - Obtener libros mas prestados en los ultimos 6 meses
* `GET /api/loans/prestamos-por-genero` - Obtener libros prestados por genero
* `GET /api/loans/prestamos-por-autor` - Obtener libros prestados por autor

---

## 📦 Estructura del Proyecto

```
📁 src
├── 📂 Library.Domain
├── 📂 Library.Infrastructure
├── 📂 Library.WebAPI
└── 📄 Library.sln

📁 tests
└── 📂 Library.Tests
```


## ✅ Requisitos Cubiertos

* Autenticación JWT
* Autorización por roles
* CRUD para Autores, Libros y Préstamos
* Consultas optimizadas
* Documentación Swagger
* Pruebas unitarias
* Arquitectura limpia
* Custom Responses



## 🧑‍💻 Autor

Desarrollado por Yeins Mancera para la prueba técnica de **Dextra**.

```
