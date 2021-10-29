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
    }
}
