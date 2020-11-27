using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NinjaStore.DAL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

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

        //LOG CONSOLE
        //private readonly ILogger<RegisterModel> _logger;

        //LOG FILE
        readonly ILogger<RegisterModel> _log;

        public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<RegisterModel> logger, ILogger<RegisterModel> log)
        {
            _userManager = userManager;
            _signInManager = signInManager;
           // _logger = logger;
            _log = log;
        }

        public IActionResult OnGet()
        {
            string Message = $"GET Register Page {DateTime.UtcNow.ToLongTimeString()}";
            _log.LogInformation(Message);
           // _logger.LogInformation(Message);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
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
                string Message = $"POST User created at {DateTime.UtcNow.ToLongTimeString()}";
                _log.LogInformation(Message);
              //  _logger.LogInformation(Message);
                await _signInManager.SignInAsync(user, false);
                return RedirectToPage("../Index");
            }

			foreach (var error in result.Errors)
			{
                ModelState.AddModelError("", error.Description);
                _log.LogInformation(error.Description);
                //_logger.LogInformation(error.Description);
            }
            return Page();
        }
    }
}
