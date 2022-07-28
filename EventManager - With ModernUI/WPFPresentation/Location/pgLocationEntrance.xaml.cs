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

namespace WPFPresentation.Location
{
    /// <summary>
    /// Interaction logic for pgLocationEntrance.xaml
    /// 
    /// Kris Howell
    /// Created: 2022/03/24
    /// 
    /// Description:
    /// Pulled from old pgViewLocationDetails page while separating functions
    /// into separate pages
    /// </summary>
    public partial class pgLocationEntrance : Page
    {
        DataObjects.Location _location;
        User _user;
        ManagerProvider _managerProvider;
        IEntranceManager _entranceManager;
        List<Entrance> _entrances;
        Entrance _entrance;
        internal pgLocationEntrance(ManagerProvider managerProvider, DataObjects.Location location, User user)
        {
            _managerProvider = managerProvider;
            _entranceManager = managerProvider.EntranceManager;
            _location = location;
            _user = user;
            InitializeComponent();
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Click event handler to send a user to the view Entrances page
        /// 
        /// Update:
        /// Kris Howell
        /// Updated: 2022/03/24
        /// 
        /// Description:
        /// Pulled from old pgViewLocationDetails format, separated into its own page
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.lblLocationName.Text = _location.Name + " Entrances";
                _entrances = _entranceManager.RetrieveEntranceByLocationID(_location.LocationID);
                if (_entrances.Count == 0)
                {
                    lblNoEntrances.Content = "No entrances for this location yet. Use the Create button to create an entrance.";
                }
                datViewEntrances.ItemsSource = _entrances;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Alaina Gilson
        /// Created 2022/03/04
        /// 
        /// Description:
        /// Click event handler for creating an add edit entrance page
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateEntrance_Click(object sender, RoutedEventArgs e)
        {
            Page page = new pgAddEditEntrance(_entrance, _location, _managerProvider, _user, 1);
            this.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created 2022/03/08
        /// 
        /// Description:
        /// Click event handler for going to the add edit entrance page
        /// in update mode
        /// 
        /// Update:
        /// Kris Howell
        /// Updated: 2022/03/26
        /// 
        /// Description:
        /// Check if selected item is null so that it doesn't throw an exception if user double
        /// clicks empty space.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datViewEntrances_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datViewEntrances.SelectedItem != null)
            {
                _entrance = (Entrance)datViewEntrances.SelectedItem;

                Page page = new pgAddEditEntrance(_entrance, _location, _managerProvider, _user, 2);
                this.NavigationService.Navigate(page);
            }
        }
    }
}
