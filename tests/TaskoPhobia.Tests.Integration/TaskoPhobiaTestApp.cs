using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace TaskoPhobia.Tests.Integration;

internal sealed class TaskoPhobiaTestApp : WebApplicationFactory<Program>
{
    public TaskoPhobiaTestApp(Action<IServiceCollection> services)
    {
        Client = base.WithWebHostBuilder(builder =>
        {
            if (services is not null)
            {
                builder.ConfigureServices(services);
            }
            builder.UseEnvironment("test");
        }).CreateClient();
    }

    public HttpClient Client { get; }
}