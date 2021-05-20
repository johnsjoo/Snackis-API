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



        [HttpPost("CreateCategoty")]

        public async Task<ActionResult> CreateCategoty([FromBody] PostCategoryModel model)
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


        [HttpPost("CreateSubCategory")]

        public async Task<ActionResult> CreateSubCategory([FromBody] PostSubCategoryModel model)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("root") || roles.Contains("admin"))
            {
               
              // Under uppbyggnad
                SubCategory subCat = new SubCategory();           
                var apa2 = _context.Categories
                .Where(x => x.Id == subCat.CategoryId);
                subCat.Title = model.Title;
                subCat.Description = model.Description;
                var findRightCat = _context.Categories
                        .Where(x => x.Title == model.Category);
                
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
    }
}
