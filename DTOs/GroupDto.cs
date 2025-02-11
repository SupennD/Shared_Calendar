namespace DTOs;

public class GroupDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    

    public int CreatorId { get; set; }
}