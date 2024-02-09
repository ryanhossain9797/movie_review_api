using imdb.Models;

namespace imdb.Repository;

public interface IMovieRepository {
    IEnumerable<Movie> GetMovies();
}