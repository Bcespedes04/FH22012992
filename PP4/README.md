# PP4 — BooksEF

## Estudiante: Brandon Céspedes Morales  
## Carné: FH22012992

---

## Objetivo

Aplicar los conocimientos adquiridos al utilizar Entity Framework Core con la estrategia Code First, además de investigar cómo lograr la lectura de archivos CSV y la escritura de archivos TSV en una aplicación de consola.

---

## Los comandos de dotnet utilizados (CLI)

### 1) Crear una nueva solución
dotnet new sln -n BooksEf
dotnet new console -n Books.Console
dotnet sln add .\Books.Console\Books.Console.csproj

shell
Copy code

### 2) Instalar paquetes necesarios
dotnet add .\Books.Console\Books.Console.csproj package Microsoft.EntityFrameworkCore
dotnet add .\Books.Console\Books.Console.csproj package Microsoft.EntityFrameworkCore.Design
dotnet add .\Books.Console\Books.Console.csproj package Microsoft.EntityFrameworkCore.Sqlite

shell
Copy code

### 3) Crear carpetas
mkdir Models
mkdir Data

shell
Copy code

### 4) Migración y base de datos
dotnet build
dotnet ef migrations add InitialCreate
dotnet ef database update

yaml
Copy code

---

## Enlaces

1. Microsoft Learn – Entity Framework Core: Code First
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
2. Microsoft Learn – Leer archivos en C#
https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllines 
3. Stack Overflow – Cómo leer CSV y separarlo por comas respetando comillas
https://stackoverflow.com/questions/769621/dealing-with-commas-in-a-csv-file
4. Microsoft Learn – Cómo escribir archivos con File.WriteAllText
https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext
5. C# Corner – Cómo exportar datos a formato TSV
https://www.c-sharpcorner.com/article/exporting-data-to-csv-or-tsv-using-c-sharp/

---

## Prompts a ChatGPT

### 1) Prompt: ¿Cómo puedo crear una base de datos SQLite con Entity Framework Code First?
**Respuesta:**  
Me explicó cómo instalar los paquetes de Entity Framework, crear las clases de modelo y usar el comando `dotnet ef migrations add InitialCreate` para generar la base de datos automáticamente.

### 2) Prompt: ¿Cómo leo un archivo CSV en C#?
**Respuesta:**  
Me enseñó a usar File.ReadAllLines y cómo separar las columnas con comas, incluyendo los casos donde hay comillas dobles en los nombres de los autores.

### 3) Prompt: ¿Cómo puedo verificar si la base de datos está vacía antes de llenarla?
**Respuesta:**  
Me recomendó usar await db.Authors.AnyAsync() para saber si ya existen registros y, con eso, decidir si se debe leer el archivo CSV o generar los TSV.



## Preguntas y respuestas

### ¿Cómo cree que resultaría el uso de la estrategia de Code First para crear y actualizar una base de datos de tipo NoSQL (como por ejemplo MongoDB)? ¿Y con Database First? ¿Cree que habría complicaciones con las Foreign Keys?

El uso de la estrategia Code First en bases de datos NoSQL como MongoDB puede funcionar conceptualmente, ya que permite definir las clases o modelos primero y luego mapearlos a documentos dentro de las colecciones. Sin embargo, no ofrece las mismas ventajas que en bases relacionales, pues MongoDB no posee un esquema rígido ni migraciones estructuradas; los documentos pueden variar en formato y las relaciones entre entidades se manejan mediante referencias o documentos embebidos, no mediante claves foráneas reales.  

Por otro lado, la estrategia Database First resulta poco práctica en NoSQL, debido a la ausencia de un esquema formal que permita generar clases a partir de la estructura de los datos existentes. Además, el uso de Foreign Keys es incompatible con la naturaleza de MongoDB, ya que la integridad referencial no es gestionada por el motor de base de datos, sino por la lógica de la aplicación. Esto puede generar complicaciones al mantener la coherencia de los datos o al implementar relaciones entre colecciones.



### ¿Cuál carácter, además de la coma (,) y el Tab (\t), se podría usar para separar valores en un archivo de texto con el objetivo de ser interpretado como una tabla (matriz)? ¿Qué extensión le pondría y por qué? Por ejemplo: Pipe (|) con extensión .pipe.

Otro carácter común para separar valores en un archivo de texto es el punto y coma (;), ya que evita conflictos en regiones donde la coma se usa como separador decimal. Este formato es frecuente en archivos exportados desde aplicaciones europeas o configuraciones regionales específicas. La extensión recomendada sería .csv (Comma-Separated Values), ya que es ampliamente reconocida por programas como Excel o R, aunque en realidad puede usar cualquier delimitador, incluyendo el punto y coma.  

Alternativamente, se puede usar el pipe (|) con extensión .psv (Pipe-Separated Values), lo que facilita distinguir los valores cuando el texto contiene comas o espacios. Esta opción es útil para archivos con textos descriptivos, ya que mejora la legibilidad y reduce errores al importar los datos a bases de datos o lenguajes de programación.


## Señales de mejora en la ortografía del profesor

1. **aplicación de consola:** solo los nombres propios deben ir con mayúscula, por lo que está incorrecto que se utilice mayúscula en este caso.
