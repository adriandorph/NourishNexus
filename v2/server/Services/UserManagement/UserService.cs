using System.Net;
using System.Security.Cryptography;
using server.Core.Infrastructure.DataBase;
using server.Core.Services.Authentication;
using server.Core.Services.Authentication.DTOs;
using server.Core.Services.UserManagement;
using server.Core.Services.UserManagement.DTOs;

namespace server.Services.UserManagement;

public class UserService(
    IUserRepository userRepo, 
    IImageRepository imageRepo,
    IAuthenticationRepository authRepo,
    IAuthenticationService authenticationService) : IUserService
{
    private readonly IUserRepository _userRepo = userRepo;
    private readonly IImageRepository _imageRepo = imageRepo;
    private readonly IAuthenticationRepository _authRepo = authRepo;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public async Task<IActionResult> SignUpAsync(SignUpDTO signUpRequest)
    {
        if (signUpRequest == null || string.IsNullOrEmpty(signUpRequest.Password))
        {
            return new BadRequestObjectResult("Invalid sign-up request.");
        }

        // Attempt to create the user
        IActionResult createdUserResult = await CreateUser(signUpRequest);
        if (createdUserResult is not OkObjectResult okResult)
        {
            return createdUserResult;
        }

        if (okResult.Value is not User createdUser)
        {
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }

        // Attempt to create authentication for the user
        var auth = await CreateAuthentication(signUpRequest, createdUser);
        if (auth == null)
        {
            await _userRepo.DeleteUser(createdUser.Id!);
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }

        return new OkObjectResult(createdUser);
    }

    public async Task<IActionResult> UpdatePasswordAsync(PasswordUpdateDTO passwordUpdate)
    {
        if (passwordUpdate == null 
            || string.IsNullOrEmpty(passwordUpdate.Email) 
            || string.IsNullOrEmpty(passwordUpdate.NewPassword) 
            || string.IsNullOrEmpty(passwordUpdate.OldPassword)
        ) return new BadRequestObjectResult("Invalid password update request.");

        LoginDTO loginDTO = new(passwordUpdate.Email, passwordUpdate.OldPassword);
        string? token =  await _authenticationService.AuthenticateAsync(loginDTO);
        if (token == null) return new UnauthorizedResult();

        var user = await _userRepo.GetUserByEmail(passwordUpdate.Email);
        if (user == null) return new StatusCodeResult((int) HttpStatusCode.NotFound);

        var (passwordHash, passwordSalt) = CreatePasswordHash(passwordUpdate.NewPassword!);
        
        var auth = new AuthenticationModel
        {
            UserId = user.Id!,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await _authRepo.UpdateAuthentication(auth);
        return new OkResult();
    }

    public async Task<User?> UpdateUserDetailsAsync(UpdateUserDetailsDTO updateUserDetailsDTO)
    {
        var user = await _userRepo.GetUserById(updateUserDetailsDTO.UserId);
        if (user == null) return null;

        if (updateUserDetailsDTO.Email != null) user.Email = updateUserDetailsDTO.Email;
        if (updateUserDetailsDTO.Nickname != null) user.Nickname = updateUserDetailsDTO.Nickname;
        if (updateUserDetailsDTO.Bio != null) user.Bio = updateUserDetailsDTO.Bio;

        return await _userRepo.UpdateUser(user);
    }

    public async Task<Image?> UpdateProfilePictureAsync(UpdateProfilePictureDTO updateProfilePictureDTO)
    {
        //TODO: Add authorization check so only the user can update their profile picture
        var user = await _userRepo.GetUserById(updateProfilePictureDTO.UserId);
        if (user == null) return null;

        if (user.ProfilePictureId != null)
        {
            //Update existing image
            var existingImage = await _imageRepo.GetImageByIdAsync(user.ProfilePictureId);

            if (existingImage == null) 
                return await CreateImage(updateProfilePictureDTO.ImageBase64, user);

            existingImage.ImageBase64 = updateProfilePictureDTO.ImageBase64;
            return await _imageRepo.UpdateImageAsync(existingImage);
        }

        return await CreateImage(updateProfilePictureDTO.ImageBase64, user);
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        //TODO: Remove all references to the user from other collections
        //TODO: Delete the profile picture
        await DeleteProfilePictureAsync(userId);
        await _userRepo.DeleteUser(userId);
        await _authRepo.DeleteAuthentication(userId);
        return true;
    }

    public async Task<bool> DeleteProfilePictureAsync(string userId) 
    {
        //TODO: Add authorization check so only the user can delete their profile picture
        var user = await _userRepo.GetUserById(userId);
        if (user == null || user.ProfilePictureId == null) return false;

        return await _imageRepo.DeleteImageAsync(user.ProfilePictureId);
    }

    public async Task<List<User>> GetAllUsersAsync() => await _userRepo.GetAllUsers();

    public async Task<User?> GetUserByEmailAsync(string email) => await _userRepo.GetUserByEmail(email);

    public async Task<User?> GetUserByIdAsync(string userId) => await _userRepo.GetUserById(userId);

    public async Task<List<User>> GetUsersByIdsAsync(List<string> userIds) => await _userRepo.GetUsersByIds(userIds);


    private static (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return (passwordHash, passwordSalt);
    }

    private async Task<IActionResult> CreateUser(SignUpDTO signUpRequest)
    {
        var user = new User
        {
            Email = signUpRequest.Email,
            Nickname = signUpRequest.Nickname
        };

        // Check if user with email already exists
        if (user.Email == null) return new StatusCodeResult((int) HttpStatusCode.BadRequest);

        var existingUser = await _userRepo.GetUserByEmail(user.Email!);
        if (existingUser != null) return new StatusCodeResult((int) HttpStatusCode.Conflict);

        var createdUser = await _userRepo.CreateUser(user);
        if (createdUser == null) return new StatusCodeResult((int) HttpStatusCode.InternalServerError);

        return new OkObjectResult(createdUser);
    }

    private async Task<AuthenticationModel?> CreateAuthentication(SignUpDTO signUpRequest, User user)
    {
        var (passwordHash, passwordSalt) = CreatePasswordHash(signUpRequest.Password!);

        var auth = new AuthenticationModel
        {
            UserId = user.Id!,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        return await _authRepo.CreateAuthentication(auth);
    }

    private async Task<Image?> CreateImage(string imageBase64, User user)
    {
        var image = new Image { ImageBase64 = imageBase64 };
        var createdImage = await _imageRepo.CreateImageAsync(image);
        if (createdImage == null || createdImage.Id == null) return null;

        user.ProfilePictureId = createdImage.Id;
        var updatedUser = await _userRepo.UpdateUser(user);

        if (updatedUser == null) {
            await _imageRepo.DeleteImageAsync(createdImage.Id);
            return null;
        }

        return createdImage;
    }
}