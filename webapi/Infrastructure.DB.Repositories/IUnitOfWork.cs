namespace CallCenterApi.Infrastructure.DB.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task Commit(Func<Task> func);

    Task Commit(Action action);
}