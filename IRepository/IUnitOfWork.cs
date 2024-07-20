using TaskScheduler.Data.Models;

namespace TaskScheduler.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        
        ITaskListRepository TaskLists { get; }
    }
}
