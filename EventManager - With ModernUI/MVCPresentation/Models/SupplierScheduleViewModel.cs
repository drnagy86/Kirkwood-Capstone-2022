using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentation.Models
{
    public class SupplierScheduleViewModel
    {
        public Supplier Supplier { get; set; }
        public List<AvailabilityVM> Availability { get; set; }
        public List<Availability> AvailabilityException { get; set; }
        public List<ServiceVM> Services { get; set; }
    }
}