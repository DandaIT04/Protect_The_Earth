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

                List<EventUsers> allEventUsersList = eventUsers.GetAllEventUsers();
                List<EventConnect> allEventsList = eventContext.GetAllEvents();

                bool isSameTime = false;
                foreach (EventUsers eventUsers in allEventUsersList)
                {
                    if (neweUsers.UserID == eventUsers.UserID)
                    {
                        foreach(EventConnect eventList in allEventsList)
                        {
                            if(eventList.EventID == eventUsers.EventID)
                            {
                                foreach (EventConnect eventList2 in allEventsList)
                                {
                                    if(neweUsers.EventID == eventList2.EventID)
                                    {
                                        if(eventList2.StartDate == eventList.StartDate ||
                                           eventList2.StartDate >= eventList.StartDate && eventList2.EndDate <= eventList.EndDate)
                                        {
                                            isSameTime = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                }

                if(isSameTime == true)
                {
                    TempData["EventCollision"] = "Event time collided with a booked event!";
                    return RedirectToAction("Index", "Connect");
                }

                else
                {
                    //Update record to database
                    eventUsers.AddUsers(neweUsers);
                    return RedirectToAction("Index", "Connect");
                }
            }

            else
            {
                //Input validation fails, return to the view
                //to display error message

                return RedirectToAction("Index", "Connect");
            }
        }

        public ActionResult CreateEventView()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
(HttpContext.Session.GetString("Role") != "User"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View("/Views/Connect/CreateEventView.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(EventConnect theEvent)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "User"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                TempData["actualUserID"] = HttpContext.Session.GetString("LoginID");
                string a = Convert.ToString(TempData["actualUserID"]);
                int b = Convert.ToInt32(a);

                theEvent.UserID = b;

        
                List<EventConnect> eventList = eventContext.GetAllEvents();

                foreach(EventConnect allTheEvents in eventList)
                {
                    if (theEvent.UserID == allTheEvents.UserID &&
                        theEvent.EventName == allTheEvents.EventName &&
                        theEvent.EventLocation == allTheEvents.EventLocation &&
                        theEvent.StartDate == allTheEvents.StartDate &&
                        theEvent.EndDate == allTheEvents.EndDate ||
                        theEvent.EventName == allTheEvents.EventName &&
                        theEvent.EventLocation == allTheEvents.EventLocation &&
                        theEvent.StartDate == allTheEvents.StartDate &&
                        theEvent.EndDate == allTheEvents.EndDate)
                    {
                        TempData["SimilarEvent"] = "Similar Event Exists!";

                        return RedirectToAction("CreateEventView", "Connect");
                    }
                }

                DateTime isRightNow = DateTime.Now;

                if(theEvent.StartDate < isRightNow || theEvent.EndDate < isRightNow)
                {
                    TempData["SimilarEvent"] = "Start date/End date cannot be set to before current time!";

                    return RedirectToAction("CreateEventView", "Connect");
                }

                if(theEvent.StartDate < isRightNow.AddHours(2))
                {
                    TempData["SimilarEvent"] = "Event must occur at least more than two hours from now!";

                    return RedirectToAction("CreateEventView", "Connect");
                }

                //Update record to database
                eventContext.Add(theEvent);
                return RedirectToAction("Index", "Connect");
            }

            else
            {
                //Input validation fails, return to the view
                //to display error message

                return RedirectToAction("CreateEventView", "Connect");
            }

        }

    }
}
