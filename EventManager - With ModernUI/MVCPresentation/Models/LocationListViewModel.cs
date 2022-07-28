using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class LocationListViewModel
    {
        public IEnumerable<Location> Locations { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}