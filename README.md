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
  - Serilog: Simple .NET logging with fully-structured events

_CONCEPTS USED_

- REST (Representational State Transfer)
- DDD (Domain Driven Development)
- Asynchronous programming
- Design patterns: Repository
- Environment variables
- Authentication and Authorization, JWT Token
- Image upload to a local structure and serving static files through API
- Logging into a file

_TODO_

- API
  - Create an IRepository<T> and refactor all repositories?
  - Correct navigation properties showing null value when Song Update
  - Add more filtering and sorting for Song
  - Add filtering and sorting for Artist
  - Add pagination for Artist
  - Add Genre, Language and Service Controllers
  - Add logging to all action methods
  - Change logging messages by using const strings from Utility project. Add reflection to get method name and class name to add it into logging message.
  - Add API versioning?
- UI
  - Refactor base URL to appsettings

_BUGS_

