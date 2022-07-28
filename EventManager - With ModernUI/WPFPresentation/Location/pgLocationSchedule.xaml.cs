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
using System.Collections.ObjectModel;

namespace WPFPresentation.Location
{
    /// <summary>
    /// Interaction logic for pgLocationSchedule.xaml
    /// 
    /// Kris Howell
    /// Created: 2022/03/24
    /// 
    /// Description:
    /// Pulled from old pgViewLocationDetails page while separating functions
    /// into separate pages
    /// 
    /// Jace Pettinger
    /// Created: 2022/05/04
    /// 
    /// Description:
    /// Removed unused buttons
    /// into separate pages
    /// </summary>
    public partial class pgLocationSchedule : Page
    {
        ManagerProvider _managerProvider;
        IActivityManager _activityManager;
        IEventManager _eventManager;
        IEventDateManager _eventDateManager;
        ILocationManager _locationManager;
        ISublocationManager _sublocationManager;

        DataObjects.Location _location;
        List<EventDateVM> _eventDatesForLocation;
        List<Sublocation> _sublocations;
        List<Activity> _activitiesForSublocation;
        List<Availability> _selectedDateAvailabilities = new List<Availability>();
        int _sublocationID;

        internal pgLocationSchedule(ManagerProvider managerProvider, DataObjects.Location location)
        {
            _activityManager = managerProvider.ActivityManager;
            _eventManager = managerProvider.EventManager;
            _eventDateManager = managerProvider.EventDateManager;
            _locationManager = managerProvider.LocationManager;
            _sublocationManager = managerProvider.SublocationManager;
            _location = location;
            _eventDatesForLocation = _eventDateManager.RetrieveEventDatesByLocationID(_location.LocationID);
            _sublocations = _sublocationManager.RetrieveSublocationsByLocationID(_location.LocationID);

            InitializeComponent();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// When the user selects a date on the calendar, the data grid below will shows the location's
        /// schedule (events planned for that day).
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void calLocationCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            loadCalendarData();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// When the user selects a location or sublocation from the combo box drop down, 
        /// the data grid below will shows the location's / sublocation's schedule (events planned for that day).
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void cboSchedulePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboSchedulePicker.SelectedItem.Equals(_location.Name))
                {
                    colActivityName.Visibility = Visibility.Collapsed;
                    colEventName.Visibility = Visibility.Visible;
                    loadLocationSchedule();
                    //loadCalendarData();
                }
                else
                {
                    colActivityName.Visibility = Visibility.Visible;
                    colEventName.Visibility = Visibility.Collapsed;
                    _sublocationID = _sublocations.First(m => m.SublocationName == cboSchedulePicker.SelectedItem.ToString()).SublocationID;
                    loadSublocationSchedule();
                    //loadCalendarData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            if(calLocationCalendar.SelectedDate != null)
            {
                loadCalendarData();
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created 2022/03/01
        /// 
        /// Description: Button to delete the schedule item in the same line
        /// </summary>
        private void btnDeleteScheduleItem_Click(object sender, RoutedEventArgs e)
        {
            EventDateVM selectedEventDateVM = new EventDateVM();
            Activity selectedActivity = new Activity();
            int selectedEventID = 0;
            bool isEvent = true;
            if (datLocationSchedule.SelectedItem.GetType().Equals(selectedEventDateVM.GetType()))
            {
                selectedEventDateVM = (EventDateVM)datLocationSchedule.SelectedItem;
                selectedEventID = selectedEventDateVM.EventID;
                isEvent = true;
            }
            else if (datLocationSchedule.SelectedItem.GetType().Equals(selectedActivity.GetType()))
            {
                selectedActivity = (Activity)datLocationSchedule.SelectedItem;
                selectedEventID = selectedActivity.ActivityID;
                isEvent = false;
            }

            if (MessageBox.Show("Delete Schedule Item", "Are You Sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                MessageBox.Show("Scheduled Item was not Deleted.");
            }
            else
            {
                try
                {
                    if (isEvent)
                    {
                        _eventManager.UpdateEventLocationByEventID(selectedEventID, _location.LocationID, null);
                        _eventDatesForLocation.Remove(selectedEventDateVM);
                    }
                    else
                    {
                        _activityManager.UpdateActivitySublocationByActivityID(selectedEventID, _sublocationID, null);
                        _activitiesForSublocation.Remove(selectedActivity);
                    }
                    _eventManager.UpdateEventLocationByEventID(selectedEventID, _location.LocationID, null);
                    _eventDatesForLocation.Remove(selectedEventDateVM);
                    loadCalendarData();
                    loadLocationSchedule();
                    loadSublocationSchedule();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Could not Delete Schedule Item!", ex.Message);
                }
            }



        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// When the "Site Schedule" button is clicked, the location's schedule will populate the screen
        /// 
        /// Update:
        /// Kris Howell
        /// Updated: 2022/03/24
        /// 
        /// Description:
        /// Imported to new page for location page frame restructure
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtLocationNamesSchedule.Text = _location.Name + "'s Schedule";

            if (cboSchedulePicker.Items.Count == 0)
            {
                cboSchedulePicker.Items.Add(_location.Name);

                foreach (Sublocation sublocation in _sublocations)
                {
                    cboSchedulePicker.Items.Add(sublocation.SublocationName);
                }
            }

            blackOutCalendarDates();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// The helper method that loads the specific sublocation's schedule
        /// </summary>
        private void loadSublocationSchedule()
        {
            txtLocationNamesSchedule.Text = cboSchedulePicker.SelectedItem + "'s Schedule";
            lblBookings.Text = "Booked Activities";
            _activitiesForSublocation = _activityManager.RetrieveActivitiesBySublocationID(_sublocationID);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// The helper method that loads the specific location's schedule
        /// </summary>
        private void loadLocationSchedule()
        {
            if (ValidationHelpers.EditOngoing)
            {
                MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    ValidationHelpers.EditOngoing = false;
                }
            }

            txtLocationNamesSchedule.Text = _location.Name + "'s Schedule";
            lblBookings.Text = "Booked Events";
            _eventDatesForLocation = _eventDateManager.RetrieveEventDatesByLocationID(_location.LocationID);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// The helper method that loads the calendars data
        /// 
        /// Update:
        /// Kris Howell
        /// Updated: 2022/03/24
        /// 
        /// Description:
        /// Imported to new page for location page frame restructure
        /// </summary>
        private void loadCalendarData()
        {
            if (cboSchedulePicker.SelectedItem.Equals(_location.Name))
            {
                List<EventDateVM> eventDatesForDataGrid = new List<EventDateVM>();

                foreach (EventDateVM eventDate in _eventDatesForLocation)
                {
                    if (eventDate.EventDateID == calLocationCalendar.SelectedDate)
                    {
                        eventDatesForDataGrid.Add(eventDate);
                    }
                }

                datLocationSchedule.ItemsSource = eventDatesForDataGrid;
                DateTime date = calLocationCalendar.SelectedDate.Value.Date;

                lblLocationDate.Text = date.ToString("MMMM dd, yyyy");
            }
            else
            {
                List<Activity> activitiesForDataGrid = new List<Activity>();
                foreach (Activity activity in _activitiesForSublocation)
                {
                    if (activity.EventDateID == calLocationCalendar.SelectedDate)
                    {
                        activitiesForDataGrid.Add(activity);
                    }
                }

                datLocationSchedule.ItemsSource = activitiesForDataGrid;
                DateTime date = calLocationCalendar.SelectedDate.Value.Date;

                lblLocationDate.Text = date.ToString("MMMM dd, yyyy");
            }

            _selectedDateAvailabilities = _locationManager.RetrieveLocationAvailabilityByLocationIDAndDate(_location.LocationID, (DateTime)calLocationCalendar.SelectedDate);


            datLocationAvailabilities.ItemsSource = new ObservableCollection<Availability>(from a in _selectedDateAvailabilities
                                                                                           orderby a.TimeStart ascending
                                                                                           select a);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// The helper method that blacks out any dates that are not available for the current location
        /// for the visible month and half of the following month and half of the previous month (so the user 
        /// is not able to select a date that otherwise would not be able to be selected)
        /// 
        /// </summary>
        private void blackOutCalendarDates()
        {
            int month = calLocationCalendar.DisplayDate.Month;
            int year = calLocationCalendar.DisplayDate.Year;
            CalendarBlackoutDatesCollection calendarDateRanges = calLocationCalendar.BlackoutDates;
            calendarDateRanges.Clear();
            this.Cursor = Cursors.Wait;

            //BLACK OUT CURRENT MONTH THAT IS BEING VIEWED
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DateTime date = new DateTime(year, month, i);
                List<Availability> availability = _locationManager.RetrieveLocationAvailabilityByLocationIDAndDate(_location.LocationID, date);

                if (availability.Count == 0 || availability[0].TimeStart == null)
                {
                    calLocationCalendar.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }

            if (month + 1 > 12)
            {
                year++;
                month = 1;
            }
            else
            {
                month++;
            }
            // BLACK OUT NEXT MONTH ON CALENDAR
            // SHORTEN THE DAYS TO ONLY BE THE FIRST 15 (only the first few days are visible)
            for (int i = 1; i <= DateTime.DaysInMonth(year, month) - 15; i++)
            {
                DateTime date = new DateTime(year, month, i);
                List<Availability> availability = _locationManager.RetrieveLocationAvailabilityByLocationIDAndDate(_location.LocationID, date);

                if (availability.Count == 0 || availability[0].TimeStart == null)
                {
                    calLocationCalendar.BlackoutDates.Add(new CalendarDateRange(date));
                }

            }



            // LOGIC TO GO BACK A MONTH / TWO MONTHS (most likely can be changed to 
            // month = calendar.DisplayDate.Month - 1)
            if (month - 1 < 1)
            {
                year--;
                month = 11;
            }
            else if (month - 2 < 1)
            {
                year--;
                month = 12;
            }
            else
            {
                month -= 2;
            }
            // BLACK OUT PREVIOUS MONTH ON CALENDAR
            // SHORTEN THE DAYS TO ONLY BE THE LAST 15 (only the last few days are visible)
            for (int i = 15; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DateTime date = new DateTime(year, month, i);
                List<Availability> availability = _locationManager.RetrieveLocationAvailabilityByLocationIDAndDate(_location.LocationID, date);

                if (availability.Count == 0 || availability[0].TimeStart == null)
                {
                    calLocationCalendar.BlackoutDates.Add(new CalendarDateRange(date));
                }

            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Event handler that will black out the months when the display dates (going to a different 
        /// month or year) 
        /// 
        /// </summary>
        private void calLocationCalendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            blackOutCalendarDates();
        }
    }
}
