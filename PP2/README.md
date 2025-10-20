# PP2 — Conversor y Validador de Números Binarios (ASP.NET Core MVC)

# Estudiante: Brandon Céspedes Morales
# Carné: FH22012992

# Objetivo

Crear una aplicación en ASP.NET Core MVC (.NET 8) que:

Valide si una cadena es binaria (solo 0 y 1).

# Convierta:

Binario → Decimal.

Decimal → Binario.

Los comandos de dotnet utilizados (CLI).

## Ver versión instalada de .NET SDK
dotnet --version

## Crear una nueva solución vacía
dotnet new sln -n BinarioMVC

## Crear un nuevo proyecto MVC (ASP.NET Core)
dotnet new mvc -n BinarioApp

## Agregar el proyecto a la solución
dotnet sln BinarioMVC.sln add BinarioApp/BinarioApp.csproj

## Restaurar dependencias del proyecto
dotnet restore

## Compilar en configuración Release
dotnet build -c Release

## Ejecutar la aplicación
dotnet run --project BinarioApp/BinarioApp.csproj

## Limpiar archivos de compilación
dotnet clean

## Ver información del SDK y runtime instalados
dotnet --info

# Enlaces

Stack Overflow – Conversión entre binario y decimal en C#
https://stackoverflow.com/questions/2350283/how-do-i-convert-an-int-to-binary-in-c

Microsoft Learn – Tutorial de ASP.NET Core MVC (Controladores, Vistas y Modelos)
https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-8.0

Stack Overflow – Cómo validar si una cadena es binaria
https://stackoverflow.com/questions/38449975/how-to-check-if-a-string-is-binary-in-c-sharp

Microsoft Docs – Uso de ModelState.IsValid y validaciones en ASP.NET Core
https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0

Stack Overflow – Cómo pasar datos del modelo a la vista en MVC
https://stackoverflow.com/questions/4286675/how-to-pass-data-from-controller-to-view-in-asp-net-mvc

Microsoft Learn – Uso de etiquetas asp-for y asp-action en formularios Razor
https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-8.0

# Prompts a chatgpt

Prompt: “¿Valido binario con Regex o recorro caracteres?”
Respuesta: “Regex.IsMatch(text, ^[01]+$) es directo; recorrer chars también sirve y evita dependencias.”

Prompt: “¿Cómo convierto binario a decimal y decimal a binario en C#?”
Respuesta: “Usa Convert.ToInt32(bin, 2) y Convert.ToString(n, 2); en .NET 8+, n.ToString("B").” 

Prompt: “¿Qué pasa con números negativos y ceros a la izquierda?”
Respuesta: “Negativos: define regla (permitir o no). Convert.ToString(-5, 2) usa complemento a dos; ceros a la izquierda no afectan valor.” 

## Las respuestas a las siguientes preguntas:

¿Cuál es el número que resulta al multiplicar, si se introducen los valores máximos permitidos en a y b? Indíquelo en todas las bases (binaria, octal, decimal y hexadecimal).

Tipo de dato: int (32 bits con signo)

Valor máximo: int.MaxValue = 2,147,483,647

Cálculo:

a × b=2 147 483 647 × 2 147 483 647 = 4 611 686 014 132 420 609

Este valor excede el rango permitido para int, por lo tanto, produce overflow (resultado negativo al ejecutarse dentro de unchecked).

Base 

Binario: 1011010011101100000100000000001

Octagonal: 13266010001

Decimal: 1 (resultado efectivo mod 2³², dependiendo del wrap-around)

Hexagonal: 0x7FFFFFFF * 0x7FFFFFFF = 0x00000001 (mod 2³²)

¿Es posible hacer las operaciones en otra capa? Si sí, ¿en cuál sería?

Si, en una aplicación ASP.NET Core MVC, la lógica de negocio (como validar binarios o convertir entre bases) no debe estar en el controlador, sino en una capa de servicios o de dominio

En sistemas más grandes, incluso podría separarse en una capa de lógica de negocio (BLL) y una capa de acceso a datos (DAL), dejando el controlador únicamente como intermediario.
