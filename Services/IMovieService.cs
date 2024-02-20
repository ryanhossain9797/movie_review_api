using MovieReviewApi.Models;

namespace MovieReviewApi.Services;

public interface IMovieService {
    public Task<Movie?> GetMovieById(int id);

    public Task<ICollection<Movie>> GetMoviesByIds(ICollection<int> ids);

    IQueryable<Movie> GetMovies();

    Task<bool> CreateMovie(Movie movie);
}