using DTOs;

namespace CalendarApp.Services;

public interface IEventService
{
    Task<EventDto> CreateEventAsync(CreateEventDto createEventDto);
    Task<EventDto?> GetEventByIdAsync(int id);
    Task<List<EventDto>> GetEventsAsync(string? name);
    Task UpdateEventAsync(int id, CreateEventDto eventDto);
    Task DeleteEventAsync(int id);
    Task<List<EventDto>> GetEventsForMonthAsync(DateTime month);
    Task<List<EventDto>> GetEventsForGroupsAsync(List<int> groupIds);
    
}