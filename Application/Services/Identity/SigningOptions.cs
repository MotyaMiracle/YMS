using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.Services.Identity
{
    public static class SigningOptions
    {
        public static string SignIssuer = "MyAuthServer";
        public static string SignAudience = "YardSystem";
        static string SignKey = "supersecret_yard_key!321";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SignKey));
    }
}