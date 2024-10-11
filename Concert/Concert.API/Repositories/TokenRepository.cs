using Concert.API.Data;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.Utility;
using DotEnv.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Concert.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public string CreateJWTToken(IdentityUser identityUser, List<string> roles)
        {
            // Create claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Read environment variables.
            new EnvLoader().Load();
            var envVarReader = new EnvReader();
            string jwtSecretKey = string.Empty;
            string jwtIssuer = string.Empty;
            string jwtAudience = string.Empty;

            // Environment variables management.
            string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (envName == Environments.Development)
            {
                jwtSecretKey = envVarReader["Jwt_SecretKey"];
                jwtIssuer = envVarReader["Jwt_Issuer"];
                jwtAudience = envVarReader["Jwt_Audience"];
            }
            else if (envName == Environments.Production)
            {
                jwtSecretKey = Environment.GetEnvironmentVariable("Jwt_SecretKey");
                jwtIssuer = Environment.GetEnvironmentVariable("Jwt_Issuer");
                jwtAudience = Environment.GetEnvironmentVariable("Jwt_Audience");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(SD.JWT_TOKEN_EXPIRATION_MINUTES),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
