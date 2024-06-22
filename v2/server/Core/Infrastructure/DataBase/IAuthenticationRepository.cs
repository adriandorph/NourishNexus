namespace server.Core.Infrastructure.DataBase;
public interface IAuthenticationRepository
{
    Task<AuthenticationModel?> CreateAuthentication(AuthenticationModel authentication);
    Task<bool> UpdateAuthentication(AuthenticationModel authentication);
    Task DeleteAuthentication(string userId);
    Task<AuthenticationModel?> GetAuthenticationByUserId(string userId);
}