using MovieReviewApi.Data;
using MovieReviewApi.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MovieReviewApi.Repository;

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

    public async Task<bool> CreateUser(User user)
    {
        _dataCotext.Add(user);

        return await _dataCotext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateUser(User user)
    {
        _dataCotext.Update(user);

        return await _dataCotext.SaveChangesAsync() > 0;
    }
}