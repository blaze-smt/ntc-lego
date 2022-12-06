using System.Security.Claims;

namespace NTC_Lego.Client.Services
{
    public static class UserService
    {
        public static string? UserJWT { get; set; }
        public static IEnumerable<Claim> GetClaims()
        {
            if (UserJWT != null)
            {
                return CustomAuthStateProvider.ParseClaimsFromJwt(UserJWT);
            }
            else
            {
                return new List<Claim>();
            }
        }
    }
}
