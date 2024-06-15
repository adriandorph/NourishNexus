namespace server.Services.DataSource;

public interface IUserRepository
{
    Task<UserModel?> CreateUser(UserModel user);
    Task UpdateUser(UserModel user);
    Task DeleteUser(string userId);
    Task<UserModel?> GetUserById(string userId);
    Task<UserModel?> GetUserByEmail(string email);
    Task<List<UserModel>> GetAllUsers();
    Task<List<UserModel>> GetUsersByIds(List<string> userIds);
}
