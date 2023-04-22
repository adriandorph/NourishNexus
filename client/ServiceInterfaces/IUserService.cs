namespace client.ServiceInterfaces;
using server.Core.EF.DTO;

public interface IUserService
{
        Task<HttpResponseMessage> RegisterUser(UserCreateDTO user);
        Task<HttpResponseMessage> Login(LoginRequest loginRequest);
        Task<HttpResponseMessage> UpdateUser(UserUpdateDTO user);
        Task<UserDTO> GetUserByID(int id);
        Task<UserNutritionDTO> GetUserNutritionByID(int id);
}
