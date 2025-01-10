using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mops_fullstack.Server.Core;
using Mops_fullstack.Server.Datalayer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace Mops_fullstack.Server.Datalayer.Jwt
{
    public class JwtUtils : IJwtUtils
    {
        public AppSettings _appSettings;

        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }

        public string GenerateJwtToken(Player player)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var appPrivateKey = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", player.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(appPrivateKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public int? ValidatePlayerJwtToken(string token)
        {
            if (token == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var appPrivateKey = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(appPrivateKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validationToken);

                var jwtToken = (JwtSecurityToken)validationToken;
                var playerId = jwtToken.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

                return playerId == null ? null : int.Parse(playerId);
            }
            catch(ArgumentException error)
            {
                Console.WriteLine(error);
                return null;
            }
            catch(FormatException error)
            {
                Console.WriteLine($"Unable to parse player id value: '{error}'");
                return null;
            }
        }
    }
}
