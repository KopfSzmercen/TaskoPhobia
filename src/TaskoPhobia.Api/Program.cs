using TaskoPhobia.Application;
using TaskoPhobia.Core;
using TaskoPhobia.Infrastructure;
using TaskoPhobia.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddShared()
    .AddApplication()
    .AddCore()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

var app = builder.Build();

app.UseInfrastructure();

app.Run();