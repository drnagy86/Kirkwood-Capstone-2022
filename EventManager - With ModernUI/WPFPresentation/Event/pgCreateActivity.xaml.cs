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
using Microsoft.Win32;

namespace WPFPresentation.Event
{
    /// <summary>
    /// Interaction logic for pgCreateActivity.xaml
    /// </summary>
    public partial class pgCreateActivity : Page
    {
        ManagerProvider _managerProvider;
        IActivityManager _activityManager;
        ISublocationManager _sublocationManager;
        ILocationManager _locationManager;
        IImageHelper _imageHelper;
        DataObjects.EventVM _event;
        DataObjects.User _user;
        List<DateTime> _dates = new List<DateTime>();
        string _originalImagePath = "";
        string _oldFileName = "";

        internal pgCreateActivity(DataObjects.User user, DataObjects.EventVM eventParam, ManagerProvider managerProvider)
        {
            _managerProvider = managerProvider;
            _activityManager = managerProvider.ActivityManager;
            _sublocationManager = managerProvider.SublocationManager;
            _locationManager = managerProvider.LocationManager;
            _imageHelper = managerProvider.ImageHelper;
            _user = user;
            _event = eventParam;

            InitializeComponent();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Some validation and load combo box choices on page load
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Raise EditOngoing flag on load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // if event has no event dates, cannot create activity
            if (_event.EventDates.Count == 0)
            {
                MessageBox.Show("This event does not have any registered dates.\n" +
                                "Please add an event date before adding an activity to this event.");
                pgViewActivities viewActivitiesPage = new pgViewActivities(_event, _managerProvider);
                this.NavigationService.Navigate(viewActivitiesPage);
            }

            // if event has no location, cannot create activity
            try
            {
                cboSublocation.ItemsSource = _sublocationManager.RetrieveSublocationsByLocationID((int)_event.LocationID);
                cboSublocation.DisplayMemberPath = "SublocationName";
                txtLocation.Text = _locationManager.RetrieveLocationByLocationID((int)_event.LocationID).Name;
            }
            catch (Exception)
            {
                MessageBox.Show("This event does not have a registered location.\n" +
                                "Please register a location before adding an activity to this event.");
                // pgViewActivities viewActivitiesPage = new pgViewActivities(_event, _managerProvider);
                // this.NavigationService.Navigate(viewActivitiesPage);
            }

            txtEvent.Text = _event.EventName;
            foreach (EventDate eventDate in _event.EventDates)
            {
                _dates.Add(eventDate.EventDateID);
            }
            cboDate.ItemsSource = _dates;

            ValidationHelpers.EditOngoing = true;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Validate form fields and attempt to create activity object and insert it
        /// into the database
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lower EditOngoing flag on successful save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Activity activity = new Activity();
            DateTime startTime;
            DateTime endTime;
            Sublocation sublocation = (Sublocation)cboSublocation.SelectedItem;

            // VALIDATION
            // name is empty
            if (txtName.Text == "")
            {
                MessageBox.Show("Please enter a name for the activity");
                txtName.Focus();
                return;
            }

            // no selected date
            if (cboDate.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a date for the activity");
                cboDate.Focus();
                return;
            }

            // no selected sublocation
            if (cboSublocation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a location area for the activity");
                cboSublocation.Focus();
                return;
            }

            // no selected privacy setting
            if (!(bool)rdoPrivate.IsChecked && !(bool)rdoPublic.IsChecked)
            {
                MessageBox.Show("Please select whether your activity is public or private");
                rdoPublic.Focus();
                return;
            }

            startTime = (DateTime)cboDate.SelectedItem;
            startTime = startTime.AddHours(ucStartTime.Hour);
            startTime = startTime.AddMinutes(ucStartTime.Minutes);
            endTime = (DateTime)cboDate.SelectedItem;
            endTime = endTime.AddHours(ucEndTime.Hour);
            endTime = endTime.AddMinutes(ucEndTime.Minutes);

            // end time before start time
            if (startTime.CompareTo(endTime) >= 0)
            {
                MessageBox.Show("The activity's end time must be after the start time");
                ucStartTime.Focus();
                return;
            }



            activity.EventID = _event.EventID;
            activity.ActivityName = txtName.Text;
            activity.ActivityDescription = txtDescription.Text;
            activity.StartTime = startTime;
            activity.EndTime = endTime;
            activity.EventDateID = (DateTime)cboDate.SelectedItem;
            activity.PublicActivity = (bool)rdoPublic.IsChecked;
            activity.SublocationID = sublocation.SublocationID;
            // Image creation
            activity.ActivityImageName = "";
            if (_originalImagePath != "") // image has been added with button
            {
                try // save image and get new file name to store
                {
                    activity.ActivityImageName = _imageHelper.SaveImageReturnsNewImageName(_oldFileName, _originalImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem saving this image.\n" + ex.Message, "Image Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // save activity to database
            try
            {
                _activityManager.CreateActivity(activity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Activity added successfully");
            ValidationHelpers.EditOngoing = false;

            // return to activities page with new activity
            pgViewActivities viewActivitiesPage = new pgViewActivities(_event, _managerProvider);
            this.NavigationService.Navigate(viewActivitiesPage);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Discard changes and return to the current event's activity itinerary page
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lower EditOngoing flag on successful cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("This will discard any unsaved changes.  Are you sure?",
                                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ValidationHelpers.EditOngoing = false;
                pgViewActivities viewActivitiesPage = new pgViewActivities(_event, _managerProvider);
                this.NavigationService.Navigate(viewActivitiesPage);
            }
        }

        private void btnImageUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (openFile.ShowDialog() == true)
            {
                _originalImagePath = openFile.FileName;
                _oldFileName = openFile.SafeFileName;

                Button button = (Button)sender;
                button.Content = "Added!";
                button.IsEnabled = false;
            }
        }
    }
}
