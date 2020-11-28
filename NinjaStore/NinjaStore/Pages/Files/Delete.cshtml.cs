using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    [Authorize(Roles = Roles.ADMIN)]
    public class DeleteModel : PageModel
    {
        private readonly IStoreLogic _logic;

        //LOG CONSOLE
        //private readonly ILogger<DeleteModel> _logger;

        //LOG FILE
        readonly ILogger<DeleteModel> _log;

        public DeleteModel(IStoreLogic logic, ILogger<DeleteModel> logger, ILogger<DeleteModel> log)
        {
            _logic = logic;
            _log = log;
            // _logger = logger;
        }

        [BindProperty]
        public CaffMetadata CaffMetadata { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                string Message = $"GET ERROR: MetaData ID not found {DateTime.UtcNow.ToLongTimeString()}";
                _log.LogInformation(Message);
                //_logger.LogInformation(Message);
                return NotFound();
            }

            CaffMetadata = await _logic.GetMetadataWithCommentsAsync(id);

            if (CaffMetadata == null)
            {
                string Message2 = $"GET ERROR: MetaData value is null {DateTime.UtcNow.ToLongTimeString()}";
                _log.LogInformation(Message2);
                //_logger.LogInformation(Message2);
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                string Message = $"POST ERROR: MetaData ID not found {DateTime.UtcNow.ToLongTimeString()}";
                _log.LogInformation(Message);
                //_logger.LogInformation(Message);
                return NotFound();
            }

            CaffMetadata = await _logic.GetMetadataWithCommentsAsync(id);

            if (CaffMetadata != null)
            {
                string Message2 = $"POST CaffMetadata is deleted {DateTime.UtcNow.ToLongTimeString()}";
                _log.LogInformation(Message2);
               // _logger.LogInformation(Message2);
                string Message3 = $"POST CaffMetadata ID was {id}";
                _log.LogInformation(Message3);
                //_logger.LogInformation(Message3);
                await _logic.DeleteFileAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
