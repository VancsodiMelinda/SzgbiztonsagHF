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
    [Authorize(Roles = Roles.ADMIN)]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(UserManager<User> userManager, ILogger<DeleteModel> logger)
		{
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                string Message = $"POST Username is empty null or only consists of whitespaces : {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return NotFound();
            }
            else
            {
                var user = await _userManager.FindByNameAsync(username);
                bool isAdmin = _userManager.IsInRoleAsync(user, Roles.ADMIN).Result;
                if (user == null)
                {
                    string Message = $"POST User not found {DateTime.UtcNow.ToLongTimeString()}";
                    _logger.LogInformation(Message);
                    return NotFound();
                } 
                else if(isAdmin)
                { 
                    string Message = $"POST Can not delete admin. {DateTime.UtcNow.ToLongTimeString()}";
                    _logger.LogInformation(Message);

                    // TODO Dani: Exception - can not delete admin

                }
                else
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToPage("./List");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return RedirectToPage("./List");
            }
        }
    }
}