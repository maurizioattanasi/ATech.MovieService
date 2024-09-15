# ATech.MovieService

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![MongoDB](https://img.shields.io/badge/MongoDB-%234ea94b.svg?style=for-the-badge&logo=mongodb&logoColor=white)

Example design of a .NET 8 service adopting the following paradigms and/or tools:

- [Clean Architecture](https://betterprogramming.pub/the-clean-architecture-beginners-guide-e4b7058c1165)
- [Domain Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [CQRS](https://learn.microsoft.com/it-it/azure/architecture/patterns/cqrs)
- [MediaR](https://github.com/jbogard/MediatR)
- [.NET 8](https://dotnet.microsoft.com/it-it/download/dotnet/8.0)
- [Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-8.0)
- [Entity Framework Core 8](https://learn.microsoft.com/en-gb/ef/)
- [FastEndpoints](https://fast-endpoints.com)
- [MongoDb](https://www.mongodb.com)

## Project Description

This project is an example of a possible implementation of some of the concepts, strategies and technologies, listed in the preface that, to date, I use in my services.

### Solution anatomy
### Solution anatomy

The solution is structured around one of the concepts defined by clean architecture, *Onion Architecture*, a software design pattern that offers several advantages, particularly in terms of maintainability, testability, and flexibility.
The solution is structured around one of the concepts defined by clean architecture, *Onion Architecture*, a software design pattern that offers several advantages, particularly in terms of maintainability, testability, and flexibility.

![onion-architecture](./img/onion-architecture.jpeg)

The image above is one of the possible representations of onion architecture. Each of the layers represented in the diagram corresponds to a design of the solution.

| Name                              | Description                                                                                                                       | Type     |
| :-------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------- | :------- |
| ATech.MovieService.Domain         | The layer encapsulates the heart of the domain models and business logic.                                                         | classlib |
| ATech.MovieService.Application    | The Application layer manages business logic, harnessing the services of the Domain and Infrastructure layers.                    | classlib |
| ATech.MovieService.Infrastructure | Infrastructure manages database access and external services.                                                                     | classlib |
| ATech.MovieService.Api            | Presentation layer that enables interaction with users or external systems, using the services provided by the Application layer. | web      |
|                                   |                                                                                                                                   |          |

THe three layers are injected as services in **Program.cs**.

```cs
var builder = WebApplication.CreateBuilder(args);

...

{
    builder
        .Services
        .AddPresentation()
        .AddApplication(configuration)
        .AddInfrastructure(configuration);
}
```

1. **Presentation Layer**: The AddPresentation() method registers services related to the presentation layer, which typically includes controllers, views, and other UI components.
2. **Application Layer**: The AddApplication(configuration) method registers services for the application layer, which contains the business logic and application-specific rules.
3. **Infrastructure Layer**: The AddInfrastructure(configuration) method registers services for the infrastructure layer, which handles data access, external services, and other infrastructure-related concerns.
   
By organizing the code in this manner, the application adheres to the principles of Onion Architecture, promoting a clear separation of concerns and dependency inversion. This ensures that each layer interacts with the others through well-defined interfaces, enhancing maintainability and testability.