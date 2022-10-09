using EshopApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EshopApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult post([FromBody] Login login)
        {
            IdentityModelEventSource.ShowPII = true;
            if (!ModelState.IsValid)
            {
                return BadRequest("the username password are wrong");
            }
            if (login.UserName.ToLower() != "ariyan" || login.Password != "1234")
            {
                return Unauthorized();
            }
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AriyanProjectKeyValue"));

            var signingcredential = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var tokenoption = new JwtSecurityToken(
                issuer: "http://localhost:34642",
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name,login.UserName),
                    new Claim(ClaimTypes.Role,"Admin")
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingcredential
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenoption);
            return Ok(new { token = token });
        }
    }
}
