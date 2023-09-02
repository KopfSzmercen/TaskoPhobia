using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresPaymentRepository : IPaymentRepository
{
    private readonly DbSet<Payment> _payments;

    public PostgresPaymentRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _payments = dbContext.Payments;
    }

    public async Task AddAsync(Payment payment)
    {
        await _payments.AddAsync(payment);
    }

    public async Task<Payment> FindByIdAsync(PaymentId paymentId)
    {
        return await _payments
            .Where(x => x.Id == paymentId)
            .SingleOrDefaultAsync();
    }

    public async Task<Payment> FindByOrderIdAsync(OrderId orderId)
    {
        return await _payments
            .Where(x => x.OrderId == orderId)
            .FirstOrDefaultAsync();
    }

    public Task UpdateAsync(Payment payment)
    {
        _payments.Update(payment);
        return Task.CompletedTask;
    }
}