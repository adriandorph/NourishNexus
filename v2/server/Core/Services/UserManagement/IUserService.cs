using server.Core.Services.UserManagement.DTOs;

namespace server.Core.Services.UserManagement;

public interface IUserService
{
    Task<IActionResult> SignUpAsync(SignUpDTO signUpRequest);
    Task<IActionResult> UpdatePasswordAsync(PasswordUpdateDTO passwordUpdate);
    Task<Image?> UpdateProfilePictureAsync(UpdateProfilePictureDTO updateProfilePictureDTO);

    /// <summary>
    /// Only updates fields that are not null in the UpdateUserDetailsDTO
    /// </summary>
    Task<User?> UpdateUserDetailsAsync(UpdateUserDetailsDTO updateUserDetailsDTO);
    Task<bool> DeleteProfilePictureAsync(string userId);
    Task<bool> DeleteUserAsync(string userId);
    Task<User?> GetUserByIdAsync(string userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<List<User>> GetAllUsersAsync();
    Task<List<User>> GetUsersByIdsAsync(List<string> userIds);
}