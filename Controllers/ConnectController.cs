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
    public class ConnectController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "User")
            {
                return RedirectToAction("Index", "Home");
            }

            else if (HttpContext.Session.GetString("Role") == "User")
            {
                return View("Index", "Connect");
            }    

            return View();
        }
    }
}
