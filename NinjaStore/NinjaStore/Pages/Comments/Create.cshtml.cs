using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.BLL;

namespace NinjaStore.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly IStoreLogic _logic;

        public CreateModel(IStoreLogic logic)
        {
            _logic = logic;
        }
        public async Task<IActionResult> OnPostAsync(string fileId, string commentText)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO Csilla: username
            await _logic.InsertCommentAsync(fileId, "Csilla", commentText);
            return RedirectToPage("../Files/Details", new {id = fileId });
        }
    }
}