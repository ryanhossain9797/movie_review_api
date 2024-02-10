using imdb.Models;

namespace imdb.Services;

public interface IMovieService {
    public Task<Movie?> GetMovieById(int id);
    public Task<ICollection<Movie>> GetMoviesByIds(ICollection<int> ids);
}