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
using LogicLayer;
using System.Collections.ObjectModel;

namespace WPFPresentation.Supplier
{
    /// <summary>
    /// Interaction logic for pgViewSupplierListing.xaml
    /// 
    /// Update:
    /// Austin Timmerman
    /// Updated: 2022/02/27
    /// 
    /// Description:
    /// Added the ManagerProvider instance variable and modified page parameters
    /// 
    /// Update:
    /// Derrick Nagy
    /// Created: 2022/04/05
    /// 
    /// Description:
    /// Added private variables to add a request to a supplier
    /// </summary>
    public partial class pgViewSupplierListing : Page
    {
        private DataObjects.Supplier _supplier;
        private List<Reviews> _reviews;
        private List<string> _images;
        private List<Service> _services = null;
        private ManagerProvider _managerProvider;
        private ISupplierManager _supplierManager;
        private IServiceManager _serviceManager = null;
        private IActivityManager _activityManager;
        private User _user;
        private List<Service> _selectedServices = new List<Service>();
        private List<Availability> selectedDateAvailabilities = new List<Availability>();
        private List<ActivityVM> selectedDateActivities = new List<ActivityVM>();
        private EventVM _selectedEvent = new EventVM();
        private List<EventDate> _eventDates = new List<EventDate>();
        private bool _eventIsSelected = false;
        


        internal pgViewSupplierListing(DataObjects.Supplier supplier, ManagerProvider managerProvider, User user)
        {
            InitializeComponent();
            _supplier = supplier;
            _managerProvider = managerProvider;
            _supplierManager = managerProvider.SupplierManager;
            _serviceManager = managerProvider.ServiceManager;
            _activityManager = managerProvider.ActivityManager;
            _user = user;
            

            // TO ALLOW FOR BLACKOUT DATES
            calSupplierCalendar.SelectedDate = null;

            // CHANGE FOLDER NAME FROM LocationImages, TO SERVICE IMAGES ONCE CREATED
            AppData.DataPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + @"Images\LocationImages";
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Page loaded event. Loads data into all elements that need it.
        /// 
        /// Derrick Nagy
        /// Created: 2022/05/01
        /// 
        /// Description:
        /// Commented out name change because long names don't fit in button. 
        /// 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.lblName.Content = _supplier.Name;
            this.lblType.Content = _supplier.TypeID;
            this.txtAbout.Text = _supplier.Description;
            //this.btnSupplierPricing.Content = _supplier.Name + "'s Pricing";
            //this.btnSupplierSchedule.Content = _supplier.Name + "'s Schedule";
            this.btnSupplierDetails.Background = new SolidColorBrush(Colors.Gray);
            this.txtSupplierSchedule.Text = _supplier.Name + "'s Schedule";

            try
            {
                _supplier.Tags = _supplierManager.RetrieveSupplierTagsBySupplierID(_supplier.SupplierID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n\n" + ex.InnerException.Message);
            }

            if (_supplier.Tags != null)
            {
                this.lblTags.Content = "Tags: " + string.Join(", ", _supplier.Tags);
            }
            else
            {
                this.lblTags.Content = "Tags: ";
            }
            this.lblEmail.Content = _supplier.Email;
            this.lblPhone.Content = _supplier.Phone;

            try
            {
                _images = _supplierManager.RetrieveSupplierImagesBySupplierID(_supplier.SupplierID);
                _reviews = _supplierManager.RetrieveSupplierReviewsBySupplierID(_supplier.SupplierID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n\n" + ex.InnerException.Message);
            }

            // Following block is lifted from pgViewLocationDetails.
            // Author: Austin Timmerman
            try
            {
                int avg = 0;
                int total = 0;

                foreach (Reviews review in _reviews)
                {
                    avg += review.Rating;
                    total++;
                }

                int sum = avg / total;

                switch (sum)
                {
                    case 1:
                        pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarTwo.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarTwo.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarThree.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarThree.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                        break;
                    case 2:
                        pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarThree.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarThree.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                        break;
                    case 3:
                        pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarThree.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarThree.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                        break;
                    case 4:
                        pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarThree.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarThree.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarFour.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarFour.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                        break;
                    case 5:
                        pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarThree.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarThree.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarFour.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarFour.Fill = new SolidColorBrush(Colors.Gold);
                        pathStarFive.Stroke = new SolidColorBrush(Colors.Purple);
                        pathStarFive.Fill = new SolidColorBrush(Colors.Gold);
                        break;
                    default:
                        pathStarOne.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarOne.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarTwo.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarTwo.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarThree.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarThree.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                        pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                        pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }

            if(_reviews.Any())
            {
                PopulateTextBoxWithReview(this.txtFirstReview, _reviews[0]);
            }
            if(_reviews.Count > 1)
            {
                PopulateTextBoxWithReview(this.txtSecondReview, _reviews[1]);
            }
            if(_reviews.Count < 3)
            {
                this.btnMoreReviews.Visibility = Visibility.Collapsed;
            }

            try
            {
                Uri source = new Uri(AppData.DataPath + @"\" + _images[0], UriKind.Absolute);
                BitmapImage img = new BitmapImage(source);
                this.imgSupplier.Source = img;
            }
            catch (Exception)
            {
                this.btnNext.Visibility = Visibility.Collapsed;
                this.btnBack.Visibility = Visibility.Collapsed;
                this.lblPhotoHeading.Visibility = Visibility.Collapsed;
                return;
            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Helper method to hide different grids and scroll viewers 
        /// when moving between details
        /// 
        /// Kris Howell
        /// Updated: 2022/03/10
        /// Added supplier schedule to collapse, and added button color changing
        /// 
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Added button to add a supplier request
        /// 
        /// </summary>
        private void hideDetails()
        {
            grdSupplierListing.Visibility = Visibility.Collapsed;
            grdSupplierPricing.Visibility = Visibility.Collapsed;
            grdSupplierSchedule.Visibility = Visibility.Collapsed;
            grdRequestSupplier.Visibility = Visibility.Collapsed;

            btnSupplierDetails.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSupplierSchedule.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSupplierPricing.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnRequestSupplier.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// helper method to do review formatting in text boxes. 
        /// Largely lifted and editted from pgViewLocationDetails.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="review"></param>
        private void PopulateTextBoxWithReview(TextBox box, Reviews review)
        {
            var fullStar = "\u2605";
            var emptyStar = "\u2606";

            for (int i = 0; i < 5; i++)
            {
                if (review.Rating > i)
                {
                    box.Text += fullStar;
                }
                else
                {
                    box.Text += emptyStar;
                }
            }
            box.Text += "     " + review.FullName;
            box.Text += "\n" + review.Review + "\n";
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Method that loads the supplier's services page up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupplierPricing_Click(object sender, RoutedEventArgs e)
        {
            hideDetails();
            grdSupplierPricing.Visibility = Visibility.Visible;
            this.btnSupplierPricing.Background = new SolidColorBrush(Colors.Gray);

            loadSupplierServices();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Helper method to populate the screen with the supplier's services
        /// </summary>
        private void loadSupplierServices()
        {
            _services = _serviceManager.RetrieveServicesBySupplierID(_supplier.SupplierID);
            txtSupplierServices.Text = _supplier.Name + "'s Services";

            List<ServiceVM> serviceVMs = new List<ServiceVM>();
            foreach (Service service in _services)
            {
                if(service.ServiceImagePath == null)
                {
                    serviceVMs.Add(new ServiceVM()
                    {
                        ServiceID = service.ServiceID,
                        SupplierID = service.SupplierID,
                        ServiceName = service.ServiceName,
                        Price = service.Price,
                        Description = service.Description
                    });
                }
                else
                {
                    try
                    {
                        Uri _src;                        
                        _src = new Uri(AppData.DataPath + @"\" + service.ServiceImagePath, UriKind.Absolute);
                        serviceVMs.Add(new ServiceVM()
                        {
                            ServiceID = service.ServiceID,
                            SupplierID = service.SupplierID,
                            ServiceName = service.ServiceName,
                            Price = service.Price,
                            Description = service.Description,
                            ImageUri = _src
                        });
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error");
                    }
                }
                }
            imageDataGrid.ItemsSource = serviceVMs;
        }

        private void btnSupplierDetails_Click(object sender, RoutedEventArgs e)
        {
            hideDetails();
            grdSupplierListing.Visibility = Visibility.Visible;
            this.btnSupplierDetails.Background = new SolidColorBrush(Colors.Gray);
        }

        private void btnSupplierSchedule_Click(object sender, RoutedEventArgs e)
        {
            hideDetails();
            // CANNOT BLACK OUT SELECTEDDATE
            grdSupplierSchedule.Visibility = Visibility.Visible;
            btnSupplierSchedule.Background = new SolidColorBrush(Colors.Gray);
            //calSupplierCalendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(2022, 3, 10), new DateTime(2022, 3, 13)));

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
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Added some error catching
        /// 
        /// </summary>
        private void blackOutCalendarDates()
        {
            int month = calSupplierCalendar.DisplayDate.Month;
            int year = calSupplierCalendar.DisplayDate.Year;
            CalendarBlackoutDatesCollection calendarDateRanges = calSupplierCalendar.BlackoutDates;
            calendarDateRanges.Clear();
            this.Cursor = Cursors.Wait;

            string errorMessage = "";
            bool isError = false;

            //BLACK OUT CURRENT MONTH THAT IS BEING VIEWED
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DateTime date = new DateTime(year, month, i);
                List<Availability> availability = new List<Availability>();
                
                try
                {
                    availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, date);
                }
                catch (Exception ex)
                {
                    errorMessage += ex.Message + "\n";
                    break;
                }
                

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
                List<Availability> availability = new List<Availability>();

                try
                {
                    availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, date);
                }
                catch (Exception ex)
                {
                    errorMessage += ex.Message + "\n";
                    break;
                }

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
                List<Availability> availability = new List<Availability>();

                try
                {
                    availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, date);
                }
                catch (Exception ex)
                {
                    errorMessage += ex.Message + "\n";
                    break;
                }

                if (availability.Count == 0 || availability[0].TimeStart == null)
                { 
                    calSupplierCalendar.BlackoutDates.Add(new CalendarDateRange(date));
                }

                DayOfWeek day = calSupplierCalendar.DisplayDate.DayOfWeek;

            }
            if (isError)
            {
                MessageBox.Show("There was a problem retrieving the schedule for the supplier.\n" + errorMessage, "Problem with Supplier Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Created?
        /// Unknown original author and date
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/05
        /// 
        /// Description:
        /// Added error catching with message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calSupplierCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                selectedDateAvailabilities = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, (DateTime)calSupplierCalendar.SelectedDate);
                selectedDateActivities = _activityManager.RetrieveActivitiesBySupplierIDAndDate(_supplier.SupplierID, (DateTime)calSupplierCalendar.SelectedDate);
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was a problem retrieving the schedule for the supplier.\n" + ex.Message, "Problem with Supplier Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Click handler for requesting a supplier
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestSupplier_Click(object sender, RoutedEventArgs e)
        {
            // hide old stuff, make sure this stuff is hidden when not wanted
            hideDetails();
            grdRequestSupplier.Visibility = Visibility.Visible;
            this.btnRequestSupplier.Background = new SolidColorBrush(Colors.Gray);
            clearRequestForm();


            List<EventVM> events = new List<EventVM>();
            List<string> eventNames = new List<string>();



            try
            {
                events = _managerProvider.EventManager.RetrieveEventListForUpcomingDatesForUser(_user.UserID);
                cmboEventSelection.ItemsSource = events;
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was a problem finding your events.\n" + ex.Message, "No Events Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // pull up information about the supplier - name, description, email, phone
            lblNameSupplierRequest.Content = _supplier.Name;
            lblPhoneSupplierRequest.Content = _supplier.Phone;
            lblEmailSupplierRequest.Content = _supplier.Email;




            // date picker with blacked out dates
            //datePickerRequestDate.Visibility = Visibility.Visible;
            datePickerRequestDate.DisplayDateStart = DateTime.Today;
            datePickerRequestDate.DisplayDateEnd = DateTime.Today.AddDays(90);

            List<DateTime> blackOutDates = new List<DateTime>();
            List<Availability> availabilityList = new List<Availability>();
            List<DateTime> availableDates = new List<DateTime>();

            // find dates that the supplier will not work
            try
            {
                availableDates = _supplierManager.SupplierAvailabilityForNextThreeMonths(_supplier.SupplierID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem finding the dates this supplier is available.\n" + ex.Message, "No Availability Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                

            for (int i = 1; i <= 90; i++)
            {
                blackOutDates.Add(DateTime.Now.AddDays(i));
            }

            foreach (DateTime date in availableDates)
            {
                var dateToRemove = blackOutDates.Find(d => d.Date == date.Date);
                blackOutDates.Remove(dateToRemove);
            }

            foreach (var blackOut in blackOutDates)
            {
                datePickerRequestDate.BlackoutDates.Add(new CalendarDateRange(blackOut));
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Event handler for when the calender closes.
        /// Creates the rest of the form based on the date selection.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datePickerRequestDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            // unhide everything

            if (datePickerRequestDate.SelectedDate.HasValue)
            {
                cmboStartandEndTimeRequest.Visibility = Visibility.Visible;
                lblSelectService.Visibility = Visibility.Visible;
                scrollViewerServices.Visibility = Visibility.Visible;
                lblAdditionalComments.Visibility = Visibility.Visible;
                txtAdditionalComments.Visibility = Visibility.Visible;
                stckSendCancel.Visibility = Visibility.Visible;

                DatePicker datePicker = (DatePicker)sender;
                bool foundAvailability = false;

                // time constraints pulled from db and added to combo boxes
                // availability on a date
                List<Availability> dateAvailability = new List<Availability>();
                try
                {
                    dateAvailability = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, datePicker.SelectedDate.Value);
                    foundAvailability = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem finding the hours available for this day.\n" + ex.Message, "Problem Finding Hour", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (foundAvailability)
                {
                    // set times in combo boxes
                    DateTime tempStartHour = (DateTime)dateAvailability[0].TimeStart;
                    DateTime tempEndHour = (DateTime)dateAvailability[0].TimeEnd;

                    int startHour = tempStartHour.Hour;
                    int endHour = tempEndHour.Hour;

                    cmboStartandEndTimeRequest.SetStartandEndHours(startHour, endHour, datePickerRequestDate.SelectedDate.Value);

                    setSupplierServicesforICServices();
                }
            }
            else
            {
                MessageBox.Show("Please select a date to view availability and services.", "Please Select Date", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Set ItemsControl - check list with services and prices.
        /// If services not already loaded, get them.
        /// </summary>
        private void setSupplierServicesforICServices()
        {
            
            if (_services == null)
            {
                try
                {
                    _services = _serviceManager.RetrieveServicesBySupplierID(_supplier.SupplierID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem finding the services for this supplier.\n" + ex.Message, "Problem Finding Services", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (_services.Count > 0)
            {
                icServices.ItemsSource = _services;
            }
            else
            {
                scrollViewerServices.Visibility = Visibility.Collapsed;
                lblSelectService.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Message box to confirm canceling the request
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelRequest_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel? The request will not be sent and your selections will not be saved.", "Cancel Request?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    clearRequestFormAndRedirectToSupplierDetails();
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Validates form and creates a message to be sent to supplier.
        /// Sends message.
        /// If errors, informs user
        /// 
        /// Derrick Nagy
        /// Created: 2022/05/01
        /// 
        /// Description:        
        /// Added validation check to make sure a date is selected
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendRequest_Click(object sender, RoutedEventArgs e)
        {
            bool isValidForm = false;
            string errorMessage = "";

            // check to make sure everything is valid
            if (!cmboStartandEndTimeRequest.StartIsBeforeEnd)
            {
                isValidForm = false;
                errorMessage += "There is problem with the times selected\n";                
            }
            else if (_selectedServices.Count == 0)
            {
                isValidForm = false;
                errorMessage += "Please select at least one service";
            }
            else if (chckBoxOnlyForEvents.IsChecked == true)
            {
                isValidForm = cmboEventDates.SelectedItem != null;
                errorMessage += "Please select a date";
            }
            else if (chckBoxOnlyForEvents.IsChecked == false)
            {
                isValidForm = datePickerRequestDate.SelectedDate != null;
                errorMessage += "Please select a date";
            }
            else
            {
                isValidForm = true;
            }

            if (!isValidForm)
            {
                MessageBox.Show(errorMessage, "Problem Sending Request", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                DateTime eventDate = new DateTime();
                if (chckBoxOnlyForEvents.IsChecked == true)
                {
                    eventDate = ((EventDate)cmboEventDates.SelectedItem).EventDateID;
                }
                else
                {
                    eventDate = (DateTime)datePickerRequestDate.SelectedDate;
                }

               
                // make the message
                StringBuilder emailMessage = new StringBuilder();
                emailMessage.AppendLine("Dear " + _supplier.Name + ",");
                emailMessage.AppendLine();
                emailMessage.AppendLine("You have a new request to provide service for an event made on our app, Tadpole Events.\n");
                emailMessage.AppendLine("The details for the event are as follows:");
                emailMessage.AppendLine("Event Name: " + _selectedEvent.EventName);
                emailMessage.AppendLine("Event Description: " + _selectedEvent.EventDescription);
                
                emailMessage.AppendLine("Date for service request: " + eventDate.Date.ToShortDateString());
                emailMessage.AppendLine("Times for service request:");
                emailMessage.AppendLine("Start Time: " + cmboStartandEndTimeRequest.StartTime.ToString("hh:mm t"));
                emailMessage.AppendLine("End Time: " + cmboStartandEndTimeRequest.EndTime.ToString("hh:mm t"));
                emailMessage.AppendLine("Service(s) requested: ");

                
                foreach (Service item in _selectedServices)
                {
                    emailMessage.AppendLine(item.ServiceName);
                }

                if (txtAdditionalComments.Text.Length > 0)
                {
                    emailMessage.AppendLine("Additional messsage from Event Planner:");
                    emailMessage.AppendLine(txtAdditionalComments.Text);

                }

                emailMessage.AppendLine("Information about the customer:");
                emailMessage.AppendLine("Event Planner Name: " + _user.GivenName + " " + _user.FamilyName);
                emailMessage.AppendLine("Email: " + _user.EmailAddress);
                emailMessage.AppendLine();

                emailMessage.AppendLine("Please contact the customer and update your availability on our app if you choose to work the event.");
                emailMessage.AppendLine();
                emailMessage.AppendLine("Thank you,");
                emailMessage.AppendLine("TadPole Events");


                bool emailSent = false;

                try
                {
                    _managerProvider.EmailProvider.SendEmail("New Request for Service for a Tadpole Event", emailMessage.ToString(), _supplier.Email);
                    emailSent = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem sending email. Please contact supplier." + ex.Message,"Problem Sending Email", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (emailSent)
                {
                    MessageBox.Show("Email sent. Please wait for a response from the supplier.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }


                clearRequestFormAndRedirectToSupplierDetails();

            }
            
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Adds service to selected service list that the user would like to have.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int checkBoxSupplierID = Int32.Parse(checkBox.Tag.ToString());

            _selectedServices.Add(_services.Find(s => s.ServiceID == checkBoxSupplierID));
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Removes service from selected service list
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int checkBoxSupplierID = Int32.Parse(checkBox.Tag.ToString());

            _selectedServices.Remove(_services.Find(s => s.ServiceID == checkBoxSupplierID));
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Helper method that clears form and redirects to supplier details page
        /// </summary>
        private void clearRequestFormAndRedirectToSupplierDetails()
        {
            clearRequestForm();
            cmboEventSelection.ItemsSource = null;

            hideDetails();
            grdRequestSupplier.Visibility = Visibility.Collapsed;
            grdSupplierListing.Visibility = Visibility.Visible;
            this.btnSupplierDetails.Background = new SolidColorBrush(Colors.Gray);

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Helper method that clears form 
        /// </summary>
        private void clearRequestForm()
        {
            datePickerRequestDate.SelectedDate = null;
            cmboStartandEndTimeRequest.Clear();            
            icServices.ItemsSource = null;
            txtAdditionalComments.Text = "";
            cmboStartandEndTimeRequest.Visibility = Visibility.Hidden;
            lblSelectService.Visibility = Visibility.Hidden;
            scrollViewerServices.Visibility = Visibility.Hidden;
            lblAdditionalComments.Visibility = Visibility.Hidden;
            txtAdditionalComments.Visibility = Visibility.Hidden;
            stckSendCancel.Visibility = Visibility.Hidden;


            chckBoxOnlyForEvents.IsChecked = true;
            stckPnlStartAndEndTimes.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Creates the form for the supplier availiablity when the user selects an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmboEventSelection_DropDownClosed(object sender, EventArgs e)
        {
            if (_eventIsSelected)
            {
                clearRequestForm();
            }

            txtBlockNoDatesForEvent.Visibility = Visibility.Collapsed;
            if (cmboEventSelection.SelectedIndex != -1)
            {
                stckPnlStartAndEndTimes.Visibility = Visibility.Visible;
                
                // get event dates
                try
                {
                    _selectedEvent = (EventVM)cmboEventSelection.SelectedItem;
                    _eventDates = _managerProvider.EventDateManager.RetrieveEventDatesByEventID(_selectedEvent.EventID);
                    cmboEventDates.ItemsSource = _eventDates;

                    if (chckBoxOnlyForEvents.IsChecked == true)
                    {
                        if (_eventDates == null)
                        {
                            cmboEventDates.Visibility = Visibility.Collapsed;
                            txtBlockNoDatesForEvent.Visibility = Visibility.Visible;
                            datePickerRequestDate.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            cmboEventDates.Visibility = Visibility.Visible;
                            txtBlockNoDatesForEvent.Visibility = Visibility.Collapsed;
                            datePickerRequestDate.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        cmboEventDates.Visibility = Visibility.Collapsed;
                        txtBlockNoDatesForEvent.Visibility = Visibility.Collapsed;
                        datePickerRequestDate.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem finding the dates this event.\n" + ex.Message, "No Event Dates Found", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                _eventIsSelected = true;
            }
            else
            {
                stckPnlStartAndEndTimes.Visibility = Visibility.Collapsed;
                txtBlockNoDatesForEvent.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Sets option and appropriate controls based on if the user would like to see
        /// supplier availability beyond their currently selected dates for the event
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chckBoxOnlyForEvents_Checked(object sender, RoutedEventArgs e)
        {
            if (datePickerRequestDate != null)
            {
                datePickerRequestDate.Visibility = Visibility.Collapsed;

                if (_eventDates == null)
                {
                    cmboEventDates.Visibility = Visibility.Collapsed;
                    txtBlockNoDatesForEvent.Visibility = Visibility.Visible;
                }
                else
                {
                    cmboEventDates.Visibility = Visibility.Visible;
                    txtBlockNoDatesForEvent.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Sets option and appropriate controls based on if the user would like to see
        /// supplier availability beyond their currently selected dates for the event
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chckBoxOnlyForEvents_Unchecked(object sender, RoutedEventArgs e)
        {
            if (datePickerRequestDate != null)
            {
                datePickerRequestDate.Visibility = Visibility.Visible;
                cmboEventDates.Visibility = Visibility.Collapsed;
                txtBlockNoDatesForEvent.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Creates the form for the supplier availiablity when the user selects an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmboEventDates_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            EventDate selectedEventDate = (EventDate)comboBox.SelectedItem;

            if (comboBox.SelectedIndex != -1)
            {
                cmboStartandEndTimeRequest.Visibility = Visibility.Visible;
                lblSelectService.Visibility = Visibility.Visible;
                scrollViewerServices.Visibility = Visibility.Visible;
                lblAdditionalComments.Visibility = Visibility.Visible;
                txtAdditionalComments.Visibility = Visibility.Visible;
                stckSendCancel.Visibility = Visibility.Visible;

                List<Availability> availability = new List<Availability>();
                DateTime startTime = new DateTime();
                DateTime endTime = new DateTime();

                // finding times available for selected date

                try
                {
                    availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(_supplier.SupplierID, selectedEventDate.EventDateID);
                    startTime = (DateTime)availability[0].TimeStart;
                    endTime = (DateTime)availability[0].TimeEnd;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("There was a problem finding the hours available for this day.\n" + ex.Message, "Problem Finding Hour", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (availability.Count != 0)
                {
                    cmboStartandEndTimeRequest.SetStartandEndHours(startTime.Hour, endTime.Hour, selectedEventDate.EventDateID);
                    setSupplierServicesforICServices();
                }
                else
                {
                    MessageBox.Show("There was no availablity for the supplier that day. Please select a diferent day.", "Not Available", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                
            }

        }

    }


}
