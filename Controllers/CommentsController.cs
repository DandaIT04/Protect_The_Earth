using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PFD_SaveTheEnvironment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PFD_SaveTheEnvironment.Controllers
{
    public class CommentsController : Controller
    {
        public async Task<ActionResult> Index()
        {
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
    }
}
