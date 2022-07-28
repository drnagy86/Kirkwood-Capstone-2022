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
using LogicLayer;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessFakes;

namespace WPFPresentation.Event
{
    /// <summary>
    /// Mike Cahow
    /// Create: 2022/02/21
    /// 
    /// Description:
    /// Interaction logic for pgViewActivityDetails.xaml
    /// 
    /// Update:
    /// Austin Timmerman
    /// Updated: 2022/02/27
    /// 
    /// Description:
    /// Added the ManagerProvider instance variable and modified page parameters
    /// </summary>
    public partial class pgViewActivityDetails : Page
    {
        IActivityManager _activityManager = null;
        ISublocationManager _sublocationManager = null;
        ManagerProvider _managerProvider = null;
        DataObjects.ActivityVM _activity = null;
        DataObjects.EventVM _event = null;

        User _user = null;

        internal pgViewActivityDetails(DataObjects.ActivityVM selectedActivity, DataObjects.EventVM activityEvent, 
            ManagerProvider managerProvider, User user)
        {
            _managerProvider = managerProvider;
            _activityManager = managerProvider.ActivityManager;

            _sublocationManager = managerProvider.SublocationManager;
            _activity = selectedActivity;
            _event = activityEvent;
            _user = user;


            InitializeComponent();
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/21
        /// 
        /// Description:
        /// Load event handler to populate empty fields with data from selected Activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            setViewActivitiesDetailsMode();
        }

        /// <summary>
        /// Mike Cahow
        /// Created 2022/02/21
        /// 
        /// Description:
        /// Method used to populate the text boxes
        /// </summary>
        private void populateControls()
        {
            lblActivityName.Text = _activity.ActivityName;
            txtActivityDescripiton.Text = _activity.ActivityDescription;
            if (_activity.PublicActivity == true)
            {
                radYesPublicActivity.IsChecked = true;
            }
            else
            {
                radNoPublicActivity.IsChecked = true;
            }
            txtStartTime.Text = _activity.StartTime.ToShortTimeString();
            txtEndTime.Text = _activity.EndTime.ToShortTimeString();
            txtEventSublocation.Text = _activity.SublocationName;
            txtEventDate.Text = _activity.EventDateID.ToShortDateString();
            txtEventName.Text = _event.EventName;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/21
        /// 
        /// Description:
        /// Method used to set all populated data to read only
        /// </summary>
        private void setViewActivitiesDetailsMode()
        {
            populateControls();

            txtActivityDescripiton.IsReadOnly = true;
            radYesPublicActivity.IsEnabled = false;
            radNoPublicActivity.IsEnabled = false;
            txtStartTime.IsReadOnly = true;
            txtEndTime.IsReadOnly = true;
            txtEventSublocation.IsReadOnly = true;
            txtEventDate.IsReadOnly = true;
            txtEventName.IsReadOnly = true;
        }
    }
}
