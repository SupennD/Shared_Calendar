using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Group
{
    [Key]public int Id { get; set; }
    public required string Name { get; set; }
    

    public int CreatorId { get; set; }
    public User Creator { get; set; } = default!;

    // Navigation Properties
    public List<User> Members { get; set; } = new();
    public List<Event> Events { get; set; } = new();
}