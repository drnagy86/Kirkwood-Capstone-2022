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

namespace WPFPresentation.Supplier
{
    /// <summary>
    /// Interaction logic for pgSupplierFrame.xaml
    /// </summary>
    public partial class pgSupplierFrame : Page
    {
        ManagerProvider _managerProvider;
        DataObjects.Supplier _supplier;
        User _user;

        internal pgSupplierFrame(ManagerProvider managerProvider, DataObjects.Supplier supplier)
        {
            _managerProvider = managerProvider;
            _supplier = supplier;

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
            pgSupplierDetails details = new pgSupplierDetails(_managerProvider, _supplier);
            this.SupplierFrame.NavigationService.Navigate(details);
            btnSupplierDetails.Background = new SolidColorBrush(Colors.Gray);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Inject a new Supplier Details page into the supplier frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupplierDetails_Click(object sender, RoutedEventArgs e)
        {
            Page details = new pgSupplierDetails(_managerProvider, _supplier);
            if (TryNavigateTo(details))
            {
                ResetButtonColors();
                btnSupplierDetails.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Inject a new Supplier Schedule page into the supplier frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupplierSchedule_Click(object sender, RoutedEventArgs e)
        {
            Page schedule = new pgSupplierSchedule(_managerProvider, _supplier);
            if (TryNavigateTo(schedule))
            {
                ResetButtonColors();
                btnSupplierSchedule.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Inject a new Supplier Pricing page into the supplier frame if it is safe to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupplierPricing_Click(object sender, RoutedEventArgs e)
        {
            Page pricing = new pgSupplierPricing(_managerProvider, _supplier);
            if (TryNavigateTo(pricing))
            {
                ResetButtonColors();
                btnSupplierPricing.Background = new SolidColorBrush(Colors.Gray);
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
                    this.SupplierFrame.NavigationService.Navigate(page);
                    return true;
                }
            }

            // no edit ongoing
            this.SupplierFrame.NavigationService.Navigate(page);
            return true;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Helper method to reset all button colors to default
        /// </summary>
        private void ResetButtonColors()
        {
            btnSupplierDetails.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSupplierSchedule.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            btnSupplierPricing.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
        }
    }
}
