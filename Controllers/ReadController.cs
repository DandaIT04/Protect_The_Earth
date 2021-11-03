using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFD_SaveTheEnvironment.Controllers
{
    public class ReadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Organizations()
        {
            return View();
        }

        public IActionResult Cars()
        {
            return View();
        }

        public IActionResult Electronics()
        {
            return View();
        }

        public IActionResult Fashion()
        {
            return View();
        }

        public IActionResult FoodandBeverages()
        {
            return View();
        }
    }
}
