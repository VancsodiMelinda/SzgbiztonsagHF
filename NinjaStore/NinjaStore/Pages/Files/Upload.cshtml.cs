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
using NinjaStore.BLL.Exceptions;
using NinjaStore.DAL.Models;
using NinjaStore.Parser.Exceptions;

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
                string Message = $"POST Model State is not Valid {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return Page();
            }

			try
			{
				using (var memoryStream = new MemoryStream())
				{
					await NewFile.CopyToAsync(memoryStream);
					byte[] preview = memoryStream.ToArray();

					string savedFileId = await _logic.UploadFileAsync(User.Identity.Name, FileName, Description, preview);

                    string Message2 = $"POST Redirecting to page ./Details at {DateTime.UtcNow.ToLongTimeString()}";
                    _logger.LogInformation(Message2);

                    return RedirectToPage("./Details", new { id = savedFileId });
				}
			}
			catch (InvalidFileNameException)
			{
                ModelState.AddModelError("", "Invalid file name");
			}
            catch (EmptyFileException)
            {
                ModelState.AddModelError("", "Cannot upload empty file");
            }
            catch (LargeFileException lfe)
            {
                ModelState.AddModelError("", $"File size exceeds the maximum of {lfe.MaxSize} bytes");
            }
            catch (FileNameTakenException)
            {
                ModelState.AddModelError("", "File name already taken by this user");
            }
            catch (InvalidCaffFileContentException)
            {
                ModelState.AddModelError("", "Invalid CAFF file content");
            }

            return Page();
        }
    }
}
