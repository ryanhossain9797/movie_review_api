using MovieReviewApi.Models;
using MovieReviewApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace MovieReviewApi.Services;

public class MovieService: IMovieService {
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public Task<Movie?> GetMovieById(int id)
    {
        return _movieRepository.GetMovieById(id);
    }

    public async Task<ICollection<Movie>> GetMoviesByIds(ICollection<int> ids)
    {
        return
            await
            _movieRepository
            .GetMovies()
            .Where(u => ids.Contains(u.Id))
            .ToListAsync();
    }

    public IQueryable<Movie> GetMovies()
    {
        return
            _movieRepository
            .GetMovies();
    }

    public Task<bool> CreateMovie(Movie movie)
    {
        return _movieRepository.CreateMovie(movie);
    }
}