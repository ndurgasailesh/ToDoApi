using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TaskScheduler.Controllers;
using TaskScheduler.Data.DBModels;
using TaskScheduler.Data.Models;
using TaskScheduler.Dtos;
using TaskScheduler.Services.IService;

namespace TaskScheduler.ControllerTest
{
    public class ToDosControllerTests
    {
        private readonly Mock<ITaskListService> _mockTaskListService;
        private readonly ToDosController _controller;

        public ToDosControllerTests()
        {
            _mockTaskListService = new Mock<ITaskListService>();
            _controller = new ToDosController(_mockTaskListService.Object);

            // Mock the user claim
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("userId", "test-user-id"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task GetUserTasksAsync_ReturnsOkResult_WithTasks()
        {
            // Arrange
            var tasks = new List<TaskListDto>
            {
                new TaskListDto { Id = 1, Title = "Test Task" }
            };

            _mockTaskListService.Setup(service => service.GetUserTasksAsync(It.IsAny<string>()))
                .ReturnsAsync(tasks);

            // Act
            var result = await _controller.GetUserTasksAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTasks = Assert.IsType<List<TaskListDto>>(okResult.Value);
            Assert.Single(returnTasks);
        }

        [Fact]
        public async Task GetAllUserTasksAsync_ReturnsOkResult_WithTasks()
        {
            // Arrange
            var applicationUser = new ApplicationUser
            {
                TaskLists = new List<TaskList> { new TaskList { Id = 1, Title = "Test Task" } }
            };

            var applicaitonUsers = new List<ApplicationUser>();

            applicaitonUsers.Add(applicationUser);

            _mockTaskListService.Setup(service => service.GetAllUserTasksAsync())
            .ReturnsAsync(applicaitonUsers);

           
            // Act
            var result = await _controller.GetAllUserTasksAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTasks = Assert.IsType<List<ApplicationUser>>(okResult.Value);
            Assert.Single(returnTasks);
        }

        [Fact]
        public async Task Post_ReturnsOkResult_WithTask()
        {
            // Arrange
            var task = new TaskListDto { Id = 1, Title = "Test Task" };

            _mockTaskListService.Setup(service => service.CreateTaskListAsync(task))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Post(task);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(task, okResult.Value);
        }

        [Fact]
        public async Task Put_ReturnsOkResult_WithUpdatedTask()
        {
            // Arrange
            var task = new TaskListDto { Id = 1, Title = "Updated Task" };
            _mockTaskListService.Setup(service => service.GetTaskDetailsAsync(task.Id.Value))
                .ReturnsAsync(task);

            _mockTaskListService.Setup(service => service.UpdateTaskDetailsAsync(task))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Put(task.Id.Value, task);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedTask = Assert.IsType<TaskListDto>(okResult.Value);
            Assert.Equal(task.Title, updatedTask.Title);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenTaskDeleted()
        {
            // Arrange
            var taskId = 1;
            _mockTaskListService.Setup(service => service.GetTaskDetailsAsync(taskId))
                .ReturnsAsync(new TaskListDto { Id = taskId });
            _mockTaskListService.Setup(service => service.DeleteTaskAsync(taskId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(taskId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
