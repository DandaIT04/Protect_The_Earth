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

namespace PFD_SaveTheEnvironment.Controllers
{
    public class UserController : Controller
    {
        private UsersDAL userContext = new UsersDAL();

        public ActionResult Create()
        {
            ViewData["SalutationList"] = GetSalutations();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users user)
        {
            //Lists for the View
            ViewData["SalutationList"] = GetSalutations();
            if (ModelState.IsValid)
            {
                user.UserID = userContext.Add(user);
                TempData["Success"] = "User Creation Successful!";
                return RedirectToAction("LoginPage", "Home");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(user);
            }
        }
        private List<SelectListItem> GetSalutations()
        {
            List<SelectListItem> sal = new List<SelectListItem>();
            sal.Add(new SelectListItem
            {
                Value = "Dr",
                Text = "Dr"
            }); sal.Add(new SelectListItem
            {
                Value = "Mr",
                Text = "Mr"
            }); sal.Add(new SelectListItem
            {
                Value = "Ms",
                Text = "Ms"
            }); sal.Add(new SelectListItem
            {
                Value = "Mrs",
                Text = "Mrs"
            }); sal.Add(new SelectListItem
            {
                Value = "Mdm",
                Text = "Mdm"
            });

            return sal;
        }
    }
}
