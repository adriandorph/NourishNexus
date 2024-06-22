using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using server.Services.DataSource.UserSource;
using server.Services.DataSource.Authentication;
using server.Core;
using server.Services.UserManagement.Models;

namespace server.Services.UserManagement;

public class UserService(IUserRepository userRepo, IAuthenticationRepository authRepo) : IUserService
{
    private readonly IUserRepository _userRepo = userRepo;
    private readonly IAuthenticationRepository _authRepo = authRepo;

    public async Task<IActionResult> SignUp(SignUpDto signUpRequest)
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
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        // Attempt to create authentication for the user
        var auth = await CreateAuthentication(signUpRequest, createdUser);
        if (auth == null)
        {
            await _userRepo.DeleteUser(createdUser.Id!);
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        return new OkObjectResult(createdUser);
    }

    public async Task<IActionResult> UpdateUser(User user)
    {
        await _userRepo.UpdateUser(user);
        return new OkResult();
    }

    public async Task<IActionResult> UpdatePassword(PasswordUpdateDto passwordUpdate)
    {
        if (passwordUpdate == null || string.IsNullOrEmpty(passwordUpdate.Email) || string.IsNullOrEmpty(passwordUpdate.Password)) 
            return new BadRequestObjectResult("Invalid password update request.");

        var user = await _userRepo.GetUserByEmail(passwordUpdate.Email);
        if (user == null) return new StatusCodeResult((int) HttpStatusCode.NotFound);

        var (passwordHash, passwordSalt) = CreatePasswordHash(passwordUpdate.Password!);
        
        var auth = new AuthenticationModel
        {
            UserId = user.Id!,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await _authRepo.UpdateAuthentication(auth);
        return new OkResult();
    }

    public async Task<IActionResult> DeleteUser(string userId) 
    {
        await _userRepo.DeleteUser(userId);
        await _authRepo.DeleteAuthentication(userId);
        return new OkResult();
    }

    public async Task<List<User>> GetAllUsers() => await _userRepo.GetAllUsers();

    public async Task<User?> GetUserByEmail(string email) => await _userRepo.GetUserByEmail(email);

    public async Task<User?> GetUserById(string userId) => await _userRepo.GetUserById(userId);

    public async Task<List<User>> GetUsersByIds(List<string> userIds) => await _userRepo.GetUsersByIds(userIds);


    private static (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return (passwordHash, passwordSalt);
    }

    private async Task<IActionResult> CreateUser(SignUpDto signUpRequest)
    {
        var user = new User
        {
            Email = signUpRequest.Email,
            Username = signUpRequest.Nickname
        };

        // Check if user with email already exists
        if (user.Email == null) return new StatusCodeResult((int) HttpStatusCode.BadRequest);

        var existingUser = await _userRepo.GetUserByEmail(user.Email!);
        if (existingUser != null) return new StatusCodeResult((int) HttpStatusCode.Conflict);

        var createdUser = await _userRepo.CreateUser(user);
        if (createdUser == null) return new StatusCodeResult((int) HttpStatusCode.InternalServerError);

        return new OkObjectResult(createdUser);
    }

    private async Task<AuthenticationModel?> CreateAuthentication(SignUpDto signUpRequest, User user)
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
}