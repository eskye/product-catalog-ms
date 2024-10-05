# product-catalog-ms

# Introduction

This project is aimed at using the concept of a microservice-based architecture to build a product catalog managing system.

# Design

To achieve this, we setup 4 projects, Product.Api Service, Inventory Service, Identity Service and Api gateway.

The product service implement the following endpoints:
    - `GET /api/products`: Retrieve a paginated list of products, supporting optional filtering, sorting, and caching.
    - `POST /api/products`: Add a new product.
    - `GET /api/products/{id}`: Retrieve details of a specific product.
    - `PUT /api/products/{id}`: Update an existing product.
    - `DELETE /api/products/{id}`: Remove a product.

The Identity service is basically used for managing user access into the entire system. It contains 2 endpoints, generate access token and refresh expired access token feature.

In a production environment, the API Gateway works as a single entrypoint leveraging ocelot to handle the request to each routes. All the services can be accessed through the API Gateway url `http://localhost:8080/swagger/index.html` and the swagger page for all the services can be accessed.

# Technology Used
- .NET 8 Web API
- SwaggerUI (OpenAPI)
- FluentValidation
- JWT
- Ocelot 
- Serilog 
- Postgres Database
- Docker Compose

# Running the project
## To get started, follow the below steps:

- Install Docker Desktop (for Windows) / Docker (for Linux/Mac)
- Clone the Solution into your Local Directory
- On the Repository root you can find the docker-compose.yml file
- Run the below command to build and run the solution in Docker (requires a working Docker installation)
```
 docker-compose build --force-rm --no-cache && docker-compose up

 ```
- Once the containers start successfully navigate to http://localhost:8080/swagger/index.html
  

