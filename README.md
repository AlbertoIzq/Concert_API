# Concert API

ASP .NET Core Web API project to manage a database of songs.

_TECHNOLOGIES USED_

- ASP .NET Core Web API:
- Entity Framework Core: Object Relational Mapping (ORM) framework
- SQL Server: Relational DataBase Management System (RDBMS)
- C# libraries:
  - Automapper: Object-to-object mapping library
  - DotEnv.Core: Manage env. files
  - Identity: Manage authentication and authorization
  - JWT Token: Manage JWT Token for authentication

_CONCEPTS USED_

- REST (Representational State Transfer)
- DDD (Domain Driven Development)
- Asynchronous programming
- Design patterns: Repository
- Environment variables
- Authentication and Authorization, JWT Token
- Image upload to a local structure and serving static files through API

_TODO_

- Create an IRepository<T> and refactor all repositories?
- Correct navigation properties showing null value when Song Update
- Add more filtering and sorting for Song
- Add filtering and sorting for Artist
- Add pagination for Artist
- Add Genre, Language and Service Controllers

_BUGS_

