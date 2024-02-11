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
}