# IcTest

The application is implemented in the latest .net version and is split it in 3 main parts.
1.	Blocks Importer which is a background service that is responsible to seed the database with the new values
2.	Api Blocks which is the main module that interacts with the storage to read, prepare the data for the client
3.	Gateway which is the entry point for the whole application
The solution is dockerized and is using 5 containers, one for each part of the application plus other 2 that are used for the database (PostgreSQL) and the caching mechanism (Redis)

The application is using 
1. The implementation is based on CQRS pattern using the MediaR library to send and receive the message on the block domain.
2. Fluent validation for validation
3. Standard API response
4. Centralized exception handling 
5. Repository Pattern
6. Caching using redis and Decorator pattern
7. Logging
8. Minimal APIs using Carter library
9. Health checks & CORs
10. Swagger
11. Use of Mapster
12. Pagination
13. Background Hosted Services


## API Endpoints

## How to run
1. Make sure that docker is running
2. Navigate on the terminal to root folder where the docker-compose.yml file exists
3. Run on the console `docker compose up --build`
