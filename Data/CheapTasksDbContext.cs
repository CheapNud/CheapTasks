using CheapTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace CheapTasks.Data;

public class CheapTasksDbContext(DbContextOptions<CheapTasksDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskItem>(e =>
        {
            e.HasIndex(t => t.OwnerId);
            e.HasIndex(t => new { t.OwnerId, t.Done });
        });
    }
}
