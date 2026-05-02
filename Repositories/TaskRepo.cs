using CheapTasks.Data;
using CheapTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace CheapTasks.Repositories;

public class TaskRepo(IDbContextFactory<CheapTasksDbContext> dbFactory)
{
    public async Task<List<TaskItem>> GetForOwnerAsync(string ownerId, CancellationToken ct = default)
    {
        await using var db = await dbFactory.CreateDbContextAsync(ct);
        return await db.Tasks
            .Where(t => t.OwnerId == ownerId)
            .OrderBy(t => t.Done)
            .ThenByDescending(t => t.IsPinned)
            .ThenByDescending(t => t.CreatedUtc)
            .ToListAsync(ct);
    }

    public async Task<List<string>> GetDistinctLocationsAsync(string ownerId, CancellationToken ct = default)
    {
        await using var db = await dbFactory.CreateDbContextAsync(ct);
        return await db.Tasks
            .Where(t => t.OwnerId == ownerId && t.Location != null && t.Location != "")
            .Select(t => t.Location!)
            .Distinct()
            .OrderBy(l => l)
            .ToListAsync(ct);
    }

    public async Task<TaskItem?> GetAsync(int id, string ownerId, CancellationToken ct = default)
    {
        await using var db = await dbFactory.CreateDbContextAsync(ct);
        return await db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.OwnerId == ownerId, ct);
    }

    public async Task<TaskItem> AddAsync(TaskItem task, CancellationToken ct = default)
    {
        await using var db = await dbFactory.CreateDbContextAsync(ct);
        db.Tasks.Add(task);
        await db.SaveChangesAsync(ct);
        return task;
    }

    public async Task UpdateAsync(TaskItem task, CancellationToken ct = default)
    {
        await using var db = await dbFactory.CreateDbContextAsync(ct);
        db.Tasks.Update(task);
        await db.SaveChangesAsync(ct);
    }

    public async Task<bool> SetDoneAsync(int id, string ownerId, bool done, CancellationToken ct = default)
    {
        await using var db = await dbFactory.CreateDbContextAsync(ct);
        var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.OwnerId == ownerId, ct);
        if (task is null) return false;

        task.Done = done;
        task.CompletedUtc = done ? DateTime.UtcNow : null;
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> SetPinnedAsync(int id, string ownerId, bool pinned, CancellationToken ct = default)
    {
        await using var db = await dbFactory.CreateDbContextAsync(ct);
        var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.OwnerId == ownerId, ct);
        if (task is null) return false;

        task.IsPinned = pinned;
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, string ownerId, CancellationToken ct = default)
    {
        await using var db = await dbFactory.CreateDbContextAsync(ct);
        var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.OwnerId == ownerId, ct);
        if (task is null) return false;

        db.Tasks.Remove(task);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
