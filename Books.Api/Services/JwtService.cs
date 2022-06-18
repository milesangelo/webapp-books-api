using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Books.Api.Models;
using Books.Api.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Books.Api.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwt;
        private readonly UserManager<BooksUser> _userManager;
        private readonly string _secureKey = "98CF847E-3CE4-44FB-A7F3-A327D9B78B5C";

        /// <summary>
        /// </summary>
        /// <param name="jwt"></param>
        public JwtService(IOptions<JwtOptions> jwt,
            UserManager<BooksUser> userManager)
        {
            _jwt = jwt.Value;
            _userManager = userManager;
        }

        /// <summary>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userClaims"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public JwtSecurityToken Generate(BooksUser user, IList<Claim> userClaims, IList<string> userRoles)
        {
            var roleClaims = userRoles.Select(role => new Claim("roles", role)).ToList();

            var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                _jwt.Issuer,
                _jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public string WriteToken(JwtSecurityToken jwt)
        {
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
        
        public async Task<BooksUser?> GetUserFromJwtAsync(string jwt)
        {
            var jwtSecurityToken = Verify(jwt);
            BooksUser? user = null;
            
            var userId = jwtSecurityToken.Claims.First(c => c.Type == "uid").Value;
            user = await _userManager.FindByIdAsync(userId);

            return user ?? null;
        }
    }
}
