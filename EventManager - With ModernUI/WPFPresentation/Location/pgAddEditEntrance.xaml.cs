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
using DataAccessInterfaces;
using DataObjects;

namespace WPFPresentation.Location
{
    /// <summary>
    /// Interaction logic for pgAddEditEntrance.xaml
    /// </summary>
    public partial class pgAddEditEntrance : Page
    {
        IEntranceManager _entranceManager;
        ManagerProvider _managerProvider;

        Entrance _entrance;
        DataObjects.Location _location;
        User _user;
        int _mode;

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Initializes component and sets up entrance manager with fake and default accessors
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/03/25
        /// Description: added manager provider and removed fake
        /// 
        /// Kris Howell
        /// Updated: 2022/03/26
        /// Description:
        /// Changed constructor to take location object rather than locationID, because we have it
        /// where this is being called, and it is needed to redirect to pgLocationEntrance from here
        /// </summary>
        /// <param name="entrance"></param>
        /// <param name="location"></param>
        /// <param name="managerProvider"></param>
        /// <param name="user"></param>
        /// <param name="mode">1 == add, 2 == edit</param>
        internal pgAddEditEntrance(Entrance entrance, DataObjects.Location location, ManagerProvider managerProvider, User user, int mode)
        {
            _entrance = entrance;
            _location = location;
            _managerProvider = managerProvider;
            _entranceManager = _managerProvider.EntranceManager;
            _user = user;
            _mode = mode;
            InitializeComponent();
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Checks which mode the page should be in and
        /// updates labels accordingly
        /// 
        /// Kris Howell
        /// Updated: 2022/03/27
        /// 
        /// Description:
        /// Initialized Name and Description fields with old values in edit mode,
        /// so they can edit starting with what was already there.
        /// 
        /// Logan Baccam
        /// Updated: 2022/03/29
        /// 
        /// Description:
        /// Hide the delete button when in create entrance mode
        /// 
        /// Kris Howell
        /// Updated: 2022/04/01
        /// 
        /// Description:
        /// Raise EditOngoing flag on page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ValidationHelpers.EditOngoing = true;

            // create entrance mode
            if(_mode == 1)
            {
                btnDeleteEntrance.Visibility = Visibility.Collapsed;
                txtBlkAddEditEntrance.Text = "Create New Entrance";
                btnEntranceAddEdit.Content = "Add";
            }

            if (_mode == 2)
            {
                txtBlkAddEditEntrance.Text = "Update Entrance";
                btnEntranceAddEdit.Content = "Save";
                txtBoxEntranceName.Text = _entrance.EntranceName;
                txtBoxEntranceDescription.Text = _entrance.Description;
            }
        }

        /// <summary>
        /// Alaina Gilson
        /// Created 2022/03/03
        /// 
        /// Description:
        /// Button cancel to take user back to view entrances page
        /// 
        /// Kris Howell
        /// Updated: 2022/03/26
        /// 
        /// Description:
        /// Redirect to new pgLocationEntrance rather than old pgViewLocationDetails
        /// 
        /// Kris Howell
        /// Updated: 2022/03/26
        /// 
        /// Description:
        /// Lower EditOngoing flag on successful cancel
        /// </summary>
        private void btnEntranceCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?\nUnsaved changes will be discarded.",
                               "Cancel",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                ValidationHelpers.EditOngoing = false;
                pgLocationEntrance page = new pgLocationEntrance(_managerProvider, _location, _user);
                this.NavigationService.Navigate(page);
            }
        }

        /// <summary>
        /// Alaina Gilson
        /// Created 2022/03/03
        /// 
        /// Description:
        /// Adds a new entrance to database
        /// 
        /// Alaina Gilson
        /// Updated: 2022/03/08
        /// 
        /// Description:
        /// Added edit mode
        /// 
        /// Kris Howell
        /// Updated: 2022/03/26
        /// 
        /// Description:
        /// Redirect to new pgLocationEntrance rather than old pgViewLocationDetails
        /// 
        /// Kris Howell
        /// Updated: 2022/04/01
        /// 
        /// Description:
        /// Lower EditOngoing flag on successful create or update
        /// </summary>
        private void btnEntranceAddEdit_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBoxEntranceName.Text;
            string description = txtBoxEntranceDescription.Text;

            // create entrance mode
            if(_mode == 1)
            {
                if (name == "")
                {
                    MessageBox.Show("Please enter an entrance name.");
                    txtBoxEntranceName.Focus();
                }
                else if (description == "")
                {
                    MessageBox.Show("Please enter an entrance description.");
                    txtBoxEntranceDescription.Focus();
                }
                else
                {
                    try
                    {
                        _entranceManager.CreateEntrance(_location.LocationID, name, description);
                        //MessageBox.Show("Entrance has been added successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem creating a new entrance.\n\n" + ex.Message);
                    }
                    finally
                    {
                        ValidationHelpers.EditOngoing = false;
                        pgLocationEntrance page = new pgLocationEntrance(_managerProvider, _location, _user);
                        this.NavigationService.Navigate(page);
                    }
                }
            }

            // update entrance mode
            if (_mode == 2)
            {
                if (name == "")
                {
                    MessageBox.Show("Please enter an entrance name.");
                    txtBoxEntranceName.Focus();
                }
                else if (description == "")
                {
                    MessageBox.Show("Please enter an entrance description.");
                    txtBoxEntranceDescription.Focus();
                }
                else
                {
                    try
                    {
                        Entrance newEntrance = new Entrance()
                        {
                            EntranceID = _entrance.EntranceID,
                            EntranceName = name,
                            Description = description
                        };

                        _entranceManager.UpdateEntrance(_entrance, newEntrance);
                        MessageBox.Show("Entrance has been saved successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem saving the entrance.\n\n" + ex.Message);
                    }
                    finally
                    {
                        ValidationHelpers.EditOngoing = false;
                        pgLocationEntrance page = new pgLocationEntrance(_managerProvider, _location, _user);
                        this.NavigationService.Navigate(page);
                    }
                }
            }
        }

        private void btnDeleteEntrance_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete this entrance?", "Are you sure you would like to cancel?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (_mode == 2) 
            {
                switch (result) 
                {
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Yes:
                        try
                        {
                            _entranceManager.RemoveEntranceByEntranceID(_entrance.EntranceID);
                            MessageBox.Show(_entrance.EntranceName + " entrance deleted.");
                            ValidationHelpers.EditOngoing = false;
                            pgLocationEntrance page = new pgLocationEntrance(_managerProvider, _location, _user);
                            this.NavigationService.Navigate(page);
                        }
                        catch (Exception ex) 
                        {
                            MessageBox.Show("Something went wrong when trying to delete this entrance.");
                            btnDeleteEntrance.Focus();
                        }
                            break;
                    default:
                        break;
                }
            }
        }
    }
}
