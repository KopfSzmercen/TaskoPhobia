using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Url;

namespace TaskoPhobia.Shared.Abstractions.Payments;

public record PaymentLinkDto(Url PaymentLink);