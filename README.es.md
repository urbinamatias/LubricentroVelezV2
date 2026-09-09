# Lubricentro Vélez — Sistema de Gestión (v2)

[English](README.md) · **Español**

Aplicación de escritorio desarrollada para un lubricentro real, destinada a
gestionar su operación diaria: clientes y sus vehículos, órdenes de trabajo de
servicio, y el catálogo de aceites, aditivos y filtros utilizados en cada
trabajo.

Es una **reescritura completa** de un sistema interno heredado. La aplicación se
rehízo desde cero sobre la **base de datos SQL Server ya existente** del negocio,
corrigiendo errores arrastrados de la versión original y completando
funcionalidades que nunca se habían terminado.

> Es software en producción entregado a un negocio concreto, no una plantilla
> genérica. El repositorio es público para que su alcance y su código puedan
> revisarse como proyecto de portfolio.

![.NET](https://img.shields.io/badge/.NET-6.0-512BD4)
![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-0078D6)
![EF Core](https://img.shields.io/badge/ORM-EF%20Core%206-512BD4)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927)
![C#](https://img.shields.io/badge/C%23-11-239120)

---

## Qué hace

- **Órdenes de trabajo** — alta, consulta, edición y baja lógica de órdenes de
  servicio, cada una vinculada a un vehículo, un aceite, un aditivo y el conjunto
  de filtros reemplazados.
- **Carga de órdenes más rápida** — al escribir una patente se autocompletan los
  últimos datos conocidos de ese vehículo (propietario, teléfono, modelo,
  servicio anterior), de modo que las visitas recurrentes se cargan en segundos.
- **Clientes y vehículos** — personas y sus autos, relacionados uno a muchos.
- **Gestión de catálogo** — alta, edición y baja completas de aceites y aditivos,
  con normalización de marca y nombre para los desplegables de carga de órdenes.
- **Panel principal** — una grilla de todas las órdenes de trabajo, con búsqueda
  y ordenamiento, y filtrado escalonado (primero por patente, con el nombre del
  propietario como alternativa) que conserva la posición de scroll y la selección
  mientras se escribe.
- **Resiliente por defecto** — cualquier error no controlado se registra en un
  archivo junto al ejecutable y se muestra al usuario con un mensaje claro en
  lugar de cerrar la aplicación.

---

## Contexto y restricciones de ingeniería

La máquina de producción del negocio corre **Windows 7**, un detalle que recién
apareció en una etapa avanzada del desarrollo:

- El código se escribió apuntando a **.NET 8** para aprovechar las herramientas y
  características de lenguaje actuales.
- Windows 7 solo admite hasta **.NET 6**, por lo que el proyecto se **fija a
  `net6.0-windows7.0` con EF Core 6 para la versión entregada (Release)**.
- Nada depende de APIs posteriores a .NET 6, así que avanzar el framework de
  destino es un cambio de una sola línea cuando se actualice el entorno del
  negocio.

El **esquema de la base de datos es propiedad del negocio**, no de esta
aplicación. Las clases de entidad y el `DbContext` se **generan a partir de la
base de datos en uso** con
[EF Core Power Tools](https://github.com/ErikEJ/EFCorePowerTools): la aplicación
se adapta al esquema en lugar de migrarlo.

---

## Arquitectura

En capas, con una única dirección de dependencia:

```
Forms  ──►  Services  ──►  Repositories  ──►  OrdenesContext (EF Core)  ──►  SQL Server
(WinForms)   (lógica de     (acceso a datos,   (generado a partir de
             negocio,        consultas LINQ)    la base existente)
             proyecciones)
```

- **Patrón Repository + Service** detrás de interfaces — la UI nunca accede a
  EF Core de forma directa.
- **Raíz de composición manual** en `Program.cs` — las dependencias se arman a
  mano y se inyectan por constructor, deliberadamente sin contenedor de IoC.
- **`IDbContextFactory<OrdenesContext>`** — cada llamada al repositorio crea y
  descarta su propio `DbContext` de vida corta, evitando estado obsoleto del
  change tracker en un proceso de escritorio de larga duración.
- **Proyecciones a DTO** — las consultas proyectan directamente a DTOs, de modo
  que cada pantalla trae solo las columnas que necesita.
- **Acceso a datos asíncrono** de extremo a extremo, con **tipos de referencia
  nullable** habilitados.

### Estructura del proyecto

```
LubricentroVelezV2/
├── Program.cs        # Punto de entrada, raíz de composición, manejo global de errores
├── Forms/            # Vistas WinForms (panel, orden de trabajo, diálogos de catálogo)
├── Services/         # Interfaz + implementación — lógica de negocio
├── Repositories/     # Interfaz + implementación — acceso a datos con EF Core
├── Models/           # Entidades generadas + OrdenesContext
└── DTOs/             # Proyecciones de consultas
```

---

## Modelo de dominio

| Entidad           | Representa                                                  |
| ----------------- | --------------------------------------------------------- |
| `Personas`        | Clientes                                                  |
| `Autos`           | Vehículos, cada uno propiedad de una `Persona`            |
| `OrdenesTrabajos` | Órdenes de trabajo de servicio — la entidad central       |
| `Aceites`         | Catálogo de aceites                                       |
| `Aditivos`        | Catálogo de aditivos                                      |
| `Filtros`         | Catálogo de filtros                                       |
| `FiltrosXot`      | Tabla intermedia: qué filtros se usaron en qué orden      |

Una orden de trabajo registra el detalle completo del servicio: kilometraje,
aceite, aditivo, filtros reemplazados y campos de texto libre para los distintos
fluidos y controles que abarca un servicio completo (líquido de frenos,
refrigerante, caja, diferenciales, batería, limpiaparabrisas, entre otros).

---

## Tecnologías

| Área            | Tecnología                                                  |
| --------------- | --------------------------------------------------------- |
| Lenguaje        | C# 11                                                     |
| Runtime         | .NET 6 (`net6.0-windows7.0`) — desarrollado sobre .NET 8  |
| UI              | Windows Forms                                             |
| Acceso a datos  | Entity Framework Core 6 (proveedor para SQL Server)       |
| Base de datos   | Microsoft SQL Server (esquema preexistente, database-first) |

---

<sub>Proyecto personal y extracurricular entregado a un negocio privado. Sin afiliación con ninguna institución.</sub>
