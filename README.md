# DemoStore API

## Getting Started

Desarrollo de API Rest para manejo de cuentas de usuarios y productos.

## Testing the Api

Está publicada para pruebas en el siguiente [enlace](https://demo-store-api.herokuapp.com):
```
https://demo-store-api.herokuapp.com/
```

Utilizar la siguiente [colección de PostMan](https://www.getpostman.com/collections/706063ee0547fcb7019a):
```
https://www.getpostman.com/collections/706063ee0547fcb7019a
```

## Local test

1. Clonar el repositorio
2. Cambiar la cadena de conexión en appsettings.json
```
"ConnectionStrings": {
    "DefaultConnection": "Tu cadena de conexión",   
  }
```

3. Ejecutar Update-Database para generar la base de datos:
  - En Visual Studio Package Manager Console:
  ```
  Update-Database
  ```
  - En .NET Core CLI
  ```
  dotnet ef database update
  ```
  
  4. Ejecutar la aplicación 
  
  5. Al ejecutar la aplicación por primera vez se hace el seed de datos de pruebas.
  
  
