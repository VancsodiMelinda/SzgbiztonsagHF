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
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IStoreLogic logic, ILogger<CreateModel> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // HTTP 405 Method Not Allowed
            return StatusCode(405);
        }

        public async Task<IActionResult> OnPostAsync(string fileId, string commentText)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _logic.InsertCommentAsync(fileId, User.Identity.Name, commentText);
            string Message = $"POST Comment added to file {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);

            return RedirectToPage("../Files/Details", new {id = fileId });
        }
    }
}