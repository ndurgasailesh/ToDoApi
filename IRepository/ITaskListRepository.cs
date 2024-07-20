using TaskScheduler.Data.DBModels;
using TaskScheduler.Data.Models;
using TaskScheduler.Dtos;

namespace TaskScheduler.Repository
{
    public interface ITaskListRepository : IDataRepository<TaskList>
    {

        Task<IEnumerable<ApplicationUser>> GetAllUserTasksAsync();

        Task<IEnumerable<TaskListDto>> GetUserTasksAsync(string userId);

        Task<TaskListDto?> GetTaskDetailsAsync(int taskId);
    }
}
