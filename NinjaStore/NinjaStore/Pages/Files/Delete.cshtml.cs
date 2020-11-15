using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NinjaStore.DAL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    public class DeleteModel : PageModel
    {
        private readonly NinjaStore.DAL.StoreContext _context;

        public DeleteModel(NinjaStore.DAL.StoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CaffMetadata CaffMetadata { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CaffMetadata = await _context.CaffMetadata
                .Include(c => c.File).FirstOrDefaultAsync(m => m.FileId == id);

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

            CaffMetadata = await _context.CaffMetadata.FindAsync(id);

            if (CaffMetadata != null)
            {
                _context.CaffMetadata.Remove(CaffMetadata);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
