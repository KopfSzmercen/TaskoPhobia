# What is this repo?

It's not a tutorial. It's a repo for a project in which I'll be learning creating WebApis using.NET platform and I want to create a documentation and steps involved in the whole process therefore techniques and practices in this project are not meant to be followed by anyone. It's heavily based on [This course](https://platform.devmentors.io/courses/solidne-web-api) .

# Project initialization

## Creating the project

The project will be initialized using .NET CLI

1. Create a new solution → _dotnet new sln_
2. Add standard catalogues → src and tests
3. Add a project of type Web API in src. I’m gonna use the name of my project with a suffix API: _dotnet new webapi -n TaskoPhobia.Api_
4. Add API project to the main solution file _dotnet sln add src/TaskoPhobia.Api/TaskoPhobia.Api.csproj_
5. Verify if everything is OK: ~TaskoPhobia> ****\*\*\*\*****dotnet build****\*\*\*\*****

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
2. To use Swagger middleware create ****************\*****************WebApplication UseInfrastructure****************\***************** static method in Infrastructure.Extensions and call it in Program.cs
