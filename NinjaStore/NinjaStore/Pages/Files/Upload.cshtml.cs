using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    [Authorize(Roles = Roles.USER)]
    public class UploadModel : PageModel
    {
        private readonly IStoreLogic _logic;

        //LOG CONSOLE
        //private readonly ILogger<UploadModel> _logger;

        //LOG FILE
        readonly ILogger<UploadModel> _log;

        public UploadModel(IStoreLogic logic, ILogger<UploadModel> logger, ILogger<UploadModel> log)
        {
            _logic = logic;
            _log = log;
            // _logger = logger;
        }

        public IActionResult OnGet()
        {
            string Message = $"GET Upload page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _log.LogInformation(Message);
            // _logger.LogInformation(Message);
            return Page();
        }

        [BindProperty]
        [Required(ErrorMessage ="The name of the file is required.")]
        public string FileName { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a file.")]
        public IFormFile NewFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var memoryStream = new MemoryStream())
            {
                await NewFile.CopyToAsync(memoryStream);
                byte[] preview = memoryStream.ToArray();

                // TODO Csilla:  SQL Exception
                //string savedFileId = await _logic.UploadFileAsync(User.Identity.Name, FileName, Description, preview);
                string savedFileId = await _logic.UploadFileAsync("Csilla", FileName, Description, preview);
                return RedirectToPage("./Details", new { id = savedFileId });
            }
        }
    }
}
