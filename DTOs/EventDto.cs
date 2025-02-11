namespace DTOs;

public class EventDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public  DateTime? StartTime { get; set; }
    public  DateTime? EndTime { get; set; }

    public int GroupId { get; set; }
    
    public int CreatorId { get; set; }
    public string CreatorName { get; set; } 
}