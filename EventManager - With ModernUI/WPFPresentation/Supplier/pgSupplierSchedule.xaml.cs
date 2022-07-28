using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPFPresentation.Supplier
{
    /// <summary>
    /// Interaction logic for pgSupplierSchedule.xaml
    /// </summary>
    public partial class pgSupplierSchedule : Page
    {
        private ManagerProvider _managerProvider;
        private DataObjects.Supplier _supplier;
        private ISupplierManager _supplierManager;
        private IActivityManager _activityManager;

        List<Availability> selectedDateAvailabilities = new List<Availability>();
        List<ActivityVM> selectedDateActivities = new List<ActivityVM>();


        internal pgSupplierSchedule(ManagerProvider managerProvider, DataObjects.Supplier supplier)
        {
            _managerProvider = managerProvider;
            _supplier = supplier;
            _supplierManager = managerProvider.SupplierManager;
            _activityManager = managerProvider.ActivityManager;

            InitializeComponent();

            // TO ALLOW FOR BLACKOUT DATES
            calSupplierCalendar.SelectedDate = null;
        }

        private void calSupplierCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDateAvailabilities = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, (DateTime)calSupplierCalendar.SelectedDate);
            selectedDateActivities = _activityManager.RetrieveActivitiesBySupplierIDAndDate(_supplier.SupplierID, (DateTime)calSupplierCalendar.SelectedDate);

            DateTime date = calSupplierCalendar.SelectedDate.Value.Date;
            lblSupplierDate.Text = date.ToString("MMMM dd, yyyy");
            datSupplierAvailabilities.ItemsSource = new ObservableCollection<Availability>(from a in selectedDateAvailabilities
                                                                                           orderby a.TimeStart ascending
                                                                                           select a);
            datSupplierActivities.ItemsSource = selectedDateActivities;
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
        private void calSupplierCalendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            blackOutCalendarDates();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtSupplierSchedule.Text = _supplier.Name + "'s Schedule";
            // BLACK OUT CALENDAR DATES INITIAL MONTH
            blackOutCalendarDates();
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
            int month = calSupplierCalendar.DisplayDate.Month;
            int year = calSupplierCalendar.DisplayDate.Year;
            CalendarBlackoutDatesCollection calendarDateRanges = calSupplierCalendar.BlackoutDates;
            calendarDateRanges.Clear();
            this.Cursor = Cursors.Wait;

            //BLACK OUT CURRENT MONTH THAT IS BEING VIEWED
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DateTime date = new DateTime(year, month, i);
                List<Availability> availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, date);

                if (availability.Count == 0 || availability[0].TimeStart == null)
                {
                    calSupplierCalendar.BlackoutDates.Add(new CalendarDateRange(date));
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
                List<Availability> availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, date);

                if (availability.Count == 0 || availability[0].TimeStart == null)
                {
                    calSupplierCalendar.BlackoutDates.Add(new CalendarDateRange(date));
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
                List<Availability> availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, date);

                if (availability.Count == 0 || availability[0].TimeStart == null)
                {
                    calSupplierCalendar.BlackoutDates.Add(new CalendarDateRange(date));
                }

                DayOfWeek day = calSupplierCalendar.DisplayDate.DayOfWeek;

            }

            this.Cursor = Cursors.Arrow;
        }
    }
}
