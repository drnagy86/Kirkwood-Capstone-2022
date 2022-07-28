using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class LocationScheduleViewModel
    {
        public Location Location { get; set; }
        public List<AvailabilityVM> Availability { get; set; }
        public List<Availability> AvailabilityException { get; set; }
    }
}