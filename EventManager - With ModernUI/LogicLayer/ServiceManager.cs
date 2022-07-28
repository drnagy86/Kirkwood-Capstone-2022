using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/02
    /// 
    /// Class for handling Service manager methods
    /// </summary>
    public class ServiceManager : IServiceManager
    {
        IServiceAccessor _serviceAccessor = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Default constructor for service manager using the live service accessor
        /// </summary>
        public ServiceManager()
        {
            _serviceAccessor = new ServiceAccessor();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Constructor for service manager using a given service accessor
        /// </summary>
        /// <param name="serviceAccessor"></param>
        public ServiceManager(IServiceAccessor serviceAccessor)
        {
            _serviceAccessor = serviceAccessor;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Method to create a service
        /// </summary>
        /// <param name="service">service to be passed to the database</param>
        /// <returns>true if one row affected, otherwise false</returns>
        public bool CreateService(Service service)
        {
            bool result = false;
            try
            {
                result = 1 == _serviceAccessor.InsertService(service);
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to create service", ex);
            }
            return result;
        }

        public bool DeleteService(int serviceID)
        {
            bool result = false;
            try
            {
                result = 1 == _serviceAccessor.DeleteService(serviceID);
            }catch(Exception ex)
            {
                throw new ApplicationException("Failed to delete service", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Method to edit a service
        /// </summary>
        /// <param name="oldService">Service to be replaced in database</param>
        /// <param name="newService">Service to use to replace in the database</param>
        /// <returns>true if one row affected, otherwise false</returns>
        public bool EditService(Service oldService, Service newService)
        {
            bool result = false;
            try
            {
                result = 1 == _serviceAccessor.UpdateService(oldService, newService);
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to update service", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Function to retrieve a single service by its serviceID
        /// </summary>
        /// <param name="serviceID">ID to retrieve</param>
        /// <returns>The service with the matching serviceID</returns>
        public Service RetrieveServiceByServiceID(int serviceID)
        {
            Service result = null;
            try
            {
                result = _serviceAccessor.SelectServiceByServiceID(serviceID);
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to retrieve service.", ex);
            }
            return result;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Method to retrieve a list of services by supplierID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of Service objects</returns>
        public List<Service> RetrieveServicesBySupplierID(int supplierID)
        {
            List<Service> services = new List<Service>();

            try
            {
                services = _serviceAccessor.SelectServicesBySupplierID(supplierID);
            }
            catch (Exception)
            {

                throw;
            }

            return services;
        }
    }
}
