using TaskoPhobia.Application;
using TaskoPhobia.Core;
using TaskoPhobia.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplication()
    .AddCore()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();



var app = builder.Build();

app.UseInfrastructure();

app.Run();
