# eShopOnContainersMicroservices
Development of an ECommerce in an environment with microservices architecture in .NET 5.0

Implementation of an ECommerce using a complex microservices architecture with several popular and main technologies of the Microsoft network (stack .NET 5.0).

![Microsoft_eShopOn](/helperResourcesAssets/eShopOnContainers-Architecture-Microsoft.png)

Key architectural references:
* [Introducing eShopOnContainers reference app](https://docs.microsoft.com/en-us/dotnet/architecture/cloud-native/introduce-eshoponcontainers-reference-app)
* [.NET Microservices: Architecture for Containerized .NET Applications](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/)

## Final design goal and current state of development:

![Microservices_Arch_Applied](/helperResourcesAssets/Applied%20Project%20Architecture.png)

Implementations being carried out and planned:
- RabbitMQ and MassTransit for Checkout Order

## Microservices created and implemented:

> 💡 SOLID, Clean Architecture, Domain Driven Design (DDD), Clean Code, Repository Pattern, logging, validation, exception handling, Swagger Open API and other standards/features were highly used for designing the projects.

#### Catalog microservice: 
* ASP.NET Core Web API application 
* REST API (CRUD operations)
* **MongoDB database** connection and containerization
* Repository Pattern Implementation

#### Basket microservice: 
* ASP.NET Core Web API application 
* REST API (CRUD operations)
* **Redis database** connection and containerization
* Consume **Discount Grpc Service** for inter-service sync communication to calculate product final price
* Repository Pattern Implementation

#### Discount microservice which includes;
* ASP.NET **Grpc Server** application
* Build a Highly Performant **inter-service gRPC Communication** with Basket Microservice
* Exposing Grpc Services with creating **Protobuf messages**
* Using **Dapper for micro-orm implementation** to simplify data access and ensure high performance
* **PostgreSQL database** connection and containerization
* Application of retry policy for database migration

#### Ordering Microservice
* ASP.NET Core Web API application 
* Developing **CQRS with using MediatR, FluentValidation and AutoMapper packages**
* Consuming **RabbitMQ** BasketCheckout event queue with using **MassTransit-RabbitMQ** Configuration
* **SqlServer database** connection and containerization
* Using **Entity Framework Core ORM** and auto migrate to SqlServer when application startup

#### Docker Compose establishment with all microservices on docker:
* Containerization of microservices
* Containerization of databases
* YML files configuration
* Use Portainer to manage docker containers

## Setup and Run Project

### Requirements

* [Visual Studio](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 5](https://dotnet.microsoft.com/download/dotnet-core/5)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Steps to get start

1. At the root directory which include **docker-compose.yml** files, run below command to inicialize docker microservices containers:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
> Wait for docker compose all microservices, some microservices need extra time to work

> It is also possible to launch the project in visual studio by running (F5) when pointing to the docker-compose startup project

2. Microservices urls:

* **Catalog API -> http://localhost:8000/swagger/index.html**
* **Basket API -> http://localhost:8001/swagger/index.html**
* **Discount API -> http://localhost:8002/swagger/index.html**
* **Ordering API -> http://localhost:8004/swagger/index.html**
* **Portainer -> http://localhost:9000** - User: admin/admin1234
* **pgAdmin PostgreSQL -> http://localhost:5050** - admin@postgres.com/admin1234
