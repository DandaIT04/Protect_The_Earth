using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using PFD_SaveTheEnvironment.DAL;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PFD_SaveTheEnvironment.Controllers
{
    public class EntertainmentController : Controller
    {
        private UsersDAL userContext = new UsersDAL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FlappyEarth()
        {
            return View();
        }

        public IActionResult dinoGame()
        {

            return View();
        }

        public IActionResult pointRedemption()
        {
            return View();
        }

        public ActionResult AddPointsDino(int id)
        {
            if (HttpContext.Session.GetString("LoginID") != null && HttpContext.Session.GetString("Role") == "User")
            {
                userContext.AddGamePoint(HttpContext.Session.GetString("LoginID"), id);
            }
            return RedirectToAction("dinoGame");
        }
        public ActionResult AddPointsGravit(int id)
        {
            if (HttpContext.Session.GetString("LoginID") != null && HttpContext.Session.GetString("Role") == "User")
            {
                userContext.AddGamePoint(HttpContext.Session.GetString("LoginID"), id);
            }
            return RedirectToAction("FlappyEarth");
        }
    }
}
