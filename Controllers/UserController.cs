using System.Net;
using AutoMapper;
using MovieReviewApi.Dto;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApi.Repository;
using MovieReviewApi.Models;
using MovieReviewApi.Services;
using Microsoft.EntityFrameworkCore;

namespace MovieReviewApi.Controllers;

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

    public class GetAllUsersQuery
    {
        public int Limit { get; set; }
    }

    [HttpPost("Take")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<UserDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllUsers(GetAllUsersQuery query)
    {
        var users = await _userService.GetUsers().Include(u => u.FavoritedMovies).Take(query.Limit).ToListAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userDtos = _mapper.Map<List<UserDto>>(users);

        return Ok(userDtos);
    }

    public class GetUserQuery
    {
        public int UserId { get; set; }
    }

    [HttpPost("Get")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUser([FromBody] GetUserQuery query)
    {
        var user = await _userService.GetUserById(query.UserId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (user is null)
            return NotFound();

        var userDto = _mapper.Map<UserDto>(user);

        return Ok(userDto);
    }

    public class GetFavoriteMoviesOfUserQuery
    {
        public int UserId { get; set; }
    }

    [HttpPost("FavoriteMovies/Get")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FavoriteMoviesOfUserDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetFavoriteMoviesOfUser([FromBody] GetFavoriteMoviesOfUserQuery query)
    {
        if (await _userService.GetUserById(query.UserId) is null)
            return NotFound();

        var movies = await _userService.GetFavoriteMoviesOfUser(query.UserId).ToListAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var data =
            new FavoriteMoviesOfUserDto
            {
                UserId = query.UserId,
                FavoriteMovies = _mapper.Map<List<MovieDto>>(movies)
            };

        return Ok(data);
    }

    [HttpPost("FavoriteMovies/Add")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(String))]
    public async Task<IActionResult> FavoriteMovie([FromBody] UserFavoriteMovieDto userFavoriteMovie)
    {
        try
        {
            if (await _userService.FavoriteMovie(userFavoriteMovie.UserId, userFavoriteMovie.MovieId) is false)
                return BadRequest();

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpPost("Create")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        try
        {
            var userResult =
                await MovieReviewApi.Models.User.Construct(_userService, _mapper, userDto);

            if (userResult.IsError)
                return BadRequest(userResult.ErrorValue);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }
}