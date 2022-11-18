using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NTC_Lego.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NTC_Lego.Server.Util
{
    public class JWTUtil
    {
        public static string CreateToken(User user, IConfiguration configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // AppSetting:Token value is used as a key for the signature
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSetting:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
