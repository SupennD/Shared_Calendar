using DTOs;

namespace CalendarApp.Services;

public interface IGroupService
{
    Task<GroupDto> CreateGroupAsync(CreateGroupDto createGroupDto);
    Task<GroupDto?> GetGroupByIdAsync(int id);
    Task<List<GroupDto>> GetUserGroupsAsync(int userId);
    Task<List<GroupDto>> GetAvailableGroupsAsync(int userId);
    Task UpdateGroupAsync(int id, CreateGroupDto groupDto);
    Task DeleteGroupAsync(int id);
    Task<bool> JoinGroupAsync(int groupId, int userId);
}