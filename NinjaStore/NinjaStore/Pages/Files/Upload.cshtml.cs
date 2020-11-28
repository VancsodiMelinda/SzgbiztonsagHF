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
    [Authorize(Roles = Roles.ADMIN + "," + Roles.USER)]
    public class UploadModel : PageModel
    {
        private readonly IStoreLogic _logic;
        private readonly ILogger<UploadModel> _logger;

        public UploadModel(IStoreLogic logic, ILogger<UploadModel> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            string Message = $"GET Upload page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
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

                string savedFileId = await _logic.UploadFileAsync(User.Identity.Name, FileName, Description, preview);
                return RedirectToPage("./Details", new { id = savedFileId });
            }
        }
    }
}
