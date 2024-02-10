using Microsoft.AspNetCore.Mvc;
using imdb.Repository;
using imdb.Models;
using imdb.Services;
using Microsoft.EntityFrameworkCore;

namespace imdb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : Controller
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
    public async Task<IActionResult> GetMovie(int id)
    {
        var movie = await _movieService.GetMovieById(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if(movie is null)
            return NotFound();

        return Ok(movie.ToDto());
    }
}