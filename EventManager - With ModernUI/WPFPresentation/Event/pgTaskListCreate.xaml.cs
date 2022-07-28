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
using WPFPresentation.Event;

namespace WPFPresentation
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Interaction logic for pgTaskListCreate.xamls
    /// </summary
    public partial class pgTaskListCreate : Page
    {

        ITaskManager _taskManager = null;
        IEventManager _eventManager = null;
        ISublocationManager _sublocationManager = null;
        IVolunteerManager _volunteerManager = null;
        ManagerProvider _managerProvider = null;
        IVolunteerNeedManager _needManager = null;
        DataObjects.EventVM _event = null;
        User _user = null;

        // priority values to populate cboPriority
        List<Priority> _priorities = new List<Priority>();

        // volunteers to populate cboAssign
        List<Volunteer> _volunteers = new List<Volunteer>();

        // list of volunteers assigned to task
        List<String> _assigned = new List<String>();
        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Initializes component and sets up task manager with either fake or default accessors
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
        /// </summary>
        /// <param name="selectedEvent"></param>
        /// <param name="managerProvider"></param>
        /// <param name="user"></param>
        internal pgTaskListCreate(DataObjects.EventVM selectedEvent, ManagerProvider managerProvider, User user)
        {
            _managerProvider = managerProvider;
            _taskManager = managerProvider.TaskManager;
            _eventManager = managerProvider.EventManager;
            _volunteerManager = managerProvider.VolunteerManager;
            _needManager = managerProvider.NeedManager;
            _event = selectedEvent;
            _user = user;

            _sublocationManager = managerProvider.SublocationManager;

            InitializeComponent();
        }

        /// <summary>
        /// Mike Cahow
        /// Created 2022/01/24
        /// 
        /// Description:
        /// Using a load event to populate the priority drop down with it's string values of Description
        /// rather than the int values that are used in the Tasks data object
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Raise EditOngoing flag when page is loaded
        /// Jace Pettinger
        /// Updated 2022/03/20
        /// 
        /// Description:
        /// Adding functionality to combo box for assign volunteers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            txtBlkEventName.Text = _event.EventName;
            try
            {
                _priorities = _taskManager.RetrieveAllPriorities();
                cboPriority.ItemsSource = from p in _priorities
                                          orderby p.PriorityID
                                          select p.Description;
                _volunteers = _volunteerManager.RetrieveAllVolunteers();
                foreach (var volunteer in _volunteers)
                {
                    cboAssign.Items.Add(volunteer.GivenName);
                }


                //if (cboSchedulePicker.Items.Count == 0)
                //{
                //    cboSchedulePicker.Items.Add(_location.Name);

                //    foreach (Sublocation sublocation in _sublocations)
                //    {
                //        cboSchedulePicker.Items.Add(sublocation.SublocationName);
                //    }
                //}
                //_sublocationID = _sublocations.First(m => m.SublocationName == cboSchedulePicker.SelectedItem.ToString()).SublocationID;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Priority list was not retrieved." + "\n\n" + ex.Message);
            }

            ValidationHelpers.EditOngoing = true;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Click event handler to create a task
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/03/05
        /// 
        /// Description: Added logic to handle requesting volunteers during task creation
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lowered EditOngoing flag when task is saved successfully
        /// Jace Pettinger
        /// Updated: 2022/03/20
        /// 
        /// Description: Finished logic to assign volunteers to task
        /// </summary>
        private void btnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            int eventID = _event.EventID;

            if (txtTaskName.Text == "" || txtTaskName.Text == null)
            {
                MessageBox.Show("Task name cannot be blank.");
                txtTaskName.Focus();
                return;
            }

            string taskName = txtTaskName.Text.ToString();

            if (txtTaskDescription.Text == "" || txtTaskDescription.Text == null)
            {
                MessageBox.Show("Task description cannot be blank.");
                txtTaskDescription.Focus();
                return;
            }

            string taskDescription = txtTaskDescription.Text.ToString();

            if (dtpTaskDueDate.SelectedDate == null)
            {
                MessageBox.Show("Please set a due date before continuing");
                dtpTaskDueDate.Focus();
                return;
            }

            DateTime taskDueDate = (DateTime)dtpTaskDueDate.SelectedDate;

            if (cboPriority.SelectedItem == null)
            {
                MessageBox.Show("Please set a priority before continuing.");
                cboPriority.Focus();
                return;
            }
            int priority = _priorities.First(p => p.Description == cboPriority.Text.ToString()).PriorityID;

            if (_assigned.Count == 0)
            {
                string message = "Continue without assigning volunteers?";
                string title = "No volunteers assigned.";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage image = MessageBoxImage.Warning;
                var result = MessageBox.Show(message, title, buttons, image);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }
            var task = new Tasks()
            {
                EventID = eventID,
                Name = taskName,
                Description = taskDescription,
                DueDate = taskDueDate,
                Priority = priority
            };
            int numTotalVolunteers = (int)sldrNumVolunteers.Value;
            try
            {
                int taskID = _taskManager.AddTask(task, numTotalVolunteers);
                int taskAssignmentID = _taskManager.AddTaskAssignment(taskID);
                if (_assigned.Count != 0)
                {
                    foreach (var volunteerName in _assigned)
                    {
                        int taskVolunteerID = _volunteers.First(m => m.GivenName == volunteerName).UserID;
                        int volunteerID = (int)taskVolunteerID;
                        _taskManager.AddVolunteerToTaskAssignment(taskAssignmentID, volunteerID);
                        var need = _needManager.RetrieveVolunteerNeedByTaskID(taskID);
                        _needManager.UpdateCurrVolunteers(need, 1);
                    }

                }
                MessageBox.Show("Task has been added.");
                ValidationHelpers.EditOngoing = false;
                pgTaskListView viewTasksPage = new pgTaskListView(_event, _managerProvider, _user);
                this.NavigationService.Navigate(viewTasksPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating the new task." + "\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Create: 2022/02/04
        /// 
        /// Description:
        /// Event handler for the cancel button. If the yes is clicked in the dialog box
        /// it sends user back to pgTaskListView. If no is clicked then user remains on 
        /// current page
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lowered EditOngoing flag on successful cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelTask_Click(object sender, RoutedEventArgs e)
        {
            string message = "Task will not be saved if you stop now.";
            string title = "Stop creating Task?";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.Warning;
            var result = MessageBox.Show(message, title, buttons, image);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                ValidationHelpers.EditOngoing = false;
                pgTaskListView viewTasksPage = new pgTaskListView(_event, _managerProvider, _user);
                this.NavigationService.Navigate(viewTasksPage);
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Event handler to show and hide assign volunteers depending on the number volunteers needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sldrNumVolunteers_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var numAssigned = datAssigned.Items.Count;
            var numRequested = sldrNumVolunteers.Value;
            if (numRequested < numAssigned)
            {
                sldrNumVolunteers.Value = numAssigned;
                MessageBox.Show("Cannot request less volunteers than the number of volunteers assigned");
            }
            else if (numRequested == 0)
            {
                datAssigned.Visibility = Visibility.Hidden;
                txtBlkAssigned.Visibility = Visibility.Hidden;
                cboAssign.Visibility = Visibility.Hidden;
                txtBlkTaskAssign.Visibility = Visibility.Hidden;
            }
            else
            {
                datAssigned.Visibility = Visibility.Visible;
                txtBlkAssigned.Visibility = Visibility.Visible;
                cboAssign.Visibility = Visibility.Visible;
                txtBlkTaskAssign.Visibility = Visibility.Visible;
                btnAssignVolunteer.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Event handler to add volunteer to list of assigned volunteers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAssignVolunteer_Click(object sender, RoutedEventArgs e)
        {
            if (_assigned.Count >= 10)
            {
                MessageBox.Show("Cannot assign more than 10 volunteers to a task.");
            }
            else if (cboAssign.SelectedItem == null)
            {
                MessageBox.Show("Please select a volunteer to assign");
            }
            else
            {
                string volunteerName = cboAssign.SelectedItem.ToString();
                _assigned.Add(volunteerName);
                cboAssign.Items.Remove(volunteerName);
                datAssigned.Items.Add(volunteerName);
                sldrNumVolunteers.Value = datAssigned.Items.Count;
            }
        }
    }
}
