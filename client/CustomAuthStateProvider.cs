namespace client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class CustomAuthStateProvider : AuthenticationStateProvider
{

    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;

    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
    {

        _localStorage = localStorage;
        _http = http;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      
        
        string token =  await _localStorage.GetItemAsStringAsync("nourish_nexus_auth_token");
        
        var identity = new ClaimsIdentity();
        _http.DefaultRequestHeaders.Authorization = null;

        if(!string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Custom auth state provider token is " + token);
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Replace("\"",""));

        }
            

        var user = new ClaimsPrincipal(identity);
        Console.WriteLine("User is" + user);
        var state = new AuthenticationState(user);
        Console.WriteLine("State read");
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    #region helpers
    /*
    This region of code, "helpers", is taken from *https://github.com/SteveSandersonMS/presentation-2019-06-NDCOslo/blob/master/demos/MissionControl/MissionControl.Client/Util/ServiceExtensions.cs*
    */
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    #endregion helpers
}