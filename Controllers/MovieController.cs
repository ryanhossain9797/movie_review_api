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
public class MovieController : Controller
{

    private readonly IMapper _mapper;

    private readonly IMovieService _movieService;

    public MovieController(IMapper mapper, IMovieService movieService)
    {
        _mapper = mapper;
        _movieService = movieService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MovieDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetMovie(int id)
    {
        var movie = await _movieService.GetMovieById(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (movie is null)
            return NotFound();

        var movieDto = _mapper.Map<MovieDto>(movie);

        return Ok(movieDto);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateMovie([FromBody] MovieDto? movieDto)
    {
        try
        {
            if (movieDto is null)
                ModelState.AddModelError("", "Required data not submitted");

            var movie = _mapper.Map<Movie>(movieDto);

            if (await _movieService.CreateMovie(movie) is false)
                ModelState.AddModelError("", "Movie could not be created");

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