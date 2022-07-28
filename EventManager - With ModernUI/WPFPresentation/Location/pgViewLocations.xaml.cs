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
using LogicLayer;
using LogicLayerInterfaces;
using DataAccessFakes;
using DataObjects;

namespace WPFPresentation.Location
{
    /// <summary>
    /// Interaction logic for pgViewLocations.xaml
    /// </summary>
    public partial class pgViewLocations : Page
    {
        ILocationManager _locationManager = null;
        ISublocationManager _sublocationManager;
        ManagerProvider _managerProvider;

        User _user = null;

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Initialize location manager and page
        /// 
        /// Update:
        /// Austin Timmerman
        /// Updated: 2022/02/27
        /// 
        /// Description:
        /// Added the ManagerProvider instance variable and modified page parameters
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/03/01
        /// 
        /// Description:
        /// Added _user to constructor
        /// </summary>
        /// <param name="managerProvider"></param>
        internal pgViewLocations(ManagerProvider managerProvider, User user)
        {
            _managerProvider = managerProvider;
            _locationManager = managerProvider.LocationManager;
            _sublocationManager = managerProvider.SublocationManager;
            _user = user;

            InitializeComponent();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Populate list of locations table with all active locations
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                datLocationsList.ItemsSource = _locationManager.RetrieveActiveLocations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Original author and creation date missing.
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/03/01
        /// 
        /// Description:
        /// Added _user to page constructor
        /// 
        /// Update:
        /// Kris Howell
        /// Updated: 2022/03/27
        /// 
        /// Description:
        /// Redirect to new pgLocationFrame page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datLocationsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datLocationsList.SelectedItem == null)
            {
                return;
            }
            DataObjects.Location location = (DataObjects.Location)datLocationsList.SelectedItem;

            pgLocationFrame page = new pgLocationFrame(_managerProvider, location, _user);
            this.NavigationService.Navigate(page);
        }
    }
}
