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

        public async Task<bool> CreateTaskList(TaskListDto objTaskItem)
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

        public async Task<bool> DeleteTask(int taskId)
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


        public  IEnumerable<TaskListDto> GetUserTasks(string userId)
        {

                if (userId != null)
                {
                    var taskLists = _unitOfWork.TaskLists.GetUserTasks(userId);
                    return taskLists;
                } 
              return Enumerable.Empty<TaskListDto>();
              
        }



        public  IEnumerable<TaskListDto> GetAllUserTasks()
        {

                var taskLists = _unitOfWork.TaskLists.GetAllUserTasks();
                return taskLists;
        }

        public async Task<TaskListDto?> GetTaskDetails(int taskId)
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

        public async Task<bool> UpdateTaskDetails(TaskListDto objTaskItem)
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
