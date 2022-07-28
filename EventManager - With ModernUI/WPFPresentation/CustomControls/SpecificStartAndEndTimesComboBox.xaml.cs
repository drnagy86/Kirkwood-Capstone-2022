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

namespace WPFPresentation.CustomControls
{
    /// <summary>
    /// Interaction logic for SpecificStartAndEndTimesComboBox.xaml
    /// </summary>
    public partial class SpecificStartAndEndTimesComboBox : UserControl
    {
        private List<string> startTimes = new List<string>();
        private List<string> endTimes = new List<string>();
        public DateTime StartTime { get;  private set; }
        public DateTime EndTime { get; private set; }
        public bool StartIsBeforeEnd { get; private set; }
        private DateTime date;

        public SpecificStartAndEndTimesComboBox()
        {
            InitializeComponent();
            StartIsBeforeEnd = false;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Set the start and end hours for combo boxes
        /// </summary>
        /// <param name="startHour">Start hour</param>
        /// <param name="endHour">End Hour</param>
        /// <param name="date">Date of hours</param>
        public void SetStartandEndHours(int startHour, int endHour, DateTime date)
        {            
            for (int i = startHour; i <= endHour; i++)
            {
                if (i == 0)
                {
                    startTimes.Add("12:00 AM");
                    startTimes.Add("12:30 AM");

                    endTimes.Add("12:00 AM");
                    endTimes.Add("12:30 AM");
                }
                else if (i < 12 )
                {
                    startTimes.Add(i + ":00 AM");
                    startTimes.Add(i + ":30 AM");

                    endTimes.Add(i + ":00 AM");
                    endTimes.Add(i + ":30 AM");
                }
                else if (i == 12)
                {
                    startTimes.Add("12:00 PM");
                    startTimes.Add("12:30 PM");

                    endTimes.Add("12:00 PM");
                    endTimes.Add("12:30 PM");
                }
                else
                {
                    startTimes.Add(i - 12 + ":00 PM");
                    startTimes.Add(i - 12 + ":30 PM");

                    endTimes.Add(i - 12 + ":00 PM");
                    endTimes.Add(i - 12 + ":30 PM");
                }
            }
            cmboStartHour.ItemsSource = startTimes;
            cmboEndHour.ItemsSource = endTimes;
            this.date = date;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Clears the values in the combo boxes for start and end hours
        /// </summary>
        public void Clear()
        {
            cmboStartHour.ItemsSource = null;
            cmboEndHour.ItemsSource = null;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Sets the startime property to the selected item. Validates to make sure that the start is before the end if an end time and start time is selected
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmboStartHour_DropDownClosed(object sender, EventArgs e)
        {
            if (cmboStartHour.SelectedItem != null)
            {
                string[] startTimeArray = cmboStartHour.SelectedItem.ToString().Split(' ', ':');
                int[] startTimeIntArray = new int[2];
                bool isPM = (startTimeArray[2] == "PM") ? true : false;

                for (int i = 0; i < startTimeArray.Length - 1; i++)
                {
                    startTimeIntArray[i] = Int32.Parse(startTimeArray[i]);
                }

                if (isPM && startTimeIntArray[0] != 12)
                {
                    startTimeIntArray[0] += 12;
                }
                if (!isPM && startTimeIntArray[0] == 12)
                {
                    startTimeIntArray[0] = 0;
                }

                StartTime = new DateTime(date.Year, date.Month, date.Day, startTimeIntArray[0], startTimeIntArray[1], 0);

                validateTimes();
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Sets the EndTime property to the selected item. Validates to make sure that the start is before the end if an end time and start time is selected
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmboEndHour_DropDownClosed(object sender, EventArgs e)
        {
            if (cmboEndHour.SelectedItem != null)
            {
                string[] endTimeArray = cmboEndHour.SelectedItem.ToString().Split(' ', ':');
                int[] endTimeIntArray = new int[2];
                bool isPM = (endTimeArray[2] == "PM") ? true : false;

                for (int i = 0; i < endTimeArray.Length - 1; i++)
                {
                    endTimeIntArray[i] = Int32.Parse(endTimeArray[i]);
                }

                if (isPM && endTimeIntArray[0] != 12)
                {
                    endTimeIntArray[0] += 12;
                }
                if (!isPM && endTimeIntArray[0] == 12)
                {
                    endTimeIntArray[0] = 0;
                }

                EndTime = new DateTime(date.Year, date.Month, date.Day, endTimeIntArray[0], endTimeIntArray[1], 0);
                validateTimes();
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// If both combo boxes have a value selected, makes sure that one is before the other.
        /// Gives a validation message to the user if invalid.
        /// 
        /// </summary>
        private void validateTimes()
        {
            txtStartHourError.Text = "";
            txtStartHourError.Visibility = Visibility.Hidden;

            if (StartTime != DateTime.MinValue && EndTime != DateTime.MinValue)
            {
                if (StartTime < EndTime)
                {
                    StartIsBeforeEnd = StartTime < EndTime;
                }
                else if (StartTime == EndTime)
                {
                    txtStartHourError.Text = "The start time and end time are the same";
                    txtStartHourError.Visibility = Visibility.Visible;
                    StartIsBeforeEnd = false;
                }
                else
                {
                    txtStartHourError.Text = "The start time can't be after the end time";
                    txtStartHourError.Visibility = Visibility.Visible;
                    StartIsBeforeEnd = false;
                }
            }
        }


    }
}
