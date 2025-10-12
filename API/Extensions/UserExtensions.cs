using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class UserExtensions
{
    public static UserDto ToDto(this User user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            ImageUrl = user.ImageUrl,
            Token = tokenService.CreateToken(user)
        };
    }
}
