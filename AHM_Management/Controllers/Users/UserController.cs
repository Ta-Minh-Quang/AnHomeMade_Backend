using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal_API.Controllers.User
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {

        private IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [Route("do-login")]
        [HttpGet]
        public ActionResult<UserInfo> Login(string username, string password)
        {
            
            UserInfo user = AuthenticateUser(username, password);
            if(user != null)
            {
                user.Token = GenerateJSONWebToken(user);
                return user;
            }

            return new UserInfo();
        }

        private string GenerateJSONWebToken(UserInfo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.User_Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserInfo AuthenticateUser(string username, string password)
        {
            UserInfo user = new UserDA().AuthenUser(username, password);
            if(user != null && user.User_Name != "")
            {
                return user;
            }   
            return null;
        }
    }
}
