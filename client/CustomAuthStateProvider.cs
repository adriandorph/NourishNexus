namespace client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var user = httpContext?.User;

        if (user?.Identity?.IsAuthenticated == true)
        {
            var jwtCookie = httpContext.Request.Cookies["jwt"];
            if (!string.IsNullOrEmpty(jwtCookie))
            {
                var principal = new ClaimsPrincipal(user);
                return Task.FromResult(new AuthenticationState(principal));
            }
        }

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
    }
}