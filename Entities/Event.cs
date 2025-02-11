using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Event
{
    [Key]public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public required DateTime? StartTime { get; set; }
    public required DateTime? EndTime { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; } = default!;
    
    public int CreatorId { get; set; }
    public User Creator { get; set; } = default!;
}