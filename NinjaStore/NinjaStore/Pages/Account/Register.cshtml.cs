using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NinjaStore.DAL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Account
{
    [ResponseCache(CacheProfileName = "Default30")]
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<RegisterModel> _logger;

        public class InputModel
		{
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        [BindProperty]
		public InputModel Input { get; set; }

        public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            bool isSignedIn = _signInManager.IsSignedIn(User);
            if (isSignedIn)
            {
                return RedirectToPage("../Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                string Message = $"POST Model State is not Valid at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return Page();
            }

            var user = new User
            {
                UserName = Input.Username,
                Email = Input.Email,
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
			{
                var roleResult = await _userManager.AddToRoleAsync(user, Roles.USER);

                if (roleResult.Succeeded)
				{
                    string Message = $"POST User created at {DateTime.UtcNow.ToLongTimeString()}";
                    _logger.LogInformation(Message);

                    await _signInManager.SignInAsync(user, false);

                    string Message2 = $"POST Redirect to page ../Index at {DateTime.UtcNow.ToLongTimeString()}";
                    _logger.LogInformation(Message2);

                    return RedirectToPage("../Index");
                }
                else
				{
                    await _userManager.DeleteAsync(user);
				}
            }

			foreach (var error in result.Errors)
			{
                ModelState.AddModelError("", error.Description);
                _logger.LogInformation(error.Description);
            }

            return Page();
        }
    }
}
