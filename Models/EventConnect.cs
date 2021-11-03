using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PFD_SaveTheEnvironment.Models
{
    public class EventConnect
    {
        public int EventID { get; set; }
        public int UserID { get; set; }

        //EventName
        [Required]
        [StringLength(100)]
        public string EventName { get; set; }

        //EventLocation
        [Required]
        [StringLength(100)]
        public string EventLocation { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

    }
}
