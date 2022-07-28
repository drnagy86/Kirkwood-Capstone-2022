using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IServiceAccessor
    {
        List<Service> SelectServicesBySupplierID(int supplierID);
        Service SelectServiceByServiceID(int serviceID);
        int UpdateService(Service oldService, Service newService);
        int InsertService(Service newService);
        int DeleteService(int serviceID);
    }
}
