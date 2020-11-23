using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    public class DownloadModel : PageModel
    {
        private readonly IStoreLogic _logic;

        public DownloadModel(IStoreLogic logic)
        {
            _logic = logic;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            CaffFile file = await _logic.DownloadFileAsync(id);

            if (file != null)
            {
                return File(file.Data, "application/octet-stream", file.Metadata.FileName + ".caff");
            }
            return NotFound();
        }
    }
}