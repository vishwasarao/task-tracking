using System.Security.Claims;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using TaskTracker.Api.Data;
using TaskTracker.Api.Services;
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
builder.Services.Configure<ServiceBusSettings>(
    builder.Configuration.GetSection("ServiceBus")
);

builder.Services.AddSingleton<ServiceBusClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<ServiceBusSettings>>().Value;
    return new ServiceBusClient(options.ConnectionString);
});
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IMessageBusService, MessageBusService>();



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
