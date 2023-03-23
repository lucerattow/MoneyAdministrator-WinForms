# MoneyAdministrator

Recreacion del proyecto MyMoneyAdmin utilizando el patron de arquitectura MVP

## Arquitectura de la aplicacion

La applicacion esta dividida en 4 capas:

- **MoneyAdministrator**: Este proyecto contendrá la interfaz de usuario y las clases de presentador. Será un proyecto de Windows Forms.
- **MoneyAdministrator.Models**: Este proyecto contendrá las clases de modelo y las interfaces de repositorio. Será un proyecto de biblioteca de clases .NET.
- **MoneyAdministrator.DataAccess**: Este proyecto contendrá las implementaciones de repositorio y unit of work para la persistencia de datos. Será un proyecto de biblioteca de clases .NET.
- **MoneyAdministrator.Services**: Este proyecto contendrá las clases de servicio que manejan la lógica de negocio. Será un proyecto de biblioteca de clases .NET.

Estos proyectos estan relacionados de la siguiente manera:

- **MoneyAdministrator**: debe hacer referencia a **MoneyAdministrator.Models** y **MoneyAdministrator.Services**.
- **MoneyAdministrator.Services**: debe hacer referencia a **MoneyAdministrator.Models** y **MoneyAdministrator.DataAccess**.
- **MoneyAdministrator.DataAccess**: debe hacer referencia a **MoneyAdministrator.Models**.