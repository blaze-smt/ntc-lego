using System.Security.Claims;
using System.Text.Json;

namespace NTC_Lego.Client
{
    /// <summary>
    /// Class used to check/update the current AuthenticationState of the user during login and logout
    /// </summary>
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        /// <summary>
        /// Get and update the authentication state
        /// </summary>
        /// <returns>The current AuthenticationState of the user</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Create a string for current JWT token in local storage
            string token = await _localStorage.GetItemAsStringAsync("token");

            var identity = new ClaimsIdentity();

            // Check if token exists
            // If token does NOT exist, the new ClaimsIdentity will remain empty and the user will have no role or authorization
            if (!string.IsNullOrEmpty(token))
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            // Set the user and their AuthenticationState
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            // Change current AuthenticationState of user
            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        /// <summary>
        /// Parses Json Web Tokens
        /// </summary>
        /// <param name="jwt">Json Web Token to be parsed.</param>
        /// <returns>Parsed token</returns>
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
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
    }
}
