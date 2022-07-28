using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IServiceManager
    {
        List<Service> RetrieveServicesBySupplierID(int supplierID);
        Service RetrieveServiceByServiceID(int serviceID);
        bool EditService(Service oldService, Service newService);
        bool CreateService(Service service);
        bool DeleteService(int serviceID);
    }
}
