namespace TaskoPhobia.Infrastructure.DAL;

internal sealed class PostgresUnitOfWork : IUnitOfWork
{
    private readonly TaskoPhobiaDbContext _dbContext;

    public PostgresUnitOfWork(TaskoPhobiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await action();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}