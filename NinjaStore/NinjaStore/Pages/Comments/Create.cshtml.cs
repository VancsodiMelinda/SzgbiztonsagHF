using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.BLL;

namespace NinjaStore.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly IStoreLogic _logic;

        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IStoreLogic logic, ILogger<CreateModel> logger)
        {
            _logic = logic;
            _logger = logger;
        }
        public async Task<IActionResult> OnPostAsync(string fileId, string commentText)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO Csilla: username
            string Message = $"POST Comment added to file {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            string Message2 = $"POST File ID is {fileId}";
            _logger.LogInformation(Message2);
            await _logic.InsertCommentAsync(fileId, "Csilla", commentText);
            return RedirectToPage("../Files/Details", new {id = fileId });
        }
    }
}