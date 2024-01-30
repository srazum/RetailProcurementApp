# RetailProcurementApp

This app is a simple app that allows you to create a purchase order and manage all entities for a simple procurement logic. This app is built using the following technologies:
ASP.NET Core 8
Entity Framework Core 8
PostgreSQL 12
SignalR
Docker
XUnit
etc.

Due to its simplicity all functionality is contained in a single project, although a different approach would be used for app that would continue to grow.

## Getting Started

### Deploying the app with PostgreSQL with docker-compose

To deploy the app with docker, you will need to have docker and docker-compose installed on your machine. You can download docker from [here](https://www.docker.com/products/docker-desktop). Once you have docker installed, you can run the following command from the root directory of the project:

```
docker-compose up
```

This will build the docker images and run the containers. Once the containers are running, you can access the app at https://localhost:8080/swagger.


### Logging in Swagger

Swagger uses a simple authentication mechanism. To log in:
1. user needs to use Login endpoint and send username and password in the following format:

        {
            "username": "<username set in docker-compose.yml or project usersecrets>",
            "password": "<project set in docker-compose.yml or project usersecrets>"
        }

2. Result received is a Token, which needs to be entered in the following way:

        1. Click on Authorize button in Swagger UI
        2. In Value field, enter "<token>" (without quotes)
        3. Click Authorize button

3. After successful login, user can use other endpoints

### Running an app with Visual Studio

To setup environment, it is neccessary to set client secrets in Visual Studio.
    1. Right click on RetailProcurementApp project and select Manage User Secrets
    2. Set username and password in the following format:

    {
      "USER": "user",
      "PASSWORD": "password",
      "Kestrel:Certificates:Development:Password": "3b55081a-d607-4939-b90e-dfc7f908f024",
      "JWT_KEY": "thisisasecretkey@123thisisasecretkey@123",
      "JWT_ISSUER": "RetailProcurementApp",
      "JWT_AUDIENCE": "http://localhost:49173",
      "ConnectionStrings:DefaultConnection": "host=<db_server_ip>;port=5432;database=retailprocurementapp;username=postgres;password=postgres;"
    }

### Running tests

Unit tests are testing Web API controllers, and need password and username to be set in user secrets file in the following format:

    {
      "Settings": {
        "USERNAME": "user",
      "PASSWORD": "password"
      }
    }


## Features

Solution contains: 
- endpoints CRUD operations over StoreItems, Suppliers, Orders and Statistics
   - Orders endpoint is an added functionality, which allows client to create new order and get some statistics afterwards
- business logic and relations over those entities
- ef core code first database design and migrations (migrations are done automatically on startup, due to simple use-case)
- added Bogus seed data for all entities and relations
- swagger UI for testing endpoints
- basic authentication and authorization mechanism
- SignalR hub for sending notification to clients when new order is created (only hub is implemented, client side is not implemented)
- unit tests for controllers (using in memory database  and starts its own test server)
- integration tests for business logic (using in memory database)
- Dockerfile for building docker image of API project and docker-compose.yml for running API and PostgreSQL in docker containers

What was done partially:
- SignalR hub provides hub for sending notifications to clients when new order is created, but client side is not implemented
- unit tests and integration test were implemented only for some of the endpoints and some business logic

What could be improved:
- better error handling and more versatile error messages and return status codes
- implement logging and monitoring with tools like Seq or more advanced tools
- more creative way of using OOP patterns and principles
- implement caching for statistics or quarterly plans (quarterly plans and statistics may be cached for some time based on how often is data updated)
- using automapper or manual mapping mechanism (didn't use it due to unknown business requirements

### OOP patterns and principles used

1. Generic repository pattern

Generic repository pattern allows us to have single generic interface and implementation classes which operate on different entities. This pattern is used in this app for entities that can be used for simple CRUD operations.

    public interface IGenericEntityOperations<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }

2. Factory pattern

Factory pattern is used to create instances of data managers. This pattern is used in this app to create instances of OrderManager and StatisticsProvider.

    public static class DataManagerFactory
    {
        public static IOrderManager GetOrderManager(RetailProcurementDbContext dbContext)
        {
            return new OrderManager(dbContext);
        }
        public static IStatisticsProvider GetStatisticsProvider(RetailProcurementDbContext dbContext)
        {
            return new StatisticsProvider(dbContext);
        }
    }

3. Throughout code there is also extensive usage of dependency injection
