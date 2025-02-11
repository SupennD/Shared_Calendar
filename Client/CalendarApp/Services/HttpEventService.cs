using DTOs;
using System.Net.Http.Json;

namespace CalendarApp.Services;

public class HttpEventService : IEventService
{
    private readonly HttpClient _httpClient;

    public HttpEventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EventDto> CreateEventAsync(CreateEventDto createEventDto)
    {
        var response = await _httpClient.PostAsJsonAsync("events", createEventDto);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<EventDto>();
        }
        else
        {
            throw new Exception("Failed to create event.");
        }
    }

    public async Task<EventDto?> GetEventByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"events/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<EventDto>();
        }
        return null;
    }

    public async Task<List<EventDto>> GetEventsAsync(string? name)
    {
        var response = await _httpClient.GetFromJsonAsync<List<EventDto>>($"events?name={name}");
        return response ?? new List<EventDto>();
    }

    public async Task UpdateEventAsync(int id, CreateEventDto eventDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"events/{id}", eventDto);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to update event.");
        }
    }

    public async Task DeleteEventAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"events/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to delete event.");
        }
    }

    public async Task<List<EventDto>> GetEventsForMonthAsync(DateTime month)
    {
        var formattedMonth = month.ToString("yyyy-MM");
        var response = await _httpClient.GetFromJsonAsync<List<EventDto>>($"events/month/{formattedMonth}");
        return response ?? new List<EventDto>();
    }
    
    public async Task<List<EventDto>> GetEventsForGroupsAsync(List<int> groupIds) // Implement this method
    {
        var response = await _httpClient.PostAsJsonAsync("events/groups", groupIds);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<EventDto>>();
        }
        else
        {
            throw new Exception("Failed to fetch events for groups.");
        }
    }
}