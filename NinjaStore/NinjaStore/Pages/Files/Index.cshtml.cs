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

        //LOG CONSOLE
        //private readonly ILogger<UploadModel> _logger;

        //LOG FILE
        readonly ILogger<IndexModel> _log;

        public IndexModel(IStoreLogic logic, ILogger<IndexModel> logger, ILogger<IndexModel> log)
        {
            _logic = logic;
            _log = log;
            // _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

		public IList<CaffMetadata> CaffMetadata { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CaffMetadata = await _logic.QueryMetadataByFreeTextAsync(Filter);
            string Message = $"GET MetaData Query at {DateTime.UtcNow.ToLongTimeString()}";
            _log.LogInformation(Message);
            //_logger.LogInformation(Message);
            return Page();
        }
    }
}
