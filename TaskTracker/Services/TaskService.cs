// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Data;
using TaskTracker.Api.Domain;
using TaskTracker.Api.Dtos;

namespace TaskTracker.Services
{

    public interface ITaskService
    {
        Task<IEnumerable<TaskReadDto>> GetAllAsync();
        Task<TaskReadDto> GetAsync(int id);
        Task<TaskReadDto> UpdateAsync(int id, TaskUpdateDto updatedTask);
        Task<TaskReadDto> CreateAsync(TaskCreateDto task);
    }

    public class TaskService(TaskDbContext _dbc, ILogger<TaskService> _logger) : ITaskService
    {
        public async Task<IEnumerable<TaskReadDto>> GetAllAsync()
        {
            _logger.LogInformation("------ GetAll ----");
            return await _dbc.TaskItems.Select(t => new TaskReadDto(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedDate)).ToListAsync();
        }

        public async Task<TaskReadDto> GetAsync(int id)
        {
            _logger.LogInformation("------ Get ----");
            var task = await _dbc.TaskItems
                .Where(t => t.Id == id)
                .Select(t => new TaskReadDto(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedDate))
                .FirstOrDefaultAsync();

            if (task is null)
                throw new KeyNotFoundException($"Task with id {id} not found.");
            return task;
        }

        public async Task<TaskReadDto> UpdateAsync(int id, TaskUpdateDto updatedTask)
        {
            _logger.LogInformation("------ Update ----");
            var existingTask = await _dbc.TaskItems.FindAsync(id);

            if (existingTask == null)
            {
                _logger.LogError($"Task ID {id} not found.");
                throw new KeyNotFoundException($"Could not find task with Id {id}");
            }
            if (updatedTask.Title != null)
                existingTask.Title = updatedTask.Title;


            if (updatedTask.Description != null)
                existingTask.Description = updatedTask.Description;

            existingTask.IsCompleted = updatedTask.IsCompleted;
            //_dbc.Update(existingTask);
            _dbc.SaveChanges();

            return new TaskReadDto(existingTask.Id, existingTask.Title, existingTask.Description, existingTask.IsCompleted, existingTask.CreatedDate);
        }

        public async Task<TaskReadDto> CreateAsync(TaskCreateDto newTask)
        {
            _logger.LogInformation("------ Create ----");

            var entity = new TaskItem
            {
                Title = newTask.Title,
                Description = newTask.Description,
                IsCompleted = false,
                ProjectId = newTask.ProjectId,
                CreatedDate = DateTime.UtcNow // Set server-side creation timestamp
            };


            _dbc.TaskItems.Add(entity);
            await _dbc.SaveChangesAsync();
            return new TaskReadDto(
                        entity.Id,
                        entity.Title,
                        entity.Description,
                        entity.IsCompleted,
                        entity.CreatedDate
                        );
        }


    }
}
