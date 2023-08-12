using TaskoPhobia.Core.Entities.Users.Events;
using TaskoPhobia.Shared.Events;

namespace TaskoPhobia.Application.DomainNotificationHandlers;

public sealed class
    SendWelcomeEmailHandler : IDomainNotificationHandler<UserRegisteredDomainEvent>
{
    public Task HandleAsync(UserRegisteredDomainEvent domainEvent)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine($"Sending welcome email to user {domainEvent.UserId}");

        return Task.CompletedTask;
    }
}