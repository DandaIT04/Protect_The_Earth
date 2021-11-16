using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using PFD_SaveTheEnvironment.DAL;
using System.Threading.Tasks;

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
    }
}
