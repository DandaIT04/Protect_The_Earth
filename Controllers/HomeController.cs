using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PFD_SaveTheEnvironment.DAL;
using PFD_SaveTheEnvironment.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

namespace PFD_SaveTheEnvironment.Controllers
{
    public class HomeController : Controller
    {
        private UsersDAL userContext = new UsersDAL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginPage()
        {
            if (HttpContext.Session.GetString("Role") == "User")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Login(IFormCollection FormData)
        {
            if (HttpContext.Session.GetString("Role") == "User")
            {
                return RedirectToAction("Index", "Home");
            }

            string userID = FormData["txtLoginID"].ToString();
            string password = FormData["txtPassword"].ToString();
            DateTime DateTiming = DateTime.Now;

            if (userContext.ValidUserLogin(userID, password) == true)
            {
                string Role = "User";
                // Store Login ID in session with the key "LoginID"
                HttpContext.Session.SetString("LoginID", userID);
                // Store user role "Judge" as a string in session with the key "Role"
                HttpContext.Session.SetString("Role", Role);
                // Store date and time of the user when it has logged in
                HttpContext.Session.SetString("DateTiming", DateTiming.ToString());

                return RedirectToAction("Index");
            }
            else
            {
                // Store an error message to TempData for display at the index view
                TempData["Message"] = "Invalid Login Credentials";
                return RedirectToAction("Login");
            }
        }
    }
}
