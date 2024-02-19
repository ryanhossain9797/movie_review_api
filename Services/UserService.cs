using AutoMapper;
using imdb.Models;
using imdb.Repository;
using Microsoft.EntityFrameworkCore;

namespace imdb.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMovieService _movieService;

    public UserService(
        IUserRepository userRepository,
        IMovieService movieService)
    {
        _userRepository = userRepository;
        _movieService = movieService;
    }

    public Task<User?> GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }

    public IQueryable<User> GetUsersByIds(ICollection<int> ids)
    {
        return
            _userRepository
                .GetUsers()
                .Where(u => ids.Contains(u.Id));
    }

    public IQueryable<User> GetUsers()
    {
        return
            _userRepository
            .GetUsers();
    }

    public IQueryable<Movie> GetFavoriteMoviesOfUser(int id)
    {
        return
            _userRepository
            .GetUsers()
            .Where(u => u.Id == id)
            .SelectMany(u => u.FavoritedMovies.Select(fm => fm.Movie));
    }

    public Task<bool> CreateUser(User user)
    {
        return _userRepository.CreateUser(user);
    }

    public async Task<bool> FavoriteMovie(int userId, int movieId)
    {
        var user =
            (await
            GetUsersByIds(new List<int> { userId })
                .Include(u => u.FavoritedMovies)
                .ToListAsync()).FirstOrDefault();

        var movie = await _movieService.GetMovieById(movieId);

        if(user is null)
            throw new Exception("User Not Found");
        if(movie is null)
            throw new Exception("Movie Not Found");

        if(user.FavoritedMovies.Any(ufm => ufm.MovieId == movieId))
            return true;

        user.FavoritedMovies.Add(new() { User = user, Movie = movie });

        return await _userRepository.UpdateUser(user);
    }
}