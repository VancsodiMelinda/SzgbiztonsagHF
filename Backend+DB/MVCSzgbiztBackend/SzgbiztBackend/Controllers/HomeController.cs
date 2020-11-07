using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SzgbiztBackend.Models;
using DataLibrary;
using static DataLibrary.BusinessLogic.EmployeeProcessor;

namespace SzgbiztBackend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "User Sign Up";

            return View();
        }

        public ActionResult ViewEmployees()
        {
            ViewBag.Message = "User List";

            var data = LoadEmployees();
            List<EmployeeModel> employees = new List<EmployeeModel>();

            foreach (var row in data)
            {
                employees.Add(new EmployeeModel
                {
                    EmployeeId = row.EmployeeId,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    EmailAddress = row.EmailAddress,
                   // ConfirmEmail = row.EmailAddress,
                    Password = row.Password,
                    Role = row.Role
                });
            }

            return View(employees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(EmployeeModel model)
        {
            ViewBag.Message = "User Sign Up";

            if(ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if(!(model.Role.Equals("Administrator") || model.Role.Equals("User")))
                    {
                        return RedirectToAction("Error");
                    }

                    int recordsCreated = CreateEmployee(model.EmployeeId,
                        model.FirstName,
                        model.LastName,
                        model.EmailAddress,
                        //A model.Password változót kellene hash-elni.
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