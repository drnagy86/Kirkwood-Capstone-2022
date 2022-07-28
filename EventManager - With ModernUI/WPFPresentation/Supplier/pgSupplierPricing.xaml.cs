using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataObjects;
using LogicLayerInterfaces;

namespace WPFPresentation.Supplier
{
    /// <summary>
    /// Interaction logic for pgSupplierPricing.xaml
    /// </summary>
    public partial class pgSupplierPricing : Page
    {
        ManagerProvider _managerProvider;
        private DataObjects.Supplier _supplier;
        private List<Service> _services;
        private IServiceManager _serviceManager;

        internal pgSupplierPricing(ManagerProvider managerProvider, DataObjects.Supplier supplier)
        {
            _managerProvider = managerProvider;
            _supplier = supplier;
            _serviceManager = managerProvider.ServiceManager;

            InitializeComponent();

            // CHANGE FOLDER NAME FROM LocationImages, TO SERVICE IMAGES ONCE CREATED
            AppData.DataPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + @"Images\SupplierImages";
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// method to populate the screen with the supplier's services
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _services = _serviceManager.RetrieveServicesBySupplierID(_supplier.SupplierID);
            txtSupplierServices.Text = _supplier.Name + "'s Services";

            List<ServiceVM> serviceVMs = new List<ServiceVM>();
            foreach (Service service in _services)
            {
                if (service.ServiceImagePath == null)
                {
                    serviceVMs.Add(new ServiceVM()
                    {
                        ServiceID = service.ServiceID,
                        SupplierID = service.SupplierID,
                        ServiceName = service.ServiceName,
                        Price = service.Price,
                        Description = service.Description
                    });
                }
                else
                {
                    try
                    {
                        Uri _src;
                        _src = new Uri(AppData.DataPath + @"\" + service.ServiceImagePath, UriKind.Absolute);
                        serviceVMs.Add(new ServiceVM()
                        {
                            ServiceID = service.ServiceID,
                            SupplierID = service.SupplierID,
                            ServiceName = service.ServiceName,
                            Price = service.Price,
                            Description = service.Description,
                            ImageUri = _src
                        });
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
            imageDataGrid.ItemsSource = serviceVMs;
        }
    }
}
