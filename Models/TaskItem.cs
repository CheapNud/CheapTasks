using System.ComponentModel.DataAnnotations;

namespace CheapTasks.Models;

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

    public bool Done { get; set; }

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    public DateTime? DueUtc { get; set; }

    public DateTime? CompletedUtc { get; set; }
}
