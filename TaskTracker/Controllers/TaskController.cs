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
    public class TaskController(TaskDbContext _dbContext, ITaskService _taskService, ILogger<TaskController> _logger) : ControllerBase
    {
       
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }
    }
}
