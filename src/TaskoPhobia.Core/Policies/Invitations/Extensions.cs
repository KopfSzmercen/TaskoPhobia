using Microsoft.Extensions.DependencyInjection;

namespace TaskoPhobia.Core.Policies.Invitations;

internal static class Extensions
{
    public static IServiceCollection AddCreateInvitationPolicies(this IServiceCollection services)
    {
        /* var assembly = Assembly.GetExecutingAssembly();
 
         var serviceTypes = assembly.GetTypes()
             .Where(type => typeof(ICreateInvitationPolicy).IsAssignableFrom(type) && !type.IsInterface);
 
         foreach (var serviceType in serviceTypes)
         {
             services.AddSingleton<ICreateInvitationPolicy, type>();
         }
 
         //services.AddSingleton<ICreateInvitationPolicy, InvitationAlreadySentPolicy>();
         */
        services.Scan(scan => scan
            .FromAssemblies(typeof(ICreateInvitationPolicy).Assembly)
            .AddClasses(c => c.AssignableTo<ICreateInvitationPolicy>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );


        return services;
    }
}