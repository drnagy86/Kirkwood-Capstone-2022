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
    /// Interaction logic for pgViewActivities.xaml
    /// </summary>
    public partial class pgViewActivities : Page
    {
        IActivityManager _activityManager = null;
        ISublocationManager _sublocationManager = null;
        ManagerProvider _managerProvider = null;
        DataObjects.EventVM _event;
        List<ActivityVM> activities;
        User _user;
        ActivityFilter activityFilter;

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
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
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Removed constructors that were for "all activities all events" button which was removed
        /// </summary>
        /// <param name="eventParam"></param>
        /// <param name="managerProvider"></param>
        internal pgViewActivities(DataObjects.EventVM eventParam, ManagerProvider managerProvider)
        {
            _event = eventParam;
            _managerProvider = managerProvider;
            _sublocationManager = managerProvider.SublocationManager;
            _activityManager = managerProvider.ActivityManager;

            InitializeComponent();
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Handler for loading activities
        /// 
        /// Logan Baccam
        /// Updated: 2022/02/25
        /// Description:
        /// Updated loading activities to data grid 
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                setActivityFilterEnum();
                
                if (_event is null)
                {
                    lblActivityEventName.Content = "All Activities";
                    activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                    datEventActivities.ItemsSource = activities;
                }
            
                else
                {
                    radAllActivities.Visibility = Visibility.Hidden;
                    stckFilterItems.Height = 115;
                    lblActivityEventName.Content =_event.EventName + " Activities";
                    activities = _activityManager.RetrieveActivitiesByEventIDForVM(_event.EventID);
                    datEventActivities.ItemsSource = activities;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/21
        /// 
        /// Description:
        /// Click event handler to launch pgViewActivityDetails.xaml
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/02/26
        /// 
        /// Description:
        /// Added _user to be passed with page constructor
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datEventActivities_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datEventActivities.SelectedItem == null)
            {
                return;
            }
            ActivityVM selectedActivity = (ActivityVM)datEventActivities.SelectedItem;
            pgViewActivityDetails activityDetailsPage = new pgViewActivityDetails(selectedActivity, _event, _managerProvider, _user);
            this.NavigationService.Navigate(activityDetailsPage);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Handler for when changing filter from the radio buttons
        /// </summary>
        private void radActivityFilterClick(object sender, RoutedEventArgs e)
        {
            setActivityFilterEnum();
            switch (activityFilter)
            {
                case ActivityFilter.ActivityName:
                    if (_event is null)
                    {
                        txtSearch.Visibility = Visibility.Visible;
                        btnFind.Visibility = Visibility.Visible;
                        datePickerActivityDate.Visibility = Visibility.Hidden;
                        lblSearch.Visibility = Visibility.Visible;
                        activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                        datEventActivities.ItemsSource = activities;
                        txtSearch.Focus();
                    }
                    else
                    {
                        txtSearch.Visibility = Visibility.Visible;
                        btnFind.Visibility = Visibility.Visible;
                        datePickerActivityDate.Visibility = Visibility.Hidden;
                        lblSearch.Visibility = Visibility.Visible;
                        activities = _activityManager.RetrieveActivitiesByEventIDForVM(_event.EventID);
                        datEventActivities.ItemsSource = activities;
                        txtSearch.Focus();
                    }
                    break;
                case ActivityFilter.ActivityDate:
                    if (_event is null)
                    {
                        lblSearch.Visibility = Visibility.Visible;
                        txtSearch.Visibility = Visibility.Hidden;
                        datePickerActivityDate.Visibility = Visibility.Visible;
                        btnFind.Visibility = Visibility.Visible;
                        activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                        datEventActivities.ItemsSource = activities;
                        datePickerActivityDate.Focus();
                    }
                    else 
                    {
                        lblSearch.Visibility = Visibility.Visible;
                        txtSearch.Visibility = Visibility.Hidden;
                        datePickerActivityDate.Visibility = Visibility.Visible;
                        btnFind.Visibility = Visibility.Visible;
                        activities = _activityManager.RetrieveActivitiesByEventIDForVM(_event.EventID);
                        datEventActivities.ItemsSource = activities;
                        datePickerActivityDate.Focus();
                    }
                    break;
                case ActivityFilter.ActivitySublocation:
                    if (_event is null)
                    {
                        lblSearch.Visibility = Visibility.Visible;
                        txtSearch.Visibility = Visibility.Visible;
                        btnFind.Visibility = Visibility.Visible;
                        datePickerActivityDate.Visibility = Visibility.Hidden;
                        activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                        datEventActivities.ItemsSource = activities;
                        txtSearch.Focus();
                    }
                    else 
                    {
                        lblSearch.Visibility = Visibility.Visible;
                        txtSearch.Visibility = Visibility.Visible;
                        btnFind.Visibility = Visibility.Visible;
                        datePickerActivityDate.Visibility = Visibility.Hidden;
                        activities = _activityManager.RetrieveActivitiesByEventIDForVM(_event.EventID);
                        datEventActivities.ItemsSource = activities;
                        txtSearch.Focus();
                    }
                    break;
                case ActivityFilter.AllActivities:
                        txtSearch.Visibility = Visibility.Hidden;
                        lblSearch.Visibility = Visibility.Hidden;
                        datePickerActivityDate.Visibility = Visibility.Hidden;
                        btnFind.Visibility = Visibility.Hidden;
                        lblActivityEventName.Content = "All Activities";
                        activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
                        datEventActivities.ItemsSource = activities;
                        break;
                    
            }
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// method for setting the activity filter enum
        /// </summary>
        public void setActivityFilterEnum()
        {
            txtSearch.Focus();
            if (radActivityDateFilter.IsChecked == true)
            {
                txtSearch.Visibility = Visibility.Hidden;
                datePickerActivityDate.Visibility = Visibility.Visible;
                activityFilter = ActivityFilter.ActivityDate;
            }
            if (radActivtyNameFilter.IsChecked == true)
            {

                activityFilter = ActivityFilter.ActivityName;
            }
            if (radActivitySublocationFilter.IsChecked == true)
            {
                activityFilter = ActivityFilter.ActivitySublocation;
            }
            if (radAllActivities.IsChecked == true)
            {
                activityFilter = ActivityFilter.AllActivities;

            }
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Enumerations for activity filter
        /// </summary>
        enum ActivityFilter
        {
            ActivityDate,
            ActivityName,
            ActivitySublocation,
            AllActivities
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Handler for returning a filtered list to the data grid
        /// </summary>
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<ActivityVM> filiteredActivities = new List<ActivityVM>();

            switch (activityFilter)
            {
                case ActivityFilter.ActivityDate:
                    try
                    {
                        foreach (ActivityVM activity in activities)
                        {
                            if (datePickerActivityDate.SelectedDate.Value.CompareTo(activity.EventDateID) == 0)
                            {
                                filiteredActivities.Add(activity);
                            }
                        }
                        datEventActivities.ItemsSource = filiteredActivities;
                        datePickerActivityDate.Text = "";
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please make sure the date you entered is in the format: \ndd-mm-yyyy with no leading zeros.");
                        datePickerActivityDate.Focus();
                    }
                    break;
                case ActivityFilter.ActivityName:
                    try
                    {
                        foreach (ActivityVM activity in activities)
                        {
                            if (txtSearch.Text.ToString().ToLower().CompareTo(activity.ActivityName.ToLower()) == 0)
                            {
                                filiteredActivities.Add(activity);
                            }
                        }
                        datEventActivities.ItemsSource = filiteredActivities;
                        txtSearch.Text = "";

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid Name.");
                        txtSearch.Focus();
                    }
                    break;

                case ActivityFilter.ActivitySublocation:
                    try
                    {
                        foreach (ActivityVM activity in activities)
                        {
                            if (txtSearch.Text.ToString().ToLower().CompareTo(activity.SublocationName.ToLower()) == 0)
                            {
                                filiteredActivities.Add(activity);
                            }
                        }
                        datEventActivities.ItemsSource = filiteredActivities;
                        txtSearch.Text = "";

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid Sublocation.");
                        txtSearch.Focus();
                    }
                    break;
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Click handler for button to add activity to current event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddActivity_Click(object sender, RoutedEventArgs e)
        {
            pgCreateActivity CreateActivityPage = new pgCreateActivity(_user, _event, _managerProvider);
            this.NavigationService.Navigate(CreateActivityPage);
        }
    }
}
