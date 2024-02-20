using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieReviewApi.Dto;
using Microsoft.FSharp.Core;
using MovieReviewApi.Services;

namespace MovieReviewApi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<UserFavoriteMovie> FavoritedMovies { get; set; } = new List<UserFavoriteMovie>();

    public static async Task<FSharpResult<Unit?, string>> Construct(
        IUserService userService,
        IMapper mapper,
        UserDto? userDto)
    {
        if (userDto is null)
            return FSharpResult<Unit?, string>.NewError("Required data not submitted");

        var maybeExistingUser =
            await
            userService
            .GetUsers()
            .Where(usr => usr.Email == userDto.Email)
            .FirstOrDefaultAsync();

        if (maybeExistingUser is not null)
            return FSharpResult<Unit?, string>.NewError("User with email already exists");

        var user = mapper.Map<User>(userDto);

        if (await userService.CreateUser(user) is false)
            return FSharpResult<Unit?, string>.NewError("User could not be created");

        return FSharpResult<Unit?, string>.NewOk(null);
    }
}