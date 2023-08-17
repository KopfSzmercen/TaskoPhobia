using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Infrastructure.DAL.DatabaseInitializer;
using TaskoPhobia.Infrastructure.DAL.Decorators;
using TaskoPhobia.Infrastructure.DAL.Repositories;
using TaskoPhobia.Infrastructure.DAL.Services;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Queries;
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

        services.AddDbContext<TaskoPhobiaReadDbContext>(x => x.UseNpgsql(options.ConnectionString));
        services.AddDbContext<TaskoPhobiaWriteDbContext>(x => x.UseNpgsql(options.ConnectionString));

        services.AddRepositories();
        services.AddScoped<IUserReadService, PostgresUserReadService>();
        services.AddScoped<IProjectReadService, PostgresProjectReadService>();
        services.AddScoped<IInvitationReadService, PostgresInvitationReadService>();
        services.AddScoped<IProjectParticipationReadService, PostgresProjectParticipationReadService>();

        services.AddDatabaseInitializer(configuration);
        services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        services.AddQueries();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    public static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, IPagedQuery query,
        CancellationToken cancellationToken = default)
    {
        return await data.PaginateAsync(query.Page, query.Results, cancellationToken);
    }

    private static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, int page, int results,
        CancellationToken cancellationToken = default)
    {
        if (page <= 0) page = 1;

        results = results switch
        {
            <= 0 => 10,
            > 100 => 100,
            _ => results
        };

        var totalResults = await data.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalResults / (double)results);

        var result = await data.Skip((page - 1) * results).Take(results).ToListAsync(cancellationToken);

        return new Paged<T>(result, page, results, totalPages, totalResults);
    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}