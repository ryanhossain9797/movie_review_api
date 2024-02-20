namespace MovieReviewApi.Repository;

using MovieReviewApi.Models;

public interface IUserRepository {
    Task<User?> GetUserById(int id);

    IQueryable<User> GetUsers();

    Task<bool> CreateUser(User user);

    Task<bool> UpdateUser(User user);
}