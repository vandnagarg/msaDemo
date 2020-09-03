using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<JWTAuthentication> jwtAuthentication;
        readonly ILogger<AuthController> _log;


        public AuthController(IOptions<JWTAuthentication> _authentication, ILogger<AuthController> log)
        {
            jwtAuthentication = _authentication;
            _log = log;
        }

        [HttpGet]
        public List<User> Get()
        {
            try
            {
                _log.LogInformation("Getting the list of all the users.");
                return UserDbContext.userList;
            }
            catch(Exception ex)
            {
                _log.LogInformation("Error in getting the users");
                throw ex;
            }
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] User user)
        {
            try
            {
                var users = UserDbContext.userList;
                if (users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault() != null)
                {
                    _log.LogInformation("Logged in user : " + user.UserName);

                    var currentUser = users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
                    var symmetricKey = jwtAuthentication.Value.SymmetricSecurityKey;

                    //signing credentials 
                    var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);

                    //create token
                    var token = new JwtSecurityToken(
                        issuer: jwtAuthentication.Value.ValidIssuer,
                        audience: jwtAuthentication.Value.ValidAudience,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signingCredentials,
                        claims: new[]
                        {
                    new Claim("userId",Guid.NewGuid().ToString()),
                    new Claim("userName",currentUser.UserName),
                    new Claim(ClaimTypes.Role,currentUser.Role)
                        }
                        );


                    //return token
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    _log.LogInformation("Unable to login the user, as wrong usrename or password: ");
                    return BadRequest("Wrong username or password");
                }
            }
            catch(Exception ex)
            {
                _log.LogInformation("Unable to login the user, as wrong usrename or password: ");
                throw ex;
            }
        }
    }
}