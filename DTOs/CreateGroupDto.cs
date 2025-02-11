namespace DTOs;

public class CreateGroupDto
{
    public required string Name { get; set; }
    
    public  int CreatorId { get; set; }
}