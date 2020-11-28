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
    public class ListModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ListModel> _logger;

        public ListModel(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<ListModel> logger)
		{
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}