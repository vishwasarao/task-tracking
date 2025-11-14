using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using TaskTracker.Api.Data;
using TaskTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Tasks.Read.AppRole", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Tasks.Read")); // Use the exact App Role value
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var taskDbConStr = builder.Configuration.GetConnectionString("TaskDbCon");
builder.Services.AddDbContext<TaskDbContext>
    (
      (option) =>
      {
         option.UseSqlServer(taskDbConStr);
      }
    
    );
builder.Services.AddTransient<ITaskService, TaskService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
