using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly IStoreLogic _logic;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IStoreLogic logic, ILogger<IndexModel> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

		public IList<CaffMetadata> CaffMetadata { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CaffMetadata = await _logic.QueryMetadataByFreeTextAsync(Filter);
            string Message = $"GET MetaData Query at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            return Page();
        }
    }
}
