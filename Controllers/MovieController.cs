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
public class MovieController : Controller
{

    private readonly IMapper _mapper;

    private readonly IMovieService _movieService;

    public MovieController(IMapper mapper, IMovieService movieService)
    {
        _mapper = mapper;
        _movieService = movieService;
    }

    public class GetAllMoviesQuery
    {
        public int Limit { get; set; }
    }

    [HttpPost("Take")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<MovieDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllMovies([FromBody] GetAllMoviesQuery query)
    {
        var movies = await _movieService.GetMovies().Take(query.Limit).ToListAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var movieDtos = _mapper.Map<List<MovieDto>>(movies);

        return Ok(movieDtos);
    }

    public class GetMovieQuery
    {
        public int MovieId { get; set; }
    }

    [HttpPost("Get")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MovieDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetMovie([FromBody] GetMovieQuery query)
    {
        var movie = await _movieService.GetMovieById(query.MovieId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (movie is null)
            return NotFound();

        var movieDto = _mapper.Map<MovieDto>(movie);

        return Ok(movieDto);
    }

    [HttpPost("Create")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateMovie([FromBody] MovieDto? movieDto)
    {
        try
        {
            var movieResult =
                await MovieReviewApi.Models.Movie.Construct(_movieService, _mapper, movieDto);

            if (movieResult.IsError)
                return BadRequest(movieResult.ErrorValue);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }
}