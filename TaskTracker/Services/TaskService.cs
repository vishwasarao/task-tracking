// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Dtos;
using TaskTracker.Api.Data;
using TaskTracker.Api.Domain;

namespace TaskTracker.Services
{

    public interface ITaskService
    {
        Task<IEnumerable<TaskReadDto>> GetAllAsync();
        Task<TaskReadDto> GetAsync(int id);
        Task<TaskReadDto> UpdateAsync(int id);
        Task<TaskReadDto> CreateAsync(int id);
    }

    public class TaskService(TaskDbContext _dbc, ILogger<TaskService> _logger) : ITaskService
    {
        public async Task<IEnumerable<TaskReadDto>> GetAllAsync()
        {
            _logger.LogInformation("------ GetAll ----");
            return await _dbc.TaskItems.Select(t => new TaskReadDto(t.Id,t.Title, t.Description, t.IsCompleted, t.CreatedDate)).ToListAsync();
        }

        public async Task<TaskReadDto> GetAsync(int id)
        {
            _logger.LogInformation("------ Get ----");
            return await _dbc.TaskItems.Where(t => t.Id == id)?.Select(t => new TaskReadDto(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedDate)).FirstOrDefaultAsync();
        }

        public async Task<TaskReadDto> UpdateAsync(int id)
        {
            _logger.LogInformation("------ Update ----");
            return await _dbc.TaskItems.Where(t => t.Id == id)?.Select(t => new TaskReadDto(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedDate)).FirstOrDefaultAsync();
        }

        public async Task<TaskReadDto> CreateAsync(int id)
        {
            _logger.LogInformation("------ Create ----");
            return await _dbc.TaskItems.Where(t => t.Id == id)?.Select(t => new TaskReadDto(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedDate)).FirstOrDefaultAsync();
        }


    }
}
