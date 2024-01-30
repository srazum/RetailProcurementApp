
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RetailProcurement.WebAPI.Auth.Interfaces;
using RetailProcurement.WebAPI.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetailProcurement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthDetailsProvider _authDetailsProvider;
        public LoginController(IAuthDetailsProvider authDetailsProvider) {
            _authDetailsProvider = authDetailsProvider;
        }

        [HttpPost]
        public IActionResult Login(UserDto userDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(userDTO.UserName) ||
                string.IsNullOrEmpty(userDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                if (userDTO.UserName.Equals(_authDetailsProvider.GetUserName()) &&
                userDTO.Password.Equals(_authDetailsProvider.GetPassword()))
                {
                    var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(_authDetailsProvider.GetJwtKey()));
                    var signinCredentials = new SigningCredentials
                    (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: _authDetailsProvider.GetJwtIssuer(),
                        audience: _authDetailsProvider.GetJwtAudience(),
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }
            }
            catch (Exception ex)
            {
                return BadRequest
                ($"An error occurred in generating the token. {ex.Message}");
            }
            return Unauthorized();
        }
    }
}
