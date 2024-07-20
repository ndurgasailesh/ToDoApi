using Microsoft.AspNetCore.Mvc;
using TaskScheduler.Dtos;
using Microsoft.AspNetCore.Authorization;
using TaskScheduler.Services.IService;

namespace TaskScheduler.Controllers
{

    [Authorize(Roles = "Admin")] 
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {

        public readonly ITaskListService _taskListService;



        public TaskListController( ITaskListService taskListService
             )
        {
            this._taskListService = taskListService;
        }
       

        [Route("user")]
        [HttpGet]
        public  IActionResult GetUserTasks()
        {
            string userId = string.Empty;
            if (User != null && User.FindFirst("userId") != null)
            {
                userId = User.FindFirst("userId")!.Value;
            }
            List<TaskListDto> taskLists = _taskListService.GetUserTasks(userId!).ToList();

            if (taskLists == null)
            {
                return NotFound("TaskList not found");
            }
            return Ok(taskLists);
        }

        [Route("user/all")]
        [HttpGet]
        public IActionResult GetAllUserTasks()
        {
          
            List<TaskListDto> taskLists = _taskListService.GetAllUserTasks().ToList();

            if (taskLists == null)
            {
                return NotFound("TaskList not found");
            }
            return Ok(taskLists);
        }



        [HttpPost]
        public IActionResult Post([FromBody] TaskListDto objTasklist)
        {
            if (objTasklist == null)
            {
                return BadRequest("TaskList is empty");
            }
            if (User != null  && User.FindFirst("userId") != null)
            {
                objTasklist.UserId = User.FindFirst("userId")!.Value;
            }

            _taskListService.CreateTaskList(objTasklist);
            return Ok(objTasklist);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TaskListDto taskList)
        {
 
            TaskListDto? dbTaskList = _taskListService.GetTaskDetails(id).Result;
            if (dbTaskList == null)
            {
                return NotFound("TaskList not found");
            }
            if (taskList == null)
            {
                return BadRequest("TaskList is empty");
            }
            taskList.Id = id;

            _taskListService.UpdateTaskDetails(taskList);
            //_taskListDataRepo.Update(dbTaskList, taskList);
            return Ok(dbTaskList);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TaskListDto? dbTaskList = _taskListService.GetTaskDetails(id).Result;
            if (dbTaskList == null)
            {
                return NotFound("TaskList not found");
            }
            _taskListService.DeleteTask(id);
            //_taskListDataRepo.Delete(dbTaskList);
            return NoContent();
        }
    }
}
