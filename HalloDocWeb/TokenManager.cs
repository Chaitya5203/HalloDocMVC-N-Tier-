using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace HalloDocWeb
{
    public class TokenManager
    {
        private readonly IConfiguration _configuration;

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(1), // Set token expiration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //public static string GenerateToken(login aspuser, IConfiguration _config)
        //{
        //    var key = _config["Jwt:Key"];
        //    var Issuer = _config["Jwt:Issuer"];
        //    var Audience = _config["Jwt:Audience"];
        //    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Email,aspuser.Email),

        //    };
        //    var token = new JwtSecurityToken(Issuer,
        //        Audience,
        //        claims,
        //        expires: DateTime.Now.AddMinutes(5),
        //        signingCredentials: credentials);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}