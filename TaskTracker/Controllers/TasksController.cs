using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using TaskTracker.Api;
using TaskTracker.Api.Data;
using TaskTracker.Api.Dtos;
using TaskTracker.Api.Services;
using TaskTracker.Services;

namespace TaskTracker.Api.Controllers
{
    [Authorize(Policy = "Tasks.Read.AppRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService _taskService, ILogger<TasksController> _logger) : ControllerBase
    {


        [AllowAnonymous]
        [HttpGet("debug/whoami")]
        public IActionResult WhoAmI()
        {
            var claims = User?.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(new
            {
                IsAuthenticated = User?.Identity?.IsAuthenticated ?? false,
                AuthenticationType = User?.Identity?.AuthenticationType,
                Claims = claims
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("------ Getting All Tasks -----");
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var tasks = await _taskService.GetAsync(id);
            return Ok(tasks);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto taskToCreate)
        {
            var task = await _taskService.CreateAsync(taskToCreate);
            return Ok(task);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpdateDto updatedTask)
        {
            var task = await _taskService.UpdateAsync(id, updatedTask);
            return Ok(task);
        }

    }
}
