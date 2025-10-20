# PP3 — API

# Estudiante: Brandon Céspedes Morales
# Carné: FH22012992

## Objetivo

Aplicar los conocimientos adquiridos al utilizar una Minimal API con la herramienta ASP.NET Core Minimal API del Framework .NET 8.0.

# Los comandos de dotnet utilizados (CLI)

## 1) Crear una nueva solución 
dotnet new sln -n MySolution

## 2) Crear el proyecto de tipo Web API (Minimal API)
dotnet new web -n MyProject

## 3) Agregar el proyecto a la solución
dotnet sln add .\MyProject\MyProject.csproj

## 4) Instalar el paquete Swagger (Swashbuckle)
dotnet add .\MyProject\MyProject.csproj package Swashbuckle.AspNetCore

## 5) Restaurar dependencias
dotnet restore

## 6) Ejecutar la aplicación
dotnet run --project .\MyProject\MyProject.csproj

# Enlaces

1) Microsoft Learn – Documentación de ASP.NET Core Minimal APIs
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis

2) Microsoft Learn – Agregar y configurar Swagger (Swashbuckle) en ASP.NET Core
https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle

3) Stack Overflow – Cómo desactivar antiforgery en endpoints Minimal API (.NET 8)
https://stackoverflow.com/questions/77901683/asp-net-core-8-minimal-api-endpoints-throws-anti-forgery-middleware-not-found

4) C# Corner – Devolver respuestas en formato JSON y XML en ASP.NET Core Minimal API
https://www.c-sharpcorner.com/article/how-to-return-json-and-xml-in-asp-net-core-minimal-api

5) Microsoft Learn – Atributos [FromRoute], [FromQuery], [FromHeader] y [FromForm]
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/parameter-binding

6) Stack Overflow – Cómo devolver diferentes formatos de respuesta según el encabezado
https://stackoverflow.com/questions/63926241/returning-json-or-xml-based-on-header-in-asp-net-core

# Prompts a ChatGPT

1) Prompt: ¿Cómo estructuro una Minimal API con varios endpoints en .NET 8 sin usar controladores?
Respuesta: Crea el archivo Program.cs con WebApplication.CreateBuilder(args) y usa app.MapGet, app.MapPost, app.MapPut y app.MapDelete. Define funciones locales para validaciones y utilidades, y termina con app.Run().

2) Prompt: ¿Cómo soluciono el error CS8803: Las instrucciones de nivel superior deben preceder a las declaraciones de espacio de nombres y de tipos?
Respuesta: Asegúrate de colocar todas las clases auxiliares (record o class) después de app.Run() o dentro de un namespace. En las Minimal APIs, las declaraciones de tipos no pueden ir antes del código principal.

3) Prompt: Swagger muestra error: ‘Endpoint contains anti-forgery metadata’ al usar [FromForm]. ¿Cómo lo resuelvo?
Respuesta: En .NET 8 se agrega protección antifalsificación automáticamente. Si no usas tokens antiforgery, desactívalo con .DisableAntiforgery() en los endpoints que usan [FromForm].

# Las respuestas a las siguientes preguntas:

## ¿Es posible enviar valores en el Body (por ejemplo, en el Form) del Request de tipo GET?

No, no es posible ni recomendable enviar valores en el cuerpo (Body) de una petición HTTP de tipo GET.
Según la especificación del protocolo HTTP (RFC 7231, sección 4.3.1), las solicitudes GET están diseñadas para recuperar información y no deben tener efectos secundarios en el servidor. Además, su semántica no contempla el uso de un cuerpo en el mensaje.

Adicionalmente, para el proyecto el GET no se usa para recibir datos ni procesar texto (como los otros endpoints), sino únicamente para inicializar y redirigir al Swagger UI.

## ¿Qué ventajas y desventajas observa con el Minimal API si se compara con la opción de utilizar Controllers?

Ventajas:

1) Simplicidad y rapidez de desarrollo: La sintaxis es más directa y ligera, ya que no requiere definir clases de controladores ni atributos como [HttpGet], [HttpPost], etc.
2) Mejor rendimiento: Las Minimal APIs eliminan parte de la sobrecarga del enrutamiento y la reflexión que usan los controladores tradicionales, lo cual mejora los tiempos de inicio y respuesta en aplicaciones pequeñas o microservicios.
3) Menor configuración inicial: Todo el código puede residir en un solo archivo (Program.cs), facilitando el despliegue rápido o el uso en entornos educativos y demostrativos.
4) Integración moderna: Están pensadas para aprovechar las nuevas características de .NET 8+, como inyección de dependencias simplificada, Results<T>, serialización directa a JSON y soporte integrado para Swagger/OpenAPI.

Desventajas:

1) Menor organización en proyectos grandes: En aplicaciones con muchos endpoints, mantener toda la lógica en un solo archivo puede volverse difícil de leer y escalar. En esos casos, la estructura por controladores y servicios resulta más mantenible.
2) Menos separación de responsabilidades: Al no existir controladores ni clases separadas, la lógica de negocio tiende a mezclarse con la definición de rutas, reduciendo la claridad del diseño arquitectónico (MVC).
3) Menor compatibilidad con convenciones existentes: Algunos middleware o frameworks externos aún dependen de las convenciones de los controladores (ControllerBase, filtros, ModelState, etc.), lo que puede limitar la extensibilidad del enfoque Minimal.