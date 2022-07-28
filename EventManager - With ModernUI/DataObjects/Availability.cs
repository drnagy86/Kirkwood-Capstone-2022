using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Availability
    {
        public int ForeignID { get; set; }
        public int AvailabilityID { get; set; }
        public DateTime? DateID { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public DateTime? TimeStart { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        public DateTime? TimeEnd { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }

    public class AvailabilityVM : Availability
    {
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }
}
