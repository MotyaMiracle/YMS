using Domain.Entity;
using Domain.Services.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.Identity
{
    public class Authorization
    {
        public static ClaimsIdentity GetIdentity(LoginDto loginDto, User user)
        {
            if (user == null)
                return null;

            var role = user.Role.Name;
            if (role == null)
                return null;

            var passwordHash = GetHash(loginDto.Password);
            if (passwordHash != user.PasswordHash)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                new Claim("Id", user.Id.ToString())
            };

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
        public static string GenerateJwtToken(IEnumerable<Claim> claims, TimeSpan lifetime)
        {
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: SigningOptions.SignIssuer,
                audience: SigningOptions.SignAudience,
                notBefore: now,
                claims: claims,
                expires: now + lifetime,
                signingCredentials: new SigningCredentials(SigningOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        public static string GetHash(string password)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
