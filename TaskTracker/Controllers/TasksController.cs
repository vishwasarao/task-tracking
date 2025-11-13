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
        public async Task<IActionResult> CreateTask(int id)
        {
            var tasks = await _taskService.CreateAsync(id);
            return Ok(tasks);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(int id)
        {
            var tasks = await _taskService.UpdateAsync(id);
            return Ok(tasks);
        }

    }
}
