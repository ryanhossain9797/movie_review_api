namespace imdb.Repository;

using imdb.Models;

public interface IUserRepository {
    Task<User?> GetUserById(int id);

    IQueryable<User> GetUsers();

    Task<bool> CreateUser(User user);

    Task<bool> UpdateUser(User user);
}