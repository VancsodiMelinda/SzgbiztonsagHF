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
    public class IndexModel : PageModel
    {
        private readonly NinjaStore.DAL.StoreContext _context;

        public IndexModel(NinjaStore.DAL.StoreContext context)
        {
            _context = context;
        }

        [BindProperty(Name = "filter", SupportsGet = true)]
        public string Filter { get; set; }

		public IList<CaffMetadata> CaffMetadata { get;set; }

        public async Task OnGetAsync()
        {
            CaffMetadata = await _context.CaffMetadata
                .Where(m => string.IsNullOrWhiteSpace(Filter) || m.FileName.Contains(Filter))
                .Include(c => c.File).ToListAsync();
        }
    }
}
