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
        public async Task<ActionResult> GetAll()
        {            
            var posts = _context.Posts.ToList();
            var getUsers = _context.Users.ToList();
            var cat = _context.Categories.ToList();
            //var postdisc = _context.PostDiscussions.ToList(); 


            try
            {
                return Ok(posts);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
            }
        }


        [HttpPost("CreateDiscussion")]
        public async Task<ActionResult> CreateDiscussion([FromBody] PostReplyModel model) 
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);
            if (user!= null)
            {
                var p = _context.Posts.ToList();
                
                PostDiscussion discussion = new PostDiscussion
                {
                    Date = model.date,
                    Discussion = model.Discussion,
                    PostId = model.PostId,
                    UserId = user.Id
                };

                

                try
                {
                    _context.PostDiscussions.Add(discussion);
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
        // Funkar inte just nu, måste fixa med postId
        //[HttpPost("reply")]
        //public async Task<ActionResult> ReplyPost([FromBody] PostReplyModel model)
        //{
        //    User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
        //    var roles = await _userManager.GetRolesAsync(user);
        //    var getPosts = _context.Posts.ToList();
        //    if (user != null)
        //    {
        //        PostDiscussion postDiscussion = new PostDiscussion
        //        {
        //            Discussion = model.Discussion,
        //            Date = DateTime.Now,
        //            UserId = user.Id,

        //        };
        //        foreach (var item in getPosts)
        //        {
        //            postDiscussion.Id = item.Id;
        //        }
        //        try 
        //        {
        //            _context.PostDiscussions.Add(postDiscussion);
        //            await _context.SaveChangesAsync();
        //            return Ok();
        //        }
        //        catch (Exception ex)
        //        {

        //            return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
        //        }
        //    }

        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}


    }
}
