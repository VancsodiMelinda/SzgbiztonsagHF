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
    [Authorize(Roles = Roles.ADMIN + "," + Roles.USER)]
    public class DetailsModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DetailsModel> _logger;

        public string Username { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

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

        public DetailsModel(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<DetailsModel> logger)
		{
            _signInManager = signInManager;
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
                // TODO Gergő log - user not found
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

            if (User.IsInRole(Roles.USER))
            {
                if (string.IsNullOrWhiteSpace(Input.OldPassword))
                {
                    // TODO Gergő: log
                    // TODO Dani: exception -> old pass is required when user want to change pass
                }
                else
                {
                    // change email
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        // TODO Gergő log - user not found
                    }
                    user.Email = Input.Email;
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                    // change password
                    if (!string.IsNullOrWhiteSpace(Input.NewPassword))
                    {
                        var resultP = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);

                        if (resultP.Succeeded)
                        {
                            // Upon successfully changing the password refresh sign-in cookie
                            await _signInManager.RefreshSignInAsync(user);
                            TempData["Success"] = "Your password is successfully changed.";
                            return Page();
                        }

                        foreach (var error in resultP.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            } else
            {
                
            }
            return Page();
        }
    }
}