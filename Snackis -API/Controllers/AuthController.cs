using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Models;
using Api.Areas.Identity.Data;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {

        private Context _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(Context context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {

            User user = model.User.Contains("@") ? await _userManager.FindByEmailAsync(model.User) : await _userManager.FindByNameAsync(model.User);

            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (signInResult.Succeeded)
                {

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("default-key-xxxx-aaaa-qqqq-default-key-xxxx-aaaa-qqqq");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {


                        // Add your claims to the JWT token
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email)


                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString, UserID = user.Id });
                }
                else
                {
                    return Ok("No user or password matched, try again.");
                }
            }
            else
            {
                return Ok("No such user exists");
            }
        
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterModel model)
        {
            User user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.Phone,
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,

            };
            if (user.UserName.Contains(' '))
            {
                return BadRequest("wrong username");
            }
            var userExists = await _userManager.FindByNameAsync(model.Username);
            var mailExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest("There is alrerady an account with this username");
            }
            if (mailExists != null)
            {
                return BadRequest("There is alrerady an account with this email");
            }


            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                User newUser = await _userManager.FindByNameAsync(user.UserName);

                await _userManager.AddToRoleAsync(newUser, "User");

                UserSettings settings = new UserSettings()
                {
                    Id = Guid.NewGuid().ToString(),
                    DarkMode = true,
                    User = newUser
                };

                UserGDPR gdpr = new UserGDPR()
                {
                    Id = Guid.NewGuid().ToString(),
                    UseMyData = false,
                    User = newUser
                };

                _context.UserSettings.Add(settings);
                _context.UserGDPR.Add(gdpr);
                _context.SaveChanges();


            }
            return Ok(user.Id);
        }


        [AllowAnonymous]
        [HttpPost("changepassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {

            if (model.NewPassword == model.ConfirmNewPassword)
            {
                User user = ToolBox.IsValidEmail(model.User) ? await _userManager.FindByEmailAsync(model.User) : await _userManager.FindByNameAsync(model.User);

                if (user is not null)
                {
                    if (await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
                    {
                        await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                        return Ok(new { message = "Password has been updated." });
                    }
                    else
                    {
                        return Ok(new { message = $"Your password is incorrect. ({user.AccessFailedCount}) failed attempts." });
                    }
                }
                else
                {
                    return Ok(new { message = "No such user found." });
                }
            }
            else
            {
                return Ok(new { message = "Your password does not match." });
            }
        
        }

    }
}
