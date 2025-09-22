# PP1 — Suma de Números Naturales (C#, .NET 8)

Este repositorio contiene la carpeta **PP1** con la solución y el proyecto de consola solicitados.
El repositorio **no** incluye los artefactos compilados (`bin/`, `obj/`), conforme a la consigna.

## Estructura
```text
.
├─ .gitignore
├─ README.md
└─ PP1/
   ├─ (archivo .sln de la solución)
   └─ (carpeta del proyecto con .csproj, Program.cs, etc.)
```

## Requisitos
- .NET SDK **8.0** (LTS)
- Visual Studio 2022 o CLI `dotnet`

## Ejecución (CLI)
```bash
cd PP1
dotnet run
```

## Descripción
El programa implementa:
- `SumFor(n) = n*(n+1)/2` (con `unchecked` sobre `int`).
- `SumIte(n)` iterativo (1+2+…+n) también en `int` con `unchecked`.

Recorre los valores de `n` de forma ascendente (1→Max) y descendente (Max→1) para hallar el último/primer `sum > 0` en cada método y muestra los resultados en consola.
