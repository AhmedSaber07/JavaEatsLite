using JaveatsLiteApi.DTO;
using JaveatsLiteApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<User> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        [HttpPost("Register")]//api//account/register
        public async Task<IActionResult> Register(RegisterUserDto registerUser)
        {
            if(ModelState.IsValid)
            {
                User user = new User();
                user.UserName = registerUser.UserName;
                user.Email = registerUser.Email;
                user.FirstName = registerUser.FirstName;
                user.LastName = registerUser.LastName;
                user.Created_at = DateTime.UtcNow;
                var result =await _userManager.CreateAsync(user, registerUser.Password);
                if(!result.Succeeded)
                {
                    return BadRequest(result.Errors.FirstOrDefault().ToString());
                }
                await _userManager.AddToRoleAsync(user, "User");
                return Ok(registerUser);
            }
            return BadRequest(ModelState);
        }
        
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Email);
                if (user is null)
                    return Unauthorized();
                var checkPassword = await _userManager.CheckPasswordAsync(user, userLogin.Password);
                if (!checkPassword)
                    return Unauthorized();
                //create claims 
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                //signinCredintial
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                //add roles
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                //create token
                JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer:_configuration["JWT:Issuer"], // (provider) web api provide service
                    audience: _configuration["JWT:Audiance"], // (consumer) angular use service
                    claims:claims,
                    expires:DateTime.Now.AddHours(1), //experation date
                    signingCredentials:credentials //signature
                    );

                return Ok(new
                    {
                        email = userLogin.Email,
                        token =new JwtSecurityTokenHandler().WriteToken(jwt),
                        expiration = jwt.ValidTo
                    });
            }
            return Unauthorized(ModelState);
        }
       [HttpPost("ChangePassword")]
       [Authorize]
       public async Task<IActionResult> ChangePassword( ChangePasswordDto changePassword)
        {
            if(ModelState.IsValid)
            {
               var currentUser = await _userManager.GetUserAsync(HttpContext.User);
               // var currentUser = await _userManager.FindByIdAsync(id);
               var result =  await _userManager.ChangePasswordAsync(currentUser, changePassword.oldPassword, changePassword.newPassword);
                if (!result.Succeeded)
                    return BadRequest(result.Errors.FirstOrDefault().ToString());
                return Ok(new
                {
                    status= "Password Changed Successfully"
                });
            }
            return BadRequest(ModelState);
        }
    }
}
