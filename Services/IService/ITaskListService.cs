using TaskScheduler.Data.DBModels;
using TaskScheduler.Dtos;

namespace TaskScheduler.Services.IService
{
    public interface ITaskListService
    {
        Task<bool> CreateTaskList(TaskListDto objTaskItem);

        IEnumerable<TaskListDto> GetUserTasks(string userId);

        IEnumerable<TaskListDto> GetAllUserTasks();


        Task<TaskListDto?> GetTaskDetails(int taskId);

        Task<bool> UpdateTaskDetails(TaskListDto objTaskItem);

        Task<bool> DeleteTask(int taskId);
    }
}
