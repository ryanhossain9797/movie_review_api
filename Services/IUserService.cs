using imdb.Models;

namespace imdb.Services;

public interface IUserService {
    public Task<User?> GetUserById(int id);
    public Task<ICollection<User>> GetUsersByIds(ICollection<int> ids);
    IQueryable<Movie> GetFavoriteMoviesOfUser(int id);
}