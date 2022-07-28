using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

using DataAccessFakes;
using Ninject.Web.Common;

using DataObjects;
using LogicLayerInterfaces;
using LogicLayer;

namespace MVCPresentation.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            // live
            kernel.Bind<IEventManager>().To<LogicLayer.EventManager>().InRequestScope();
            kernel.Bind<IUserManager>().To<LogicLayer.UserManager>().InRequestScope();
            kernel.Bind<IVolunteerManager>().To<VolunteerManager>();
            kernel.Bind<ILocationManager>().To<LocationManager>();
            kernel.Bind<ISupplierManager>().To<SupplierManager>();
            kernel.Bind<IVolunteerRequestManager>().To<VolunteerRequestManager>();
            kernel.Bind<IActivityManager>().To<ActivityManager>();
            kernel.Bind<IEventDateManager>().To<EventDateManager>();
            kernel.Bind<IServiceManager>().To<ServiceManager>();
            kernel.Bind<ISublocationManager>().To<SublocationManager>();
            kernel.Bind<IParkingLotManager>().To<ParkingLotManager>();
            kernel.Bind<IVolunteerApplicationsManager>().To<VolunteerApplicationsManager>();
            kernel.Bind<ITaskManager>().To<TaskManager>();
            kernel.Bind<IVolunteerReviewManager>().To<VolunteerReviewManager>();
            //kernel.Bind<IEmailProvider>().To<EmailProvider>();


            // fake
            //kernel.Bind<IEventManager>().To<LogicLayer.EventManager>().WithConstructorArgument("eventAccessor", new EventAccessorFake());
            //kernel.Bind<IVolunteerApplicationsManager>().To<VolunteerApplicationsManager>().WithConstructorArgument("volunteerApplicationsAccessor", new VolunteerApplicationsAccessorFake());
            kernel.Bind<IEmailProvider>().To<EmailProviderFake>();            
            

            
            


            

        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}