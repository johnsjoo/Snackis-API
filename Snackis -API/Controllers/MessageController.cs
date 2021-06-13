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
    public class MessageController : Controller
    {
        private Context _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public MessageController(Context context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet("messages")]
        public async Task<ActionResult> GetAllPostReplay()
        {

           
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            var users = _context.Users.ToList();
            var myMessages = _context.messages
                .Where(x => x.UserId == user.Id).ToList();
            var myReplies = _context.messages
                .Where(x => x.MessageReceiver == user.Id).ToList();

            var conversation = myMessages.Concat(myReplies).ToList();

            try
            {
                return Ok(conversation);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
            }
        }
       

        [HttpPost("send")]
        public async Task<ActionResult> SendMessage( [FromBody] MessageModel messageModel)
        {
            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null)
            {

                Message message = new Message
                {
                    UserId = messageModel.UserId,
                    message = messageModel.Message,
                    MessageReceiver = messageModel.MessageReceiver,
                    Date = DateTime.Now
                };

                try
                {
                    _context.messages.Add(message);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
                }
                return Ok(message);
            }
            return Unauthorized();


        }
        [HttpGet("GetUserIdsMessage")]
        public async Task<ActionResult> Test()
        {
            List<User> listOfUsers = new List<User>();

            User user = await _userManager.FindByNameAsync(User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value);
            var roles = await _userManager.GetRolesAsync(user);

            var users = _context.Users.ToList();
            var messages = _context.messages.ToList();

            var result = messages.Where(x => x.MessageReceiver == user.Id).ToList();
            var result1 = messages.Where(x => x.UserId == user.Id).ToList();

            var q1 = result.Select(x => x.User.Id).ToList();
            var q2 = result1.Select(x => x.MessageReceiver).ToList();
            var q3 = q1.Concat(q2).ToList();

            try
            {
                foreach (var item in q3)
                {
                    foreach (var userids in users)
                    {
                        if (item == userids.Id)
                        {
                            listOfUsers.Add(userids);
                        }
                    }

                }

                return Ok(listOfUsers.Distinct()) ;
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Sorry, something happend. {ex.ToString()}" });
            }
        }
    }
}
