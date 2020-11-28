using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Account
{
    [Authorize(Roles = Roles.ADMIN)]
    public class DetailsAdminModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DetailsModel> _logger;

        [BindProperty]
        public string Username { get; set; }

        public class InputModel
        {
            public string Username { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public DetailsAdminModel(UserManager<User> userManager, ILogger<DetailsModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = User.Identity.Name;
            }
            var user = await _userManager.FindByNameAsync(id);
            if (user == null)
            {
                string Message = $"GET User not found at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
            }
            Input = new InputModel();
            Input.Email = user.Email;
            Username = user.UserName;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Username);
            if (user == null)
            {
                string Message = $"GET User not found at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
            }

            // change email
            user.Email = Input.Email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            TempData["Success"] = "User email is successfully changed.";
            // change password
            if (!string.IsNullOrWhiteSpace(Input.NewPassword))
                {
                    await _userManager.RemovePasswordAsync(user);
                    result = await _userManager.AddPasswordAsync(user, Input.NewPassword);

                    if (result.Succeeded)
                    {
                        TempData["Success"] = "User password is successfully changed.";
                        return Page();
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            return Page();
        }
    }
}