using DTOs;

namespace CalendarApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto createUserDto);
    public Task<bool> isValidUserAsync(int userId);
    public Task UpdateUserAsync(UserDto userDto);
    public Task<UserDto?> GetUserByIdAsync(int userId);

}

