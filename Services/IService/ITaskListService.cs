using TaskScheduler.Data.DBModels;
using TaskScheduler.Dtos;

namespace TaskScheduler.Services.IService
{
    public interface ITaskListService
    {
        Task<bool> CreateTaskListAsync(TaskListDto objTaskItem);

        Task<IEnumerable<TaskListDto>> GetUserTasksAsync(string userId);

        Task<IEnumerable<ApplicationUser>> GetAllUserTasksAsync();


        Task<TaskListDto?> GetTaskDetailsAsync(int taskId);

        Task<bool> UpdateTaskDetailsAsync(TaskListDto objTaskItem);

        Task<bool> DeleteTaskAsync(int taskId);
    }
}
