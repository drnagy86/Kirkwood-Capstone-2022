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
using LogicLayerInterfaces;
using DataObjects;
using DataAccessFakes;
using System.Text.RegularExpressions;
using System.Globalization;
using WPFPresentation.Duplicate;

namespace WPFPresentation
{
    public partial class pgCreateEvent : Page
    {

        IEventManager _eventManager = null;
        IEventDateManager _eventDateManager = null;
        ILocationManager _locationManager = null;
        ISublocationManager _sublocationManager = null;
        ITaskManager _taskManager = null;
        IVolunteerNeedManager _needManager = null;
        IZipManager _zipManager = null;

        ManagerProvider _managerProvider = null;
        DataObjects.Event newEvent = null;
        List<DataObjects.Location> _locations = null;
        User _user = null;
        List<Zip> _zips = null;

        EventVM newEventVM = new EventVM();
        EventVM copyEventVM = new EventVM();

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Initializes component and sets up event manager with fake and default accessors
        /// 
        /// Update
        /// Vinayak Deshpande
        /// 2022/02/02
        /// Added some logic for requesting volunteers
        /// 
        /// Update
        /// Jace Pettinger
        /// 2022/02/17
        /// Added user parameter for constructor
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager
        /// 
        /// Update:
        /// Austin Timmerman
        /// Updated: 2022/02/27
        /// 
        /// Description:
        /// Added the ManagerProvider instance variable and modified page parameters
        /// 
        /// Update:
        /// Vinayak Deshpande
        /// Updated: 2022/03/31
        /// 
        /// Description: defaulted sldr to 0 instead of 25 since max value is 10
        /// 
        /// Update
        /// Vinayak Deshpande
        /// Updated: 2022/04/13
        /// 
        /// Description: 
        /// Added functionality for setting up the locations drop down selection
        /// </summary>
        /// <param name="user"></param>
        /// <param name="managerProvider"></param>
        internal pgCreateEvent(User user, ManagerProvider managerProvider)
        {
            _managerProvider = managerProvider;
            _eventManager = managerProvider.EventManager;
            _eventDateManager = managerProvider.EventDateManager;
            _locationManager = managerProvider.LocationManager;
            _taskManager = managerProvider.TaskManager;
            _needManager = managerProvider.NeedManager;
            _zipManager = managerProvider.ZipManager;
            _sublocationManager = managerProvider.SublocationManager;
            _user = user;
            _locations = _locationManager.RetrieveActiveLocations();
            _zips = _zipManager.RetrieveAllZIPCodes();
            InitializeComponent();
            // ValidationHelpers.EditOngoing = true;
            // Looked up how to set the calendar to not display past dates
            // https://stackoverflow.com/questions/17401488/how-to-disable-past-days-in-calender-in-wpf/45780931
            datePickerEventDate.DisplayDateStart = DateTime.Today;
            sldrNumVolunteers.Value = 0;
            cboExistingLocations.ItemsSource = _locations;
            //disable tabs that should not be viewed
            tabAddEventDate.IsEnabled = false;
            tabsetAddEventLocation.IsEnabled = false;
            tabAddEventVolunteer.IsEnabled = false;

        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/30
        /// 
        /// Description: Alternate Constructor for the pgCreateEvent that takes an event
        /// object so that it can be duplicated into a new object to make a repeat
        /// event. fills all the values for description and budget as well as location and
        /// volunteer needs, but leaves the dates blank. name defaults to old name + (copy) 
        /// to prevent locations not being added. Also leaves name editable so that it can be
        /// changed.
        /// 
        /// 
        /// Update
        /// Vinayak Deshpande
        /// Updated: 2022/04/13
        /// 
        /// Description: 
        /// Added functionality for the dropdown locations menu when duplicating an event
        /// </summary>
        /// <param name="user"></param>
        /// <param name="managerProvider"></param>
        /// <param name="eventVM"></param>
        internal pgCreateEvent(User user, ManagerProvider managerProvider, EventVM eventVM)
        {
            _managerProvider = managerProvider;
            _eventManager = managerProvider.EventManager;
            _eventDateManager = managerProvider.EventDateManager;
            _locationManager = managerProvider.LocationManager;
            _taskManager = managerProvider.TaskManager;
            _needManager = managerProvider.NeedManager;
            _zipManager = managerProvider.ZipManager;
            _sublocationManager = managerProvider.SublocationManager;
            _user = user;
            copyEventVM = eventVM;
            VolunteerNeed copyNeed = null;
            DataObjects.Location copyLocation = null;
            TasksVM copyTask = null;
            _zips = _zipManager.RetrieveAllZIPCodes();
            // ValidationHelpers.EditOngoing = true;
            InitializeComponent();

            txtBoxEventName.Text = copyEventVM.EventName + " (copy)";
            txtBoxEventDescription.Text = copyEventVM.EventDescription;
            txtBoxEventDescription.IsReadOnly = true;
            txtBoxTotalBudget.Text = copyEventVM.TotalBudget.ToString();
            txtBoxTotalBudget.IsReadOnly = true;
            try
            {
                copyLocation = _locationManager.RetrieveLocationByLocationID((int)copyEventVM.LocationID);
                if (copyLocation != null)
                {
                    _locations = new List<DataObjects.Location>();
                    _locations.Add(copyLocation);
                    cboExistingLocations.ItemsSource = _locations;
                    cboExistingLocations.Text = copyLocation.Name;
                    txtBoxLocationName.Text = copyLocation.Name;
                    txtBoxLocationName.IsReadOnly = true;
                    txtBoxStreet.Text = copyLocation.Address1;
                    txtBoxStreet.IsReadOnly = true;
                    txtBoxCity.Text = copyLocation.City;
                    txtBoxCity.IsReadOnly = true;
                    cboState.Text = copyLocation.State;
                    cboState.IsReadOnly = true;
                    txtBoxZip.Text = copyLocation.ZipCode;
                    txtBoxZip.IsReadOnly = true;

                }
                else
                {
                    _locations = _locationManager.RetrieveActiveLocations();
                    cboExistingLocations.ItemsSource = _locations;
                    txtBoxLocationName.Text = "";
                    txtBoxLocationName.IsReadOnly = true;
                    txtBoxStreet.Text = "";
                    txtBoxStreet.IsReadOnly = true;
                    txtBoxCity.Text = "";
                    txtBoxCity.IsReadOnly = true;
                    cboState.Text = "";
                    cboState.IsReadOnly = true;
                    txtBoxZip.Text = "";
                    txtBoxZip.IsReadOnly = true;
                }

            }
            catch (Exception)
            {

                MessageBox.Show("This Event did not have a Location originally!");
            }
            
            
            

            // Looked up how to set the calendar to not display past dates
            // https://stackoverflow.com/questions/17401488/how-to-disable-past-days-in-calender-in-wpf/45780931
            sldrNumVolunteers.Value = 5;
            foreach (var task in _taskManager.RetrieveAllActiveTasksByEventID(copyEventVM.EventID))
            {
                if (task.Name.Equals("General Help"))
                {
                    copyTask = task;
                }
            }
            if (copyTask != null)
            {
                copyNeed = _needManager.RetrieveVolunteerNeedByTaskID(copyTask.TaskID);
                chkBxNeedVolunteers.IsChecked = true;
                sldrNumVolunteers.Value = copyNeed.NumTotalVolunteers;
            }
            datePickerEventDate.DisplayDateStart = DateTime.Today;
            
            btnEventNext.Content = "Duplicate Event";
            btnVolunteersNext.Content = "Finish Duplication";
            //disable tabs that should not be viewed
            tabAddEventDate.IsEnabled = false;
            tabsetAddEventLocation.IsEnabled = false;
            tabAddEventVolunteer.IsEnabled = false;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Click event handler for creating a new event
        /// </summary>
        /// Derrick Nagy
        /// Updated: 2022/01/30
        /// 
        /// Description:
        /// Got rid of message box that was there for testing purposes and changed focus to next tab. Disabled Event tab.
        /// 
        /// Update:
        /// Vinayak Deshpande
        /// 2022/02/03
        /// Description:
        /// Changed the next button to cycle through the tabs in the set rather than just swtiching to a set tab.
        /// 
        /// Update:
        /// Vinayak Deshpande
        /// 2022/02/04
        /// Description:
        /// Moved event creation back to first tab of event creation page.
        /// 
        /// Update:
        /// Derrick Nagy
        /// 2022/02/17
        /// Description:
        /// Changed the manager method that creates the event to CreateEventReturnsEventID from CreateEvent
        /// 
        /// Update:
        /// Alaina Gilson
        /// 2022/02/23
        /// Description:
        /// Added the TotalBudget field and validation to check that the string input is a decimal
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/04/15
        /// 
        /// Description:
        /// Added more information the screen during event creation
        /// </summary>
        private void btnEventNext_Click(object sender, RoutedEventArgs e)
        {
            string value = txtBoxTotalBudget.Text;
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            CultureInfo provider = new CultureInfo("en-US");
            decimal totalPlanned;

            try
            {
                totalPlanned = Decimal.Parse(value, style, provider);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating a new event.\n\nPlease enter a decimal budget.");
                txtBoxTotalBudget.Text = "";
                txtBoxTotalBudget.Focus();
                return;
            }

            try
            {
                if (txtBoxEventName.Text == "" || txtBoxEventDescription.Text == "")
                {
                    MessageBox.Show("Please enter all fields for the event.");
                    txtBoxEventName.Focus();
                }
                else
                {
                    newEvent = new DataObjects.Event()
                    {
                        EventID = _eventManager.CreateEventReturnsEventID(txtBoxEventName.Text, txtBoxEventDescription.Text, totalPlanned, _user.UserID),
                        EventName = txtBoxEventName.Text,
                        EventDescription = txtBoxEventDescription.Text
                    };

                    tabAddEventDate.IsEnabled = true;
                    txtBlkAddEventDateTitle.Text = "Select Date(s) for Event: " + newEvent.EventName;
                    tabAddEventDate.Focus();
                    btnEventNext.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating a new event.\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Click event handler for canceling creating an event
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?\n Unsaved changes will be discarded.",
                               "Cancel",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Page page = new pgViewEvents(_user, _managerProvider);
                this.NavigationService.Navigate(page);
            }

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxEventStartTimeHour_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;

            if (textBox.IsValidHour())
            {
                if (textBox.Length == 2)
                {
                    txtBoxEventStartTimeMinute.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                box.Text = "";
                txtBlockEventAddValidationMessage.Text = "Please enter a valid time.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxEventStartTimeMinute_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;

            if (textBox.IsValidMinute())
            {
                if (textBox.Length == 2)
                {
                    cmbStartTimeAMPM.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                box.Text = "";
                txtBlockEventAddValidationMessage.Text = "Please enter a valid time.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// 
        /// Update:
        /// Derrick Nagy
        /// 2022/03/12
        /// Description:
        /// Changes to update hour controls to combo boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxEventEndTimeHour_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;

            if (textBox.IsValidHour())
            {
                if (textBox.Length == 2)
                {
                    //txtBoxEventEndTimeMinute.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                box.Text = "";
                txtBlockEventAddValidationMessage.Text = "Please enter a valid time.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// 
        /// Update:
        /// Derrick Nagy
        /// 2022/03/12
        /// Description:
        /// Changes to update hour controls to combo boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxEventEndTimeMinute_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;

            if (textBox.IsValidMinute())
            {
                if (textBox.Length == 2)
                {
                    //cmbEndTimeAMPM.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                box.Text = "";
                txtBlockEventAddValidationMessage.Text = "Please enter a valid time.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Click handler for adding a date to an event
        /// 
        /// Update:
        /// Derrick Nagy
        /// 2022/03/12
        /// Description:
        /// Changes to update hour controls to combo boxes
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/04/15
        /// 
        /// Description:
        /// Button text change to add another date if one has been added.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDateAdd_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerEventDate.SelectedDate == null)
            {
                txtBlockEventAddValidationMessage.Text = "Please enter a date.";
                txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
            }
            else
            {
                DateTime dateTimeToAdd = new DateTime();
                int year = 0;
                int month = 0;
                int day = 0;
                int secconds = 0;

                int startHour = ucStartTime.Hour;
                int startMin = ucStartTime.Minutes;
                int endHour = ucEndTime.Hour;
                int endMin = ucEndTime.Minutes;

                try
                {
                    dateTimeToAdd = (DateTime)datePickerEventDate.SelectedDate;
                    year = dateTimeToAdd.Year;
                    month = dateTimeToAdd.Month;
                    day = dateTimeToAdd.Day;

                    // validate time
                    IntegerValidationHelpers.ValidateStartTimeBeforeEndTime(startHour, startMin, endHour, endMin);

                    EventDate eventDate = new EventDate()
                    {
                        EventDateID = dateTimeToAdd,
                        EventID = newEvent.EventID,
                        StartTime = new DateTime(year, month, day, startHour, startMin, secconds),
                        EndTime = new DateTime(year, month, day, endHour, endMin, secconds),
                        Active = true
                    };

                    newEventVM.EventDates.Add(eventDate);

                    // show event date summary table
                    datCurrentEventDates.ItemsSource = null;
                    datCurrentEventDates.ItemsSource = newEventVM.EventDates;
                    datCurrentEventDates.Visibility = Visibility.Visible;
                    txtBlkCurrentEventDates.Visibility = Visibility.Visible;

                    // prepare form to add another date
                    btnEventDateAdd.Content = "Add Another Date";
                    datePickerEventDate.SelectedDate = null;
                    datePickerEventDate.BlackoutDates.Add(new CalendarDateRange(dateTimeToAdd));
                    DateTime startMinusMonth = newEventVM.EventDates.First().EventDateID.AddMonths(-1);
                    datePickerEventDate.DisplayDateStart = startMinusMonth > DateTime.Now ? startMinusMonth : DateTime.Now;
                    datePickerEventDate.DisplayDateEnd = newEventVM.EventDates.First().EventDateID.AddMonths(1);
                    ucStartTime.Reset();
                    ucEndTime.Reset();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem adding the date to the event.\n" + ex.Message, "Problem Adding Date", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/17
        /// 
        /// Description:
        /// Click handler for continuing on from event dates tab
        /// 
        /// Update
        /// Derrick Nagy
        /// 2022/02/22
        /// 
        /// Desription:
        /// Added call to database to add the dates
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/04/15
        /// 
        /// Description:
        /// Added more information the screen during event creation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDateNext_Click(object sender, RoutedEventArgs e)
        {
            if (datCurrentEventDates.Items.Count == 0) // no dates were added, confirm want to continue
            {
                var result = MessageBox.Show("Continue without adding a date?",
                               "No Dates",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    tabsetAddEventLocation.IsEnabled = true;
                    lblSetEventLocation.Text = "Select A Location for Event: " + newEvent.EventName;
                    btnEventDateNext.IsEnabled = false;
                    tabsetAddEventLocation.Focus();
                }
            }
            else
            {

                // add dates
                try
                {
                    foreach (EventDate date in newEventVM.EventDates)
                    {
                        _eventDateManager.CreateEventDate(date);
                    }

                    String message = (newEventVM.EventDates.Count == 1) ? "The event date was successfully added." : "The " + newEventVM.EventDates.Count + " event dates were successfully added.";

                    MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    tabsetAddEventLocation.IsEnabled = true;
                    lblSetEventLocation.Text = "Select a Location for Event: " + newEvent.EventName;
                    btnEventDateNext.IsEnabled = false;
                    btnEventDateAdd.IsEnabled = false;
                    tabsetAddEventLocation.Focus();
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem adding the date to the event.\n" + ex.Message, "Problem Adding Date", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Vinyak Deshpande
        /// Created: 2022/01/29
        /// Description: Allows the user to create a generic event after selecting the I need volunteers checkbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBxNeedVolunteers_Checked(object sender, RoutedEventArgs e)
        {
            txtBlkNumVolunteers.Visibility = Visibility.Visible;
            dcPnlNumVolunteers.Visibility = Visibility.Visible;
            btnRequestVolunteers.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/17
        /// 
        /// Description:
        /// Click event handler for navigating to next tab from volunteers tab
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/04/01
        /// 
        /// Description: Added duplication functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVolunteersNext_Click(object sender, RoutedEventArgs e)
        {
            
            if (btnVolunteersNext.Content.Equals("Finish"))
            {
                System.Windows.MessageBox.Show("Event details added");
                pgViewEvents viewEventsPage = new pgViewEvents(_user, _managerProvider);
                // ValidationHelpers.EditOngoing = false;
                this.NavigationService.Navigate(viewEventsPage);
            }
            else if (btnVolunteersNext.Content.Equals("Finish Duplication"))
            {
                MessageBoxResult result;
                var message = "Would You like to Duplicate Tasks?";
                var caption = "Finished Duplicating";
                MessageBoxButton buttons = MessageBoxButton.YesNo;

               result =  MessageBox.Show(message, caption, buttons,
                     MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    System.Windows.MessageBox.Show("Event Duplication Complete.");
                    pgViewEvents viewEventsPage = new pgViewEvents(_user, _managerProvider);
                    this.NavigationService.Navigate(viewEventsPage);
                }
                if (result == MessageBoxResult.Yes)
                {
                    
                    pgDuplicateEvent duplicateEventPage = new pgDuplicateEvent(copyEventVM, newEvent, _managerProvider, _user);
                    this.NavigationService.Navigate(duplicateEventPage);
                    
                }
            }
            
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Click event handler for adding a location to an event and inserting a new event
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/09
        /// Removed call to create event and just set event location ID.
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// Updated code to use variables that were assigned and never accessed.
        /// Removed creation of an event object to access a location object
        /// Made it optional to add a location
        /// Added validation
        /// 
        /// 
        /// Vinayk Deshpande
        /// Updated 2022/04/01
        /// Description: changed reference to eventid from retrieved object to 
        /// created newEvent object
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/04/15
        /// 
        /// Description:
        /// Added more information the screen during event creation
        /// </summary>
        private void btnEventLocationAdd_Click(object sender, RoutedEventArgs e)
        {
            DataObjects.Location eventLocation = new DataObjects.Location();

            string eventName = txtBoxEventName.Text;
            string eventDescription = txtBoxEventDescription.Text;
            string locationName = txtBoxLocationName.Text; // added variable for repeated code -jp
            string locationStreet = txtBoxStreet.Text; // added variable for repeated code -jp
            string locationCity = txtBoxCity.Text; // added variable for repeated code -jp
            string locationState = cboState.Text; // added variable for repeated code -jp
            string locationZip = txtBoxZip.Text; // added variable for repeated code -jp

            if (locationName == "" && locationStreet == "" && locationCity == "" && locationState == "" && locationZip == "")
            {
                //MessageBox.Show("Please enter a value for all location fields.");
                //txtBoxLocationName.Focus();
                var result = MessageBox.Show("Continue without adding a location?", // added -jp
                               "No location",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    tabAddEventVolunteer.IsEnabled = true;
                    txtBlkAddEventVolunteerTitle.Text = "Request Volunteers for Event: " + newEvent.EventName;
                    tabAddEventVolunteer.Focus();
                }
            }
            else if (!ValidationHelpers.IsValidName(locationName)) // added validation -jp
            {
                txtBlockLocationValidationMessage.Text = "Please enter a name for the location";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                txtBoxLocationName.Focus();
            }
            else if (!ValidationHelpers.IsValidName(locationStreet)) // added validation -jp
            {
                txtBlockLocationValidationMessage.Text = "Please enter a name for the location";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                txtBoxStreet.Focus();
            }
            else if (!ValidationHelpers.IsValidCityName(locationCity)) // added validation -jp
            {
                txtBlockLocationValidationMessage.Text = "Please enter a valid city name";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                txtBoxCity.Focus();
            }
            else if (locationState == null) // added validation -jp
            {
                txtBlockLocationValidationMessage.Text = "Please select a state";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                cboState.Focus();
            }
            else if (!ValidationHelpers.IsValidZipCode(locationZip)) // added validation -jp
            {
                txtBlockLocationValidationMessage.Text = "Please enter a valid zip code";
                txtBlockLocationValidationMessage.Visibility = Visibility.Visible;
                txtBoxZip.Focus();
            }
            else
            {
                txtBlockLocationValidationMessage.Visibility = Visibility.Hidden;
                try
                {

                    eventLocation = _locationManager.RetrieveLocationByNameAndAddress(locationName, locationStreet);
                    if (eventLocation is null || eventLocation.LocationID == 0)
                    {
                        _locationManager.CreateLocation(locationName, locationStreet, locationCity, locationState, locationZip);
                        eventLocation = _locationManager.RetrieveLocationByNameAndAddress(locationName, locationStreet);
                        // DataObjects.EventVM eventObj = _eventManager.RetrieveEventByEventNameAndDescription(eventName/*txtBoxEventName.Text*/, eventDescription/*txtBoxEventDescription.Text*/);
                        _eventManager.UpdateEventLocationByEventID(newEvent.EventID, null, eventLocation.LocationID);
                        locationName = "";
                        locationStreet = "";
                        locationCity = "";
                        locationState = null;
                        locationZip = "";
                    }
                    else if (eventLocation != null)
                    {
                        //int? eventLocationID = null; if the event location is not null that means there is a locationID
                        int newEventLocationID = eventLocation.LocationID;
                        // EventVM eventObj = _eventManager.RetrieveEventByEventNameAndDescription(eventName/*txtBoxEventName.Text*/, eventDescription /*txtBoxEventDescription.Text*/);
                        int? oldEventLocationID = null; // added

                        if (newEvent.LocationID != null)
                        {
                            oldEventLocationID = newEvent/*.Location*/.LocationID;
                        }
                        _eventManager.UpdateEventLocationByEventID(newEvent.EventID, oldEventLocationID /*eventLocationID*/, newEventLocationID /*eventLocation.LocationID*/);
                        //locationName = ""; // cannot add multiple locations, this step is not needed 
                        //locationStreet = "";
                        //locationCity = "";
                        //locationState = null;
                        //locationZip = "";
                    }

                    MessageBox.Show("Event Location Added");
                    tabAddEventVolunteer.IsEnabled = true;
                    txtBlkAddEventVolunteerTitle.Text = "Request Volunteers for Event: " + newEvent.EventName;
                    tabAddEventVolunteer.Focus();
                    // do not allow user to go back
                    btnEventLocationAdd.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem adding this location to the event.\n\n" + ex.Message + "\n\n\n" + ex.InnerException.Message);
                }
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/17
        /// 
        /// Description:
        /// Added click event for Event date. Adds the list of event dates to the database
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDateNext_Helper(object sender, RoutedEventArgs e)
        {

            if (newEventVM.EventDates.Count > 0)
            {
                try
                {
                    foreach (EventDate date in newEventVM.EventDates)
                    {
                        _eventDateManager.CreateEventDate(date);
                    }

                    String message = (newEventVM.EventDates.Count == 1) ? "The event date was successfully added." : "The " + newEventVM.EventDates.Count + " event dates were successfully added.";

                    MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Page page = new pgViewEvents(_user, _managerProvider);
                    this.NavigationService.Navigate(page);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem adding the date to the event.\n" + ex.Message, "Problem Adding Date", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you are finished with this tab?", "Finished?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        break;
                    case MessageBoxResult.Yes:

                        newEventVM.EventDates = new List<EventDate>();
                        Page page = new pgViewEvents(_user, _managerProvider);
                        this.NavigationService.Navigate(page);

                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Vinayak Deshpande
        /// Added 2022/03/04
        /// Description: Adds a generic event with the number of volunteers
        /// the organziers would like on hand to use as needed.
        /// 
        /// Derrick Nagy
        /// Upadate: 2022/03/27
        /// 
        /// Description:
        /// Handled errors that occur for events with no dates
        /// 
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/03/31
        /// 
        /// Description: removed setting the sldr to 0 by default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestVolunteers_Click(object sender, RoutedEventArgs e)
        {
            DateTime dueDate = new DateTime();
            
            try
            {
                dueDate = _eventDateManager.RetrieveEventDatesByEventID(newEvent.EventID).ElementAt(0).EventDateID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not retrieve event dates.");
            }
            
            


            int numTotalVolunteers = (int)sldrNumVolunteers.Value;
            DataObjects.Tasks genericTask = new Tasks()
            {
                EventID = newEvent.EventID,
                Name = "General Help",
                Description = "Help out with the event as needed on the day of.",
                // cboAssign variable,

                
                DueDate = dueDate,

                Priority = 3
            };
            try
            {
                int addedTasks = _taskManager.AddTask(genericTask, numTotalVolunteers);
                if (addedTasks > 1)
                {
                    MessageBox.Show("Volunteers have been requested.");
                    btnRequestVolunteers.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Request has failed!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/13
        /// 
        /// Description: 
        /// Added functionality for selecting a pre existing location from a drop down menu
        /// it then populates the old system which is hidden and runs like it used to.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboExistingLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataObjects.Location tempLoc = (DataObjects.Location)cboExistingLocations.SelectedItem;
            if (tempLoc != null)
            {
                txtBoxLocationName.Text = tempLoc.Name;
                txtBoxLocationName.IsReadOnly = true;
                // txtBoxLocationName.Visibility = Visibility.Hidden;
                // lblName.Visibility = Visibility.Hidden;
                txtBoxStreet.Text = tempLoc.Address1;
                txtBoxStreet.IsReadOnly = true;
                // txtBoxStreet.Visibility = Visibility.Hidden;
                // lblStreet.Visibility = Visibility.Hidden;
                txtBoxZip.Text = tempLoc.ZipCode;
                txtBoxZip.IsReadOnly = true;
                txtBoxCity.Text = tempLoc.City;
                txtBoxCity.IsReadOnly = true;
                // txtBoxCity.Visibility = Visibility.Hidden;
                // lblCity.Visibility = Visibility.Hidden;
                cboState.Text = tempLoc.State;
                cboState.IsReadOnly = true;
                // cboState.Visibility = Visibility.Hidden;
                // lblState.Visibility = Visibility.Hidden;
                
                // txtBoxZip.Visibility = Visibility.Hidden;
                // lblZip.Visibility = Visibility.Hidden;
            }
            
        }

        /// <summary>
        /// 
        /// Vinayak Deshpande
        /// Created: 2022/04/13
        /// 
        /// Description: 
        /// Shows the old location set up if you want to add a custom location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCustomLocation_Click(object sender, RoutedEventArgs e)
        {
            cboExistingLocations.Text = "Custom Location";
            // txtBoxLocationName.Visibility = Visibility.Visible;
            // lblName.Visibility = Visibility.Visible;
            txtBoxLocationName.Text = "";
            txtBoxLocationName.IsReadOnly = false;
            txtBoxLocationName.Focus();
            txtBoxLocationName.Background = Brushes.Beige;
            // lblStreet.Visibility = Visibility.Visible;
            // txtBoxStreet.Visibility = Visibility.Visible;
            txtBoxStreet.Text = "";
            txtBoxStreet.IsReadOnly = false;
            txtBoxStreet.Background = Brushes.Beige;
            // txtBoxCity.Visibility = Visibility.Visible;
            // lblCity.Visibility = Visibility.Visible;
            txtBoxZip.Text = "";
            txtBoxZip.IsReadOnly = false;
            txtBoxZip.Background = Brushes.Beige;
            txtBoxCity.Text = "";
            txtBoxCity.IsReadOnly = true;
            txtBoxCity.Background = Brushes.Beige;
            // cboState.Visibility = Visibility.Visible;
            // lblState.Visibility = Visibility.Visible;
            cboState.Text = "";
            cboState.IsReadOnly = true;
            cboState.Background = Brushes.Beige;
            // txtBoxZip.Visibility = Visibility.Visible;
            // lblZip.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/13
        /// 
        /// Description:
        /// Autofills city and state when entering zip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxZip_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBoxZip.Text.Length == 5)
            {
                bool isZip = false;
                string tempZip = txtBoxZip.Text;
                isZip = _zips.Exists(z => z.ZIPCode == tempZip);
                if (isZip)
                {

                }
                
                txtBoxCity.Text = isZip ? _zips.Find(z => z.ZIPCode == tempZip).City : "" ;
                cboState.Text = isZip ? _zips.Find(z => z.ZIPCode == tempZip).State : "";

                
                

            }
            if (txtBoxZip.Text.Length <= 4 || txtBoxZip.Text.Length >= 6)
            {
                txtBoxCity.Text = "";
                cboState.Text = "";
            }
        }
    }
}

