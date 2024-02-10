using imdb.Dto;

namespace imdb.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<UserFavoriteMovie> FavoritedMovies { get; set; } = new List<UserFavoriteMovie>();

    public UserDto ToDto()
    {
        return
            new UserDto()
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email
            };
    }
}