using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataObjects
{
    public class Service
    {
        public int ServiceID { get; set; }
        public int SupplierID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ServiceImagePath { get; set; }
    }

    public class ServiceVM : Service
    {
        public Uri ImageUri { get; set; }
    }
}
