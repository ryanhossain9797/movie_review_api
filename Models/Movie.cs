namespace imdb.Models;

public class Movie {
    int Id { get; set; }
    string Name { get; set; }
    ICollection<UserFavoriteMovie> FavoritedByUsers { get; set; } = new List<UserFavoriteMovie> ();
}