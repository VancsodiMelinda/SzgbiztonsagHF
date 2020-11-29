using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Account
{
    [Authorize(Roles = Roles.ADMIN)]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IStoreLogic _logic;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(UserManager<User> userManager, IStoreLogic logic, ILogger<DeleteModel> logger)
		{
            _userManager = userManager;
            _logic = logic;
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

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                string Message = $"POST User not found {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return NotFound();
            }

            bool isAdmin = await _userManager.IsInRoleAsync(user, Roles.ADMIN);
            
            if (isAdmin)
            {
                string Message = $"POST Can not delete admin. {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return Forbid();
            }

            await _logic.RemoveUserFromFilesAsync(username);
            var result = await _userManager.DeleteAsync(user);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToPage("./List");
        }
    }
}