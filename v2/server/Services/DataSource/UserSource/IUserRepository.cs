namespace server.Services.DataSource.UserSource;

public interface IUserRepository
{
    Task<User?> CreateUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(string userId);
    Task<User?> GetUserById(string userId);
    Task<User?> GetUserByEmail(string email);
    Task<List<User>> GetAllUsers();
    Task<List<User>> GetUsersByIds(List<string> userIds);
}
