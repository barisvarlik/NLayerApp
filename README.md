# NLayerApp

Backend and API application written following the Clean Architecture. 

## Core Layer

Contains core functionality of the application. Entity Models, Data Transfer Objects (DTOs), and Contracts (Interfaces of the other layers) are located in this layer.

## Repository Layer

Contains databaaase related operations. Repository and UnitOfWork implementations are located here. Also contains other db related logic like Entity Configurations, Migrations and Context.

## Configuration Layer

Contains constant values which will be needed in multiple outer layer like JWT secret key and database connection string.

## Service Layer

Contains the business logic of the application. Services, Exceptions, Mappings and Validations are implemented here.

## Caching Layer

Contains Caching related implementations.

## API Layer

Contains API implementation that will communicate with the frontend. Controllers, Middewares, Modules and Filters are implemented here.

## Web Layer

An MVC Project that will communicate with the N-Layer backend. Soon to be implemented.
