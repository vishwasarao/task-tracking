using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Api.Controllers;
using TaskTracker.Api.Dtos;
using TaskTracker.Services;

namespace TaskTracker.Api.Test.Controllers
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskService> _taskServiceMock;
        private readonly Mock<ILogger<TasksController>> _loggerMock;
        private readonly TasksController _taskController;

        public TasksControllerTests()
        {
            _taskServiceMock = new Mock<ITaskService>();
            _loggerMock = new Mock<ILogger<TasksController>>();
            _taskController = new TasksController(_taskServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetReturnsOkWithTasks()
        {
            var taskDtos = new TaskReadDto(11, "TestTask", "This is for testing", false, DateTime.UtcNow);
            _taskServiceMock.Setup(t => t.GetAsync(11)).ReturnsAsync(taskDtos);
            var result = await _taskController.GetAsync(11);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<TaskReadDto>(okResult.Value);
            Assert.Equal(11, returnedDto.Id);
            Assert.Equal("TestTask", returnedDto.Title);

        }
    }
}
