using imdb.Data;
using imdb.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace imdb.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataCotext;

    public UserRepository(DataContext dataContext)
    {
        _dataCotext = dataContext;
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _dataCotext.Users.FindAsync(id);
    }

    public IQueryable<User> GetUsers()
    {
        return _dataCotext.Users.AsQueryable();
    }
}