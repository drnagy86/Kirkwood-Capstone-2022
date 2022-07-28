using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class LocationDetailsViewModel
    {
        public Location Location { get; set; }
        public List<Reviews> LocationReviews { get; set; }
        public List<string> LocationTags { get; set; }
        public List<LocationImage> LocationImages { get; set; }
    }
}