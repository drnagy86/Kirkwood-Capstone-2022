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
using DataAccessFakes;
using DataObjects;
using LogicLayerInterfaces;
using WPFPresentation.Location;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// Update
    /// Derrick Nagy
    /// Updated: 02/24/2022
    /// 
    /// Description:
    /// Changed the logged in user from internal to public static
    /// 
    /// Update:
    /// Austin Timmerman
    /// Updated: 2022/02/27
    /// 
    /// Description:
    /// Added the ManagerProvider instance variable and modified page parameters
    /// 
    /// Update:
    /// Jace Pettinger
    /// Updated: 2022/05/04
    /// 
    /// Description:
    /// Removed unused buttons
    /// </summary>
    public partial class MainWindow : Window
    {
        //User _user = null;
        //public static User User = null;
        IUserManager _userManager = null;
        IActivityManager _activityManager = null;
        IEventDateManager _eventDateManager = null;
        ILocationManager _locationManager = null;
        ISublocationManager _sublocationManager = null;
        ISupplierManager _supplierManager = null;
        ITaskManager _taskManager = null;
        IVolunteerManager _volunteerManager = null;
        IVolunteerRequestManager _volunteerRequestManager = null;

        ManagerProvider _managerProvider = new ManagerProvider();

        User _user = null;
        
        public MainWindow()
        {

            managerInitializer();

            // Keep this always safe!
            //_user = new User();
            _user = new User()
            {
                UserID = 100000,
                GivenName = "Joanne",
                FamilyName = "Smith",
                EmailAddress = "joanne@company.com",
                State = "IA",
                City = "Cedar Rapids",
                Zip = 52402,
                Active = true
            };

            InitializeComponent();
            this.btnBack.IsEnabled = false;
            
        }

        public MainWindow(User user, IUserManager userManager) : this()
        {
            InitializeComponent();
            this._user = user;
            //MainWindow.User = user;
            //this._user = user;
            //this._userManager = userManager;
            managerInitializer();
            this.mnuUser.Header = user.GivenName + " ▼";
            btnHomeWhite.Visibility = Visibility.Hidden;
            btnHomeYellow.Visibility = Visibility.Visible;
            btnCreateEvents.Foreground = Brushes.White;
            btnViewLocations.Foreground = Brushes.White;
            btnViewSuppliers.Foreground = Brushes.White;
            btnViewVolunteers.Foreground = Brushes.White;
            staLoginMsg.Text = "Welcome " + _user.GivenName + " " + _user.FamilyName + "...";
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Method to initialize the manager objects
        /// 
        /// </summary>
        public void managerInitializer()
        {
            this._userManager = _managerProvider.UserManager;
            this._activityManager = _managerProvider.ActivityManager;
            this._eventDateManager = _managerProvider.EventDateManager;
            this._locationManager = _managerProvider.LocationManager;
            this._sublocationManager = _managerProvider.SublocationManager;
            this._supplierManager = _managerProvider.SupplierManager;
            this._taskManager = _managerProvider.TaskManager;
            this._volunteerManager = _managerProvider.VolunteerManager;
            this._volunteerRequestManager = _managerProvider.VolunteerRequestManager;
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Method to handle UI transition from logged in to logged out states
        /// 
        ///     /// Update
        /// Derrick Nagy
        /// Updated: 02/24/2022
        /// 
        /// Description:
        /// Changed the logged in user from internal to public static
        /// 
        /// </summary>
        private void updateUIForLogout()
        {
            this._user = null;
            //MainWindow.User = null;
            btnHomeWhite.Visibility = Visibility.Hidden;
            btnHomeYellow.Visibility = Visibility.Visible;
            btnCreateEvents.Foreground = Brushes.White;
            btnViewLocations.Foreground = Brushes.White;
            btnViewSuppliers.Foreground = Brushes.White;
            btnViewVolunteers.Foreground = Brushes.White;
            SplashScreen splash = new SplashScreen();
            splash.Show();
            this.Close();
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/1/27
        /// 
        /// Description:
        /// Click event for Logout option. Logs the user out.
        /// 
        /// </summary>
        private void mnuLogOut_Click(object sender, RoutedEventArgs e)
        {
            staLoginMsg.Text = "Please Log in to Continue...";
            this.updateUIForLogout();

        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/1/27
        /// 
        /// Description:
        /// Click event for "Create Event" button. Brings up the Create Event screen.
        /// 
        /// Update
        /// Jace Pettinger
        /// 2022/02/17
        /// Added user parameter for create event constructor
        /// 
        /// Update
        /// Derrick Nagy
        /// Updated: 02/24/2022
        /// 
        /// Description:
        /// Changed the logged in user from internal to public static
        ///
        ///
        /// Update
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// </summary>
        private void btnCreateEvents_Click(object sender, RoutedEventArgs e)
        {
            if(_user == null)
            {
                MessageBox.Show("Please log in to create an event");
            }
            else
            {
                Page page = new pgCreateEvent(_user, _managerProvider);
                if (ValidationHelpers.EditOngoing)
                {
                    MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else // yes, discard changes
                    {
                        ValidationHelpers.EditOngoing = false;
                        
                        this.MainFrame.NavigationService.Navigate(page);
                        return;
                    }
                }
                btnHomeWhite.Visibility = Visibility.Visible;
                btnHomeYellow.Visibility = Visibility.Hidden;
                btnCreateEvents.Foreground = Brushes.Yellow;
                
                // no edit ongoing
                this.MainFrame.NavigationService.Navigate(page);
            }
        }


        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/1/27
        /// 
        /// Description:
        /// Click event for "View My Events" button. Currently brings up the Event List screen
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/02/08
        /// 
        /// Description:
        /// Added option to send user information to the view events page
        /// 
        /// Update
        /// Derrick Nagy
        /// Updated: 02/24/2022
        /// 
        /// Description:
        /// Changed the logged in user from internal to public static
        ///
        /// Update
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// </summary>
        private void btnViewEvents_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
            {
                Page page = new pgViewEvents(_user, _managerProvider);
                if (ValidationHelpers.EditOngoing)
                {
                    MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else // yes, discard changes
                    {
                        ValidationHelpers.EditOngoing = false;

                        this.MainFrame.NavigationService.Navigate(page);
                        return;
                    }
                }

                // no edit ongoing
                this.MainFrame.NavigationService.Navigate(page);
            }
            else
            {
                Page page = new pgViewEvents(_managerProvider);
                this.MainFrame.NavigationService.Navigate(page);
            }

        }

        private void btnViewVolunteers_Click(object sender, RoutedEventArgs e)
        {
            Page page = new pgViewAllVolunteers(_managerProvider);
            if (ValidationHelpers.EditOngoing)
            {
                MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else // yes, discard changes
                {
                    ValidationHelpers.EditOngoing = false;

                    this.MainFrame.NavigationService.Navigate(page);
                    return;
                }
            }

            // no edit ongoing
            this.MainFrame.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/2/4
        /// 
        /// Description:
        /// Click event for the back button. Navigates to the previous screen and handles button enabling.
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Discard changes warning if EditOngoing flag is raised when clicking back
        /// Lowers EditOngoing flag if changes are discarded
        /// </summary>
        /// <param name="sender">The back button</param>
        /// <param name="e">Arguments passed as part of the event</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(this.MainFrame.NavigationService.CanGoBack)
            {
                if (ValidationHelpers.EditOngoing)
                {
                    MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else // yes, discard changes
                    {
                        ValidationHelpers.EditOngoing = false;
                        this.MainFrame.GoBack();
                        if (!this.MainFrame.NavigationService.CanGoBack)
                        {
                            this.btnBack.IsEnabled = false;
                        }
                    }
                }
                else
                {
                    this.MainFrame.GoBack();
                    if (!this.MainFrame.NavigationService.CanGoBack)
                    {
                        this.btnBack.IsEnabled = false;
                    }
                }
            } else
            {
                this.btnBack.IsEnabled = false;
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Updated: 2022/04/17
        /// 
        /// Description:
        /// Buttons now light up the way that they should.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            
            if (MainFrame.Content.ToString().Contains("Event") || MainFrame.Content.ToString().Contains("Task") || MainFrame.Content.ToString().Contains("Activity"))
            {
                btnHomeWhite.Visibility = Visibility.Visible;
                btnHomeYellow.Visibility = Visibility.Hidden;
                btnViewEvents.Foreground = Brushes.Yellow;
                btnCreateEvents.Foreground = Brushes.White;
                btnViewLocations.Foreground = Brushes.White;
                btnViewSuppliers.Foreground = Brushes.White;
                btnViewVolunteers.Foreground = Brushes.White;
                staCurrentPageMsg.Text = "View Events";
            }
            if (MainFrame.Content.ToString().Contains("Supplier"))
            {
                btnHomeWhite.Visibility = Visibility.Visible;
                btnHomeYellow.Visibility = Visibility.Hidden;
                btnCreateEvents.Foreground = Brushes.White;
                btnViewEvents.Foreground = Brushes.White;
                btnViewLocations.Foreground = Brushes.White;
                btnViewSuppliers.Foreground = Brushes.Yellow;
                btnViewVolunteers.Foreground = Brushes.White;
                staCurrentPageMsg.Text = "Suppliers";
            }
            if (MainFrame.Content.ToString().Contains("Location") || MainFrame.Content.ToString().Contains("Parking"))
            {
                btnHomeWhite.Visibility = Visibility.Visible;
                btnHomeYellow.Visibility = Visibility.Hidden;
                btnCreateEvents.Foreground = Brushes.White;
                btnViewEvents.Foreground = Brushes.White;
                btnViewLocations.Foreground = Brushes.Yellow;
                btnViewSuppliers.Foreground = Brushes.White;
                btnViewVolunteers.Foreground = Brushes.White;
                staCurrentPageMsg.Text = "Locations";
            }
            if (MainFrame.Content.ToString().Contains("Volunteer"))
            {
                btnHomeWhite.Visibility = Visibility.Visible;
                btnHomeYellow.Visibility = Visibility.Hidden;
                btnCreateEvents.Foreground = Brushes.White;
                btnViewEvents.Foreground = Brushes.White;
                btnViewLocations.Foreground = Brushes.White;
                btnViewSuppliers.Foreground = Brushes.White;
                btnViewVolunteers.Foreground = Brushes.Yellow;
                staCurrentPageMsg.Text = "Volunteers";
            }
            if (MainFrame.Content.ToString().Contains("CreateEvent"))
            {
                btnHomeWhite.Visibility = Visibility.Visible;
                btnHomeYellow.Visibility = Visibility.Hidden;
                btnCreateEvents.Foreground = Brushes.Yellow;
                btnViewEvents.Foreground = Brushes.White;
                btnViewLocations.Foreground = Brushes.White;
                btnViewSuppliers.Foreground = Brushes.White;
                btnViewVolunteers.Foreground = Brushes.White;
                staCurrentPageMsg.Text = "Event Creation";
            }
            if (MainFrame.Content.ToString().Contains("About"))
            {
                btnHomeWhite.Visibility = Visibility.Visible;
                btnHomeYellow.Visibility = Visibility.Hidden;
                btnCreateEvents.Foreground = Brushes.White;
                btnViewEvents.Foreground = Brushes.White;
                btnViewLocations.Foreground = Brushes.White;
                btnViewSuppliers.Foreground = Brushes.White;
                btnViewVolunteers.Foreground = Brushes.White;
                staCurrentPageMsg.Text = "About";
            }
            if (this.MainFrame.NavigationService.CanGoBack)
            {
                this.btnBack.IsEnabled = true;
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
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewLocations_Click(object sender, RoutedEventArgs e)
        {
            var page = new pgViewLocations(_managerProvider, _user);
            if (ValidationHelpers.EditOngoing)
            {
                MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else // yes, discard changes
                {
                    ValidationHelpers.EditOngoing = false;

                    this.MainFrame.NavigationService.Navigate(page);
                    return;
                }
            }

            // no edit ongoing
            this.MainFrame.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Original author and creation date missing.
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Added user to page constructor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var page = new pgViewSuppliers(_managerProvider, _user);
            if (ValidationHelpers.EditOngoing)
            {
                MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else // yes, discard changes
                {
                    ValidationHelpers.EditOngoing = false;

                    this.MainFrame.NavigationService.Navigate(page);
                    return;
                }
            }

            // no edit ongoing
            this.MainFrame.NavigationService.Navigate(page);
        }

       /// <summary>
        /// Jace Pettinger
        /// Created: 2022/3/20
        /// 
        /// Description:
        /// Click event for the home button. Navigates to my events.
        /// 
        /// </summary>
        /// <param name="sender">The back button</param>
        /// <param name="e">Arguments passed as part of the event</param>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Page myEventsPage;


            myEventsPage = new pgViewEvents(_managerProvider);

            /*else
            {
                myEventsPage = new pgViewEvents(_user, _managerProvider);
             */

            this.MainFrame.NavigationService.Navigate(myEventsPage);
        }

        
    }
}
