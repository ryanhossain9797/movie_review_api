namespace imdb.Repository;

using imdb.Models;

public interface IUserRepository {
    Task<User?> GetUserById(int id);
    IQueryable<User> GetUsers();
}