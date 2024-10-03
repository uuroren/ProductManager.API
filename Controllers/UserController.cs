using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductManager.API.Dtos;
using ProductManager.API.Models;
using ProductManager.API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProductManager.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase {
        private readonly UserManager<User> _userManager;
        private readonly JwtService _jwtService;
        public UserController(UserManager<User> userManager,JwtService jwtService) {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] UserRegisterModelDto userRegister) {
            var userExists = await _userManager.FindByNameAsync(userRegister.Username);

            if(userExists != null) {
                return BadRequest(new { Message = "User already exists",});
            }

            User user = new User() {
                UserName = userRegister.Username,
            };

            var result = await _userManager.CreateAsync(user,userRegister.Password);
            if(!result.Succeeded) {
                return BadRequest(new { Message = "User creation failed!",Errors = result.Errors });
            }

            return Ok(new { Message = "User created!" });
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] UserLoginModelDto userModel) {
            var user = await _userManager.FindByNameAsync(userModel.Username);

            if(user != null && await _userManager.CheckPasswordAsync(user,userModel.Password)) {
                var token = _jwtService.GenerateToken(user);

                return Ok(new {
                    User = user,
                    Token = token,
                });

            }

            return Unauthorized();
        }

    }
}
