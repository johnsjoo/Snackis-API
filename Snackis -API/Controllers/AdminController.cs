using Api.Areas.Identity.Data;
using Api.Data;
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
    public class AdminController : Controller
    {
        private Context _context;
        private readonly UserManager<User> _userManager;

        public AdminController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("getAllCategories")]
        public async Task<ActionResult> GetAllCategories()
        {
            
            try
            {
                List<Category> categories =  _context.Categories.ToList();
                return  Ok(categories);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
                
            }

        }




        [HttpPost("createCategory")]

        public async Task<ActionResult> CreateCategory([FromBody] PostCategoryModel model)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("root") || roles.Contains("admin"))
            {
                Category cat = new Category();

                cat.Title = model.Title;
                cat.Description = model.Description;

                try
                {
                    var users = _context.Users.ToList();
                    _context.Categories.Add(cat);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
                }

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

 


        [HttpGet("reportedPosts")]
        public async Task<ActionResult> GetReportedPosts()
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("admin") || roles.Contains("root"))
            {
                var users = _context.Users.ToList();
                var reportedPosts = _context.Posts
                .Where(x => x.IsReported == true).ToList();

                try
                {
                    return Ok(reportedPosts);
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

        [HttpGet("reportedDiscussions")]
        public async Task<ActionResult> GetReportedDiscussions()
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("admin") || roles.Contains("root"))
            {
                var users = _context.Users.ToList();
                var reportedDiscussions = _context.PostDiscussions
                .Where(x => x.IsReported == true).ToList();

                try
                {
                    return Ok(reportedDiscussions);
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



        [HttpPut("reviewReportedPost/{PostId}")]
        public async Task<ActionResult> ReviewReportedPosts([FromRoute] string PostId)
            {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("admin") || roles.Contains("root"))
            {
                try
                {
                    Post post = _context.Posts.Where(x => x.Id == PostId).FirstOrDefault();
                    if (post.IsReported == true)
                    {
                        post.IsReported = false;
                    }
                    else
                    {
                        post.IsReported = true;
                    }

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

        [HttpPut("reviewReportedDiscussion/{DiscussionId}")]
        public async Task<ActionResult> ReviewReportedDiscussion([FromRoute] string DiscussionId)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("admin") || roles.Contains("root"))
            {
                try
                {
                    PostDiscussion discussion = _context.PostDiscussions.Where(x => x.Id == DiscussionId).FirstOrDefault();
                    if (discussion.IsReported == true)
                    {
                        discussion.IsReported = false;
                    }
                    else
                    {
                        discussion.IsReported = true;
                    }

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


        [HttpDelete("DeletePostById/{PostId}")]
        public async Task<ActionResult> DeletePostById([FromRoute] string PostId)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("admin") || roles.Contains("root"))
            {
                try
                {
                    var postDiscussions = _context.PostDiscussions
                        .Where(x => x.PostId == PostId).ToList();
                    Post post = _context.Posts.Where(x => x.Id == PostId).FirstOrDefault();
                    if (post != null)
                    {

                        foreach (var item in postDiscussions)
                        {
                            _context.PostDiscussions.Remove(item);
                        }
                        _context.Posts.Remove(post);

                        

                        await _context.SaveChangesAsync();
                        
                    }

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
        [HttpDelete("DeleteDiscussionById/{DiscussionId}")]
        public async Task<ActionResult> DeleteDiscussionById([FromRoute] string DiscussionId)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("admin") || roles.Contains("root"))
            {
                try
                {
                    PostDiscussion postDiscussion = _context.PostDiscussions
                        .Where(x => x.Id == DiscussionId).FirstOrDefault();
                    
                    if (postDiscussion != null)
                    {

                        _context.PostDiscussions.Remove(postDiscussion);

                        await _context.SaveChangesAsync();

                    }

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
