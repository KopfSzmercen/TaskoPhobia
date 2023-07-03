﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Infrastructure.DAL.Decorators;
using TaskoPhobia.Infrastructure.DAL.Repositories;
using TaskoPhobia.Infrastructure.DAL.Services;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Queries;

namespace TaskoPhobia.Infrastructure.DAL;

internal static class Extensions
{
    private const string SectionName = "database";

    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<PostgresOptions>(section);

        var options = GetOptions<PostgresOptions>(configuration, SectionName);

        services.AddDbContext<TaskoPhobiaDbContext>(x => x.UseNpgsql(options.ConnectionString));
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddScoped<IUserReadService, PostgresUserReadService>();
        services.AddScoped<IProjectRepository, PostgresProjectRepository>();

        services.AddHostedService<DatabaseInitializer>();
        services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        services.AddQueries();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}