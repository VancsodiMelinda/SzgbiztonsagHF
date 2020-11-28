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
    public class ListModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ListModel> _logger;

        public class UserModel
        {
            public string Username { get; set; }
            public string Email { get; set; }
        }

        [BindProperty]
        public IList<UserModel> List { get; set; }

        public ListModel(UserManager<User> userManager, ILogger<ListModel> logger)
		{
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // TODO Gergő: log
            List = new List<UserModel>();
            foreach (var user in _userManager.Users)
            {
                UserModel userModel = new UserModel();
                userModel.Username = user.UserName;
                userModel.Email = user.Email;
                List.Add(userModel);
            }

            return Page();
        }
    }
}