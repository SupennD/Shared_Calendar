using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace WebApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IResult> LoginAsync([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            User user = await _userRepository.GetSingleByNameAsync(createUserDto.Name);

            if (user == null || !user.Password.Equals(createUserDto.Password))
            {
                return Results.Json(new { message = "Username or password invalid" }, statusCode: 401);
            }

            return Results.Ok(new UserDto { Id = user.Id, Name = user.Name });
        }
        catch (Exception)
        {
            return Results.Json(new { message = "Username or password invalid" }, statusCode: 401);
        }
    }
}