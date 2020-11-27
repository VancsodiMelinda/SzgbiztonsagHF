using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    public class DetailsModel : PageModel
    {
        private readonly IStoreLogic _logic;

        //LOG CONSOLE
        //private readonly ILogger<UploadModel> _logger;

        //LOG FILE
        readonly ILogger<DetailsModel> _log;

        public DetailsModel(IStoreLogic logic, ILogger<DetailsModel> logger, ILogger<DetailsModel> log)
        {
            _logic = logic;
            _log = log;
            // _logger = logger;
        }

        public CaffMetadata CaffMetadata { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                string Message = $"GET ERROR: MetaData ID not found {DateTime.UtcNow.ToLongTimeString()}";
                _log.LogInformation(Message);
               // _logger.LogInformation(Message);
                return NotFound();
            }

            CaffMetadata = await _logic.GetMetadataWithCommentsAsync(id);
            if (CaffMetadata == null)
            {
                string Message2 = $"GET ERROR: MetaData value is null {DateTime.UtcNow.ToLongTimeString()}";
                _log.LogInformation(Message2);
               // _logger.LogInformation(Message2);
                return NotFound();
            }
            return Page();
        }
    }
}
