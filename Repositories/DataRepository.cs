using Microsoft.EntityFrameworkCore;
using TaskScheduler.Data;
using TaskScheduler.Repository;

namespace TaskScheduler.DataRepoManager
{
    public abstract class DataRepository<T> : IDataRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;

        protected DataRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<T> GetById(int? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
