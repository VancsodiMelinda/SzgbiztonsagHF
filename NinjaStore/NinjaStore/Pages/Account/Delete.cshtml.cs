using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Account
{
    public class DeleteModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<DeleteModel> logger)
		{
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                // TODO Gergő log
                return NotFound();
            }
            else
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    // TODO Gergő log
                    return NotFound();
                } 
                else if(User.IsInRole("Admin"))
                {

                }
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToPage("./List");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return RedirectToPage("./List");
            }
        }
    }
}