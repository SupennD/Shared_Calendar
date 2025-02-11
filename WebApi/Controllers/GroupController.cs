using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace WebApi.Controllers;

[ApiController]
[Route("/groups")]
public class GroupController : ControllerBase
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public GroupController(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }
    [HttpPost]
    public async Task<IResult> CreateGroupAsync(CreateGroupDto createGroupDto)
    {
        var creator = await _userRepository.GetSingleAsync(createGroupDto.CreatorId);
        if (creator == null)
        {
            return Results.NotFound("Creator not found.");
        }

        var newGroup = new Group
        {
            Name = createGroupDto.Name,
            CreatorId = createGroupDto.CreatorId,
            Members = new List<User> { creator }
        };

        await _groupRepository.AddAsync(newGroup);
        return Results.Created($"/groups/{newGroup.Id}", new GroupDto
        {
            Id = newGroup.Id,
            Name = newGroup.Name,
            CreatorId = newGroup.CreatorId
        });
    }
    
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<GroupDto>>> GetUserGroups(int userId)
    {
        var groups = await _groupRepository.GetMany()
            .Where(g => g.Members.Any(m => m.Id == userId))
            .Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.Name,
                CreatorId = g.CreatorId
            })
            .ToListAsync();

        return Ok(groups);
    }

    [HttpGet("available/{userId:int}")]
    public async Task<IResult> GetAvailableGroupsAsync(int userId)
    {
        var userGroups = await _groupRepository.GetMany()
            .Where(g => g.Members.Any(m => m.Id == userId))
            .Select(g => g.Id)
            .ToListAsync();

        var availableGroups = await _groupRepository.GetMany()
            .Where(g => !userGroups.Contains(g.Id))
            .Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.Name,
                CreatorId = g.CreatorId
            })
            .ToListAsync();

        return Results.Ok(availableGroups);
    }

    [HttpPost("{groupId:int}/join")]
    public async Task<IResult> JoinGroupAsync(int groupId, [FromBody] int userId)
    {
        var group = await _groupRepository.GetSingleAsync(groupId);
        var userToAdd = await _userRepository.GetSingleAsync(userId);

        if (group == null || userToAdd == null)
            return Results.NotFound("Group or user not found.");

        if (!group.Members.Any(m => m.Id == userToAdd.Id))
        {
            group.Members.Add(userToAdd);
            await _groupRepository.UpdateAsync(group);
            return Results.Ok();
        }

        return Results.BadRequest("User is already a member of the group.");
    }
}