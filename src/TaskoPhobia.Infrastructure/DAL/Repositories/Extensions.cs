using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Repositories;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddScoped<IProjectRepository, PostgresProjectRepository>();
        services.AddScoped<IInvitationRepository, PostgresInvitationRepository>();
        services.AddScoped<IProjectParticipationRepository, PostgresProjectParticipationRepository>();
        services.AddScoped<IProjectTaskRepository, PostgresProjectTaskRepository>();
        services.AddScoped<IAccountUpgradeProductRepository, PostgresAccountUpgradeProductRepository>();
        services.AddScoped<IOrderRepository, PostgresOrderRepository>();
        return services;
    }
}