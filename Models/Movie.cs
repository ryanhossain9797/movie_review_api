using AutoMapper;
using Microsoft.FSharp.Core;
using MovieReviewApi.Dto;
using MovieReviewApi.Services;

namespace MovieReviewApi.Models;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<UserFavoriteMovie> FavoritedByUsers { get; set; } = new List<UserFavoriteMovie>();

    public static async Task<FSharpResult<Unit?, string>> Construct(
        IMovieService movieService,
        IMapper mapper,
        MovieDto? movieDto)
    {
        if (movieDto is null)
            return FSharpResult<Unit?, string>.NewError("Required data not submitted");

        var movie = mapper.Map<Movie>(movieDto);

        if (await movieService.CreateMovie(movie) is false)
            return FSharpResult<Unit?, string>.NewError("Movie could not be created");

        return FSharpResult<Unit?, string>.NewOk(null);
    }
}