using Api.Areas.Identity.Data;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexController(Context context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //[HttpGet("category/{catId}")]
        //public async Task<IActionResult> GetSubcatId([FromRoute] string catId)
        //{
        //    try
        //    {
        //        var category = _context.SubCategories
        //            .Where(x => x.CategoryId == catId).FirstOrDefault();
        //        return Ok(category);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
        //    }
        //}
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
        //[HttpGet("post/{subId}")]
        //public async Task<IActionResult> GetPostById([FromRoute] string subId)
        //{
        //    try
        //    {
        //        var posts = _context.Posts
        //            .Where(x => x.SubCategoryId == subId).ToList();
        //        return Ok(posts);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
        //    }
        //}

    }
}
