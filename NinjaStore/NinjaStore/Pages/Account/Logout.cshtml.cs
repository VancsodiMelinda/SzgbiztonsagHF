using System;
using System.Collections.Generic;
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
    [Authorize]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // HTTP 405 Method Not Allowed
            string Message = $"GET HTTP 405 Method Not Allowed at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            return StatusCode(405);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            string Message = $"POST Redirect to Page ../Index at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            return RedirectToPage("../Index");
        }
    }
}