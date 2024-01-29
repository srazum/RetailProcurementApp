# RetailProcurementApp

This app is a simple app that allows you to create a purchase order and send it to a vendor. It also allows you to view all purchase orders that have been created and sent to a vendor. This app is built using the following technologies:
ASP.NET Core 8
Entity Framework Core 8
Angular 8
PostgreSQL 12
SignalR 3.1
Bootstrap 4.3

Due to its simplicity it is contained in a single project, although a different approach would be used for app that would continue to grow.

## Getting Started

To get started, clone this repository to your local machine. You will need to have the following installed on your machine:
- .NET Core 8
- Node.js 12
- PostgreSQL 12
- Angular CLI 8
- Visual Studio Studio (or another IDE of your choice)

### Deploying the app with PostgreSQL with docker-compose

To deploy the app with docker, you will need to have docker installed on your machine. You can download docker from [here](https://www.docker.com/products/docker-desktop). Once you have docker installed, you can run the following command from the root directory of the project:

```
docker-compose up
```

This will build the docker images and run the containers. Once the containers are running, you can access the app at http://localhost:4200.

### Logging in

The app uses a simple authentication mechanism. The username is "user" and the password is "password". Once you are logged in, you will be able to create purchase orders and view all purchase orders.

### OOP patterns used

1. Generic repository pattern
