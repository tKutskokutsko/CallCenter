namespace CallCenterApi.Infrastructure.DB.Repositories;

public abstract class RepositoryBase
{
    internal RepositoryBase(DataContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    protected DataContext DbContext { get; }
}