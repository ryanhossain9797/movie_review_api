using MovieReviewApi.Dto;

namespace MovieReviewApi.Models;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<UserFavoriteMovie> FavoritedByUsers { get; set; } = new List<UserFavoriteMovie>();
}