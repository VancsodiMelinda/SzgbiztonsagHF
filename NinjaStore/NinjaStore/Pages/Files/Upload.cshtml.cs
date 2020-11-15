using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NinjaStore.DAL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    public class UploadModel : PageModel
    {
        private readonly NinjaStore.DAL.StoreContext _context;

        public UploadModel(NinjaStore.DAL.StoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FileId"] = new SelectList(_context.CaffFiles, "FileId", "FileId");
            return Page();
        }

        [BindProperty]
        public string FileName { get; set; }

        [BindProperty]
        public string Description { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string fileId = Guid.NewGuid().ToString("N");

            byte[] preview = new byte[] { }; //System.IO.File.ReadAllBytes(@"C:\temp\test.bmp");

            CaffMetadata metadata = new CaffMetadata
            {
                FileId = fileId,
                FileName = FileName,
                Description = Description,
                Username = "Dani",
                UploadTimestamp = DateTimeOffset.UtcNow,
                DownloadCounter = 0,
                FileSize = 4,
                Lenght = 120,
                Preview = preview,
            };

            CaffFile file = new CaffFile
            {
                FileId = fileId,
                Data = new byte[] { 0x00, 0xff, 0xaa, 0x80 },
            };

            metadata.File = file;
            file.Metadata = metadata;

            _context.CaffMetadata.Add(metadata);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
