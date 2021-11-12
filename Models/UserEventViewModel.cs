using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PFD_SaveTheEnvironment.Models
{
    public class UserEventViewModel
    {
        public List<Users> userList { get; set; }
        public List<EventConnect> eventList { get; set; }
        public List<EventUsers> eventUsersList { get; set; }

        public int EventID { get; set; }

        public int UserID { get; set; }

        public UserEventViewModel()
        {
            userList = new List<Users>();
            eventList = new List<EventConnect>();
            eventUsersList = new List<EventUsers>();
        }
    }
}
