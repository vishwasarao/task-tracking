// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Data;
using TaskTracker.Api.Domain;
using TaskTracker.Api.Dtos;

namespace TaskTracker.Api.Services
{
    public interface IProjectService
    {
        Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto project);
        Task<ProjectReadDto> UpdateProjectAsync(int projectId,  ProjectUpdateDto project);
        Task<ProjectReadDto> GetProjectAsync(int projectId);
        Task<List<ProjectReadDto>> GetAllProjectsAsync();

    }


    public class ProjectService(TaskDbContext _dbc, ILogger<ProjectService> _logger) : IProjectService
    {
        public async Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto project)
        {
            var entity = new Project { Name = project.Name, Description = project.Description };
            _dbc.Projects.Add(entity);
            await _dbc.SaveChangesAsync();
            return new ProjectReadDto (entity.Id, entity.Name, entity.Description);
        }

 
        public async Task<List<ProjectReadDto>> GetAllProjectsAsync()
        {
            _logger.LogInformation("------ GetAll Projects ----");
            return await _dbc.Projects.Select(p => new ProjectReadDto(p.Id, p.Name, p.Description)).ToListAsync();
        }
        public async Task<ProjectReadDto> GetProjectAsync(int projectId)
        {
            _logger.LogInformation("------ GetAll Projects ----");
            var project = await _dbc.Projects.FindAsync(projectId);
            return new ProjectReadDto (project.Id,project.Name,project.Description);

        }
        public async Task<ProjectReadDto> UpdateProjectAsync(int projectId, ProjectUpdateDto updatedProject)
        {
            var existingProject = await _dbc.Projects.FindAsync(projectId);
            if (existingProject == null)
            {
                _logger.LogError($"Project ID {projectId} not found.");
                throw new KeyNotFoundException($"Could not find Project with Id {projectId}");
            }
            if (updatedProject.Name != null)
            {
                existingProject.Name = updatedProject.Name;
            }

            if (updatedProject.Description != null)
            {
                existingProject.Description = updatedProject.Description;
            }

            _dbc.SaveChanges();
            return new ProjectReadDto(existingProject.Id,existingProject.Name,existingProject.Description);
        }
    }
}
