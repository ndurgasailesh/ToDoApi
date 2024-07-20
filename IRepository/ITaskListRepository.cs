using TaskScheduler.Data.DBModels;
using TaskScheduler.Data.Models;
using TaskScheduler.Dtos;

namespace TaskScheduler.Repository
{
    public interface ITaskListRepository : IDataRepository<TaskList>
    {

        IEnumerable<TaskListDto> GetAllUserTasks();

        IEnumerable<TaskListDto> GetUserTasks(string userId);

        TaskListDto? GetTaskDetails(int taskId);
    }
}
