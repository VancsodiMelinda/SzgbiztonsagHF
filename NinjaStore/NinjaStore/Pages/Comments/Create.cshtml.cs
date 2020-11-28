using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly IStoreLogic _logic;
        private readonly UserManager<User> _userManager;

        //LOG CONSOLE
        //private readonly ILogger<DeleteModel> _logger;

        //LOG FILE
        readonly ILogger<CreateModel> _log;

        public CreateModel(IStoreLogic logic, UserManager<User> userManager, ILogger<CreateModel> logger, ILogger<CreateModel> log)
        {
            _logic = logic;
            _userManager = userManager;
            _log = log;
            // _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync(string fileId, string commentText)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string Message = $"POST Comment added to file {DateTime.UtcNow.ToLongTimeString()}";
            _log.LogInformation(Message);
          //  _logger.LogInformation(Message);
            string Message2 = $"POST File ID is {fileId}";
            _log.LogInformation(Message2);
            // _logger.LogInformation(Message2);

            
            // TODO Csilla:  SQL Exception
            /*var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // TODO Gergő log
            }
            await _logic.InsertCommentAsync(fileId, user.UserName, commentText);*/
            await _logic.InsertCommentAsync(fileId, "Csilla", commentText);
            return RedirectToPage("../Files/Details", new {id = fileId });
        }
    }
}