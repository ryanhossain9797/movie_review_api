namespace imdb.Repository;

using imdb.Models;

public interface IUserRepository {
    IEnumerable<User> GetUsers();
}