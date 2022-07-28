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
    /// Derrick Nagy
    /// Created: 2022/03/12
    /// 
    /// Description:
    /// Interaction logic for TimeComboBoxes.xaml
    /// </summary>
    public partial class TimeComboBoxes : UserControl
    {        
        private int hour;
        private int minutes;

        public TimeComboBoxes()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Getter for the hour value in the combo box for hour
        /// </summary>
        public int Hour
        {
            get
            {
                string selection = cmboHour.Text.ToString();

                hour = Int32.Parse(selection);

                if (cmboTimeMeridiem.Text.ToString() == "AM" && hour == 12)
                {
                    hour = 0;
                }

                if (cmboTimeMeridiem.Text.ToString() == "PM" && hour != 12)
                {
                    hour += 12;
                }

                return hour;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Getter method for the minutes in the minutes combo box
        /// </summary>
        public int Minutes
        {
            get
            {
                string selection = cmboMinutes.Text.ToString();

                minutes = Int32.Parse(selection);

                return minutes;
            }        
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Display an error message below the hour combo box
        /// </summary>
        public void DisplayInvalidHourMessage()
        {
            txtHourError.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Display an error message below the minute combo box
        /// </summary>
        public void DisplayInvalidMinuteMessage()
        {
            txtMinutesError.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Hide an error message below the hour combo box
        /// </summary>
        public void HideInvalidHourMessage()
        {
            txtHourError.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Hide an error message below the minute combo box
        /// </summary>
        public void HideInvalidMinuteMessage()
        {
            txtMinutesError.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/12
        /// 
        /// Description:
        /// Resets all the combo boxes to their default value
        /// </summary>
        public void Reset()
        {
            cmboHour.SelectedIndex = 11;
            cmboMinutes.SelectedIndex = 0;
            cmboTimeMeridiem.SelectedIndex = 0;
        }
    }
}
