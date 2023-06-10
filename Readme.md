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

# Adding User Entity

An **entity** refers to a class or object that represents a domain concept or a data structure. Entities typically map to tables in a database or other data storage systems with the help of ORMs: EF Core in our case. They encapsulate the properties and behaviors related to a specific entity in the system, such as a user, product, order, or any other meaningful concept in the domain.

As it’s gonna be the first entity in the project, in Taskophobia.Core create directory Entities in which all entities will be defined.

Next, I’ll create ValueObject directory for **value objects**: a type that represents a concept in the domain with its own attributes but does not have an identity of its own. Unlike entities, which are identified by their unique identifiers, value objects are defined by the values they contain. They are immutable. In the case of value-objects we can use implicit conversion operators so it will make easier to implicitly convert between ex. Email and string (see completed Email value object below).

At this point some values can not fit domain rules ex. email is not a valid email. For that I’ll use domain exceptions:

1. Create abstract **\*\***\*\***\*\***\*\***\*\***\*\***\*\***CustomException**\*\***\*\***\*\***\*\***\*\***\*\***\*\*** class which derives from the built-in **\*\*\*\***\*\***\*\*\*\***Exception**\*\*\*\***\*\***\*\*\*\*** class. This will be helpful later on when we’ll implement exception handling.

```csharp
public abstract class CustomException : Exception
{
    protected CustomException(string message) : base(message)
    {
    }
}
```

1. Exceptions will look like this

```csharp
public sealed class InvalidEmailException : CustomException
{
    public InvalidEmailException(string emailValue) : base($"{emailValue} is not a valid email.")
    {
    }
}
```

1. We can throw them once we encounter some clash between data provided and business/domain requirements and rules.

```csharp
public sealed record Email
{
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value) || !IsValid(value)) throw new InvalidEmailException(value);
        Value = value;
    }

    public string Value { get; }

    private static bool IsValid(string emailToValidate)
    {
        return Regex.IsMatch(emailToValidate,
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }

		public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string email) => new(email);
}
```

For now User will have

1. Id
2. Email
3. Username
4. Password
5. Role
6. CreatedAt

properties. Each one of them except for Created at will have its own value-objects.

In the next part I’m going to connect entity to DB to prepare the project for performing CRUD operations.

# Connecting User Entity with DB

To do this, I’ll create a directory Configurations in DAL directory. In it I’ll have classes responsible for EF Core intergation with domain entities.

The class UserConfiguration will implement IEntityTypeConfiguration<User>. Also the User entity has to be added as ad DbSet in the main DbContext class defined for our project.

Builder options which I’m going to use:

1. builder.HasKey: Sets the properties that make up the primary key for this entity type.
2. builder.Property(…).hasConversion(): Configures the property so that the property value is converted to and from the database using the given conversion expressions. In Entity Framework Core, the **`HasConversion`** method is used to configure value conversions for properties mapped to the database. It allows you to specify how a property should be converted between its CLR type and the corresponding database representation.
3. builder.HasIndex(): Configures an unnamed index on the specified properties. If there is an existing index on the given list of properties, then the existing index will be returned for configuration
4. IsUnique - self-explanatory
5. HasMaxLength, HasMinLength
6. IsRequired

After configuring EF Core to correctly handle UserEntity, I’m gonna use EF Core tool to generate a [migration](https://www.prisma.io/dataguide/types/relational/what-are-database-migrations). Make sure EF Core Tools package is installed, startup project should be Taskophobia.Api and make sure that it has installed Microsoft.EntityFrameworkCore.Design package as well. If everything is good, migtation files should be generated in DAL/Migrations.

Now start the application. If migration has been successfully generated you should see applying migration queries generated by EF in logs.

```
info: Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20230604121211_Add-Users'.
```

In the DB there should be 2 tables:: Users and Migrations.

## Repositories

Now I’m going to create UsersRepository to interact with DB.

Firstly, in the Core project I’ll create an interface IUserRepository which will define a contract for User Repository

```csharp
public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByUsernameAsync(Username username);
    Task AddAsync(User user);
}
```

Notice the use of asynchronous code and the suffix Async in method names.

Into the constructor of the repository pass DB context of the project and pull Users DB set from them. Once the interface is created in DAL I’ll create Repositories forlder and create a class PostgresUserRepository.

When PostgresUserRepository is finished I have to register it in DI mechanism in the Infrastructure Extensions class created earlier in the ScopedMode so for individual requests.

To avoid errors with Timestamps I added

```csharp
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
```

in the Extensions as well.

If everything went well the project should be starting and runnig with no errors. At the moment I’ll work a bit on the Application layer to intertact with repositories.

# Application Layer

Now it’s the time to implement the base for application layer.

I’ll start with some abstractions for Commands and Queries. For this I create a solution Shared in src and Abstractions class project. In the project I’ll define interfaces for commands an queries. I’ll also create ICommandDispatcher

After interfaces are added, I’ll create a class library Shared to provide implementations for them.

### IServiceProvider

This is an interface responsible for providing instances of services or dependencies. It defines a method GetService used to retrive an instance of a service based on its type.

To be able to use \_serviceProvider.CreateScope() install Microsoft.Extensions.DependencyInjection.Abstractions

In dispatcher I create a scope for each command handler of TCommand type.

### IServiceCollection

Interface used to register and configure services in DI. IServiceCollection acts as a container where we can add service registrations

Using IServiceCollection command dispatcher should be registered in a separate Extensions class in Shared class library.

Now let’s implement a mechanism to automatically register all command/query handlers. To to this I’ll install a package Scrutor. It’ll scan assemby and register every class of type ICommandHandler<> with scoped lifetime.

Now reference Shared class lib. in Application Extension class and call AddCommands().

After settimng up DI, in Application I’ll create first command - SignUpCommand. Commands are just data-objects with no behaviour so nothing fancy here. In the Commands folder I’ll create Handlers directory.

Now let’s implement basic logic of sign up, now with no password security. After that I register Command dispatcher in Users controller and I call handleAsync(). In SignUpHandler normal known logic. User is added to DB and proper exceptions are thrown if there are duplications.

In the next part I’ll implement UoW pattern as well as password hashing.

# UoW and Password Security

### Implementing Unit Of Work Decorator

In this part I’ll implement UoW pattern and password security. Let’s start with the first one. At the same time I’ll implement decorator pattern.

Decorator pattern helps to hide some functionalities in a layer or place that is not responsible for it and should not know about this functionality ex. application service handler should not be responsible for logging. Decorato also helps to sustain open-closed principle in classes. Decorators work onion-like, a class is not aware of the existence of some additional functionalities.

THE ORDER OF DECORATORS IS IMPORTANT.

We decorate from inside → outside and decorators will work from outside → inside.

Unit Of Work: a pattern which ensures that all related DB operations are treated as a single transaction.

To implement UoW, I’ll create IUnitOfWork interface with a single method ExecuteAsync. In its implementstion PostgresUnitOfWork I’ll take action as argument, handle transaction and commit or rollback it if something goes wrong.

The code below is creating an asynchronous database transaction using the **`_dbContext`** object. The **`BeginTransactionAsync`** method returns a task representing the transaction, which is then assigned to the **`transaction`** variable. The **`await using`** pattern ensures that the transaction is disposed of correctly when it's no longer needed, freeing up any associated resources

```csharp
await using var transaction = await _dbContext.Database.BeginTransactionAsync();
```

Now, register Postgres UoW in services and make the decorator generic so it’ll decorate every command handler. To make it do so, in extensions use Scrutor and TryDecorate method on IService collection which will take typeof ICommandHandler<> and UnitOfWorkCommandHandlerDecorator<> as arguments.

Also the users repository will require some changes: AddAsync will not require SaveChangesAsync to be called after that and UpdateAsync does not have to await updating and can use \_users.Update().

Decorator will be registered with the same life-cycle as the decorated object’s.

### Implementing Password Security

So far, passwords are not hashed which is not a good practice as far as security is concerned.

In Application I’ll define interfaces for password security. Implementations will go into the Infrastructure layer. For managing hashes I’ll use IPasswordHasher shipped with .NET Core. After the implementation of password manager, I’ll create Security Extension (convention I use to register services).

# User Endpoints And JWT

In this part I’ll create endpoints to get users and user by id. These endpoints are going to be available only for admin, so JWT authorization has to be implemented beforehand.

## JWT

Firstly I’ll install Microsoft.AspNetCore.Authentication.JwtBearer package in Infrastructure. I’ll create Auth directory as well. I’ll define AuthOptions class which will be responsible for getting options for generating JWT such as Issuer or SigningKey which are set in app settings.

I’ll put all these settings in appsettings.json. Of course I’ll create Extensions class in which I’ll bind configuration to AuthOptions class as in for example configuring postgres connection. In Extensions I have to register authentication using services.AddAuthentication and configure it.

Next, in AddJwtBearer configuration I configure options based on which incoming tokens will be validated.

After finishing the configuration we have to register UseAuthentication middleware in UseInfrastructure method.

After these steps the app is able to validate tokens, however still we do not generate them whatsoever.

In application layer I’ll define IAuthenticator interface as well as JwtDto. In Infrastructure I’ll implement the IAuthenticator interface. In ctor I’ll configure the class by getting proper values to generate tokens.

Important: while configuring the list of claims, UniqueName claim will be available in HttpContext later on and it’ll make it possible to get user id. When done, register Authenticator as singleton in Auth/Extensions.

Now it’s time to create SignIn command to sign in the user. In this case however we have to return something from the SignInCommand but generally CommandHandler should not return any value. For handling this case I’ll create an interface ITokenStorage. Token storage will have scoped life cycle.

For the HttpContextTokenStorage I’ll register IHttoContextAccessor which will allow me to manipulate values in http context.

Important: register HttpContextAccessor in extension class, otherwise it won’t be avaliable. TokenStorage may be registered as singleton since HttpContextAccessor has already scoped life cycle.

### Get me Quyery

For queries I’ll create abstractions and wire up handlers in the same way I did with commands. There is one difference between commands and queries: queries will have their handlers in infrastructure layer.

```csharp
public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return await (Task<TResult>)handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))?
            .Invoke(handler, new []{query});
    }
```

Using reflection I can get type of query and result, then I can get the handler for the query as object.

**`QueryAsync<TResult>`** method dynamically creates a handler type based on the query type and result type, retrieves an instance of that handler from the service provider, and invokes the **`HandleAsync`** method of the handler, passing the query as a parameter. The method then awaits the task returned by **`HandleAsync`** and returns the result.

Once I have registered Query Handlers I’ll implement GetUser handler and an extension to User entity which will allow me to construct UserDto from user entity.

To make the process of accessing authorized routes easier through swagger, I'll add the following settings.

```csharp
    services.AddEndpointsApiExplorer()
            .AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Input your JWT Authorization header to access this API. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

            })
```

Now after signing up and signing in I'm able to get information about the current used account.

# Exception Middleware

Now when the base of the app is working, I have to create exception middleware because so far whenever I throw custom exception or some other errors occur I do not get the desired response so middleware is the way to tackle this problem.

Important: having registered 3 middlewares 1,2,3 (in this order), they will start in the order 1, 2, 3 but finish in the reverse order 3, 2, 1. That is because 1 awaits next(ctx), 2 awaits next(ctx) and 3 awaits next(ctx) so if 3 awaited the result, controll is given to 2, then to 1 and the process finishes.

I’ll create the middleware in Infrastructure/Exceptions/ExceptionMiddleware.cs.

I create HandleExceptionAsync mehod in which I use destructurization to get proper code and error message.

I moved abstractions of Errors and Exceptions into Shared for better separation of code. I also adjusted swagger documentation to match error responses.

# Integration tests

In this part I’ll write e2e tests for the routes currently present in the app. For that I’ll create a new project in tests directory called TaskoPhobia.Tests.Integration. I’ll use xUnit as a testing framework. Moreover I’ll install Shouldly package to make assertions easier as well as Microsoft Mvc Testing package.

## Writing tests

1. Firstly I’ll create a class which will create an instance of the WebApp. It will extend the built in WebApplicationFactory
2. To get the builder of a project, in TaskoPhobia.Api I have to make its internals visibe to unit tests project. For that I’ll write

    ```csharp
    <ItemGroup>
        <InternalsVisibleTo Include="TaskoPhobia.Tests.Integration" />
    </ItemGroup>
    ```

   in appsettings. Also testing class has to be made internal to be able to access Program.

3. For every controller I’ll create a testing class, marking every testing method with the [Fact] annotation
4. Also I have to configure app settings to match testing environment ex. testing db
5. I’ll instaciate the app in the abstract controller testing class
6. I’ll use the approach of deleting db for each group of tests
7. For that I’ll make use of options provider used earlier and create a class TestDatabase in which I’ll implement the interface IDisposable provided by XUnit which gives me a Dispose method in which I can ensure deleting DB after every test.
8. To run tests sequentially to avoid dropping db and accessing it at the same time by different tests I’ll add [Collection] annotation to the ControllerTests class.

We can overwrite service collection for the purpose of testing using Actionc<IServiceCollection> in the constructor of the testing class. This allows for mocking some external services like payment gateway or email sender. I’ll use it for users repository just for tests. To my abstract class in tests I’ll add a virtuak method

```csharp
protected virtual void ConfigureServices(IServiceCollection services){}
```

which I will be able to use later to configure services in my testing classes.