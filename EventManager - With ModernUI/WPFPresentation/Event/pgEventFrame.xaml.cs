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


namespace WPFPresentation.Event
{
    /// <summary>
    /// Interaction logic for pgEventFrame.xaml
    /// </summary>
    public partial class pgEventFrame : Page
    {
        ManagerProvider _managerProvider;
        DataObjects.EventVM _event;
        User _user;

        internal pgEventFrame(DataObjects.EventVM eventParam, ManagerProvider managerProvider, User user)
        {
            _managerProvider = managerProvider;
            _event = eventParam;
            _user = user;

            InitializeComponent();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Populate empty frame with details page by default on load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Page details = new pgEventEditDetail(_event, _managerProvider, _user);
            this.EventFrame.NavigationService.Navigate(details);
            btnEventDetails.Background = new SolidColorBrush(Colors.Gray);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Inject a new Event Details page into the event frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDetails_Click(object sender, RoutedEventArgs e)
        {
            Page details = new pgEventEditDetail(_event, _managerProvider, _user);
            if (TryNavigateTo(details))
            {
                ResetButtonColors();
                btnEventDetails.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Inject a new Task List page into the event frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            Page taskList = new pgTaskListView(_event, _managerProvider, _user);
            if (TryNavigateTo(taskList))
            {
                ResetButtonColors();
                btnTasks.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Inject a new View Activities page into the event frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItinerary_Click(object sender, RoutedEventArgs e)
        {
            Page viewActivitiesPage = new pgViewActivities(_event, _managerProvider);
            if (TryNavigateTo(viewActivitiesPage))
            {
                ResetButtonColors();
                btnItinerary.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Helper method to safely try to navigate to a new page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>true if successfully navigated page, else false</returns>
        private bool TryNavigateTo(Page page)
        {
            if (ValidationHelpers.EditOngoing)
            {
                MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return false;
                }
                else // yes, discard changes
                {
                    ValidationHelpers.EditOngoing = false;
                    this.EventFrame.NavigationService.Navigate(page);
                    return true;
                }
            }

            // no edit ongoing
            this.EventFrame.NavigationService.Navigate(page);
            return true;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Helper method to reset all button colors to default
        /// </summary>
        private void ResetButtonColors()
        {
            btnEventDetails.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnTasks.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnItinerary.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            //btnBudget.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            //btnAdvertising.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            //btnAfterEventReport.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            //btnFiles.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            //btnInvitations.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
        }
    }
}
