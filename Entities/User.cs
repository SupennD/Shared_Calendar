using System.ComponentModel.DataAnnotations;

namespace Entities;

public class User
{ 
    [Key] public int Id { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }

    public List<Group> GroupsJoined { get; set; } = new();
    public List<Group> GroupsCreated { get; set; } = new();
    public List<Event> EventsCreated { get; set; } = new();
}