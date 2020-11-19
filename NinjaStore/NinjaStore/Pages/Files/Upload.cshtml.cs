using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.BLL;

namespace NinjaStore.Pages.Files
{
    public class UploadModel : PageModel
    {
        private readonly IStoreLogic _logic;

        public UploadModel(IStoreLogic logic)
        {
            _logic = logic;
        }

        public IActionResult OnGet()
        {
            // TODO Csilla: ask Dani
            //ViewData["FileId"] = new SelectList(_context.CaffFiles, "FileId", "FileId");

            return Page();
        }

        [BindProperty]
        public string FileName { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public IFormFile NewFile { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //string fileId = Guid.NewGuid().ToString("N");

            using (var memoryStream = new MemoryStream())
            {
                await NewFile.CopyToAsync(memoryStream);
                byte[] preview = memoryStream.ToArray();
                string savedFileId = await _logic.UploadFileAsync("Csilla", FileName, Description, preview);

                // TODO Csilla : fix file instead of metadata (ask Dani)
                //_context.CaffMetadata.Add(metadata);
                //await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
        }
    }
}
