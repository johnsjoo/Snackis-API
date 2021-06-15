using Api.Areas.Identity.Data;
using Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IndexController : Controller
    {
        private Context _context;
        public IndexController(Context context)
        {
            _context = context;
        }
        [HttpGet("{catId}")]
        public async Task<IActionResult> GetPostById([FromRoute] string catId)
        {
            var c = _context.Categories.ToList();
            var q = _context.Users.ToList();
            try
            {
                var posts = _context.Posts
                    .Where(x => x.CategoryId == catId).ToList();

                foreach (var item in posts)
                {
                    item.User = q.Where(x=>x.Id == item.UserId).FirstOrDefault();
                }
                 
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
            }
        }
        [HttpGet("post/{postId}")]
        public async Task<ActionResult> getReply([FromRoute] string postID)
        {
            try
            {
                var user = _context.Users.ToList();
                var discussion = _context.PostDiscussions.ToList();
                var reply = _context.Posts
                   .Where(x => x.Id == postID).FirstOrDefault();
                return Ok(reply);

            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" }); 
            }
        }
       

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var allCategories = _context.Categories.ToList();
                return Ok(allCategories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" }); 
            }
        }

        [HttpGet("CategoryById/{CatId}")]
        public async Task<IActionResult> GetAllCategories([FromRoute]string CatId)
        {
            try
            {
                Category category = _context.Categories
                    .Where(x => x.Id == CatId).FirstOrDefault();
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
            }
        }

        [HttpGet("/all")]
        public async Task<ActionResult> GetAllPosts()
        {
            try
            {
                var allPosts = _context.Posts.ToList();
                return Ok(allPosts);

            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" }); 
            }
        }
     
    }
}
