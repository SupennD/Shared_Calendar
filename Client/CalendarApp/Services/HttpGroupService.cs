using DTOs;
using System.Net.Http.Json;

namespace CalendarApp.Services;

public class HttpGroupService : IGroupService
{
    private readonly HttpClient _httpClient;

    public HttpGroupService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GroupDto> CreateGroupAsync(CreateGroupDto createGroupDto)
    {
            var response = await _httpClient.PostAsJsonAsync("groups", createGroupDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GroupDto>();
            }
            else
            {
                throw new Exception("Failed to create group.");
            }
    }
    public async Task<GroupDto?> GetGroupByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"groups/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<GroupDto>();
        }
        return null;
    }

    public async Task<List<GroupDto>> GetUserGroupsAsync(int userId)
    {
        var response = await _httpClient.GetAsync($"groups/user/{userId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<GroupDto>>();
    }

    public async Task<List<GroupDto>> GetAvailableGroupsAsync(int userId)
    {
        return await _httpClient.GetFromJsonAsync<List<GroupDto>>($"groups/available/{userId}");
    }

    public async Task UpdateGroupAsync(int id, CreateGroupDto groupDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"groups/{id}", groupDto);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to update group.");
        }
    }

    public async Task DeleteGroupAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"groups/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to delete group.");
        }
    }

    public async Task<bool> JoinGroupAsync(int groupId, int userId)
    {
        var response = await _httpClient.PostAsJsonAsync($"groups/{groupId}/join", userId);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to join group: {response.ReasonPhrase}. Details: {errorContent}");
        }
        return true;
    }
}