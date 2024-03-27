using HalloDocWebRepository.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace HalloDocWebServices.Interfaces
{
    public class Jwt_Service : IJwt_Service
    {
        private readonly IConfiguration _config;
        public Jwt_Service(IConfiguration config)
        {
            _config = config;
        }
        public string GetJWTToken(Aspnetuser aspnetuser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string rolename;
            if (aspnetuser.Role == "1")
                rolename = "Patient";
            else 
                rolename  = "Admin";
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, aspnetuser.Email),
                new Claim(ClaimTypes.Role, rolename),
            };
            var token = new JwtSecurityToken(
             _config["Jwt:Issuer"],
             _config["Jwt:Audience"],
             claims,
             expires: DateTime.Now.AddDays(1),
             signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)
        {
            jwtSecurityToken = null;
            if (token == null)
            {
                return false;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                jwtSecurityToken = (JwtSecurityToken)validatedToken;
                if (jwtSecurityToken != null)
                    return true;
                return false;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
