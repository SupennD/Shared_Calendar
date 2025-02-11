namespace DTOs;

public class CreateEventDto
{
    public required  string Name { get; set; }
    public string? Description { get; set; }
    public  string? Location { get; set; }
    public  DateTime? StartTime { get; set; }
    public  DateTime? EndTime { get; set; }

    public  int GroupId { get; set; }
    
    public  int CreatorId { get; set; }
}