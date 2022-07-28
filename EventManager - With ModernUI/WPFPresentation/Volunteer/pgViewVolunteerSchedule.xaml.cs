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

namespace WPFPresentation
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/30
    /// 
    /// Interaction logic for pgViewVolunteerSchedule.xaml
    /// </summary>
    public partial class pgViewVolunteerSchedule : Page
    {
        ManagerProvider _managerProvider = null;
        Volunteer _volunteer = null;
        IVolunteerManager _volunteerManager = null;
        IEventDateManager _eventDateManager = null;
        List<Availability> _selectedDateAvailabilities = null;
        List<EventDateVM> _eventDates = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Constructor that sets the _volunteer, _managerProvider, and _volunteerManager
        /// </summary>
        /// <param name="volunteer"></param>
        /// <param name="managerProvider"></param>
        internal pgViewVolunteerSchedule(Volunteer volunteer, ManagerProvider managerProvider)
        {
            _managerProvider = managerProvider;
            _volunteerManager = managerProvider.VolunteerManager;
            _eventDateManager = managerProvider.EventDateManager;
            _volunteer = volunteer;

            InitializeComponent();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// When the "Schedule" button is clicked, the volunteer's schedule will populate the screen
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtVolunteerSchedule.Text = _volunteer.GivenName + "'s Schedule";

            blackOutCalendarDates();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// The helper method that loads the specific volunteer's schedule
        /// </summary>
        //private void loadLocationSchedule()
        //{
        //    if (ValidationHelpers.EditOngoing)
        //    {
        //        MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        //        if (result == MessageBoxResult.No)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            ValidationHelpers.EditOngoing = false;
        //        }
        //    }

        //    txtVolunteerSchedule.Text = _volunteer.GivenName + "'s Schedule";
        //    //lblBookings.Text = "Booked Events";
        //    //_eventDatesForLocation = _eventDateManager.RetrieveEventDatesByLocationID(_location.LocationID);
        //}

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
            //List<EventDateVM> eventDatesForDataGrid = new List<EventDateVM>();

            //foreach (EventDateVM eventDate in _eventDatesForLocation)
            //{
                //if (eventDate.EventDateID == calLocationCalendar.SelectedDate)
                //{
                    //eventDatesForDataGrid.Add(eventDate);
                //}
            //}

            //datLocationSchedule.ItemsSource = eventDatesForDataGrid;
            //DateTime date = calLocationCalendar.SelectedDate.Value.Date;

            //lblLocationDate.Text = date.ToString("MMMM dd, yyyy");

            //List<Activity> activitiesForDataGrid = new List<Activity>();
            //foreach (Activity activity in _activitiesForSublocation)
            //{
            //    if (activity.EventDateID == calLocationCalendar.SelectedDate)
            //    {
            //        activitiesForDataGrid.Add(activity);
            //    }
            //}

            //datLocationSchedule.ItemsSource = activitiesForDataGrid;

            DateTime date = calVolunteerCalendar.SelectedDate.Value.Date;

            lblVolunteerDate.Text = date.ToString("MMMM dd, yyyy");


            _selectedDateAvailabilities = _volunteerManager.RetrieveAvailabilityByVolunteerIDAndDate(_volunteer.VolunteerID, (DateTime)calVolunteerCalendar.SelectedDate);


            datVolunteerAvailabilities.ItemsSource = new ObservableCollection<Availability>(from a in _selectedDateAvailabilities
                                                                                           orderby a.TimeStart ascending
                                                                                           select a);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// The helper method that blacks out any dates that are not available for the current volunteer
        /// for the visible month and half of the following month and half of the previous month (so the user 
        /// is not able to select a date that otherwise would not be able to be selected)
        /// 
        /// </summary>
        private void blackOutCalendarDates()
        {
            int month = calVolunteerCalendar.DisplayDate.Month;
            int year = calVolunteerCalendar.DisplayDate.Year;
            CalendarBlackoutDatesCollection calendarDateRanges = calVolunteerCalendar.BlackoutDates;
            calendarDateRanges.Clear();
            this.Cursor = Cursors.Wait;

            //BLACK OUT CURRENT MONTH THAT IS BEING VIEWED
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DateTime date = new DateTime(year, month, i);
                List<Availability> availability = _volunteerManager.RetrieveAvailabilityByVolunteerIDAndDate(_volunteer.VolunteerID, date);

                if (availability.Count == 0 || availability[0].TimeStart == null)
                {
                    calVolunteerCalendar.BlackoutDates.Add(new CalendarDateRange(date));
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
                List<Availability> availability = _volunteerManager.RetrieveAvailabilityByVolunteerIDAndDate(_volunteer.VolunteerID, date);

                if (availability.Count == 0 || availability[0].TimeStart == null)
                {
                    calVolunteerCalendar.BlackoutDates.Add(new CalendarDateRange(date));
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
                List<Availability> availability = _volunteerManager.RetrieveAvailabilityByVolunteerIDAndDate(_volunteer.VolunteerID, date);

                if (availability.Count == 0 || availability[0].TimeStart == null)
                {
                    calVolunteerCalendar.BlackoutDates.Add(new CalendarDateRange(date));
                }

            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// When the user selects a date on the calendar, the data grid below will shows the volunteer's
        /// schedule (events planned for that day).
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void calVolunteerCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            loadCalendarData();
            _eventDates = _eventDateManager.RetrieveEventDatesByUserIDAndDate(_volunteer.UserID, calVolunteerCalendar.SelectedDate.Value.Date);
            datVolunteerEvents.ItemsSource = _eventDates;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Event handler that will black out the months when the display dates (going to a different 
        /// month or year) 
        /// 
        /// </summary>
        private void calVolunteerCalendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            blackOutCalendarDates();
        }
    }
}
