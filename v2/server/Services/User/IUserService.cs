using System.Net;
using Microsoft.AspNetCore.Mvc;
using server.Services.DataSource;

namespace server.Services;

public interface IUserService
{
    Task<IActionResult> SignUp(SignUpDto signUpRequest);
    Task<IActionResult> UpdatePassword(PasswordUpdateDto passwordUpdate);
    Task<IActionResult> UpdateUser(UserModel user);
    Task<IActionResult> DeleteUser(string userId);
    Task<UserModel?> GetUserById(string userId);
    Task<UserModel?> GetUserByEmail(string email);
    Task<List<UserModel>> GetAllUsers();
    Task<List<UserModel>> GetUsersByIds(List<string> userIds);
}