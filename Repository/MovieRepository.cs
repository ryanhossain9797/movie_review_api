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

    public async Task<Movie?> GetMovieById(int id)
    {
        return await _dataCotext.Movies.FindAsync(id);
    }

    public IQueryable<Movie> GetMovies()
    {
        return _dataCotext.Movies.AsQueryable();
    }

    public async Task<bool> CreateMovie(Movie movie)
    {
        _dataCotext.Add(movie);

        return await _dataCotext.SaveChangesAsync() > 0;
    }
}