namespace imdb.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<UserFavoriteMovie> FavoritedMovies { get; set; } = new List<UserFavoriteMovie>();
}