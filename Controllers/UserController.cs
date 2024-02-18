using System.Net;
using AutoMapper;
using imdb.Dto;
using Microsoft.AspNetCore.Mvc;
using imdb.Repository;
using imdb.Models;
using imdb.Services;
using Microsoft.EntityFrameworkCore;

namespace imdb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IMapper _mapper;

    private readonly IUserService _userService;

    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (user is null)
            return NotFound();

        var userDto = _mapper.Map<UserDto>(user);

        return Ok(userDto);
    }

    [HttpGet("{userId}/favorite-movies")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FavoriteMoviesOfUserDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetFavoriteMoviesOfUser(int userId)
    {
        if (await _userService.GetUserById(userId) is null)
            return NotFound();

        var movies = await _userService.GetFavoriteMoviesOfUser(userId).ToListAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var data =
            new FavoriteMoviesOfUserDto
            {
                UserId = userId,
                FavoriteMovies = _mapper.Map<List<MovieDto>>(movies)
            };

        return Ok(data);
    }

    [HttpPost("{userId}/favorite-movies/add")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(String))]
    public async Task<IActionResult> FavoriteMovie(int userId, int movieId)
    {
        try
        {
            if (await _userService.FavoriteMovie(userId, movieId) is false)
                return BadRequest();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateUser([FromBody] UserDto? userDto)
    {
        try
        {
            if (userDto is null)
                ModelState.AddModelError("", "Required data not submitted");

            var maybeExistingUser =
                await
                    _userService
                    .GetUsers()
                    .Where(usr => usr.Email == userDto.Email)
                    .FirstOrDefaultAsync();

            if (maybeExistingUser is not null)
                ModelState.AddModelError("", "User with email already exists");

            var user = _mapper.Map<User>(userDto);

            if (await _userService.CreateUser(user) is false)
                ModelState.AddModelError("", "User could not be created");

            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }
}