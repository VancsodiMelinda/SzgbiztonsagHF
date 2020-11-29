using System;
using System.ComponentModel.DataAnnotations;
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

        [BindProperty]
        public string Username { get; set; }

        public class InputModel
        {
            public string Username { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required]
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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                string Message = $"GET User {Username} not found at {DateTime.UtcNow.ToLongTimeString()}";
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
                string Message = $"POST Model State is not valid at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return Page();
            }
            
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                string Message = $"POST User {Username} not found at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
            }
            bool oldPassIsValid = await _userManager.CheckPasswordAsync(user, Input.OldPassword);
            if (oldPassIsValid) {
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

                // change password
                if (!string.IsNullOrWhiteSpace(Input.NewPassword))
                {
                    result = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);

                    if (result.Succeeded)
                    {
                        // Upon successfully changing the password refresh sign-in cookie
                        await _signInManager.RefreshSignInAsync(user);
                        TempData["Success"] = "Your password is successfully changed.";
                        string Message = $"POST password successfully changed at {DateTime.UtcNow.ToLongTimeString()}";
                        _logger.LogInformation(Message);
                        return Page();
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            } else ModelState.AddModelError(string.Empty, "Incorrect password.Cs");


            return Page();
        }
    }
}