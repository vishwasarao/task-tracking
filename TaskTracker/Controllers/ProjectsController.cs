// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Dtos;
using TaskTracker.Api.Services;

namespace TaskTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController (IProjectService _projectService, ILogger<ProjectsController> _logger): ControllerBase
    {
      
        [HttpPost("create")]
        public async Task<IActionResult> CreateTask(ProjectCreateDto projectToCreate)
        {
            var project = await _projectService.CreateProjectAsync(projectToCreate);
            return Ok(project);
        }
    }
}
