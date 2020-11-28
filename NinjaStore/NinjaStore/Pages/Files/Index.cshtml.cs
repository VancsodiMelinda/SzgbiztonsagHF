using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    [Authorize(Roles = Roles.ADMIN + "," + Roles.USER)]
    public class IndexModel : PageModel
    {
        private readonly IStoreLogic _logic;

        readonly ILogger<IndexModel> _log;

        public IndexModel(IStoreLogic logic, ILogger<IndexModel> log)
        {
            _logic = logic;
            _log = log;
        }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

		public IList<CaffMetadata> CaffMetadata { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CaffMetadata = await _logic.QueryMetadataByFreeTextAsync(Filter);
            string Message = $"GET MetaData Query at {DateTime.UtcNow.ToLongTimeString()}";
            _log.LogInformation(Message);
            return Page();
        }
    }
}
