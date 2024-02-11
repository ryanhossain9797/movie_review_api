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
}