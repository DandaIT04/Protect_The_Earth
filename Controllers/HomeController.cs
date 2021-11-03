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

            string email = FormData["txtLoginID"].ToString();
            string password = FormData["txtPassword"].ToString();
            DateTime DateTiming = DateTime.Now;

            if (userContext.ValidUserLogin(email, password) == true)
            {
                string Role = "User";
                HttpContext.Session.SetString("Role", Role);
                string userID = Convert.ToString(userContext.GetUserID(email));
                HttpContext.Session.SetString("LoginID", userID);
                ViewData["UserID"] = userID.ToString();
                HttpContext.Session.SetString("LogInTime", DateTiming.ToString());

                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Invalid Login Credentials";
                return RedirectToAction("LoginPage");
            }
        }
        public ActionResult userLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
