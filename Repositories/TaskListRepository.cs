using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using TaskScheduler.Data;
using TaskScheduler.Data.DBModels;
using TaskScheduler.Data.Models;
using TaskScheduler.DataRepoManager;
using TaskScheduler.Dtos;
using TaskScheduler.Repository;

namespace TaskScheduler.Repositories
{
    public class TaskListRepository : DataRepository<TaskList>, ITaskListRepository
    {
        IMapper _mapper;
        public TaskListRepository(AppDbContext context, IMapper mapper) : base(context)
        {
           _mapper = mapper;
        }

        //public TaskListRepository(AppDbContext context) : base(context)
        //{
        //}

        public async Task<IEnumerable<TaskListDto?>> GetUserTasksAsync(string userId)
        {

            var user = await this._dbContext.Users
                               .Include(u => u.TaskLists)
                               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.TaskLists == null || !user.TaskLists.Any())
            {
                return Enumerable.Empty<TaskListDto>();
            }

            var taskListDtos =  user.TaskLists.Select(c => _mapper.Map<TaskListDto>(c));
            return taskListDtos;
        
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUserTasksAsync()
        {

            var taskLists = await this._dbContext.Users.AsQueryable().Include(c => c.TaskLists).ToListAsync();
         
            return taskLists;

        }

        public  async Task<TaskListDto?> GetTaskDetailsAsync(int taskId)
        {
            if (taskId > 0)
            {
                var taskDetails = await _dbContext.TaskLists.FirstOrDefaultAsync(x => x.Id == taskId);
                if (taskDetails != null)
                {
                    return _mapper.Map<TaskListDto>(taskDetails); ;
                }
            }
            return new TaskListDto();
        }

       
    }
}
