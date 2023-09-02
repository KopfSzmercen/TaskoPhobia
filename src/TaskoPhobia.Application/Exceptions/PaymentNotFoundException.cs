using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public class PaymentNotFoundException : CustomException
{
    public PaymentNotFoundException() : base("Payment not found.")
    {
    }
}