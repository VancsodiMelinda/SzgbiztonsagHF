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
    [Authorize(Roles = Roles.ADMIN + "," + Roles.USER)]
    public class DownloadModel : PageModel
    {
        private readonly IStoreLogic _logic;

        readonly ILogger<DownloadModel> _log;

        public DownloadModel(IStoreLogic logic, ILogger<DownloadModel> log)
        {
            _logic = logic;
            _log = log;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                string Message = $"GET ERROR: File not found {DateTime.UtcNow.ToLongTimeString()}";
                _log.LogInformation(Message);
                return NotFound();
            }
            CaffFile file = await _logic.DownloadFileAsync(id);

            if (file != null)
            {
                string Message2 = $"GET file downloaded at {DateTime.UtcNow.ToLongTimeString()}";
                _log.LogInformation(Message2);
                return File(file.Data, "application/octet-stream", file.Metadata.FileName + ".caff");
            }
            string Message3 = $"GET ERROR: File value is null {DateTime.UtcNow.ToLongTimeString()}";
            _log.LogInformation(Message3);
            return NotFound();
        }
    }
}