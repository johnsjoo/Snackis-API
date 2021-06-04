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
        [HttpGet("allReplies")]
        public async Task<ActionResult> GetAllPostReplay()
        {
            var posts = _context.Posts.ToList();
            var postdisc = _context.PostDiscussions.ToList();

            try
            {
                
                return Ok(postdisc);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
            }
        }


        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var posts = _context.Posts.ToList();
            var getUsers = _context.Users.ToList();
            var cat = _context.Categories.ToList();


            try
            {
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
            if (user != null)
            {

                Post post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    Date = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = model.CategoryId

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


                try
                {
                    Post post = _context.Posts.Where(x => x.Id == postId).FirstOrDefault();
                    post.IsReported = true;

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

        [HttpPost("reply")]
        public async Task<ActionResult> ReplyPost([FromBody] PostReplyModel model)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null)
            {
                PostDiscussion postDiscussion = new PostDiscussion
                {
                    Discussion = model.Discussion,
                    Date = DateTime.Now,
                    UserId = user.Id,
                    PostId = model.PostId


                };
                try
                {
                    _context.PostDiscussions.Add(postDiscussion);
                    await _context.SaveChangesAsync();
                    return Ok(postDiscussion);
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
        [AllowAnonymous]
        [HttpGet("discussionById/{postId}")]
        public async Task<ActionResult> GetDiscussionById([FromRoute]string postId) 
        {
            
            var discusson = _context.PostDiscussions
                .Where(x => x.PostId == postId).ToList();
            return Ok(discusson);
        }

    }
}
