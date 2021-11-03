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
        // Execute functions from DAL
        private EventConnectDAL eventContext = new EventConnectDAL();
        private EventUsersDAL eventUsers = new EventUsersDAL();
        private UsersDAL users = new UsersDAL();

        public IActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) || (HttpContext.Session.GetString("Role") != "User"))
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["actualUserID"] = HttpContext.Session.GetString("LoginID");

            // Basically a new model is created which stores data contained in other models
            UserEventViewModel UserEventVM = new UserEventViewModel();
            UserEventVM.eventList = eventContext.GetAllEvents();
            UserEventVM.eventUsersList = eventUsers.GetAllEventUsers();
            UserEventVM.userList = users.GetAllUsers();

            return View(UserEventVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserEventViewModel selectedEvent)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "User"))
            {
                return RedirectToAction("Index", "Home");
            }

            int selectEvent = selectedEvent.EventID;
            int selectUser = selectedEvent.UserID;

            EventUsers neweUsers = new EventUsers();

            // Assign neweUsers values with the int variable values
            neweUsers.EventID = selectEvent;
            neweUsers.UserID = selectUser;

            if (ModelState.IsValid)
            {
                if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "User"))
                {
                    return RedirectToAction("Index", "Home");
                }

                //Update record to database
                eventUsers.AddUsers(neweUsers);
                return RedirectToAction("Index", "Connect");
            }

            else
            {
                //Input validation fails, return to the view
                //to display error message

                return RedirectToAction("Index", "Connect");
            }
        }

        }
}
