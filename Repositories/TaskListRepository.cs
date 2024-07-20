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

        public TaskListRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<TaskListDto> GetUserTasks(string userId)
        {

             var taskList = this._dbContext.Users.Include(c => c.TaskLists).First(u => u.Id == userId).TaskLists.Select(c => _mapper.Map<TaskListDto>(c));
            return taskList ;
        
        }

        public IEnumerable<TaskListDto> GetAllUserTasks()
        {

            var taskList = this._dbContext.Users.Include(c => c.TaskLists).Select(c => _mapper.Map<TaskListDto>(c)).ToList();
            return taskList;

        }

        public TaskListDto? GetTaskDetails(int taskId)
        {
            if (taskId > 0)
            {
                var taskDetails = _dbContext.TaskLists.FirstOrDefault(x => x.Id == taskId);
                if (taskDetails != null)
                {
                    return _mapper.Map<TaskListDto>(taskDetails); ;
                }
            }
            return null;
        }

       
    }
}
