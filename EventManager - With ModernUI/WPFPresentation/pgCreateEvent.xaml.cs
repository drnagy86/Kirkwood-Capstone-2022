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

namespace WPFPresentation
{
    public partial class pgCreateEvent : Page
    {

        IEventManager _eventManager = null;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Initializes component and sets up event manager with fake and default accessors
        /// </summary>
        public pgCreateEvent()
        {
            // use fake accessor
            //_eventManager = new LogicLayer.EventManager(new EventAccessorFake());

            // use default accessor
            _eventManager = new LogicLayer.EventManager();

            InitializeComponent();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Click event handler for creating a new event
        /// </summary>
        private void btnEventNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _eventManager.CreateEvent(txtBoxEventName.Text, txtBoxEventDescription.Text);
                MessageBox.Show("Added event.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating a new event.\n" + ex.Message);
            }
        }
    }
}
