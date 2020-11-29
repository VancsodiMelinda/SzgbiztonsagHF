using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Comments
{
    [Authorize(Roles = Roles.ADMIN + "," + Roles.USER)]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IStoreLogic _logic;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(UserManager<User> userManager, IStoreLogic logic, ILogger<DeleteModel> logger)
        {
            _userManager = userManager;
            _logic = logic;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // HTTP 405 Method Not Allowed
            string Message = $"GET HTTP 405 Method Not Allowed {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            return StatusCode(405);
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                string Message = $"POST ERROR: Comment ID {id} not found {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return NotFound();
            }

            Comment comment = await _logic.GetCommentAsync((int)id);

            if (comment == null)
            {
                string Message2 = $"POST ERROR: Comment with id {id} has value of null {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message2);
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(User);
            
            if (user == null)
			{
                string Message = $"POST User {user.UserName} is not authorized at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return Unauthorized();
			}

            bool isOwnComment = user.Id == comment.User.Id;
            bool isAdmin = await _userManager.IsInRoleAsync(user, Roles.ADMIN);
            if (!isOwnComment && !isAdmin)
			{
                string Message = $"POST User {user.UserName} is not authorized at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return Unauthorized();
            }

            await _logic.DeleteCommentAsync(comment.Id);
            string Message3 = $"POST Comment {comment.Id} is deleted {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message3);

            return RedirectToPage("../Files/Details", new { id = comment.CaffMetadataFileId });
        }
    }
}