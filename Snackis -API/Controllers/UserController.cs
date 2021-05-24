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
    // Give it the [Authorize] attribute. It will bypass the autentication without it.  
    [Authorize]
    public class UserController : ControllerBase
    {

        private Context _context;
        private readonly UserManager<User> _userManager;

        public UserController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("profile")]
        public async Task<ActionResult> GetProfile()
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

            if (user != null)
            {
                return Ok(new { result = _context.Users.Where(x => x.Id == user.Id).FirstOrDefault() });
            }
            else
            {
                return StatusCode(404, new { message = "User does not exist" });
            }
        }

        [HttpGet("profile/{id}")]
        public async Task<ActionResult> GetProfile(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(404, new { message = "User does not exist" });
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
            if (userExists!=null)
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

        [HttpGet("toggleGDPR")]
        public async Task<ActionResult> ToggleGDPR()
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);

            if (user != null)
            {
                UserGDPR gdpr = _context.UserGDPR.Where(x => x.User == user).FirstOrDefault();

                gdpr.UseMyData = !gdpr.UseMyData;

                _context.SaveChanges();

                return Ok(gdpr.UseMyData);

                //return Ok(new { message = $"GDPR has been toggled to {user.GDPR.UseMyData}" });
            }
            else
            {
                return StatusCode(404, new { message = "User does not exist" });
            }
        }
    }
}
