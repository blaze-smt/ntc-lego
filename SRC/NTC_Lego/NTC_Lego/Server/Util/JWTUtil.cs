using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

using NTC_Lego.Shared;

namespace NTC_Lego.Server.Util
{
    /// <summary>
    /// Class used to create Json WebToken for users
    /// </summary>
    public class JWTUtil
    {
        public static string CreateToken(User user, IConfiguration configuration)
        {
            // Set userName and role
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            // The 'AppSetting:JwtPassword' value is used as a key for the signature
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSetting:JwtPassword").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Create new token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
