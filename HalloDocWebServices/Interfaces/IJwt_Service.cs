using HalloDocWebRepository.Data;
using HalloDocWebRepository.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocWebServices.Interfaces
{
    public interface IJwt_Service
    {
        //public string GenerateToken(Aspnetuser aspuser,IConfiguration config);
        //public bool ValidateToken(string token, out JwtSecurityToken jwttoken);
        public string GetJWTToken(Aspnetuser aspnetuser);
        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
        //public string GenerateToken(Aspnetuser user);
        //Task<Aspnetuser> LoggedUser(login loginViewModel);
    }
}
