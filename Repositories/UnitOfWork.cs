using TaskScheduler.Data;
using TaskScheduler.Data.Models;
using TaskScheduler.Repository;

namespace TaskScheduler.DataRepoManager
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;


        public ITaskListRepository TaskLists { get; }

        public UnitOfWork(AppDbContext dbContext,
                            ITaskListRepository taskListRepo)
        {
            _dbContext = dbContext;
            TaskLists = taskListRepo;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public AppDbContext DbContext { get { return _dbContext; } }

    }
}