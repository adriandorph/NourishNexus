namespace server.Core.Infrastructure.DataBase;

public interface IUserRepository
{
    Task<User?> CreateUser(User user);
    Task<User?> UpdateUser(User user);
    Task<bool> DeleteUser(string userId);
    Task<User?> GetUserById(string userId);
    Task<User?> GetUserByEmail(string email);
    Task<List<User>> GetAllUsers();
    Task<List<User>> GetUsersByIds(List<string> userIds);
}
