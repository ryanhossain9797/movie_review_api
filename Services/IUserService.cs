using imdb.Models;

namespace imdb.Services;

public interface IUserService {
    public Task<User?> GetUserById(int id);

    public  IQueryable<User> GetUsersByIds(ICollection<int> ids);

    public  IQueryable<User> GetUsers();

    IQueryable<Movie> GetFavoriteMoviesOfUser(int id);

    Task<bool> CreateUser(User user);

    Task<bool> FavoriteMovie(int userId, int movieId);
}