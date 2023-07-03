using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Shared.Commands;

public class InMemoryCommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryCommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        using var scope = _serviceProvider.CreateScope();
        var validator = scope.ServiceProvider.GetService<IValidator<TCommand>>();

        if (validator is not null) await validator.ValidateAndThrowAsync(command);

        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        await handler.HandleAsync(command);
    }
}