using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace WebApi.Controllers;

[ApiController]
[Route("/events")]
public class EventController
{ 
    private readonly IEventRepository _eventRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public EventController(IGroupRepository groupRepository, IUserRepository userRepository, IEventRepository eventRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _eventRepository = eventRepository;
    }
    
    [HttpPost]
    public async Task<IResult> CreateAsync(CreateEventDto eventDto)
    {
        var creator = await _userRepository.GetSingleAsync(eventDto.CreatorId);
        var group = await _groupRepository.GetSingleAsync(eventDto.GroupId);
        Event createdEvent = await _eventRepository.AddAsync(new Event
        {
            Name = eventDto.Name,
            Description = eventDto.Description,
            Location = eventDto.Location,
            StartTime = eventDto.StartTime ?? DateTime.UtcNow, 
            EndTime = eventDto.EndTime ?? DateTime.UtcNow.AddHours(1), 
            Creator = creator,
            Group = group,
            CreatorId = creator.Id,
            GroupId = group.Id
        });
        return Results.Created($"events/{createdEvent.Id}",
            new EventDto
            {
                Id = createdEvent.Id,
               Name = createdEvent.Name,
               Description = createdEvent.Description,
               Location = createdEvent.Location,
               StartTime = createdEvent.StartTime,
               EndTime = createdEvent.EndTime,
               CreatorId = createdEvent.CreatorId,
               GroupId = createdEvent.GroupId,
               CreatorName = createdEvent.Creator.Name
            });
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleAsync([FromRoute] int id)
    {
        EventDto? eventDto = await _eventRepository
            .GetMany()
            .Where(p => p.Id == id)
            .Include(u => u.Creator)
            .Include(g => g.Group)
            .Select(p => new EventDto
            {
                Id = p.Id, 
                Name = p.Name,
                Description = p.Description,
                Location = p.Location,
                StartTime = p.StartTime,
                EndTime = p.EndTime,
                CreatorId = p.CreatorId,
                GroupId = p.GroupId,
                CreatorName = p.Creator.Name
            })
            .FirstOrDefaultAsync();
        return eventDto == null ? Results.NotFound() : Results.Ok(eventDto);
    }

    [HttpGet]
    public async Task<IResult> GetManyAsync([FromQuery] string? name)
    {
        IQueryable<Event> events = _eventRepository.GetMany();

        // Filter users by the "name" query parameter
        if (!string.IsNullOrEmpty(name))
        {
            events = events.Where(u => u.Name.ToLower().Contains(name.ToLower()));
        }

        IQueryable<EventDto> eventDtos = events.Select(u => new EventDto 
            { 
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                Location = u.Location,
                StartTime = u.StartTime,
                EndTime = u.EndTime,
                CreatorId = u.CreatorId,
                GroupId = u.GroupId,
                CreatorName = u.Creator.Name
            });

        return Results.Ok(eventDtos);
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateAsync(int id, CreateEventDto eventDto)
    {
        await _eventRepository.UpdateAsync(new Event
        {
            Id = id, Name = eventDto.Name,
            Description = eventDto.Description,
            Location = eventDto.Location,
            StartTime = eventDto.StartTime,
            EndTime = eventDto.EndTime,
            CreatorId = eventDto.CreatorId,
            GroupId = eventDto.GroupId
        });
        return Results.Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteAsync(int id)
    {
        await _eventRepository.DeleteAsync(id);
        return Results.Ok();
    }
    [HttpGet("month/{month}")]
    public async Task<IResult> GetEventsForMonthAsync([FromRoute] DateTime month)
    {
        DateTime firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        var eventsInMonth = await _eventRepository
            .GetMany()
            .Where(e => e.StartTime >= firstDayOfMonth && e.StartTime <= lastDayOfMonth)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Location = e.Location,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                CreatorId = e.CreatorId,
                GroupId = e.GroupId,
                CreatorName = e.Creator.Name
            })
            .ToListAsync();

        return Results.Ok(eventsInMonth);
    }
    
    [HttpPost("groups")]
    public async Task<IResult> GetEventsForGroupsAsync([FromBody] List<int> groupIds)
    {
        var events = await _eventRepository
            .GetMany()
            .Where(e => groupIds.Contains(e.GroupId))
            .Select(e => new EventDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Location = e.Location,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                CreatorId = e.CreatorId,
                GroupId = e.GroupId,
                CreatorName = e.Creator.Name
            })
            .ToListAsync();

        return Results.Ok(events);
    }
}