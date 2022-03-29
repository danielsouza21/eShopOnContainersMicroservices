# eShopOnContainersMicroservices
Development of an ECommerce in an environment with microservices architecture in .NET 5.0

Implementation of an ECommerce using a complex microservices architecture with several popular and main technologies of the Microsoft network (stack .NET 5.0).

![Microsoft_eShopOn](/helperResourcesAssets/eShopOnContainers-Architecture-Microsoft.png)

Key architectural references:
* [Introducing eShopOnContainers reference app](https://docs.microsoft.com/en-us/dotnet/architecture/cloud-native/introduce-eshoponcontainers-reference-app)
* [.NET Microservices: Architecture for Containerized .NET Applications](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/)

## Final design goal:

![Microservices_Arch_Applied](/helperResourcesAssets/Applied%20Project%20Architecture.png)

### Microservices created and implemented:

#### Catalog microservice: 
* ASP.NET Core Web API application 
* REST API (CRUD operations)
* **MongoDB database** connection and containerization
* Repository Pattern Implementation

#### Basket microservice: 
* ASP.NET Core Web API application 
* REST API (CRUD operations)
* **Redis database** connection and containerization
* Repository Pattern Implementation

#### Docker Compose establishment with all microservices on docker:
* Containerization of microservices
* Containerization of databases
* YML files configuration
* Use Portainer to manage docker containers

> 💡 SOLID, Clean Architecture, Domain Driven Design (DDD), Clean Code, logging, validation, exception handling, Swagger Open API and other standards/features were highly used for designing the projects.

## Setup and Run Project

### Requirements

* [Visual Studio](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 5](https://dotnet.microsoft.com/download/dotnet-core/5)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

1. At the root directory which include **docker-compose.yml** files, run below command to inicialize docker microservices containers:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
> Wait for docker compose all microservices, some microservices need extra time to work

3. *Microservices* urls:

* **Catalog API -> http://host.docker.internal:8000/swagger/index.html**
* **Basket API -> http://host.docker.internal:8001/swagger/index.html**
* **Portainer -> http://host.docker.internal:9000** - User: admin/admin1234
