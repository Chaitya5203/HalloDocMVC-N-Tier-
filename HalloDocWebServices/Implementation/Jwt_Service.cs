using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace HalloDocWebServices.Interfaces
{
    public class Jwt_Service : IJwt_Service
    {
        private readonly IConfiguration _config;
        public Jwt_Service(IConfiguration config)
        {
            _config = config;
        }
        public string GetJWTToken(Aspnetuser aspnetuser)        {            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);            var claims = new[]            {                new Claim(ClaimTypes.Email, aspnetuser.Email),                new Claim(ClaimTypes.Role , "Patient")                //new Claim(ClaimTypes.Role, aspnetuser.Aspnetuserrole.Role.Name),            };            var token = new JwtSecurityToken(             _config["Jwt:Issuer"],             _config["Jwt:Audience"],             claims,             expires: DateTime.Now.AddMinutes(20),             signingCredentials: credentials             );            return new JwtSecurityTokenHandler().WriteToken(token);        }        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)        {            jwtSecurityToken = null;            if (token == null)            {                return false;            }            var tokenHandler = new JwtSecurityTokenHandler();            try            {                tokenHandler.ValidateToken(token, new TokenValidationParameters                {                    ValidateLifetime = true,                    ValidateAudience = true,                    ValidateIssuer = true,                    ValidIssuer = _config["Jwt:Issuer"],                    ValidAudience = _config["Jwt:Audience"],                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))                }, out SecurityToken validatedToken);                jwtSecurityToken = (JwtSecurityToken)validatedToken;                if (jwtSecurityToken != null)                {                    return true;                }                return false;            }            catch            {                return false;            }        }
        //public string GenerateToken(Aspnetuser user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim("UserNameClaim",user.Usarname),
        //    };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var expire = DateTime.UtcNow.AddDays(1);

        //    var token = new JwtSecurityToken(
        //         _config["Jwt:Issuer"],
        //         _config["Jwt:Audience"],
        //         claims,
        //         expires: expire,
        //         signingCredentials: creds
        //    );
        //    Console.WriteLine("hello");
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //public bool ValidateToken(string token, out JwtSecurityToken jwttoken)
        //{
        //    jwttoken = null;
        //    if (token == null)
        //    {
        //        return false;
        //    }
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        //    try
        //    {
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            ClockSkew = TimeSpan.Zero
        //        }, out SecurityToken validatedToken);

        //        jwttoken = (JwtSecurityToken)validatedToken;
        //        if (jwttoken != null)
        //            return true;
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
    //public  class Jwt_Service : IJwt_Service
    //{
    //    private readonly IConfiguration _config;
    //    public Jwt_Service(IConfiguration config)
    //    {
    //        _config = config;
    //    }

    //    public  string GenerateToken(Aspnetuser aspuser,IConfiguration con)
    //    {
    //        var key = con["Jwt:Key"];
    //        var Issuer = con["Jwt:Issuer"];
    //        var Audience = con["Jwt:Audience"];
    //        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
    //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    //        var claims = new[]
    //        {
    //            new Claim(ClaimTypes.Email,aspuser.Email),

    //        };
    //        var token = new JwtSecurityToken(Issuer,
    //            Audience,
    //            claims,
    //            expires: DateTime.Now.AddMinutes(5),
    //            signingCredentials: credentials);
    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }
    //}
}
