using DataObjects;
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

namespace WPFPresentation.Duplicate
{
    /// <summary>
    /// Vinayak Deshpande
    /// Created: 2022/04/02
    /// 
    /// Description:
    /// Interaction logic for pgDuplicateEvent.xaml
    /// </summary>
    public partial class pgDuplicateEvent : Page
    {
        ITaskManager _taskManager = null;
        IEventManager _eventManager = null;
        IVolunteerNeedManager _needManager = null;
        IActivityManager _activityManager = null;
        IEventDateManager _eventDateManager = null;
        EventVM _oldEvent = null;
        DataObjects.Event _newEvent = null;
        User _user = null;
        ManagerProvider _managerProvider = null;
        List<TasksVM> _oldTasks = new List<TasksVM>();
        List<VolunteerNeedVM> _oldNeeds = new List<VolunteerNeedVM>();
        List<ActivityVM> _oldActivities = new List<ActivityVM>();
        List<VolunteerNeedVM> _dupedNeeds = new List<VolunteerNeedVM>();
        List<ActivityVM> _dupedActivities = new List<ActivityVM>();
        internal pgDuplicateEvent(EventVM oldEvent, DataObjects.Event newEvent, ManagerProvider managerProvider, User user)
        {
            _managerProvider = managerProvider;
            _taskManager = managerProvider.TaskManager;
            _eventManager = managerProvider.EventManager;
            _needManager = managerProvider.NeedManager;
            _activityManager = managerProvider.ActivityManager;
            _eventDateManager = managerProvider.EventDateManager;
            _oldEvent = oldEvent;
            _newEvent = newEvent;
            _user = user;
            InitializeComponent();
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Logic for moving from task duplication to activity duplication.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDupeTasksNext_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Finish Duplicating Tasks and Move on to Activities?",
                               "Finish Task Duplication?",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                tabDuplicateTasks.IsEnabled = false;
                tabDuplicateActivities.IsEnabled = true;
                tabDuplicateActivities.Focus();
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// logic for deciding to not duplicate tasks or activities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDupeTasksCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Cancel Easy Duplication of Tasks and Activities?", 
                               "Cancel Duplication?",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                pgViewEvents viewEventsPage = new pgViewEvents(_user, _managerProvider);
                this.NavigationService.Navigate(viewEventsPage);
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description: logic for duplicating selected tasks from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDuplicateTasks_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Duplicate Selected Tasks?",
                               "Duplicate?",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                List<VolunteerNeedVM> tempNeeds = new List<VolunteerNeedVM>();
                foreach (var dupeTask in datDuplicationTasks.SelectedItems)
                {
                    tempNeeds.Add((VolunteerNeedVM)dupeTask);
                }
                try
                {
                    foreach (var task in tempNeeds)
                    {
                        Tasks tempTask = new Tasks()
                        {
                            EventID = _newEvent.EventID,
                            Name = task.Name,
                            Description = task.Description,
                            DueDate = _eventDateManager.RetrieveEventDatesByEventID(_newEvent.EventID)[0].EventDateID != null ? _eventDateManager.RetrieveEventDatesByEventID(_newEvent.EventID)[0].EventDateID : DateTime.MaxValue,
                            Priority = task.Priority
                        };
                        int numTotalVolunteers = task.NumTotalVolunteers;
                        try
                        {
                            _taskManager.AddTask(tempTask, numTotalVolunteers);
                            MessageBox.Show(task.Name + " has been duplicated");
                            _oldNeeds.Remove(task);
                            datDuplicationTasks.ItemsSource = _oldNeeds;
                            datDuplicationTasks.Items.Refresh();
                            btnDuplicateTasks.IsEnabled = false;
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("There was a problem duplicating " + task.Name + "." + "\n\n" + ex.Message);
                        }
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("There was a problem duplicating the selected tasks." + "\n\n" + ex.Message);
                }
                
            }
            
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:logic for duplicating selected activities from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDuplicateActivities_Click(object sender, RoutedEventArgs e)
        {
            // if event has no event dates, cannot create activity
            if (_eventDateManager.RetrieveEventDatesByEventID(_newEvent.EventID).Count == 0)
            {
                MessageBox.Show("This event does not have any registered dates.\n" +
                                "Please add an event date before adding an activity to this event.");
                btnDupeActivitiesDone_Click(sender, e);
            }
            if (_oldEvent.LocationID == null)
            {
                MessageBox.Show("This event does not have a registered location.\n" +
                               "Please register a location before adding an activity to this event.");
                btnDupeActivitiesDone_Click(sender, e);
            }
            var result = MessageBox.Show("Duplicate Selected Tasks?",
                               "Duplicate?",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                List<ActivityVM> tempActivities = new List<ActivityVM>();
                foreach (var dupeActivity in datDuplicationActivities.SelectedItems)
                {
                    tempActivities.Add((ActivityVM)dupeActivity);
                }
                try
                {
                    foreach (var activity in tempActivities)
                    {
                        ActivityVM tempActivity = new ActivityVM();
                        tempActivity.EventID = _newEvent.EventID;
                        tempActivity.ActivityName = activity.ActivityName;
                        tempActivity.ActivityDescription = activity.ActivityDescription;
                        tempActivity.StartTime = activity.StartTime;
                        tempActivity.EndTime = activity.EndTime;
                        tempActivity.EventDateID = _eventDateManager.RetrieveEventDatesByEventID(_newEvent.EventID)[0].EventDateID;
                        tempActivity.PublicActivity = activity.PublicActivity;
                        tempActivity.SublocationID = activity.SublocationID;
                        tempActivity.ActivityImageName = activity.ActivityImageName != null ? activity.ActivityImageName : "";
                        try
                        {
                            _activityManager.CreateActivity(tempActivity);
                            MessageBox.Show(tempActivity.ActivityName + " has been duplicated");
                            _oldActivities.Remove(activity);
                            datDuplicationActivities.ItemsSource = _oldActivities;
                            datDuplicationActivities.Items.Refresh();
                            btnDuplicateActivities.IsEnabled = false;
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("There was a problem duplicating " + tempActivity.ActivityName + "." + "\n\n" + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("There was a problem duplicating the selected activity." + "\n\n" + ex.Message);
                }
                
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Logic to finish the duplication process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDupeActivitiesDone_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Finish Duplicating Activities?",
                               "Finish Application Duplication?",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                System.Windows.MessageBox.Show("Activity Duplication Complete.");
                pgViewEvents viewEventsPage = new pgViewEvents(_user, _managerProvider);
                // ValidationHelpers.EditOngoing = false;
                this.NavigationService.Navigate(viewEventsPage);
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// logic for deciding not to duplicate activities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDupeActivitiesCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Cancel Easy Duplication of Activities?",
                               "Cancel Duplication?",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                pgViewEvents viewEventsPage = new pgViewEvents(_user, _managerProvider);
                // ValidationHelpers.EditOngoing = false;
                this.NavigationService.Navigate(viewEventsPage);
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Fills both datagrids when page loads
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/04/14
        /// 
        /// Description:
        /// Tells user there is nothing to duplicate and skips the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtBoxOldEventNameActivities.Text = _oldEvent.EventName;
            txtBoxOldEventNameTasks.Text = _oldEvent.EventName;
            SetUpDupeTasks();
            try
            {
                _oldActivities = _activityManager.RetrieveActivitiesByEventIDForVM(_oldEvent.EventID);
                if (_oldActivities.Count == 0)
                {
                    MessageBox.Show("This Event did not have any activites to duplicate." + "\n\n");
                    pgViewEvents viewEventsPage = new pgViewEvents(_user, _managerProvider);
                    // ValidationHelpers.EditOngoing = false;
                    this.NavigationService.Navigate(viewEventsPage);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("This Event did not have any activites to duplicate." + "\n\n" + ex.Message);
            }
            datDuplicationActivities.ItemsSource = _oldActivities;
            if (_oldActivities.Count == 0)
            {
                btnDuplicateActivities.IsEnabled = false;
            }
            tabDuplicateActivities.IsEnabled = false;

        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Code for filling list of dupable tasks
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/04/14
        /// 
        /// Description:
        /// Tells user there is nothing to duplicate and skips the screen
        /// </summary>
        private void SetUpDupeTasks()
        {
            try
            {
                _oldTasks = _taskManager.RetrieveAllTasksByEventID(_oldEvent.EventID);
                if (_oldTasks.Count == 0)
                {
                    MessageBox.Show("This Event did not have any tasks to duplicate." + "\n\n");
                    tabDuplicateTasks.IsEnabled = false;
                    tabDuplicateActivities.IsEnabled = true;
                    tabDuplicateActivities.Focus();
                }
                else
                {
                    foreach (var task in _oldTasks)
                    {
                        VolunteerNeed tempNeed = _needManager.RetrieveVolunteerNeedByTaskID(task.TaskID);
                        VolunteerNeedVM needVM = new VolunteerNeedVM();
                        needVM.TaskID = tempNeed.TaskID;
                        needVM.NumTotalVolunteers = tempNeed.NumTotalVolunteers;
                        needVM.NumCurrVolunteers = tempNeed.NumCurrVolunteers;
                        needVM.Name = task.Name;
                        needVM.Description = task.Description;
                        needVM.DueDate = task.DueDate;
                        needVM.Priority = task.Priority;
                        needVM.CompletionDate = task.CompletionDate;
                        needVM.ProofID = task.ProofID;
                        needVM.isDone = task.isDone;
                        needVM.EventID = task.EventID;
                        needVM.Active = task.Active;
                        needVM.TaskPriority = task.TaskPriority;
                        needVM.TaskEventName = task.TaskEventName;

                        _oldNeeds.Add(needVM);

                    }
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("This Event did not have any tasks to duplicate." + "\n\n" + ex.Message);
            }

            datDuplicationTasks.ItemsSource = _oldNeeds;
            if (_oldNeeds.Count == 0)
            {
                btnDuplicateTasks.IsEnabled = false;
            }
        }

        private void datDuplicationTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDuplicateTasks.IsEnabled = true;
        }

        private void datDuplicationActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDuplicateActivities.IsEnabled = true;
        }
    }
}
