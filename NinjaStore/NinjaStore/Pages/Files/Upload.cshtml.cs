using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.BLL;

namespace NinjaStore.Pages.Files
{
    public class UploadModel : PageModel
    {
        private readonly IStoreLogic _logic;

        public UploadModel(IStoreLogic logic)
        {
            _logic = logic;
        }

        public IActionResult OnGet()
        {
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
                
                // TODO Csilla: username
                string savedFileId = await _logic.UploadFileAsync(null, FileName, Description, preview);
                return RedirectToPage("./Details", new { id = savedFileId });
            }
        }
    }
}
