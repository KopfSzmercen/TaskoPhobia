# What is this repo?

It's not a tutorial. It's a repo for a project in which I'll be learning creating WebApis using.NET platform and I want to create a documentation and steps involved in the whole process therefore techniques and practices in this project are not meant to be followed by anyone. It's heavily based on [This course](https://platform.devmentors.io/courses/solidne-web-api).

# What are requirements for the project?

The project is a simple task-management tool in which:

1. To use app users have to create accounts.
2. There are 3 account types:
   1. free
   2. basic
   3. extended
3. User can create a project (3 projects for free account, 6 for basic and unlimited number of project for extended account)
4. Project can have many tasks
5. Project can have many project sections (3 for free account, 6 for basic, unlimited for extended)
6. Each project section can have many tasks
7. Each task can be assigned to one or many users
8. Project section may be finished only when all tasks are finished
9. Project can be finished once every task and section is finished
10. User can invite others to join a project. If a user rejects an invitation from another user 3 times, then they can not be invited again

# Project initialization

## Creating the project

The project will be initialized using .NET CLI

1. Create a new solution → _dotnet new sln_
2. Add standard catalogues → src and tests
3. Add a project of type Web API in src. I’m gonna use the name of my project with a suffix API: _dotnet new webapi -n TaskoPhobia.Api_
4. Add API project to the main solution file _dotnet sln add src/TaskoPhobia.Api/TaskoPhobia.Api.csproj_
5. Verify if everything is OK: ~TaskoPhobia> \***\*\*\*\*\*\*\***dotnet build\***\*\*\*\*\*\*\***

## Adding layers to the project

So far, we have just one layer which is responsible for many things like frameworks, configuration etc. To separate concerns we create 4 layers:

- API: already created by dotnet cli. Handles controllers and application startup.
- Core: the heart of our application: stores domain: enttities, value objects etc, so basically the main logic, conditions and rules in our domain.
- Application: Interacts with the application performing tasks required by the end users. Mainly focused on applications services used to orchestrate the steps required to fulfill the commands imposed by the client
- Infrastructure: encapsulates technology, responsible for managing frameworks like ORMs, Swagger etc. Delivers implementations for abstractions defined in the lower layers.

Add these layers into the project as class libraries. Add references to projects in the following way:

- API has reference to Infrastructure
- Application has reference to Core
- Core does not reference anything
- Infrastructure references Application

## Setting up Infrastructure extension class

To prevent Program.cs from having too many responsibilities with configuring of the project we make use of extension methods. This technique makes sure that every layer registers components it has.

1. Create static Extensions class in Infrastructure. Install DI abstractions package from NuGet in Infrastructure and declare _IServiceCollection AddInfrastructure()_ static method.
2. Call the metod in Program.cs on builder: _builder.Services.AddInfrastructure();_

To the same for every layer so in Program.cs every layer is added.

## Adding Swagger

Although Swagger has been automatically added by .net cli, as mentioned earlier it should be moved into infrastructure layer.

1. Instal Swashbuckle.Swagger and Swashbuckle.Annotations
2. To use Swagger middleware create **\*\***\*\*\*\***\*\***\***\*\***\*\*\*\***\*\***WebApplication UseInfrastructure**\*\***\*\*\*\***\*\***\***\*\***\*\*\*\***\*\*** static method in Infrastructure.Extensions and call it in Program.cs

# Adding Database

In this section I’ll set up connection with database using EF core. For now I’m not gonna define any entities, just bare DB context, DB connection initialization as well as automatic migrations. In this section I’ll also use some techniques of app configuration showed in the course on which this project is based. DB will be run on docker using docker-compose.

## Initial EF Core Setup

1. Install EF Core in Infrastructure.
2. For files concerned with DB, create a directory called DAL.
3. Create a db context for the project.
4. Create an extensions class in DAL to connect to the DB. The db context is added to DI in scoped mode. To use PostgreSQL, Npgsql has to be installed and specified in the db context configuration.

## Automatic Migrations on App Startup

In order to always be sure that every migration in the project has been applied to the DB, it is useful to create automatic migrations on app startup. To do this, I’ll use background tasks provided by the .NET framework.

1. Create a _DatabaseInitializer_ class in the DAL directory which implements the _IHostedService_ interface provided in the **_Microsoft.Extensions.Hosting.Abstractions_** package.
2. In the **_StartAsync_** method, use the service provider registered through the constructor and create a scope because by default the DB context is registered in scoped mode by the DI mechanism.
3. In the **_StartAsync_** method, apply pending migrations.
4. Register the **DatabaseInitializer** using _services.AddHostedService_ in the **AddPostgres** method.

## Application Configuration

To avoid hard-coding the DB connection string, password, secrets, etc., we can declare them in appsettings and retrieve them where we need them. This will make it easier to change them as well as configure the app in different environments.

There is one problem with using the interface \***\*IConfiguration\*\*** that is magic strings. In order to avoid this I’m gonna create _AppOptions_ class which will be a helper for getting values from appsettings with type-safety.

1. In AddInfrastructure give IConfiguration as a parameter and pass it fromProgram.csby using builder.Configuration
2. To bind configuration to the class add _Microsoft.Extensions.Options.ConfigurationExtensions_ package
3. As helper class responsible for configuring DB I’ll create PostgresOptions class in which I’ll expose ConnectionString.
4. To bind configuration to the PostgresOptions class I’ll install Microsoft.Extensions.Configuration.Binder package. Provided that types and names of PostgresOptionsClass aligns with appsetings section, calling _section.Bind(options)_ will assign values from appsetting to object of PostgresOptions.
5. This logic can be extracted to a separate method GetOptions<T>which will return options for a given section name

```csharp
public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
```

It is worth mentioning that point 5 included ‘this’ which makes it an extension method i.e. GetOptions method is an extension method for the IConfiguration interface. It allows you to call the GetOptions method directly on an instance of IConfiguration as if it were a member method of that interface.

This in result gives **\*\***\***\*\***AddPostgres**\*\***\***\*\*** method in this form

```csharp
public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<PostgresOptions>(section);

        var options = GetOptions<PostgresOptions>(configuration, SectionName);

        services.AddDbContext<TaskoPhobiaDbContext>(x => x.UseNpgsql(options.ConnectionString));
        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
```

If everything went well app should start with no errors and in the logs there should be some DB queries executed, which is exactly what we want since it means that the connection with DB has been successfully established.
