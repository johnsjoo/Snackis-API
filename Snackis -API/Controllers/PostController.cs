using Api.Areas.Identity.Data;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
  

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : Controller
    {
        private Context _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public PostController(Context context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [HttpPost("create")]

        public async Task<ActionResult> Create([FromBody] PostCreateModel model)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (user != null)
            {
                Post post = new Post();
                post.Title = model.Title;
                post.Content = model.Content;
                post.Date = DateTime.Now;

                try
                {
                    _context.Posts.Add(post);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
                }
                return Ok();
            }
            return Unauthorized();
        }


    }
}
