using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Dtos;
using TaskTracker.Api;
using TaskTracker.Api.Data;
using TaskTracker.Services;

namespace TaskTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService _taskService, ILogger<TasksController> _logger) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("------ Getting All Tasks -----");
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tasks = await _taskService.GetAsync(id);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto taskToCreate)
        {
            var task = await _taskService.CreateAsync(taskToCreate);
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpdateDto updatedTask)
        {
            var task = await _taskService.UpdateAsync(id, updatedTask);
            return Ok(task);
        }

    }
}
