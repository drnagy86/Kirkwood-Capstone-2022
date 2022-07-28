using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class ItineraryViewModel
    {
        public IEnumerable<ActivityVM> Activities { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}