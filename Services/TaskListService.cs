using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis;
using TaskScheduler.Data;
using TaskScheduler.Data.DBModels;
using TaskScheduler.Data.Models;
using TaskScheduler.DataRepoManager;
using TaskScheduler.Dtos;
using TaskScheduler.Repository;
using TaskScheduler.Services.IService;

namespace TaskScheduler.Services
{
    public class TaskListService : ITaskListService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public TaskListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateTaskListAsync(TaskListDto objTaskItem)
        {

       
                if (objTaskItem != null)
                {
                    var entity = _mapper.Map<TaskList>(objTaskItem);
                    await _unitOfWork.TaskLists.Add(entity);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
           
            return false;
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
                if (taskId > 0)
                {
                    var taskDetails = await _unitOfWork.TaskLists.GetById(taskId);
                    if (taskDetails != null)
                    {
                        _unitOfWork.TaskLists.Delete(taskDetails);
                        var result = _unitOfWork.Save();

                        if (result > 0)
                            return true;
                        else
                            return false;
                    }
                }
            return false;
        }


        public  async Task<IEnumerable<TaskListDto>> GetUserTasksAsync(string userId)
        {

                if (userId != null)
                {
                    var taskLists = await _unitOfWork.TaskLists.GetUserTasksAsync(userId);
                    return taskLists;
                } 
              return Enumerable.Empty<TaskListDto>();
              
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUserTasksAsync()
        {

                var taskLists = await _unitOfWork.TaskLists.GetAllUserTasksAsync();
                return taskLists;
        }

        public async Task<TaskListDto?> GetTaskDetailsAsync(int taskId)
        {

                if (taskId > 0)
                {
                    var taskDetails = await _unitOfWork.TaskLists.GetById(taskId);
                    if (taskDetails != null)
                    {
                        return _mapper.Map<TaskListDto>(taskDetails); ;
                    }
            }
            return null;
        }

        public async Task<bool> UpdateTaskDetailsAsync(TaskListDto objTaskItem)
        {

                if (objTaskItem != null)
                {
                    var taskDetails = await _unitOfWork.TaskLists.GetById(objTaskItem.Id);
                    if (taskDetails != null)
                    {
                        taskDetails.Title = objTaskItem.Title;
                        taskDetails.Description = objTaskItem.Description;
                        taskDetails.DueDate = objTaskItem.DueDate;
                        taskDetails.IsCompleted = objTaskItem.IsCompleted;

                      _unitOfWork.TaskLists.Update(taskDetails);

                        var result = _unitOfWork.Save();

                        if (result > 0)
                            return true;
                        else
                            return false;
                }
            }
            return false;
        }

    }
}
