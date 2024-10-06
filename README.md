# Introduction

This project is aimed at using the concept of a microservice-based architecture to build a product catalog managing system.

# Design

To achieve this, we setup 4 projects, Product.Api Service, Inventory Service, Identity Service and Api gateway.

The product service implement the following endpoints:
-  `GET /api/products`: Retrieve a paginated list of products, supporting optional filtering, sorting, and caching.
-  `POST /api/products`: Add a new product.
-  `GET /api/products/{id}`: Retrieve details of a specific product.
-  `PUT /api/products/{id}`: Update an existing product.
-  `DELETE /api/products/{id}`: Remove a product.

The Identity service is basically used for managing user access into the entire system. It contains 2 endpoints, generate access token and refresh expired access token feature.

When the identity service start running, some default users are seeded to the database automatically.

1. To generate access token use the payload below as the request body for `http://localhost:8080/api/v1/auth/authenticate` endpoint
```JSON
{
  "email": "admin@identity.co",
  "password": "P@ssw0rd1"
}

```

Authenticate Response Body:

```JSON
{
  "data": {
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW5AaWRlbnRpdHkuY28iLCJzdWIiOiJmMTA4YmY5MC0yMWQwLTQ2M2EtOTQ1My0zODVjNTMwYzU2YmUiLCJqdGkiOiIyMTJjNDJiNS0xNWU5LTQwYmMtYjM4YS0wNjU2NWNlZGJjN2UiLCJlbWFpbCI6ImFkbWluQGlkZW50aXR5LmNvIiwiZ2l2ZW5fbmFtZSI6IkFkbWluIiwiZmFtaWx5X25hbWUiOiJVc2VyIiwicmlkIjoiNWZmMjZhYjFmMWY3NDdhNzkzNjJjZTc2OTMxNDFjYzgiLCJleHAiOjE3MjgyMjEwMjAsImlzcyI6IkNhdGFsb2dJZGVudGl0eU1TIiwiYXVkIjoiQ2F0YWxvZ0lkZW50aXR5VXNlciJ9.bUwWOYhazCIpumD0XXbECQIoADxj4PwL2Ym5MxM_tYk",
    "isAuthenticated": true,
    "userInfo": {
      "firstName": "Admin",
      "lastName": "User",
      "email": "admin@identity.co",
      "phoneNumber": "09000000000"
    },
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW5AaWRlbnRpdHkuY28iLCJzdWIiOiJmMTA4YmY5MC0yMWQwLTQ2M2EtOTQ1My0zODVjNTMwYzU2YmUiLCJqdGkiOiI1NWI1YjViNi1hNmI1LTQxMmEtYTNhMS1jZmZjYmQ3Nzc4OGUiLCJlbWFpbCI6ImFkbWluQGlkZW50aXR5LmNvIiwiZ2l2ZW5fbmFtZSI6IkFkbWluIiwiZmFtaWx5X25hbWUiOiJVc2VyIiwiZXhwIjoxNzI4MjE3NDIwLCJpc3MiOiJDYXRhbG9nSWRlbnRpdHlNUyIsImF1ZCI6IkNhdGFsb2dJZGVudGl0eVVzZXIifQ.DZ0SOLJHGXfe-_NlmYsW5F8e3ooXD8OoBdmaODG3nS4",
    "expireInSeconds": 1728221020
  },
  "succeeded": true,
  "resultType": 1,
  "message": "Authenticated successfully",
  "validationMessages": null
}
```

1. The refresh token allow user to refresh expired access token, it can be a very bad experience for user to be logged out like every 30 minute or more depending on your access token life time, which it is not recommended to allow your access token to be a static token. Refresh token help to resolve this issue, by refreshing the expired access token with the refresh token generated when you called the `/authenticate` endpoint. 
Refresh token URL: `http://localhost:8080/api/v1/auth/refreshtoken`

Refresh token request body
```JSON
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW5AaWRlbnRpdHkuY28iLCJzdWIiOiJmMTA4YmY5MC0yMWQwLTQ2M2EtOTQ1My0zODVjNTMwYzU2YmUiLCJqdGkiOiI1NWI1YjViNi1hNmI1LTQxMmEtYTNhMS1jZmZjYmQ3Nzc4OGUiLCJlbWFpbCI6ImFkbWluQGlkZW50aXR5LmNvIiwiZ2l2ZW5fbmFtZSI6IkFkbWluIiwiZmFtaWx5X25hbWUiOiJVc2VyIiwiZXhwIjoxNzI4MjE3NDIwLCJpc3MiOiJDYXRhbG9nSWRlbnRpdHlNUyIsImF1ZCI6IkNhdGFsb2dJZGVudGl0eVVzZXIifQ.DZ0SOLJHGXfe-_NlmYsW5F8e3ooXD8OoBdmaODG3nS4",
  "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW5AaWRlbnRpdHkuY28iLCJzdWIiOiJmMTA4YmY5MC0yMWQwLTQ2M2EtOTQ1My0zODVjNTMwYzU2YmUiLCJqdGkiOiIyMTJjNDJiNS0xNWU5LTQwYmMtYjM4YS0wNjU2NWNlZGJjN2UiLCJlbWFpbCI6ImFkbWluQGlkZW50aXR5LmNvIiwiZ2l2ZW5fbmFtZSI6IkFkbWluIiwiZmFtaWx5X25hbWUiOiJVc2VyIiwicmlkIjoiNWZmMjZhYjFmMWY3NDdhNzkzNjJjZTc2OTMxNDFjYzgiLCJleHAiOjE3MjgyMjEwMjAsImlzcyI6IkNhdGFsb2dJZGVudGl0eU1TIiwiYXVkIjoiQ2F0YWxvZ0lkZW50aXR5VXNlciJ9.bUwWOYhazCIpumD0XXbECQIoADxj4PwL2Ym5MxM_tYk"
}

```

Refresh token response body

```JSON
{
  "data": {
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW5AaWRlbnRpdHkuY28iLCJzdWIiOiJmMTA4YmY5MC0yMWQwLTQ2M2EtOTQ1My0zODVjNTMwYzU2YmUiLCJqdGkiOiJmZjQ5YTA5NC0wY2U4LTQ2MzgtODBkYy1mMzJkMGM0MzVlMTkiLCJlbWFpbCI6ImFkbWluQGlkZW50aXR5LmNvIiwiZ2l2ZW5fbmFtZSI6IkFkbWluIiwiZmFtaWx5X25hbWUiOiJVc2VyIiwicmlkIjoiZWNlY2UxZDdmNTdkNDM1ZTlhMzY2NzgwZjkzNjkzMmQiLCJleHAiOjE3MjgyMjE0MTAsImlzcyI6IkNhdGFsb2dJZGVudGl0eU1TIiwiYXVkIjoiQ2F0YWxvZ0lkZW50aXR5VXNlciJ9.PUufCX07_ABfrscvXn-DEZGTAkRJf8k9Fw9MwovaZHw",
    "isAuthenticated": true,
    "userInfo": {
      "firstName": "Admin",
      "lastName": "User",
      "email": "admin@identity.co",
      "phoneNumber": "09000000000"
    },
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW5AaWRlbnRpdHkuY28iLCJzdWIiOiJmMTA4YmY5MC0yMWQwLTQ2M2EtOTQ1My0zODVjNTMwYzU2YmUiLCJqdGkiOiI2OWI2NmRjNi00M2UyLTQwYzEtYWU1ZS0yODY3NDY2MTkyOGQiLCJlbWFpbCI6ImFkbWluQGlkZW50aXR5LmNvIiwiZ2l2ZW5fbmFtZSI6IkFkbWluIiwiZmFtaWx5X25hbWUiOiJVc2VyIiwiZXhwIjoxNzI4MjE3ODEwLCJpc3MiOiJDYXRhbG9nSWRlbnRpdHlNUyIsImF1ZCI6IkNhdGFsb2dJZGVudGl0eVVzZXIifQ.o6rb9nlFylQnhiAsNtq2-EQtU7bNC2xrYb6k_RuY_AQ",
    "expireInSeconds": 1728221410
  },
  "succeeded": true,
  "resultType": 1,
  "message": "Access token refreshed successfully",
  "validationMessages": null
}

```

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
  

