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
    public class CreateModel : PageModel
    {
        private readonly IStoreLogic _logic;

        //LOG CONSOLE
        //private readonly ILogger<DeleteModel> _logger;

        //LOG FILE
        readonly ILogger<CreateModel> _log;

        public CreateModel(IStoreLogic logic, ILogger<CreateModel> logger, ILogger<CreateModel> log)
        {
            _logic = logic;
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
            await _logic.InsertCommentAsync(fileId, User.Identity.Name, commentText);
            return RedirectToPage("../Files/Details", new {id = fileId });
        }
    }
}