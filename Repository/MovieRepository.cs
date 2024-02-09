using imdb.Data;
using imdb.Models;

namespace imdb.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly DataContext _dataCotext;

    public MovieRepository(DataContext dataContext)
    {
        _dataCotext = dataContext;
    }

    public IEnumerable<Movie> GetMovies()
    {
        return _dataCotext.Movies.AsEnumerable();
    }
}