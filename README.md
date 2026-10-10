# URL Shortener

A URL shortening REST API built with **ASP.NET Core Minimal APIs and .NET 9**.

The application converts long URLs into shorter links that can be used to redirect users to their original destinations.

The project follows **Clean Architecture** to separate application layers and maintain clear boundaries between business logic, use cases, API endpoints, and infrastructure.

It exposes two main endpoints:

- **Create a short URL:** Accepts a long URL, generates a unique short code, and persists the URL mapping in the database.
- **Retrieve the original URL:** Uses a short code to retrieve the corresponding original URL. The result can then be used to redirect the user to the original destination by accessing the short URL.

The project also uses **MediatR** for request handling, **FluentValidation** for input validation, and **Entity Framework Core** for database access.
