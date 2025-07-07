# 🧱 Arquitectura limpia en C#

Este proyecto base implementa la Arquitectura Limpia (Clean Architecture) en una solución C# con ASP.NET Core. Está pensado para escalar fácilmente y mantener una buena separación de responsabilidades.

## 🗂️ Estructura del Proyecto
Este proyecto está diseñado como una API RESTful que sigue la Arquitectura Limpia (Clean Architecture), centrada principalmente en el backend. La estructura está separada en capas bien definidas que permiten una fácil escalabilidad y mantenimiento. 

```plaintext
📦 MyApp (Solución)
├── 📁 MyApp.Application                   # Lógica de negocio de la aplicación
│   ├── 📁 Dependencias                   # Servicios registrados desde esta capa
│   ├── 📁 DTOs                           # Objetos de transferencia de datos (request/response)
│   ├── 📁 Interfaces                     # Interfaces para servicios que implementan otras capas
│   ├── 📁 Mappers                        # Clases que convierten entidades ↔ DTOs
│   ├── 📁 Services                       # Servicios que encapsulan la lógica de aplicación
│   ├── 📁 UseCases                       # Casos de uso específicos que la app ejecuta
│   └── 📄 ApplicationDependencyInjection.cs   # Método de extensión para inyectar dependencias de esta capa
│
├── 📁 MyApp.Domain                       # Núcleo del negocio (independiente de tecnologías)
│   ├── 📁 Dependencias                   # Interfaces del dominio (opcional)
│   ├── 📁 Entities                       # Entidades del dominio con sus propiedades y reglas
│   └── 📁 Interfaces                     # Contratos que definen comportamientos del dominio
│
├── 📁 MyApp.Infrastructure              # Implementación técnica (BD, archivos, servicios externos)
│   ├── 📁 Dependencias                   # Configuraciones específicas de esta capa
│   ├── 📁 Context                        # DbContext y configuración de EF Core o similar
│   ├── 📁 Repositories                   # Implementación de interfaces de repositorio
│   ├── 📁 Security                       # Servicios de seguridad como autenticación/JWT
│   ├── 📁 Services                       # Servicios como email, almacenamiento, etc.
│   └── 📄 InfrastructureDependencyInjection.cs   # Configura las dependencias de esta capa
│
├── 📁 MyApp.Presentation                 # Capa expuesta, normalmente la API (ASP.NET Core)
│   ├── 📁 Connected Services             # Referencias a servicios externos (gRPC, REST)
│   ├── 📁 Dependencias                   # Configuraciones necesarias para esta capa
│   ├── 📁 Properties                     # Configuraciones automáticas del proyecto
│   ├── 📁 Controllers                    # Controladores (endpoints HTTP)
│   ├── 📁 MiddlewaresAndFilters         # Middlewares personalizados y filtros globales
│   ├── 📄 appsettings.json              # Archivo de configuración de la aplicación
│   ├── 📄 Program.cs                    # Punto de entrada del proyecto (ASP.NET Core)
│   └── 📄 README.md                     # Documentación del proyecto
│
├── 📁 MyApp.Shared                       # Recursos compartidos entre proyectos
│   ├── 📁 Dependencias                   # Clases comunes para la inyección de dependencias
│   ├── 📁 DTOs                           # DTOs que son reutilizados por varias capas
│   └── 📁 Exceptions                     # Clases para manejar errores personalizados
│
└── 📁 MyApp.Tests                        # Proyecto de pruebas unitarias
    ├── 📁 Dependencias                  # Helpers y configuración de pruebas
    ├── 📁 Application                   # Pruebas de la capa Application (casos de uso)
    ├── 📁 Infrastructure                # Pruebas de servicios y repositorios
    └── 📁 Mocks                         # Mocks/Fakes para pruebas (repos, servicios, etc.)
```

## 🧰 Tecnologías y Librerías Utilizadas
* **.NET 9**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **AutoMapper**
* **xUnit / NUnit**

## 🧾 Requisitos Previos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/es/)
- [MySQL Workbench](https://dev.mysql.com/downloads/workbench/)

## 🛠️ Configuración y ejecución del proyecto
Este proyecto utiliza **Entity Framework Core** en enfoque **Code First** para manejar el acceso y persistencia de datos.

1. **Clona el repositorio**:
```bash
git clone https://github.com/tu_usuario/tu_repositorio.git
```
2. Abre la solución MyApp.sln

3. **Restaura los paquetes**:
```bash
dotnet restore
```
4. **Aplica las migraciones**
⚙️ Paso 1: Configurar la Cadena de Conexión

Edita el archivo appsettings.Development.json (o appsettings.json) en el proyecto MyApp.Presentation:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MiBaseDeDatos;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Agrega también las variables necesarias como JwtSettings y EmailSettings:

```json
  "JwtSettings": {
    "SecretKey": "su_clave_secreta",
    "Issuer": "su_emisor",
    "Audience": "su_audience",
    "AccessTokenExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": "587",
    "SenderEmail": "tucorreo@gmail.com",
    "SenderPassword": "tu-contraseña-o-app-password"
  }
```

🧱 Paso 2: Crear la Migración Inicial

Desde la raíz del proyecto abre la terminal y ejecuta:

```bash
dotnet ef migrations add InitialCreate -s MyApp.Presentation -p MyApp.Infrastructure
```

🗃️ Paso 3: Aplicar la Migración

```bash
dotnet ef database update -s MyApp.Presentation -p MyApp.Infrastructure
```

✅ Es necesario ejecutar todos estos pasos cuando es primera vez que ejecutas el proyecto y cuando realizas un cambio.

5. **Ejecuta la aplicación**
## 🔧 Opción 1: Desde Visual Studio
    1. Establece MyApp.Presentation como proyecto de inicio
    2. Presiona F5 o haz clic en "Iniciar" para ejecutar la aplicación.

## 💻 Opción 2: Desde la terminal
    1. Abre la terminal en la raíz de la solución.
    2. dotnet run --project MyApp.Presentation

## 🚀 Swagger - Documentación Interactiva de la API
Este proyecto incluye soporte para Swagger a través de la librería Swashbuckle. Te permite visualizar y probar los endpoints directamente desde el navegador.

## 📌 Accede a Swagger UI
Una vez que ejecutes la aplicación, abre tu navegador en:

```bash
http://localhost:5229
```
## 🛡️ Autenticación en Swagger (JWT)
Si tu API requiere autenticación por token (JWT):
  1. Utiliza el endpoint **/api/User/create** para registrar un nuevo usuario. Este endpoint ya está disponible en Swagger.
  2. Accede al endpoint **/api/UserSession/login** con las credenciales del usuario creado. La respuesta incluirá un accessToken (JWT). Cópialo.

  3. Autenticarse en Swagger
  * En la interfaz de Swagger, haz clic en el botón Authorize (generalmente con el ícono 🔒 en la parte superior derecha).

  * En el cuadro que aparece, escribe el token.
  4. Haz clic en Authorize y luego en Close.
  5. **¡Listo!** Ahora puedes realizar peticiones autenticadas desde Swagger.

## ✅ Ejecución de Pruebas
El proyecto incluye pruebas unitarias para asegurar el correcto funcionamiento de los componentes principales.

🔹 **Opción 1: Desde Visual Studio**

    1. Abre el proyecto en Visual Studio.
    2. Ve al Explorador de soluciones..
    3. Haz clic derecho sobre el proyecto de pruebas (MyApp.Tests).
    4. Selecciona la opción "Ejecutar pruebas".

🔹 **Opción 2: Desde la Terminal**

    1. Abre una terminal en la raíz del proyecto 
    2. Ejecuta el siguiente comando:
```bash
  dotnet test
```

## 🤝 Contribuciones
¡Gracias por tu interés en contribuir a este proyecto! ❤️

Si deseas colaborar, por favor sigue estos pasos:

## 🧩 ¿Cómo contribuir?
1. Haz un fork del repositorio.

2. Clona tu fork en tu máquina local:
```bash
git clone https://github.com/tu-usuario/tu-repositorio.git
```
3. Crea una rama nueva para tu cambio:
```bash
git checkout -b feature/mi-cambio
```
4. Realiza tus cambios y asegúrate de seguir la estructura del proyecto y las buenas prácticas.
5. Agrega tus cambios:
```bash
git add .
git commit -m "Agrega descripción clara del cambio"
```
6. Sube tu rama a GitHub:
```bash
git push origin feature/mi-cambio
```
7. Crea un Pull Request hacia la rama main del repositorio original.

## 🎯 Convenciones de Código

- Seguimos los principios SOLID.
- Nombres de clases en PascalCase.
- Interfaces comienzan con `I`, por ejemplo: `IUserRepository`.
- Servicios terminan en `Service`, casos de uso en `UseCase`.
- DTOs separados por `Request` y `Response`.
- Evita lógica en los controladores; delega todo a los casos de uso.


## 🛡️ Recomendaciones
+ Usa nombres claros para las ramas: feature/..., fix/..., test/....

+ Añade comentarios en tu código si introduces nueva lógica.

+ Asegúrate de que las pruebas pasen antes de enviar tu Pull Request.

+ Si agregas funcionalidades nuevas, incluye pruebas si es posible.

## 📄 Licencia
Este proyecto está licenciado bajo MIT.

## ✍️ Autor o Créditos
Creado con ❤️ por [Lorainne Navarro Carrillo](https://github.com/tu_usuario)

