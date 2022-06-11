using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Books.Api.Models;

namespace Books.Api.Services
{
    public interface IJwtService
    {
        public JwtSecurityToken Generate(BooksUser userId, IList<Claim> userClaims, IList<string> userRoles);

        public JwtSecurityToken Verify(string jwt);

        public string WriteToken(JwtSecurityToken jwt);
    }
}
