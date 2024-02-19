# Tekton

The project was generated using the Clean Architecture Principles (Robert Cecil Martin Book) also JasonTaylorDev Documentation

Requirements:
- Last version of Visual Studio 2022
- Last version of .Net 8 (https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.201-windows-x64-installer)
- Minimal API implemented
- NSwag Open API Documentation
- Sql Server 2022 or a docker container with the image, in te root folder exist a docker compose file for this
- Scaffolding: API, Application, Domain, Infrastructure
- Used CQRS Pattern, Mediator, Fluent Validation, Ardemir Guards, EF 
- Change values in appsettings.json from API and FunctionalTests; for FunctionalUnitTest, the db name must be TektonTestDb or different of appsettings.json of API project
- JWT security implemented, you must do login with this credentials: "email": "administrator@localhost","password": "Administrator1!"; after this, you need to pass as Bearer {JWT generated}


Test challenge by Tekton:

Requisitos

Este challenge debe contener lo siguiente:
1.1. Crear un rest API en .net Core (última versión).
1.2. Documentar la API usando swagger.
1.3. Usar patrones (ejemplo: mediator pattern, repository pattern, cqrs, etc).
1.4. Aplicar principios SOLID y Clean Code.
1.5. Implementar la solución haciendo uso de TDD.
1.6. Usar buenos patrones para las validaciones del Request, y además,
considerar los HTTP Status Codes en cada petición realizada.
1.7. Estructurar el proyecto en N-capas.
1.8. Agregar un archivo readme (README.md) en dónde se haga una breve
descripción de los patrones o arquitectura usada en el proyecto. Además,
poner los pasos para levantar el proyecto localmente.También, se puede
agregar alguna información que se considere útil.
1.9. Subir el proyecto a github de manera pública.

2. Enunciado
Actualmente, se tiene la parte web de un sistema de registro de productos y se
desea hacer un servicio API que soporte las siguientes funcionalidades:
2.1. Realizar Insert(POST), Update(PUT) y GetById(GET) de un maestro de
productos.
2.2. Loguear el tiempo de respuesta de cada request hecho en un archivo de
texto plano.
2.3. Mantener en caché(5 min) un diccionario de estados del producto, cuyos
valores son mostrados en el siguiente cuadro:
Status(key) StatusName(value)
1 Active
0 Inactive
Se puede usar Cache estándar, Lazy Cache o cualquier otro tipo de cache
que se crea pertinente.
2.4. Grabar la información del producto localmente usando cualquier tipo de
persistencia. Los campos mandatorios son: ProductId, Name, Status(0 o 1),
Stock, Description y Price.Es posible agregar campos adicionales si es que
la persona evaluada los considera pertinente.
2.5. El método GetById debe retornar un producto con los siguientes campos:
- ProductId,
- Name,
- StatusName, Este campo se obtiene del caché creado en 2.3 basado
en el “Status”.
- Stock,
- Description,
- Price,
- Discount, Porcentaje de descuento [0-100] el cual es obtenido de
algún servicio externo basado en el ProductId. Este
servicio puede ser https://mockapi.io/ u otro.
- FinalPrice, Price * (100 - Discount) / 100


## Build

Run `dotnet build -tl` to build the solution.

## Run

Before to run application if you need you must run the migrations, target the infrastructure project and run thjs command:

````
dotnet ef database update
````
also, if you run the API project, all the migrations will be run it

To run the web application:

```bash
cd .\src\API\
dotnet watch run
```

Navigate to https://localhost:5001. The application will automatically reload if you change any of the source files.

## Code Styles & Formatting

The template includes [EditorConfig](https://editorconfig.org/) support to help maintain consistent coding styles for multiple developers working on the same project across various editors and IDEs. The **.editorconfig** file defines the coding styles applicable to this solution.

## Test

The solution contains unit and functional tests.

To run the tests:
```bash
dotnet test
```