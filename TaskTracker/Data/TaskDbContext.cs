using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.IdentityGovernance.LifecycleWorkflows.DeletedItems.Workflows.Item.Tasks.Item;
using TaskTracker.Api.Domain;

namespace TaskTracker.Api.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
            
        }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
