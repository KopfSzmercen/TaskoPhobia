using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.DomainServices.AccountUpgradeProducts;
using TaskoPhobia.Core.DomainServices.Invitations;
using TaskoPhobia.Core.DomainServices.Orders;
using TaskoPhobia.Core.DomainServices.Projects;

namespace TaskoPhobia.Core.DomainServices;

public static class Extensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IInvitationService, InvitationService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IAccountUpgradeProductService, AccountUpgradeProductService>();
        services.AddScoped<IOrdersService, OrdersService>();
        return services;
    }
}