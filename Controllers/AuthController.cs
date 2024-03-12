using ManageFinances.Helpers;
using ManageFinances.Entities;
using ManageFinances.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Authorization;

namespace ManageFinances.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly JwtSecurityTokenHandlerWrapper _jwtSecurityTokenHandler;

        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository repository, JwtSecurityTokenHandlerWrapper jwtSecurityTokenHandler)
        {
            this.userRepository = repository;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }


        [HttpPost("SignUp")]
        public dynamic SignUp([FromForm] User user)
        {
            User result = userRepository.Create(user);

            string token = _jwtSecurityTokenHandler.GenerateJwtToken(result.Id.ToString(), result.Username, result.Email, "User");

            return new
            {
                Token = token,
            };
        }


        [HttpPost("SignIn")]
        public dynamic SignIn(
            [FromForm, Required(ErrorMessage = "Username or email is required")] string usernameOrEmail,
            [FromForm, Required(ErrorMessage = "Password is required")] string password
            )
        {
            User result = userRepository.GetByUsernameOrEmail(usernameOrEmail);

            if (result == null)
            {
                return BadRequest(new { title = "User or password incorrect." });
            }

            if (Verify(password, result.Password))
            {
                string token = _jwtSecurityTokenHandler.GenerateJwtToken(result.Id.ToString(), result.Username, result.Email, "User");

                return new
                {
                    Token = token,
                };
            }

            return BadRequest(new { title = "User or password incorrect." });
        }


        [HttpGet("SetToken")]
        public dynamic SetToken([FromQuery] string? access_token)
        {
            if (access_token != null)
            {
                Response.Cookies.Append("access_token", $"{Request.Query["access_token"]}");
                return RedirectToAction("Index", "Home", new { Area = "User" });
            }
            else
            {
                Response.Cookies.Delete("access_token");
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
