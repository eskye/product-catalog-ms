# product-catalog-ms

# Introduction

This project is aimed at using the concept of a microservice-based architecture to build a product catalog managing system.

# Design

To achieve this, we setup 4 projects, Product.Api Service, Inventory Service, Identity Service and Api gateway.

The product api basically we have 5 endpoints:
1. List all products with pagination and search feature.
2. Create product
3. Removes a product
4. Update a product
5. Get Product details

The Identity service is basically used for managing user access into the entire system. It contains 2 endpoints, generate access token and refresh expired access token feature.


In a production environment, the API Gateway works as a single entrypoint, so all these 4 endpoints will pass through it, for each endpoint we have a rule:
1. On endpoint 1 we will forward to the Product service which will return the list of all products
2. On endpoint 2 we will forward to the aggregation service, which will make a gRPC call to the Product service to ensure that the product exists and then will make a call to the Basket service to add this product in the basket
3. On endpoint 3 we will forward to the Basket service to remove the product from a basket
4. On endpoint 4 we will forward aggregation service, which will make a gPRC call to the Basket service to get the items in that basket and then to the Product service to get information such as Name, Description and Price

# Running the project
It is necessary to start all services when running the project, for this you must:
1. Click on the solution with the right mouse button and select option `Configure Startup Projects...`

![Configure Startup Projects](./docs/images/ConfigureStartupProjects.png)

2. Select the `Multiple startup projects` and set the `Action` of the four projects to `Start`

![Action Start Projects](./docs/images/ActionStartProjects.jpg)

With this configuration, when running the solution, four services will be started:
- Ocelot => https://localhost:7097/swagger
- Aggregator => https://localhost:7067/swagger
- Product API => https://localhost:7087/swagger
- Basket API => https://localhost:7298/swagger

At first, only one browser will open with Ocelot's Swagger and through it you will be able to access all the other services, but if you wanted, you can also access each of the services separately through the addresses that were listed above.

![Ocelot project swagger](./docs/images/OcelotSwagger.png)
