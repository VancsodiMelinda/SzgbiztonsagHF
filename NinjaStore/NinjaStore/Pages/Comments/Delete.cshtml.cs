using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IStoreLogic _logic;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IStoreLogic logic, ILogger<DeleteModel> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                string Message = $"POST ERROR: Comment ID not found {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return NotFound();
            } else
            {
                Comment comment = await _logic.GetCommentAsync((int)id);
                if (comment == null)
                {
                    string Message2 = $"POST ERROR: Comment value is null {DateTime.UtcNow.ToLongTimeString()}";
                    _logger.LogInformation(Message2);
                    return NotFound();
                }
                await _logic.DeleteCommentAsync((int)id);
                string Message3 = $"POST Comment is deleted {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message3);
                return RedirectToPage("../Files/Details", new { id = comment.CaffMetadataFileId });
            }
        }
    }
}