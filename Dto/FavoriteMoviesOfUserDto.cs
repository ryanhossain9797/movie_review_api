using MovieReviewApi.Models;

namespace MovieReviewApi.Dto;

public class FavoriteMoviesOfUserDto
{
    public int UserId { get; set; }
    public ICollection<MovieDto> FavoriteMovies { get; set; }
}