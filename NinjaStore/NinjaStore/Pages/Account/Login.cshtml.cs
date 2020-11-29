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
    [ResponseCache(CacheProfileName = "Default30")]
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public string ReturnUrl { get; set; }

        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger)
		{
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            bool isSignedIn = _signInManager.IsSignedIn(User);
            if (isSignedIn)
            {
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }

                return RedirectToPage("../Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Username, Password, false, true);

                if (result.Succeeded)
				{
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
					{
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToPage("../Index");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return Page();
        }
    }
}