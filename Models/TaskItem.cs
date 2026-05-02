using System.ComponentModel.DataAnnotations;

namespace CheapTasks.Models;

public enum TaskKind
{
    Todo = 0,
    Decision = 1,
}

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(450)]
    public string OwnerId { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Notes { get; set; }

    public TaskKind Kind { get; set; }

    [MaxLength(1000)]
    public string? Options { get; set; }

    public bool Done { get; set; }

    public bool IsPinned { get; set; }

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    public DateTime? DueUtc { get; set; }

    public DateTime? CompletedUtc { get; set; }
}
