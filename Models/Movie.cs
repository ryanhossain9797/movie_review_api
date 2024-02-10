using imdb.Dto;

namespace imdb.Models;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<UserFavoriteMovie> FavoritedByUsers { get; set; } = new List<UserFavoriteMovie>();

    public MovieDto ToDto()
    {
        return
            new MovieDto()
            {
                Id = this.Id,
                Name = this.Name
            };
    }
}