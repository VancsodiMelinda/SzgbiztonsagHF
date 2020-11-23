using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    public class DeleteModel : PageModel
    {
        private readonly IStoreLogic _logic;

        public DeleteModel(IStoreLogic logic)
        {
            _logic = logic;
        }

        [BindProperty]
        public CaffMetadata CaffMetadata { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CaffMetadata = await _logic.GetMetadataWithCommentsAsync(id);

            if (CaffMetadata == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CaffMetadata = await _logic.GetMetadataWithCommentsAsync(id);

            if (CaffMetadata != null)
            {
                await _logic.DeleteFileAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
