using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class SupplierListViewModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}