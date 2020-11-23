using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Comments
{
    public class DeleteModel : PageModel
    {
        private readonly IStoreLogic _logic;

        public DeleteModel(IStoreLogic logic)
        {
            _logic = logic;
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } else
            {
                Comment comment = await _logic.GetCommentAsync((int)id);
                if (comment == null)
                {
                    return NotFound();
                }
                await _logic.DeleteCommentAsync((int)id);
                return RedirectToPage("../Files/Details", new { id = comment.CaffMetadataFileId });
            }
        }
    }
}