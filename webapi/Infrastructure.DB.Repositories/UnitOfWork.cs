using Microsoft.EntityFrameworkCore.Storage;

namespace CallCenterApi.Infrastructure.DB.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(
        DataContext dbContext,
        ILogger<UnitOfWork> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Commit(Func<Task> func)
    {
        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await func();

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred during in database transaction processing");
            throw;
        }
    }

    public async Task Commit(Action action)
    {
        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            action();

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred during in database transaction processing");
            throw;
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}