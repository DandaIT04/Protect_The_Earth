using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PFD_SaveTheEnvironment.DAL;
using PFD_SaveTheEnvironment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PFD_SaveTheEnvironment.Controllers
{
    public class CommentsController : Controller
    {
        private UsersDAL userContext = new UsersDAL();
        public async Task<ActionResult> Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) || (HttpContext.Session.GetString("Role") != "User"))
            {
                ViewData["Role"] = null;
            }
            else
            {
                ViewData["Role"] = "User";
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://commentsapi20211115072014.azurewebsites.net/");
            HttpResponseMessage response = await client.GetAsync("/api/comments");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Comments> commentsList = JsonConvert.DeserializeObject<List<Comments>>(data);
                return View(commentsList);
            }
            else
            {
                return View(new List<Comments>());
            }
        }
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            string commentText = collection["item.Comment"];
            // Transfer data read to a vote object
            Comments c = new Comments();
            c.Id = 0;
            c.UserName = userContext.GetUserName(HttpContext.Session.GetString("LoginID"));
            c.Comment = commentText;
            HttpClient client = new HttpClient();
            client.BaseAddress = new
            Uri("https://commentsapi20211115072014.azurewebsites.net/");
            string json = JsonConvert.SerializeObject(c);
            StringContent commentscontent = new StringContent(json, UnicodeEncoding.UTF8,
            "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/comments",
            commentscontent);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Failed to add comment!";
                return RedirectToAction("Index", "Comments");
            }
        }
    }
}
