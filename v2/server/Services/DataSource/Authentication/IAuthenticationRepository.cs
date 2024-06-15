namespace server.Services.DataSource;
public interface IAuthenticationRepository
{
    Task<AuthenticationModel?> CreateAuthentication(AuthenticationModel authentication);
    Task UpdateAuthentication(AuthenticationModel authentication);
    Task DeleteAuthentication(string userId);
    Task<AuthenticationModel?> GetAuthenticationByUserId(string userId);
}