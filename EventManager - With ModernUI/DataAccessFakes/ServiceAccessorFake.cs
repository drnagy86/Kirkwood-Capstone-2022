using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/02
    /// 
    /// The class holding all service accessor fake methods and data
    /// </summary>
    public class ServiceAccessorFake : IServiceAccessor
    {
        List<Service> _fakeServices = new List<Service>();

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// The ServiceAccessorFake custom constructor
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of Service objects</returns>
        public ServiceAccessorFake()
        {
            _fakeServices.Add(new Service()
            {
                ServiceID = 100000,
                SupplierID = 100000,
                ServiceName = "Fake Service One",
                Price = 10.10m,
                Description = "The number one fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            });
            _fakeServices.Add(new Service()
            {
                ServiceID = 100001,
                SupplierID = 100001,
                ServiceName = "Fake Service Two",
                Price = 35.12m,
                Description = "The number two fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            });
            _fakeServices.Add(new Service()
            {
                ServiceID = 100002,
                SupplierID = 100000,
                ServiceName = "Fake Service Three",
                Price = 0.73m,
                Description = "The number three fakest service out there",
                ServiceImagePath = "7263a839-3428-49f2-b26f-875d3811ef85.jpg"
            });
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// Method to delete a service from the fakes
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public int DeleteService(int serviceID)
        {
            int result = 0;
            foreach(Service service in _fakeServices)
            {
                if(service.ServiceID == serviceID)
                {
                    // No need to actually remove it. We just care about getting the return.
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Method to insert a service into the fakes
        /// </summary>
        /// <param name="newService"></param>
        /// <returns>rows affected</returns>
        public int InsertService(Service newService)
        {
            int result = 0;
            bool duplicate = false;
            foreach(Service service in _fakeServices)
            {
                if(service.ServiceID == newService.ServiceID)
                {
                    duplicate = true;
                    break;
                }
            }
            if(!duplicate)
            {
                _fakeServices.Add(newService);
                result++;
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Selects a service matching a passed serviceID
        /// </summary>
        /// <param name="serviceID">ID to match</param>
        /// <returns>The matching service</returns>
        public Service SelectServiceByServiceID(int serviceID)
        {
            Service result = null;
            foreach(Service service in _fakeServices)
            {
                if(service.ServiceID == serviceID)
                {
                    result = service;
                }
            }
            return result;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Method to select all services that match supplied supplierID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of Service objects</returns>
        public List<Service> SelectServicesBySupplierID(int supplierID)
        {
            List<Service> services = new List<Service>();

            try
            {
                foreach(Service service in _fakeServices)
                {
                    if(service.SupplierID == supplierID)
                    {
                        services.Add(service);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return services;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Updates a service in the fakes
        /// </summary>
        /// <param name="oldService"></param>
        /// <param name="newService"></param>
        /// <returns>rows affected</returns>
        public int UpdateService(Service oldService, Service newService)
        {
            int result = 0;
            for(int i = 0; i< _fakeServices.Count(); i++)
            {
                Service service = _fakeServices[i];
                if(oldService.ServiceID == service.ServiceID && oldService.Price == service.Price &&
                    oldService.ServiceImagePath.Equals(service.ServiceImagePath) && 
                    oldService.ServiceName.Equals(service.ServiceName) && oldService.SupplierID == service.SupplierID)
                {
                    _fakeServices[i] = newService;
                    result++;
                }
            }
            return result;
        }
    }
}
