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

namespace WPFPresentation.Event
{
    /// <summary>
    /// Interaction logic for pgTaskListEdit.xaml
    /// </summary>
    public partial class pgTaskListEdit : Page
    {
        DataObjects.TasksVM _task = null;
        DataObjects.EventVM _event = null;
        DataObjects.VolunteerNeed _need = null;
        ITaskManager _taskManager = null;
        IEventManager _eventManager = null;
        ISublocationManager _sublocationManager = null;
        IVolunteerNeedManager _needManager = null;
        ManagerProvider _managerProvider = null;

        User _user = null;
        bool _canEditDelete = false;

        // create a priority list to populate in a bit
        List<Priority> _priorities = new List<Priority>();

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Initializes components and sets up task/event managers as well
        /// as the selectedTask object along with a list of priorities
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
        /// Mike Cahow
        /// Updated: 2022/03/25
        /// 
        /// Description:
        /// Added a check to see if a user can edit/delete a task
        /// </summary>
        /// <param name="selectedTask"></param>
        /// <param name="selectedEvent"></param>
        /// <param name="managerProvider"></param>
        /// <param name="user"></param>
        internal pgTaskListEdit(DataObjects.TasksVM selectedTask, DataObjects.EventVM selectedEvent, 
            ManagerProvider managerProvider, User user)
        {
            _taskManager = managerProvider.TaskManager;
            _eventManager = managerProvider.EventManager;
            _managerProvider = managerProvider;
            _needManager = managerProvider.NeedManager;

            _task = selectedTask;
            _event = selectedEvent;
            _sublocationManager = managerProvider.SublocationManager;
            _task.TaskEventName = _event.EventName;
            _task.EventID = _event.EventID;
            _user = user;
            _need = _needManager.RetrieveVolunteerNeedByTaskID(_task.TaskID);
            try
            {
                _canEditDelete = _managerProvider.TaskManager.UserCanEditDeleteTask(_user.UserID);
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was a problem checking to see if you are allowed to edit or delete a task." + ex.Message,
                                    "Edit/Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            InitializeComponent();
        }

        /// <summary>
        /// ??????
        /// Created: ????/??/??
        /// 
        /// Description:
        /// Populate controls on page loaded.
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Raised EditOngoing flag when page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            populateControls();
            try
            {
                _priorities = _taskManager.RetrieveAllPriorities();
                cboPriority.ItemsSource = from p in _priorities
                                          orderby p.PriorityID
                                          select p.Description;
                ValidationHelpers.EditOngoing = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was a problem retrieving the Priority list." + "\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Void method to populate the edit page with the selected Tasks
        /// 
        /// Mike Cahow
        /// Updated: 2022/03/25
        /// 
        /// Description:
        /// Checks if the user can edit or delete a task and populates values
        /// accordingly to be read only or editable.
        /// values. To be called when needed.
        /// 
        /// Derrick Nagy
        /// Upadate: 2022/03/27
        /// 
        /// Description:
        /// Set default date picker time to DateTime.Now if the date was not previously choosen
        /// 
        /// </summary>
        private void populateControls()
        {
            txtBlkEventName.Text = _task.TaskEventName; // pass up event name from view
            txtTaskName.Text = _task.Name.ToString();
            txtTaskName.IsReadOnly = true;
            txtTaskName.IsEnabled = false;
            txtTaskDescription.Text = _task.Description.ToString();
            cboAssign.SelectedItem = "Unavailable"; // pass up volunteer when available

            


            cboPriority.SelectedItem = _task.TaskPriority;
            sldrNumVolunteers.Value = _need.NumTotalVolunteers;
            if(_canEditDelete == true)
            {
                // Edit/Delete mode

                txtBlkEventName.Text = _task.TaskEventName; // pass up event name from view
                txtTaskName.Text = _task.Name.ToString();
                txtTaskName.IsReadOnly = true;
                txtTaskName.IsEnabled = false;
                txtTaskDescription.Text = _task.Description.ToString();
                cboAssign.SelectedItem = "Unavailable"; // pass up volunteer when available
                
                if (_task.DueDate == DateTime.MinValue)
                {
                    dtpTaskDueDate.SelectedDate = DateTime.Now;
                }
                else
                {
                    dtpTaskDueDate.SelectedDate = _task.DueDate;
                }
                cboPriority.SelectedItem = _task.TaskPriority;
                sldrNumVolunteers.Value = _need.NumTotalVolunteers;
            }
            else
            {
                // Set values to read only so they cannot be edited for users that cannot edit.

                txtBlkEventName.Text = _task.TaskEventName; // pass up event name from view
                txtTaskName.Text = _task.Name.ToString();
                txtTaskName.IsReadOnly = true;
                txtTaskDescription.Text = _task.Description.ToString();
                txtTaskDescription.IsReadOnly = true;
                cboAssign.SelectedItem = "Unavailable";
                cboAssign.IsReadOnly = true;
                cboAssign.IsEnabled = false;
                
                if (_task.DueDate == DateTime.MinValue)
                {
                    dtpTaskDueDate.SelectedDate = DateTime.Now;
                }
                else
                {
                    dtpTaskDueDate.SelectedDate = _task.DueDate;
                }
                dtpTaskDueDate.IsEnabled = false;
                cboPriority.SelectedItem = _task.TaskPriority;
                cboPriority.IsReadOnly = true;
                cboPriority.IsEnabled = false;
                sldrNumVolunteers.Value = _need.NumTotalVolunteers;
                sldrNumVolunteers.IsEnabled = false;

                btnDeleteTask.Visibility = Visibility.Hidden;
                btnSaveTask.Visibility = Visibility.Hidden;
                btnCancelTask.Content = "Back";
                btnCancelTask.HorizontalAlignment = HorizontalAlignment.Left;

            }
            
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Checks if the user wants to leave the page without saving update
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// Mike Cahow
        /// Updated: 2022/03/25
        /// 
        /// Description:
        /// Added a check to see if a user can edit or delete to change the
        /// Cancel button to a back button.
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lowered EditOngoing flag before navigating away from page via back button
        /// or successful cancel edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelTask_Click(object sender, RoutedEventArgs e)
        {
            if(_canEditDelete == false)
            {
                ValidationHelpers.EditOngoing = false;
                pgTaskListView viewTasksPage = new pgTaskListView(_event, _managerProvider, _user);
                this.NavigationService.Navigate(viewTasksPage);
            }
            else
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
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Click event handler to save the updated task
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/25
        /// 
        /// Description: Added sublocation manager to navigated page.
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/03/05
        /// 
        /// Description: Added logic to handle requesting a certain number of volunteers
        /// 
        /// Derrick Nagy
        /// Update: 2022/03/27
        /// 
        /// Description:
        /// Handled errors that occur for an unsuccessful update
        /// 
        /// Kris Howell
        /// Update: 2022/03/31
        /// 
        /// Description:
        /// Lowered EditOngoing flag before navigating away in finally block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            if(txtTaskDescription.Text == "" || txtTaskDescription.Text == null)
            {
                MessageBox.Show("Please enter a description field.");
                txtTaskDescription.Focus();
                return;
            }
            int priority = _priorities.First(p => p.Description == cboPriority.Text.ToString()).PriorityID;

            var task = new TasksVM()
            {
                EventID = _task.EventID,
                TaskEventName = _task.TaskEventName,
                TaskID = _task.TaskID,
                Name = _task.Name,
                Description = txtTaskDescription.Text,
                DueDate = (DateTime)dtpTaskDueDate.SelectedDate,
                Priority = priority,
                TaskPriority = cboPriority.Text.ToString(),
                Active = true
            };
            int numTotalVolunteers = (int)sldrNumVolunteers.Value;
            try
            {
                if (_taskManager.EditTask(_task, task) && _needManager.UpdateVolunteerNeed(_need, numTotalVolunteers))
                {
                    MessageBox.Show("Task updated");
                }
                else
                {
                    MessageBox.Show("There was a problem updating the task.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem updating the task." + "\n\n" + ex.Message);
            }
            finally
            {
                ValidationHelpers.EditOngoing = false;
                pgTaskListView viewTasksPage = new pgTaskListView(_event, _managerProvider, _user);
                this.NavigationService.Navigate(viewTasksPage);
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/10
        /// 
        /// Description:
        /// Click event handler for delete button
        /// feature not implemented yet
        /// 
        /// Kris Howell
        /// Updated: 2022/03/31
        /// 
        /// Description:
        /// Lowered EditOngoing flag on successful delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this task from the Task list?\nThis cannot be undone", "Delete Task", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);

            bool taskRemoved = false;
            

            switch (result)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    try
                    {
                        _managerProvider.NeedManager.DeleteVolunteerNeed(_need);
                        taskRemoved = _managerProvider.TaskManager.RemoveTaskByTaskID(_task.TaskID);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("There was a problem deleting this task\n" + ex.Message, "Problem Deleting", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                default:
                    break;
            }

            if (taskRemoved)
            {
                MessageBox.Show("The task has been deleted successfully", "Deletion Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ValidationHelpers.EditOngoing = false;
                pgTaskListView viewTasksPage = new pgTaskListView(_event, _managerProvider, _user);
                this.NavigationService.Navigate(viewTasksPage);
            }

        }
    }
}
