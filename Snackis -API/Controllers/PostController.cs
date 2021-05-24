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

        [AllowAnonymous]
        [HttpGet("all")]
        // Not returning with user information
        public async Task<ActionResult> GetAll()
        {
            try
            {
                List<Post> posts = _context.Posts.ToList();
                return Ok(posts);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
            }
        }



        [HttpPost("create")]

        public async Task<ActionResult> Create([FromBody] PostCreateModel model)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);
            List<string> subCategorysIds = new List<string>();

            if (user != null)
            {

                Post post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    Date = DateTime.Now,
                    UserId = user.Id,
                    SubCategoryId = model.SubCategoryId
                    
                };

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

        [HttpPut("report/{postId}")]

        public async Task<ActionResult> ReportPost([FromRoute] string postId)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null)
            {
                Post post = _context.Posts.Where(x => x.Id == postId).FirstOrDefault();

                post.IsReported = true;
                
                try
                {
                    await _context.SaveChangesAsync();
                    return Ok();

                }
                catch (Exception ex)
                {

                    return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
                }
            }
            else
            {
                return Unauthorized();
            }


        }
        

    }
}
