using AutoMapper;
using imdb.Models;
using imdb.Repository;
using Microsoft.EntityFrameworkCore;

namespace imdb.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;

    }

    public async Task<User?> GetUserById(int id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<ICollection<User>> GetUsersByIds(ICollection<int> ids)
    {
        return
            await
                _userRepository
                    .GetUsers()
                    .Where(u => ids.Contains(u.Id))
                    .ToListAsync();
    }

    public IQueryable<Movie> GetFavoriteMoviesOfUser(int id)
    {
        return
            _userRepository
            .GetUsers()
            .Where(u => u.Id == id)
            .SelectMany(u => u.FavoritedMovies.Select(fm => fm.Movie));
    }
}