using AutoMapper;
using Moq;
using TaskScheduler.Data.DBModels;
using TaskScheduler.Data.Models;
using TaskScheduler.Dtos;
using TaskScheduler.Repository;
using TaskScheduler.Services;

namespace TaskScheduler.ServicesTest
{
    public class TaskListServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TaskListService _service;

        public TaskListServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _service = new TaskListService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateTaskListAsync_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            var taskDto = new TaskListDto { Id = 1, Title = "New Task" };
            var taskEntity = new TaskList { Id = 1, Title = "New Task" };

            _mockMapper.Setup(m => m.Map<TaskList>(taskDto)).Returns(taskEntity);
            _mockUnitOfWork.Setup(uow => uow.TaskLists.Add(taskEntity)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(uow => uow.Save()).Returns(1);

            // Act
            var result = await _service.CreateTaskListAsync(taskDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTaskAsync_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            var taskId = 1;
            var taskEntity = new TaskList { Id = 1 };

            _mockUnitOfWork.Setup(uow => uow.TaskLists.GetById(taskId)).ReturnsAsync(taskEntity);
            _mockUnitOfWork.Setup(uow => uow.TaskLists.Delete(taskEntity));
            _mockUnitOfWork.Setup(uow => uow.Save()).Returns(1);

            // Act
            var result = await _service.DeleteTaskAsync(taskId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetUserTasksAsync_ReturnsTasks_WhenUserExists()
        {
            // Arrange
            var userId = "test-user";
            var taskDtos = new List<TaskListDto>
                {
                    new TaskListDto { Id = 1, Title = "Test Task" }
                };

            _mockUnitOfWork.Setup(uow => uow.TaskLists.GetUserTasksAsync(userId)).ReturnsAsync(taskDtos);

            // Act
            var result = await _service.GetUserTasksAsync(userId);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllUserTasksAsync_ReturnsTasks()
        {
           
            var applicationUser = new ApplicationUser
            {
                TaskLists = new List<TaskList> { new TaskList { Id = 1, Title = "Test Task" } }
            };

            var applicaitonUsers = new List<ApplicationUser>();

            applicaitonUsers.Add(applicationUser);

            _mockUnitOfWork.Setup(uow => uow.TaskLists.GetAllUserTasksAsync()).ReturnsAsync(applicaitonUsers);

            // Act
            var result = await _service.GetAllUserTasksAsync();

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetTaskDetailsAsync_ReturnsTask_WhenFound()
        {
            // Arrange
            var taskId = 1;
            var taskEntity = new TaskList { Id = 1, Title = "Test Task" };
            var taskDto = new TaskListDto { Id = 1, Title = "Test Task" };

            _mockUnitOfWork.Setup(uow => uow.TaskLists.GetById(taskId)).ReturnsAsync(taskEntity);
            _mockMapper.Setup(m => m.Map<TaskListDto>(taskEntity)).Returns(taskDto);

            // Act
            var result = await _service.GetTaskDetailsAsync(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskDto.Title, result?.Title);
        }

        [Fact]
        public async Task UpdateTaskDetailsAsync_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            var taskDto = new TaskListDto { Id = 1, Title = "Updated Task" };
            var taskEntity = new TaskList { Id = 1 };

            _mockUnitOfWork.Setup(uow => uow.TaskLists.GetById(taskDto.Id)).ReturnsAsync(taskEntity);
            _mockMapper.Setup(m => m.Map(taskDto, taskEntity)).Verifiable();
            _mockUnitOfWork.Setup(uow => uow.TaskLists.Update(taskEntity));
            _mockUnitOfWork.Setup(uow => uow.Save()).Returns(1);

            // Act
            var result = await _service.UpdateTaskDetailsAsync(taskDto);

            // Assert
            Assert.True(result);
        }
    }
}
