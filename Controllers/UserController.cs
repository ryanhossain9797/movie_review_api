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
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Id")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (user is null)
            return NotFound();

        return Ok(user.ToDto());
    }

    [HttpGet("FavoriteMovies")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
    public async Task<IActionResult> GetFavoriteMoviesOfUser(int id)
    {
        var movies = await _userService.GetFavoriteMoviesOfUser(id).ToListAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var data =
            new FavoriteMoviesOfUserDto()
            {
                UserId = id,
                FavoriteMovies = movies.Select(m => m.ToDto()).ToList()
            };

        return Ok(data);
    }
}