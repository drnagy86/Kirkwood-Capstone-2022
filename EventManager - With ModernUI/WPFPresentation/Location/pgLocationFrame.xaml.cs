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
using LogicLayerInterfaces;
using DataObjects;

namespace WPFPresentation.Location
{
    /// <summary>
    /// Interaction logic for pgLocationFrame.xaml
    /// 
    /// Kris Howell
    /// Created: 2022/03/24
    /// 
    /// Description:
    /// Pulled from old pgViewLocationDetails page while separating functions
    /// into separate pages
    /// </summary>
    public partial class pgLocationFrame : Page
    {
        ManagerProvider _managerProvider;
        IEventManager _eventManager;
        DataObjects.Location _location;
        User _user;

        internal pgLocationFrame(ManagerProvider managerProvider, DataObjects.Location location, User user)
        {
            _managerProvider = managerProvider;
            _eventManager = managerProvider.EventManager;
            _location = location;
            _user = user;

            InitializeComponent();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Populate empty frame with details page by default on load
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Accented button color properly on load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            pgLocationDetails details = new pgLocationDetails(_managerProvider, _location, _user);
            this.LocationFrame.NavigationService.Navigate(details);
            btnSiteDetails.Background = new SolidColorBrush(Colors.Gray);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Inject a new Location Details page into the location frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteDetails_Click(object sender, RoutedEventArgs e)
        {
            pgLocationDetails details = new pgLocationDetails(_managerProvider, _location, _user);
            if (TryNavigateTo(details))
            {
                ResetButtonColors();
                btnSiteDetails.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Inject a new Location Sublocations page into the location frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteAreas_Click(object sender, RoutedEventArgs e)
        {
            pgLocationSublocations sublocations = new pgLocationSublocations(_managerProvider, _location);
            if (TryNavigateTo(sublocations))
            {
                ResetButtonColors();
                btnSiteAreas.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Inject a new Location Schedule page into the location frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteSchedule_Click(object sender, RoutedEventArgs e)
        {
            pgLocationSchedule schedule = new pgLocationSchedule(_managerProvider, _location);
            if (TryNavigateTo(schedule))
            {
                ResetButtonColors();
                btnSiteSchedule.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Inject a new Location Entrances page into the location frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteEntrances_Click(object sender, RoutedEventArgs e)
        {
            pgLocationEntrance entrances = new pgLocationEntrance(_managerProvider, _location, _user);
            if (TryNavigateTo(entrances))
            {
                ResetButtonColors();
                btnSiteEntrances.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Inject a new Location Parking page into the location frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteParking_Click(object sender, RoutedEventArgs e)
        {
            Page parking = new pgParkingLot(_managerProvider, _location, _user);
            if (TryNavigateTo(parking))
            {
                ResetButtonColors();
                btnSiteParking.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Helper method to safely try to navigate to a new page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>true if successfully navigated page, else false</returns>
        private bool TryNavigateTo(Page page)
        {
            if (ValidationHelpers.EditOngoing)
            {
                MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return false;
                }
                else // yes, discard changes
                {
                    ValidationHelpers.EditOngoing = false;
                    this.LocationFrame.NavigationService.Navigate(page);
                    return true;
                }
            }

            // no edit ongoing
            this.LocationFrame.NavigationService.Navigate(page);
            return true;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Helper method to reset all button colors to default
        /// 
        /// Kris Howell
        /// Updated: 2022/03/27
        /// 
        /// Description:
        /// Removed Map and Supplies buttons which do nothing right now.
        /// </summary>
        private void ResetButtonColors()
        {
            btnSiteDetails.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSiteAreas.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSiteSchedule.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSiteEntrances.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSiteParking.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
        }
    }
}
