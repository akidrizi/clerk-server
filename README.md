# Description

ClerkServer is a Web Application that collects random users from https://randomuser.me/api and saves them in chunks in the database. Developed with .NET Core 3.1 and MySQL 5.7.

The API serves three (3) anonymous endpoints that allow the user to control the system to either collect new or read existing user(s). All endpoints are accessible through Swagger, but for greater experience, it is highly recommended to use a well known Rest Client.

# How to run

From the root path of the project; build the Docker image:

```bash
docker-compose build
```

 By default, the API will be built on `port :5100` and MySQL on `port :3310`. Ensure these ports are free else edit `docker-compose.yml`.

```python
    ports:
    - "5100:80"
    .
    .
    .
   ports:
    - "3310:3306"
```

Run the image.

```bash
docker-compose up
```

Open the API on a browser at `localhost:5100` to view Swagger with extra documentation for the API. Feel free to connect to the database with user: `root` and password: `clerk`

## Folder Structure

ClerkServer follows best practices for folders structure and naming conventions as instructed from the official documentation of the framework.

Database engine can be changed as easy as switching ORM driver and regenerating new initial migration.

Following short explanation of each folder.

### Root Folder

    .
    ├── ClerkServer             # Project Folder
    ├── ClerkServer.UnitTest    # Unit Tests Folder
    ├── ClerkServer.sln         # Solution file for JerBrains Rider/Visual Studio
    ├── docker-compose.yml      # Docker Compose instructions
    └── README.md

### Project Folder 

    ├── Constants               # Constant primitives and objects
    ├── Contracts               # Interfaces for repository
    ├── Controllers             # Controller files with endpoint logic
    ├── DTO                     # Data Transfer Objects
    ├── Entities                # Database Entities and Database Context
    ├── Extensions              # Extension methods (segregate business/core logic)
    ├── Filters                 # Endpoint filters (.gitkeep)
    ├── Migrations              # ORM-generated database migrations
    ├── Properties              # .NET Core Launch properties
    ├── RandomUserAPI           # HTTP Client Service for RandomUserApi
    ├── Repository              # Database Repository
    ├── ClerkServer.csproj      # Project file (equivalent of a package.json)
    ├── Program.cs              # Main root file to start the Framework.
    ├── Startup.cs              # Framework startup file.
    ├── appsettings.*.json      # Various user generated strings used as settings
    └── Dockerfile

### Unit Test Folder

    ├── Constants               # Constant primitives and objects
    ├── Repository              # SUT* target namespace
    ├── ClerkServer.UnitTests.csproj
    └── Dockerfile

\*System Under Test. 
UnitTest namespaces point the file that is being tested. Each test file that requires access to the persistence layer is accompanied with a `*Fixture.cs` which creates a disposable connection with the database.

## How to Test

Updating Instructions soon.
