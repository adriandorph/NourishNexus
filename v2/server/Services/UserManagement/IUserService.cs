using Microsoft.AspNetCore.Mvc;
using server.Services.UserManagement.Models;

namespace server.Services.UserManagement;

public interface IUserService
{
    Task<IActionResult> SignUp(SignUpDto signUpRequest);
    Task<IActionResult> UpdatePassword(PasswordUpdateDto passwordUpdate);
    Task<IActionResult> UpdateUser(User user);
    Task<IActionResult> DeleteUser(string userId);
    Task<User?> GetUserById(string userId);
    Task<User?> GetUserByEmail(string email);
    Task<List<User>> GetAllUsers();
    Task<List<User>> GetUsersByIds(List<string> userIds);
}