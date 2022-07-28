using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
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

namespace WPFPresentation.Event
{
    /// <summary>
    /// Interaction logic for pgViewEventDateActivities.xaml
    /// </summary>
    public partial class pgViewEventDateActivities : Page
    {
        IActivityManager _activityManager = null;
        ManagerProvider _managerProvider = null;
        DataObjects.Event _event;
        EventDate _eventDate;

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Initializes component and sets up activity manager with fake or default accessors
        /// 
        /// Update:
        /// Austin Timmerman
        /// Updated: 2022/02/27
        /// 
        /// Description:
        /// Added the ManagerProvider instance variable and modified page parameters
        /// </summary>
        /// <param name="eventParam"></param>
        /// <param name="eventDate"></param>
        /// <param name="managerProvider"></param>
        internal pgViewEventDateActivities(DataObjects.Event eventParam, EventDate eventDate, ManagerProvider managerProvider)
        {
            _managerProvider = managerProvider;
            _event = eventParam;
            _eventDate = eventDate;
            _activityManager = managerProvider.ActivityManager;
            InitializeComponent();
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Handler for loading activities
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_eventDate != null)
                {
                    DateTime _eventDateID = (DateTime)_eventDate.EventDateID;
                    lblActivityEventName.Content = _event.EventName + " Activities for " + _eventDateID.ToLongDateString();
                }
                else
                {
                    lblActivityEventName.Content = _event.EventName + " Activities";
                }
                List<ActivityVM> activities = _activityManager.RetrieveActivitiesByEventIDAndEventDateID(_event.EventID, _eventDate.EventDateID);

                // if there are no activities to show, display text to tell the user.
                if (activities.Count == 0)
                {
                    lblNoActivities.Content = "No activities planned yet. Use the Add button to add activities to this day.";
                }

                datEventDateActivities.ItemsSource = activities;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
