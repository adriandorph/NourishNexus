using server.Core.Services.Authentication.DTOs;

namespace server.Core.Services.Authentication;

public interface IAuthenticationService
{
    Task<string?> AuthenticateAsync(LoginDTO loginDTO);
}