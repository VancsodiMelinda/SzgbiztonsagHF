using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly IStoreLogic _logic;
        public IndexModel(IStoreLogic logic)
        {
            _logic = logic;
        }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

		public IList<CaffMetadata> CaffMetadata { get;set; }

        public async Task OnGetAsync()
        {
            CaffMetadata = await _logic.QueryMetadataByFreeTextAsync(Filter);
        }
    }
}
