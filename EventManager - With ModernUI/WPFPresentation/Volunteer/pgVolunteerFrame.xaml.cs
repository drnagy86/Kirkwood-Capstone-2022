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
using DataAccessInterfaces;
using DataAccessFakes;
using DataObjects;
using LogicLayerInterfaces;
using LogicLayer;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for pgVolunteerFrame.xaml
    /// </summary>
    public partial class pgVolunteerFrame : Page
    {
        ManagerProvider _managerProvider = null;
        Volunteer _volunteer = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/29
        /// 
        /// Description:
        /// Constructor that sets the _managerProvider and _volunteer
        /// <paramref name="volunteer"/>
        /// <paramref name="managerProvider"/>
        internal pgVolunteerFrame(Volunteer volunteer, ManagerProvider managerProvider)
        {
            _managerProvider = managerProvider;
            _volunteer = volunteer;
            InitializeComponent();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/29
        /// 
        /// Description:
        /// Helper method to reset all button colors to default
        /// (Original Author: Kris Howell pgLocationFrame.xaml.cs)
        private void ResetButtonColors()
        {
            btnVolunteerDetails.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnVolunteerSchedule.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnVolunteerSupplies.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/29
        /// 
        /// Description:
        /// Event handler for when the page is loaded. Defaults to go to the 
        /// volunteer's details page
        /// <paramref name="sender"/>
        /// <paramref name="e"/>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            pgViewVolunteerDetails details = new pgViewVolunteerDetails(_volunteer, _managerProvider);
            this.VolunteerFrame.NavigationService.Navigate(details);
            if(_volunteer.VolunteerType == "Supply Donor")
            {
                btnVolunteerSupplies.Visibility = Visibility.Visible;
            }
            ResetButtonColors();
            btnVolunteerDetails.Background = new SolidColorBrush(Colors.Gray);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/29
        /// 
        /// Description:
        /// Event handler when the Volunteer Details button is clicked to 
        /// load the page into the frame
        /// <paramref name="sender"/>
        /// <paramref name="e"/>
        private void btnVolunteerDetails_Click(object sender, RoutedEventArgs e)
        {
            pgViewVolunteerDetails details = new pgViewVolunteerDetails(_volunteer, _managerProvider);
            if (_volunteer.VolunteerType == "Supply Donor")
            {
                btnVolunteerSupplies.Visibility = Visibility.Visible;
            }
            if (TryNavigateTo(details))
            {
                ResetButtonColors();
                btnVolunteerDetails.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/29
        /// 
        /// Description:
        /// Helper method to safely try to navigate to a new page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>true if successfully navigated page, else false</returns>
        /// (Original Author: Kris Howell pgLocationFrame.xaml.cs)
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
                    this.VolunteerFrame.NavigationService.Navigate(page);
                    return true;
                }
            }

            // no edit ongoing
            this.VolunteerFrame.NavigationService.Navigate(page);
            return true;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Event handler when the Volunteer Schedule button is clicked to 
        /// load the page into the frame
        /// <paramref name="sender"/>
        /// <paramref name="e"/>
        private void btnVolunteerSchedule_Click(object sender, RoutedEventArgs e)
        {
            pgViewVolunteerSchedule schedule = new pgViewVolunteerSchedule(_volunteer, _managerProvider);
            if (TryNavigateTo(schedule))
            {
                ResetButtonColors();
                btnVolunteerSchedule.Background = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
