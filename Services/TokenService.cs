using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using zavrsni_backend.Entities;
using zavrsni_backend.Models.DTO;
using zavrsni_backend.Services.Interfaces;

namespace zavrsni_backend.Services
{
    public class TokenService : ITokenService
    {
        public UserTokenDTO CreateToken(User user, string tokenKey)
        {
            IList<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserTokenDTO { Token = tokenString };
        }
    }
}
