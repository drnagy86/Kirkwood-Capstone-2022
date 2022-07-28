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
using DataObjects;
using DataAccessFakes;
using System.Text.RegularExpressions;
using WPFPresentation.Location;

namespace WPFPresentation.Event
{
    /// <summary>
    /// Interaction logic for pgEventEditDetail.xaml
    /// </summary>
    public partial class pgEventEditDetail : Page
    {
        IEventManager _eventManager = null;
        IEventDateManager _eventDateManager = null;
        ILocationManager _locationManager = null;
        ISublocationManager _sublocationManager = null;
        ManagerProvider _managerProvider = null;

        DataObjects.Location _location = null;
        DataObjects.EventVM _event = null;
        List<EventDate> _eventDates = null;
        EventDate _selectedEventDate = null;
        IVolunteerRequestManager _volunteerRequestManager = null;

        bool _userCanEditEvent = false;

        User _user = null;


        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Initializes component and sets up event manager with fake and default accessors
        /// 
        /// Update:
        /// Derrick Nagy
        /// Update: 2022/02/23
        /// Description:
        /// Changed which data grid is used for viewing and editing.
        /// Added checks for the user to edit only if they are an event planner or manager.
        /// 
        /// Update:
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
        /// </summary>
        /// <param name="selectedEvent">Must be pased an event object to view or edit</param>
        /// <param name="managerProvider"></param>
        /// <param name="user"></param>
        internal pgEventEditDetail(DataObjects.EventVM selectedEvent, ManagerProvider managerProvider, User user)
        {
            _managerProvider = managerProvider;
            _eventManager = managerProvider.EventManager;
            _eventDateManager = managerProvider.EventDateManager;
            _volunteerRequestManager = managerProvider.VolunteerRequestManager;
            _locationManager = managerProvider.LocationManager;
            _event = selectedEvent;
            _user = user;
            _sublocationManager = managerProvider.SublocationManager;

            InitializeComponent();

            // if there is a user
            if (_user != null)
            {
                try
                {
                    // If the user is an Event Planner or Manager, they can edit the event. Otherwise default view.   
                    _userCanEditEvent = _eventManager.CheckUserEditPermissionForEvent(_event.EventID, _user.UserID);
                    if (_userCanEditEvent)
                    {
                        btnEventEditSave.Visibility = Visibility.Visible;
                        btnEventEditSave.Visibility = Visibility.Visible;
                        datEditCurrentEventDates.Visibility = Visibility.Visible;
                        datCurrentEventDatesNoEdit.Visibility = Visibility.Collapsed;
                        tabEventVolunteerRequests.Visibility = Visibility.Visible;
                        btnEditEventDateAddSave.Visibility = Visibility.Visible;
                        btnEditEventDateCloseCancel.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not find permissions for the user. You will not be able to edit this event.\n" + ex.Message, "Permissions Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            // If the user is an Event Planner or Manager, they can edit the event. Otherwise default view.
           

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Handler for populating page when loaded onto screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            setGeneralTabDetailMode();
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Helper method to handle populating page elements with data
        /// </summary>
        private void populateControls()
        {
            // populate data
            txtBoxEventName.Text = _event.EventName.ToString();
            txtBoxEventDateCreated.Text = _event.EventCreatedDate.ToShortDateString();
            txtBoxEventDescription.Text = _event.EventDescription.ToString();
            txtBoxEventLocation.Text = "Not Available";    // do not have location data available to use yet\

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Helper method to handle changing the page to edit mode
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Raise EditOngoing flag when entering edit mode
        /// </summary>
        private void setGeneralTabEditMode()
        {
            ValidationHelpers.EditOngoing = true;
            populateControls();

            // Enable editing 
            txtBoxEventName.IsReadOnly = false;
            txtBoxEventDescription.IsReadOnly = false;
            txtBoxEventLocation.IsReadOnly = true; // cannot change until I can add location
            btnDeleteEvent.Visibility = Visibility.Visible;

            // not allowed to change date created
            txtBoxEventDateCreated.IsEnabled = false;
            txtBoxEventLocation.IsEnabled = false;

            // change buttons to Save and Cancel
            btnEventEditSave.Content = "Save";
            btnEventCloseCancel.Content = "Cancel";
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Helper method to handle changing the page to detail mode
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lower EditOngoing flag when returning to detail mode
        /// </summary>
        private void setGeneralTabDetailMode()
        {
            ValidationHelpers.EditOngoing = false;
            populateControls();

            // make read only
            txtBoxEventName.IsReadOnly = true;
            txtBoxEventDateCreated.IsReadOnly = true;
            txtBoxEventDescription.IsReadOnly = true;
            txtBoxEventLocation.IsReadOnly = true;
            btnDeleteEvent.Visibility = Visibility.Hidden;

            // make enabled to look nicer
            txtBoxEventDateCreated.IsEnabled = true;

            // make sure buttons are Edit and Close
            btnEventEditSave.Content = "Edit";
            btnEventCloseCancel.Content = "Close";
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click event handler for eduting or saving the event
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lower EditOngoing flag before navigating away on save
        /// 
        /// Vinayak Deshpande
        /// Updated 2022/05/04
        /// 
        /// Description: Removed Finally Block
        /// </summary>
        private void btnEventEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnEventEditSave.Content.ToString() == "Edit") // Edit record
            {
                setGeneralTabEditMode();
            }
            else // save record
            {
                if (txtBoxEventName.Text == "") // check no name
                {
                    MessageBox.Show("Please enter an event name");
                }
                else if (txtBoxEventDescription.Text == "") // check no description
                {
                    MessageBox.Show("Please enter an event description");
                }
                else // create new event object
                {
                    DataObjects.EventVM newEvent = new DataObjects.EventVM()
                    {
                        EventID = _event.EventID,
                        EventName = txtBoxEventName.Text,
                        EventCreatedDate = _event.EventCreatedDate,
                        EventDescription = txtBoxEventDescription.Text,
                        Active = true
                    };

                    try // try update
                    {
                        if (_eventManager.UpdateEvent(_event, newEvent)) // one event updated
                        {
                            MessageBox.Show("Event updated.");
                            ValidationHelpers.EditOngoing = false;
                            NavigationService.GoBack();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem editing the event.\n" + ex.Message);

                    }
                }
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click event handler for navigating back to previous page or canceling edit
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lower EditOngoing flag before navigating away
        /// </summary>
        private void btnEventCloseCancel_Click(object sender, RoutedEventArgs e)
        {
            if (btnEventCloseCancel.Content.ToString() == "Close") // return to view events page
            {
                ValidationHelpers.EditOngoing = false;
                this.NavigationService.GoBack();
            }
            else // Make sure want to close, then return to details view
            {
                var result = MessageBox.Show("Discard changes?",
                               "Canel",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    setGeneralTabDetailMode();
                }
            }

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click event handler for navigating to edit event dates tab
        /// </summary>
        private void btnEditEventDates_Click(object sender, RoutedEventArgs e)
        {
            tabEventDates.Focus();
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Handler for validating Event Name input
        /// </summary>
        private void txtBoxEventName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;
            if (textBox == "")
            {
                box.Text = "";
                txtBlockValidationMessage.Text = "Please enter a valid event name.";
                txtBlockValidationMessage.Visibility = Visibility.Visible;
                txtBoxEventName.Focus();
            }
            else
            {
                txtBlockValidationMessage.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Handler for validating Event Description input
        /// </summary>
        private void txtBoxEventDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string textBox = box.Text;
            if (textBox == "")
            {
                box.Text = "";
                txtBlockValidationMessage.Text = "Please enter a valid event description.";
                txtBlockValidationMessage.Visibility = Visibility.Visible;
            }
            else
            {
                txtBlockValidationMessage.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Button click handler for deleting an event
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lowered EditOngoing flag before navigating away in finally block
        /// 
        /// Vinayak Deshpande
        /// Updated 2022/05/04
        /// 
        /// Description: Removed Finally Block
        /// </summary>
        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?\nThis action cannot be undone",
                               "Delete Event.",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try // try update
                {
                    DataObjects.Event newEvent = new DataObjects.Event()
                    { // same Event with event set to false
                        EventID = _event.EventID,
                        EventName = _event.EventName,
                        EventDescription = _event.EventDescription,
                        EventCreatedDate = _event.EventCreatedDate,
                        Active = false
                    };
                    if (_eventManager.UpdateEvent(_event, newEvent)) // one event updated
                    {
                        MessageBox.Show("Event Deleted.");
                        ValidationHelpers.EditOngoing = false;
                        NavigationService.GoBack();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem deleting the event.\n" + ex.Message);

                }
            }
        }
        // --------------------------------------------------------- End of General Tab -----------------------------------------------------------

        // --------------------------------------------------------- Start of Date Tab -----------------------------------------------------------

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Helper method for setting Event Dates tab to detail mode
        /// 
        /// Update
        /// Derrick Nagy
        /// Updated: 2020/03/26
        /// 
        /// Description:
        /// Hid validation message for no event dates if there are event dates
        /// 
        /// Kris Howell
        /// Updated: 2022/04/01
        /// 
        /// Description:
        /// Lower EditOngoing flag when entering detail mode
        /// </summary>
        private void setEventDatesTabDetailMode()
        {
            ValidationHelpers.EditOngoing = false;
            grdAddEventDate.Visibility = Visibility.Hidden; // hide edit dates area
            btnEditEventDateAddSave.Content = "Add Dates";
            btnEditEventDateCloseCancel.Content = "Close";

            try // try to populate event dates data grid
            {
                _eventDates = _eventDateManager.RetrieveEventDatesByEventID(_event.EventID);
            }
            catch (Exception)
            {
                // not actually throwing anything, just putting error message on screen
            }
            if (_eventDates == null)
            {
                txtBlockEventDateValidationMessage.Text = "No dates added";
                txtBlockEventDateValidationMessage.Visibility = Visibility.Visible;
            }
            else
            {
                txtBlockEventDateValidationMessage.Visibility = Visibility.Hidden;
                // if the user can edit, use this table
                if (_userCanEditEvent)
                {
                    datEditCurrentEventDates.ItemsSource = _eventDates;
                }
                else
                {
                    // use this table
                    datCurrentEventDatesNoEdit.ItemsSource = _eventDates;
                }
                
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Helper method for setting Event Dates tab to edit mode
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Changed time text boxes to combo boxes
        /// 
        /// Kris Howell
        /// Updated: 2022/04/01
        /// 
        /// Description:
        /// Raise EditOngoing flag when entering edit mode
        /// </summary>
        private void setEventDateTabEditMode()
        {
            ValidationHelpers.EditOngoing = true;
            // prepare add event grid
            grdAddEventDate.Visibility = Visibility.Visible;
            datePickerEventDate.SelectedDate = null;
            //txtBoxEventStartTimeHour.Text = "";
            //txtBoxEventStartTimeMinute.Text = "";
            //cmbStartTimeAMPM.SelectedItem = "AM";
            //txtBoxEventEndTimeHour.Text = "";
            //txtBoxEventEndTimeMinute.Text = "";
            //cmbEndTimeAMPM.SelectedItem = "AM";
            ucStartTime.Reset();
            ucEndTime.Reset();


            txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
            txtBlockEventAddValidationMessage.Text = "";

            btnEditEventDateAddSave.Content = "Add";
            // change buttons
            btnEditEventDateAddSave.Content = "Add";
            btnEditEventDateCloseCancel.Content = "Cancel";

            // do not display past dates in calendar select
            datePickerEventDate.DisplayDateStart = DateTime.Today;


        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Changed time text boxes to combo boxes so this event is no longer called
        /// 
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
                    //txtBoxEventStartTimeMinute.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                    txtBlockEventAddValidationMessage.Text = "";
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
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Changed time text boxes to combo boxes so this event is no longer called
        /// 
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
                    //cmbStartTimeAMPM.Focus();
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Hidden;
                    txtBlockEventAddValidationMessage.Text = "";
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
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Changed time text boxes to combo boxes so this event is no longer called
        /// 
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
                    txtBlockEventAddValidationMessage.Text = "";
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
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Text changed handler for validating time input
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Changed time text boxes to combo boxes so this event is no longer called
        /// 
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
                    txtBlockEventAddValidationMessage.Text = "";
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
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Click handler for either starting edit mode, adding a date to an event, or updating an en event date
        /// 
        /// Update:
        /// Derrick Nagy
        /// Update: 2022/02/23
        /// Description:
        /// Changed which data grid is used for viewing and editing
        /// 
        /// Update:
        /// Jace Pettinger
        /// Updated: 2022/02/25
        /// 
        /// Description:
        /// Updated validation to fix 24 hour time errors
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Changed time text boxes to combo boxes
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/26
        /// 
        /// Description:
        /// Fixed logic error for time validation.
        /// Cleared validation message for no event dates if a date was added succecssfully
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditEventDateAddSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditEventDateAddSave.Content.Equals("Add Dates")) // show add dates grid to add a new date
            {
                setEventDateTabEditMode();
            }
            else // there should be data in grid to either add or update a record
            {
                // validate input first
                if (datePickerEventDate.SelectedDate == null)
                {
                    txtBlockEventAddValidationMessage.Text = "Please enter a date.";
                    txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                }
                //else if (txtBoxEventStartTimeHour.Text == "" ||
                //                txtBoxEventStartTimeMinute.Text == "" ||
                //                txtBoxEventEndTimeHour.Text == "" ||
                //                txtBoxEventEndTimeMinute.Text == "")
                //{
                //    txtBlockEventAddValidationMessage.Text = "Please enter times for your event to start and end.";
                //    txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                //}
                //else if (Int32.Parse(txtBoxEventStartTimeHour.Text) > 12)
                //{
                //    txtBlockEventAddValidationMessage.Text = "Please enter hours between 1 and 12.";
                //    txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                //}
                //else if (Int32.Parse(txtBoxEventEndTimeHour.Text) > 12)
                //{
                //    txtBlockEventAddValidationMessage.Text = "Please enter hours between 1 and 12.";
                //    txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                //}

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
                        secconds = 0;
                        //bool isAMHour;

                        // add 12 if the start time is in the PM
                        //isAMHour = cmbStartTimeAMPM.Text == "AM";
                        //startHour = Int32.Parse(txtBoxEventStartTimeHour.Text).ConvertTo24HourTime(isAMHour);
                        //startMin = Int32.Parse(txtBoxEventStartTimeMinute.Text);

                        // add 12 if the end time is in the PM
                        //isAMHour = cmbEndTimeAMPM.Text == "AM";
                        //endHour = Int32.Parse(txtBoxEventEndTimeHour.Text).ConvertTo24HourTime(isAMHour);
                        //endMin = Int32.Parse(txtBoxEventEndTimeMinute.Text);

                        // validate time
                        IntegerValidationHelpers.ValidateStartTimeBeforeEndTime(startHour, startMin, endHour, endMin);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem converting the date.\n" + ex.Message, "Problem Converting Date", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    // check to see that one time comes after the other
                    if (startHour > endHour)
                    {
                        //txtBoxEventEndTimeHour.Text = "";
                        //txtBoxEventEndTimeMinute.Text = "";
                        txtBlockEventAddValidationMessage.Text = "The end time is before the start time. Please change.";
                        txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                    }
                    else if (startHour == endHour && startMin >= endMin)
                    {
                        
                            //txtBoxEventEndTimeMinute.Text = "";
                            txtBlockEventAddValidationMessage.Text = "The end time is before the start time. Please change.";
                            txtBlockEventAddValidationMessage.Visibility = Visibility.Visible;
                        
                    }
                    // end of initial validation checks 
                    else
                    {
                        if (btnEditEventDateAddSave.Content.Equals("Add")) // add a new event date
                        {

                            //datEditCurrentEventDates.ItemsSource = _eventDateManager.RetrieveEventDatesByEventID(_event.EventID);

                            try
                            {
                                EventDate newEventDate = new EventDate()
                                {
                                    EventDateID = dateTimeToAdd,
                                    EventID = _event.EventID,
                                    StartTime = new DateTime(year, month, day, startHour, startMin, secconds),
                                    EndTime = new DateTime(year, month, day, endHour, endMin, secconds),
                                    Active = true
                                };

                                _eventDateManager.CreateEventDate(newEventDate);

                                // if the user can edit, use this table
                                if (_userCanEditEvent)
                                {
                                    datEditCurrentEventDates.ItemsSource = _eventDateManager.RetrieveEventDatesByEventID(_event.EventID);
                                }
                                else
                                {
                                    // use this table that can not be edited
                                    datCurrentEventDatesNoEdit.ItemsSource = _eventDateManager.RetrieveEventDatesByEventID(_event.EventID);
                                }

                                // prepare form to add another date
                                setEventDateTabEditMode();

                                txtBlockEventDateValidationMessage.Visibility = Visibility.Hidden;


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("There was a problem adding the date to the event.\n" + ex.Message, "Problem Adding Date", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                        else // update event date
                        {
                            try
                            {
                                EventDate newEventDate = new EventDate()
                                {
                                    EventDateID = dateTimeToAdd,
                                    EventID = _event.EventID,
                                    StartTime = new DateTime(year, month, day, startHour, startMin, secconds),
                                    EndTime = new DateTime(year, month, day, endHour, endMin, secconds),
                                    Active = true
                                };

                                _eventDateManager.UpdateEventDate(_selectedEventDate, newEventDate);
                                setEventDatesTabDetailMode();
                                txtBlockEventDateValidationMessage.Visibility = Visibility.Hidden;
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show("There was a problem updating the date.\n" + ex.Message, "Problem Updating Date", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Button click handler for close / cancel button in event dates tab
        /// </summary>
        private void btnEditEventDateCloseCancel_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditEventDateCloseCancel.Content.ToString() == "Close") // return to view general tab
            {
                tabGeneralEvent.Focus();
            }
            else // Make sure want to close, then return to details view
            {
                var result = MessageBox.Show("Discard unsaved changes?",
                               "Canel",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    setEventDatesTabDetailMode();
                }
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Handler for loading tab when selected
        /// </summary>
        private void tabEventDates_Loaded(object sender, RoutedEventArgs e)
        {
            setEventDatesTabDetailMode();
        }

        /// <summary>
        /// Orginal method contained no comment
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Changed time text boxes to combo boxes so validation of the text box is no longer relevant
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditEventDatesDatGrid_Click(object sender, RoutedEventArgs e)
        {
            // make sure the add/edit dates grid is visable
            grdAddEventDate.Visibility = Visibility.Visible;

            // get event date object and get its parts
            _selectedEventDate = (EventDate)datEditCurrentEventDates.SelectedItem;
            DateTime selectedDate = (DateTime)_selectedEventDate.EventDateID;
            DateTime startTime = (DateTime)_selectedEventDate.StartTime;
            DateTime endTime = (DateTime)_selectedEventDate.EndTime;
            int startHour = startTime.Hour;
            int startMin = startTime.Minute;
            int endHour = endTime.Hour;
            int endMin = endTime.Minute;
            //int startTimeAMPM = 0; // 0 is the index for AM 1 is PM
            //int endTimeAMPM = 0;

            //// subtract 12 if the start time is in the PM
            //if (startHour > 12)
            //{
            //    startHour -= 12;
            //    startTimeAMPM = 1;
            //}
            //// subtract 12 if the end time is in the PM
            //if (endHour > 12)
            //{
            //    endHour -= 12;
            //    endTimeAMPM = 1;
            //}
            ////12 am is 00:00 on 24hr clock
            //if (startHour == 0) 
            //{
            //    startHour = 12;
            //}
            
            // set the content with the data
            datePickerEventDate.SelectedDate = selectedDate;
            //txtBoxEventStartTimeHour.Text = startHour.ToString(); 
            //txtBoxEventStartTimeMinute.Text = startMin.ToString("D2"); // "D2" will give number formatting of 00
            //txtBoxEventEndTimeHour.Text = endHour.ToString();
            //txtBoxEventEndTimeMinute.Text = endMin.ToString("D2");
            //cmbStartTimeAMPM.SelectedIndex = startTimeAMPM;
            //cmbEndTimeAMPM.SelectedIndex = endTimeAMPM;

            // do not show past dates in calendar picker
            datePickerEventDate.DisplayDateStart = DateTime.Today;

            // change buttons
            btnEditEventDateAddSave.Content = "Update";
            btnEditEventDateCloseCancel.Content = "Cancel";

            //focus on first item to set
            datePickerEventDate.Focus();
        }
        // --------------------------------------------------------- End of Dates Tab -----------------------------------------------------------
        // --------------------------------------------------------- Start of Location Tab -----------------------------------------------------------
        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/15
        /// 
        /// Description:
        /// Helper method for loading location tab in location details page
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/03/01
        /// 
        /// Description:
        /// Added _user to constructor
        /// 
        /// Kris Howell
        /// Updated: 2022/03/27
        /// 
        /// Description:
        /// Load new pgLocationFrame rather than old location page
        /// </summary>
        private void tabEventLocation_Loaded(object sender, RoutedEventArgs e)
        {
            
            try
            {
                _location = _locationManager.RetrieveLocationByLocationID((int)_event.LocationID);
                if(_location.LocationID == 0)
                {
                    return;
                }
                pgLocationFrame locationDetailsPage = new pgLocationFrame(_managerProvider, _location, _user);
                locationFrame.Navigate(locationDetailsPage);
                lblLocationErrorMesage.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                // nothing to show
                lblLocationErrorMesage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/02/16
        /// Logic for viewing requests for currently selected event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabEventVolunteerRequests_Loaded(object sender, RoutedEventArgs e)
        {
            setEventVolunteerRequestTabList();
        }

        private void setEventVolunteerRequestTabList()
        {
            int eventID = _event.EventID;

            try
            {
                List<VolunteerRequestViewModel> _requests = _volunteerRequestManager.RetrieveVolunteerRequests(eventID);
                dgRequestList.ItemsSource = _requests;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/02/16
        /// To be added logic for accepting and rejecting requests.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAcceptRequest_Click(object sender, RoutedEventArgs e)
        {
            VolunteerRequest currRequest = (VolunteerRequest)dgRequestList.SelectedItem;
            // logic required to accept DNE

        }

        private void btnRejectRequest_Click(object sender, RoutedEventArgs e)
        {
            VolunteerRequest currRequest = (VolunteerRequest)dgRequestList.SelectedItem;
            // Logic required to reject DNE
        }

        // --------------------------------------------------- Vertical Buttons Click Events --------------------------------------------------------//

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/21
        /// 
        /// Description:
        /// Click event handler to bring up the View Activities page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItinerary_Click(object sender, RoutedEventArgs e)
        {
            pgViewActivities viewActivitiesPage = new pgViewActivities(_event, _managerProvider);
            this.NavigationService.Navigate(viewActivitiesPage);
        }


        // ---------------------------------------------------- End Vertical Buttons Handlers --------------------------------------------------------//
    }
}
