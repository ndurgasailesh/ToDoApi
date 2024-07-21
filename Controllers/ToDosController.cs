using Microsoft.AspNetCore.Mvc;
using TaskScheduler.Dtos;
using Microsoft.AspNetCore.Authorization;
using TaskScheduler.Services.IService;
using TaskScheduler.Data.DBModels;
using System.Collections;

namespace TaskScheduler.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        public readonly ITaskListService _taskListService;

        public ToDosController(ITaskListService taskListService)
        {
            this._taskListService = taskListService;
        }

        [Authorize(Roles = "Admin,User")]
        [Route("id")]
        [HttpGet]
        public async Task<IActionResult> GetUserTasksAsync()
        {

            string userId = string.Empty;
            if (User != null && User.FindFirst("userId") != null)
            {
                userId = User.FindFirst("userId")!.Value;
            }
            IEnumerable<TaskListDto> taskLists = await _taskListService.GetUserTasksAsync(userId!);

            if (taskLists == null)
            {
                return NotFound("TaskList not found");
            }
            return Ok(taskLists);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserTasksAsync()
        {
          
            var taskLists = await _taskListService.GetAllUserTasksAsync();

            if (taskLists == null || !taskLists.Any())
            {
                return NotFound("TaskList not found");
            }
            return Ok(taskLists);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskListDto objTasklist)
        {
            if (objTasklist == null)
            {
                return BadRequest("TaskList is empty");
            }
            if (User != null  && User.FindFirst("userId") != null)
            {
                objTasklist.UserId = User.FindFirst("userId")!.Value;
            }

            await _taskListService.CreateTaskListAsync(objTasklist);
            return Ok(objTasklist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TaskListDto taskList)
        {
            if (taskList == null)
            {
                return BadRequest("TaskList is empty");
            }

            TaskListDto? dbTaskList = await _taskListService.GetTaskDetailsAsync(id);

            if (dbTaskList == null)
            {
                return NotFound("TaskList not found");
            }
          
            taskList.Id = id;

            await _taskListService.UpdateTaskDetailsAsync(taskList);

            return Ok(dbTaskList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            TaskListDto? dbTaskList = await _taskListService.GetTaskDetailsAsync(id);
            if (dbTaskList == null)
            {
                return NotFound("TaskList not found");
            }
            await _taskListService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
