# Demo Application

A demonstration application built to showcase a layered .NET application architecture, domain modelling, Web API development, and a WPF client application.

The solution is intentionally kept in a single repository to make it easy to review and run. It demonstrates how the different application layers can work together while maintaining clear separation of responsibilities.

## Overview

The application consists of three main areas:

* **Domain / Data Model** – Core business entities and domain-related logic.
* **Web API** – REST API providing access to application functionality and data.
* **WPF Application** – Desktop client consuming the Web API.

The solution is designed as a small, self-contained example rather than a production application. The primary goal is to demonstrate code organisation, design decisions, and the interaction between the different components.

## Solution Structure

EmployeeSolution/
│
├── EmployeeDomain/
│   ├── Entities/
│   ├── Models/
│   └── ...
___
│
├── MyApi.Api/
│   ├── Controllers/
│   ├── Services/
│   ├── Configuration/
│   └── ...
│
├── Empsolution.Wpf/
│   ├── Views/
│   ├── ViewModels/
│   ├── Model/
│   ├── Services/
│
└── DemoApplication.sln

## Explanation of WPF  The WPF client follows an MVVM-oriented structure:

# Views – XAML-based UI responsible for presentation and user interaction.
# ViewModels – Expose data and commands to the Views and manage presentation state.
# RecordService – Acts as an application-level service for the ViewModels. It coordinates calls to the underlying services and organises/transforms the returned data into a form suitable for the ViewModels.
# Services – Encapsulate communication with the Web API, including HTTP requests, serialisation and API response handling.
# Model – Contains the client-side DTOs/models used when communicating with the API and binding data within the application.



### Domain / Data Model

Contains the core application concepts and data structures.

This layer is intended to remain independent of the presentation technologies. It contains the entities and models used by the application and, Any Business rule generally go in there.

### Web API

The API provides the interface between the client application and the underlying application/data layer.

It demonstrates:

* RESTful endpoints
* Request/response models
* Dependency injection
* Validation
* Error handling
* Separation between API and domain concerns
* Data access/service abstraction

### WPF Client

The WPF application provides the desktop user interface and communicates with the Web API.

The client demonstrates:

* MVVM
* Data binding
* Commands
* API communication
* Asynchronous operations
* Basic UI validation/error handling
* Separation of presentation and application logic

## Architecture

At a high level, the application follows this flow:

┌─────────────────────┐
│     WPF Client      │
│       (MVVM)        │
└──────────┬──────────┘
           │ HTTP
           ▼
┌─────────────────────┐
│      Web API        │
│ Controllers/Services│
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ Domain / Data Model │
│ Entities & Logic    │
└─────────────────────┘


The intention is to keep responsibilities separated so that the WPF application does not need to know how the underlying data is stored or how the business logic is implemented.

## Technologies

The application uses:

* C#
* .NET
* ASP.NET Core Web API
* WPF
* XAML
* MVVM
* Dependency Injection
* REST/HTTP
* [Entity Framework Core ]
* [SQL Server  other database]
* Git / GitHub

## Prerequisites

To build and run the application, install:

* [.NET SDK 9 and above]
* [Visual Studio version 2022 and above]
* [SQL Server ]

## Getting Started

### 1. Clone the repository

git clone https://github.com/[your-github-username]/[repository-name].git
cd [repository-name]
```

### 2. Open the solution

Open:

```text
DemoApplication.sln
```

in Visual Studio.

### 3. Configure the application

Update the appropriate configuration files with the required settings.

For example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "[connection string]"
  }
}
```

Do not commit production credentials, passwords, API keys, or other secrets to the repository.

### 4. Build the solution

Build the solution from Visual Studio, or run:

```bash
dotnet build
```

### 5. Start the Web API

Run the API project first.

The API should start on the configured local URL, for example:

```text
https://localhost:7001
```

The exact URL will depend on the local launch configuration.

### 6. Start the WPF application

Once the API is running, start the WPF project.

The WPF application will communicate with the API using the configured API endpoint.

## Configuration

The API URL used by the WPF application can be configured in:

https://localhost:[port]/swagger/Index.html
```

## Design Decisions

A few of the main design considerations in this demo are:

### Separation of concerns

The WPF application is responsible for presentation, while the API handles HTTP/application interaction and the domain layer contains the core application concepts.

### Dependency Injection

Dependencies are injected rather than instantiated directly where practical. This makes components easier to test, replace, and maintain.

### MVVM

The WPF application uses the MVVM pattern to keep UI concerns separate from application and presentation logic.

### Asynchronous operations

API and I/O operations use asynchronous APIs where appropriate to avoid blocking the UI thread.

### DTOs / API Models

API contracts are separated from internal domain entities where appropriate. This avoids exposing internal implementation details directly through the API.

## Testing

Unit Testing 
Example:

  MyApi.Tests 
Only  EmployeeController and ContractController Inplemented implemented

in VsCode Terminal. 
 dotnet test


## Error Handling

The application includes basic error handling for scenarios such as:

* Invalid requests
* Validation failures
* API communication failures
* Missing data
* Unexpected application errors

The intention is to demonstrate the approach rather than provide a complete production-grade error handling framework.

## What This Demo Demonstrates

This project is intended to demonstrate my approach to:

* Designing a small multi-layer application
* Domain modelling
* Building REST APIs
* Developing WPF applications
* Applying MVVM
* Using dependency injection
* Separating UI, API, and domain responsibilities
* Working with asynchronous operations
* Handling application errors and validation
* Structuring a maintainable .NET solution
* Using Git and GitHub for source control

## Known Limitations

This is a demonstration application rather than a production system.

Some areas have deliberately been kept simple, including:

* Authentication/authorisation
* Advanced logging and monitoring
* Deployment infrastructure
* Production configuration
* Comprehensive test coverage
* Advanced resilience/retry mechanisms

These could be expanded depending on the requirements of a production implementation.

## Possible Future Improvements

Potential improvements could include:

* Authentication and authorisation
* More comprehensive automated testing
* Structured logging
* Global API exception handling
* Improved validation
* API versioning
* Integration tests
* CI/CD pipeline
* Containerisation
* Production deployment configuration
* Improved WPF UX and accessibility

## Running the Demo

For a quick demonstration:

1. Start the Web API.
2. Confirm that the API is running.
3. Start the WPF application.
4. Use the WPF interface to interact with the application.
5. Optionally use Swagger to inspect and test the API directly.

## Repository Purpose

This repository has been created as a technical demonstration and is intended to provide an example of my coding style, architectural approach, and familiarity with .NET application development.

Feedback and questions are welcome.
