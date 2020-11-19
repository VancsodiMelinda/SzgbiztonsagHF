using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    public class DetailsModel : PageModel
    {
        private readonly IStoreLogic _logic;

        public DetailsModel(IStoreLogic logic)
        {
            _logic = logic;
        }

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

            // TODO Csilla : fix file instead of metadata (ask Dani)
            //await _logic.DownloadFileAsync(id);
            CaffMetadata = await _logic.GetMetadataWithCommentsAsync(id);

            if (CaffMetadata != null)
            {
                return File(CaffMetadata.Preview, "image/bmp", CaffMetadata.FileName + ".bmp");
            }

            return Page();
        }
    }
}
