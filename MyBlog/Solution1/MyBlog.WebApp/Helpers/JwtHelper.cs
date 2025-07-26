using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System;

namespace WebApp.Helpers
{
    public static class JwtHelper
    {
        public static string GetClaimFromToken(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));
            return claim?.Value;
        }
    }
} 