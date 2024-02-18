using imdb.Models;

namespace imdb.Repository;

public interface IMovieRepository {
    Task<Movie?> GetMovieById(int id);

    IQueryable<Movie> GetMovies();

    Task<bool> CreateMovie(Movie movie);
}