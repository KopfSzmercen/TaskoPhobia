using TaskoPhobia.Core.Entities.Users.Events;
using TaskoPhobia.Shared.Abstractions.Emails;
using TaskoPhobia.Shared.Events;

namespace TaskoPhobia.Application.DomainNotificationHandlers;

internal sealed class
    SendWelcomeEmailHandler : IDomainNotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IEmailSender _emailSender;


    public SendWelcomeEmailHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task HandleAsync(UserRegisteredDomainEvent domainEvent)
    {
        var emailMessage = new EmailMessage(domainEvent.Email, "Welcome in Taskophobia!", "<h1>Welcome</h1>");
        await _emailSender.SendEmailAsync(emailMessage);
    }
}