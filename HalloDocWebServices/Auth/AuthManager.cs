using HalloDocWebRepository.Data;
using HalloDocWebRepository.Interfaces;
using HalloDocWebServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace HalloDocWebService.Authentication
{
    public class AuthManager
    {
        private readonly string _role;
        public AuthManager(string role = "")
        {
            _role = role;
        }
      
    }
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public CustomAuthorize(string role = "")
        {
            this._role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwt_Service>();

            if (jwtService == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                return;
            }

            var request = context.HttpContext.Request;
            var token = request.Cookies["jwt"];

            if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtToken))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                return;
            }

            Claim roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (roleClaim.Value != _role)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                return;
            }

            //if (string.IsNullOrEmpty(_role) || roleClaim.Value != _role)
            //{
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "AccessDenied" }));
            //    return;
            //}
        }
    }

}