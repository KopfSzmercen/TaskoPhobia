using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Payments.CreatePaymentLink;

public record CreatePaymentLink(Guid OrderId) : ICommand;