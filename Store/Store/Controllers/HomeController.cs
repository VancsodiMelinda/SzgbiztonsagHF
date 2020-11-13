using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Models;
using static DataLibrary.BusinessLogic.UserProcessor;

namespace Store.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

        public ActionResult SignUp()
        {
            ViewBag.Message = "User Sign Up";

            return View();
        }

        public ActionResult ViewUsers()
        {
            ViewBag.Message = "User List";

            var data = LoadUsers();
            List<UserModel> users = new List<UserModel>();

            foreach (var row in data)
            {
                users.Add(new UserModel
                {
                    UserName = row.UserName,
                    FirstName = row.FirstName,
                    //LastName = row.LastName,
                    EmailAddress = row.EmailAddress,
                    // ConfirmEmail = row.EmailAddress,
                    Password = row.Password,
                    Role = row.Role
                });
            }

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserModel model)
        {
            ViewBag.Message = "User Sign Up";

            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (!(model.Role.Equals("Admin") || model.Role.Equals("User")))
                    {
                        return RedirectToAction("Error");
                    }

                    int recordsCreated = CreateUser(model.UserName,
                        model.FirstName,
                        model.EmailAddress,
                        //A model.Password változót itt adom át, ezelőtt kellene hash-elni.
                        model.Password,
                        model.Role);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
