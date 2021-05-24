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
        [HttpGet("GetAllCategories")]
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




        [HttpPost("CreateCategory")]

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

        [AllowAnonymous]
        [HttpGet("getAllSubCategories")]

        public async Task<ActionResult> GetAllSubCategories()
        {
            try
            {
                List<SubCategory> SubCategories = _context.SubCategories.ToList();
                return Ok(SubCategories);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
            }

        }

        [HttpPost("CreateSubCategory")]

        public async Task<ActionResult> CreateSubCategory([FromBody] PostSubCategoryModel model)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("root") || roles.Contains("admin"))
            {

                var q = _context.Categories
                    .Where(x => x.Id == x.Id);
                SubCategory subCat = new SubCategory
                {
                    Title = model.Title,
                    Description = model.Description,
                    CategoryId = model.CategoryId

                };
              

                try
                {
                    _context.SubCategories.Add(subCat);
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

        [HttpGet("reportAll")]
        public async Task<ActionResult> GetReportedPosts()
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("admin") || roles.Contains("root"))
            {
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
    }
}
