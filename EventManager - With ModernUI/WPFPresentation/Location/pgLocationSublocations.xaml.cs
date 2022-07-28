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

namespace WPFPresentation.Location
{
    /// <summary>
    /// Interaction logic for pgLocationSublocations.xaml
    /// 
    /// Kris Howell
    /// Created: 2022/03/24
    /// 
    /// Description:
    /// Pulled from old pgViewLocationDetails page while separating functions
    /// into separate pages
    /// </summary>
    public partial class pgLocationSublocations : Page
    {
        ManagerProvider _managerProvider;
        ISublocationManager _sublocationManager;

        DataObjects.Location _location;
        List<Sublocation> _sublocations;

        internal pgLocationSublocations(ManagerProvider managerProvider, DataObjects.Location location)
        {
            _managerProvider = managerProvider;
            _sublocationManager = managerProvider.SublocationManager;
            _location = location;

            InitializeComponent();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Populate information on page on initial load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadPage();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Helper method which attempts to reload the information in the page.
        /// </summary>
        private void ReloadPage()
        {
            if (ValidationHelpers.EditOngoing)
            {
                MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    ValidationHelpers.EditOngoing = false;
                }
            }
            scrSublocations.Visibility = Visibility.Visible;
            grdAddsublocation.Visibility = Visibility.Collapsed;
            try
            {
                _sublocations = _sublocationManager.RetrieveSublocationsByLocationID(_location.LocationID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n\n" + ex.InnerException.Message);
            }

            btnCancelEditAreas.Visibility = Visibility.Collapsed;
            btnSaveAreas.Visibility = Visibility.Collapsed;

            // redraw sublocations
            grdSublocationsRows.RowDefinitions.Clear();
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(150);
            grdSublocationsRows.RowDefinitions.Add(row);
            grdSublocationsRows.RowDefinitions.Add(new RowDefinition());
            populateSublocations();
        }

        /// <summary>
        /// Logan Baccam
        /// Created 2022/02/28
        /// 
        /// Description:
        /// Handler to make the addsublocation page visible
        /// 
        /// Christopher Repko
        /// Updated: 2022/03/11
        /// 
        /// Description:
        /// Added check for the edit flag
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationHelpers.EditOngoing)
            {
                MessageBoxResult result = MessageBox.Show("This will discard changes. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    ValidationHelpers.EditOngoing = false;
                }
            }
            scrSublocations.Visibility = Visibility.Collapsed;
            grdAddsublocation.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Click event for edit button. Sets the sublocations view to edit mode.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnCancelEditAreas.Visibility = Visibility.Visible;
            btnSaveAreas.Visibility = Visibility.Visible;
            foreach (UIElement element in grdSublocationsRows.Children)
            {
                if (element is Label label)
                {
                    label.Content = "Area Name: ";
                }
                else if (element is TextBox textbox)
                {
                    textbox.IsReadOnly = false;
                    textbox.IsEnabled = true;
                    textbox.Visibility = Visibility.Visible;
                }
                else if (element is Button btn)
                {
                    if (btn.Name.Contains("btnDelete"))
                    {
                        btn.Visibility = Visibility.Visible;
                    }
                }
            }
            // Need to manually update this.
            txtSublocationName.Text = _sublocations[0].SublocationName;
            ValidationHelpers.EditOngoing = true;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Click event for delete buttons. Deactivates the sublocation for that delete button.
        /// 
        /// Update:
        /// Kris Howell
        /// Updated: 2022/03/24
        /// 
        /// Dsecription:
        /// Commented call to btnSiteAreas_Click() from old page structure.
        /// Replaced it with new ReloadPage() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete0_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This will permanently remove the area. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            if (sender is Button btn)
            {
                string index = btn.Name.Replace("btnDelete", "");
                try
                {
                    int i = int.Parse(index);
                    if (i < _sublocations.Count())
                    {
                        this._sublocationManager.DeactivateSublocationBySublocationID(_sublocations[i].SublocationID);

                        // Redraw the screen for the new set of sublocations
                        ValidationHelpers.EditOngoing = false;
                        // this.btnSiteAreas_Click(sender, e);
                        ReloadPage();
                        if (_sublocations.Count() > 0)
                        {
                            this.btnEdit_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("All sublocations have been removed. Exiting edit mode...");
                        }
                    }
                    else
                    {
                        throw new IndexOutOfRangeException("Sublocation index was out of range");
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Delete button is no longer valid.");
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message + "\n\n\n\n\n" + ex.InnerException.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete sublocation: \n\n\n\n\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Logan Baccam
        /// Created 2022/02/28
        /// 
        /// Description:
        /// Handler to add a new sublocation to a location
        /// 
        /// Update:
        /// Kris Howell
        /// Updated: 2022/03/24
        /// 
        /// Dsecription:
        /// Commented call to btnSiteAreas_Click() from old page structure.
        /// Replaced it with new ReloadPage() method
        /// </summary>
        private void btnAddNewSublocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNewSublocationDesc.Text.Length >= 1001)
                {
                    MessageBox.Show("must be between 1-1000 characters.");
                    txtNewSublocationDesc.Focus();
                }
                else if (txtNewSublocationName.Text.Length >= 161 || txtNewSublocationName.Text.Length <= 0)
                {
                    MessageBox.Show("must be between 1-160 characters.");
                    txtNewSublocationName.Focus();
                }
                else
                {
                    _sublocationManager.CreateSublocationByLocationID(_location.LocationID, txtNewSublocationName.Text, txtNewSublocationDesc.Text);
                    hideAddSublocationScreen();
                    scrSublocations.Visibility = Visibility.Visible;
                    //btnSiteAreas_Click(sender, e);
                    ReloadPage();


                    MessageBox.Show("Area added.");
                    txtNewSublocationName.Text = "";
                    txtNewSublocationDesc.Text = "";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong trying to add this area.", ex.Message);
                txtNewSublocationName.Focus();
            }
        }

        /// <summary>
        /// Logan Baccam
        /// Created 2022/03/01
        /// 
        /// Description:
        /// Handler to cancel adding a new sublocation to a location
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel? Any unsaved work will be lost.", "Are you sure you would like to cancel?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    MessageBox.Show("Canceled");
                    txtNewSublocationDesc.Text = "";
                    txtNewSublocationName.Text = "";
                    hideAddSublocationScreen();
                    scrSublocations.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Click event for save button. Saves changes to sublocations.
        /// 
        /// Update:
        /// Kris Howell
        /// Updated: 2022/03/24
        /// 
        /// Dsecription:
        /// Commented call to btnSiteAreas_Click() from old page structure.
        /// Replaced it with new ReloadPage() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAreas_Click(object sender, RoutedEventArgs e)
        {
            // Stick the whole thing in a try block. We don't want to skip only one sublocation if an error occurs.
            try
            {
                if (!txtSublocationDescription.Text.Equals(_sublocations[0].SublocationDescription) || !txtSublocationName.Text.Equals(_sublocations[0].SublocationName))
                {
                    if (txtSublocationName.Text.Trim().Length > 0 && txtSublocationName.Text.Trim().Length < 160)
                    {
                        if (txtSublocationDescription.Text.Trim().Length < 1000)
                        {
                            Sublocation newSublocation = new Sublocation()
                            {
                                SublocationID = _sublocations[0].SublocationID,
                                LocationID = _sublocations[0].LocationID,
                                SublocationName = txtSublocationName.Text,
                                SublocationDescription = txtSublocationDescription.Text,
                                Active = _sublocations[0].Active,
                            };
                            this._managerProvider.SublocationManager.EditSublocationBySublocationID(_sublocations[0], newSublocation);
                            _sublocations[0] = newSublocation;

                        }
                        else
                        {
                            MessageBox.Show("Area description must be less than 1000 characters.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Area name must be between 1-100 characters.");
                        return;
                    }
                }
                for (int i = 1; i < _sublocations.Count; i++)
                {
                    TextBox txtName = (TextBox)grdSublocationsRows.FindName("txtSublocationName" + i);
                    TextBox txtDescription = (TextBox)grdSublocationsRows.FindName("txtSublocationDescription" + i);
                    if (!(txtName is null) && !(txtDescription is null) &&
                        (!txtName.Text.Equals(_sublocations[i].SublocationName) || !txtDescription.Text.Equals(_sublocations[i].SublocationDescription)))
                    {
                        if (txtName.Text.Trim().Length > 0 && txtName.Text.Trim().Length < 160)
                        {
                            if (txtDescription.Text.Trim().Length < 1000)
                            {
                                Sublocation newSublocation = new Sublocation()
                                {
                                    SublocationID = _sublocations[i].SublocationID,
                                    LocationID = _sublocations[i].LocationID,
                                    SublocationName = txtName.Text,
                                    SublocationDescription = txtDescription.Text,
                                    Active = _sublocations[i].Active,
                                };

                                this._managerProvider.SublocationManager.EditSublocationBySublocationID(_sublocations[i], newSublocation);
                                _sublocations[i] = newSublocation;
                            }
                            else
                            {
                                MessageBox.Show("Area description must be less than 1000 characters.");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Area name must be between 1-100 characters.");
                            return;
                        }
                    }
                }
                ValidationHelpers.EditOngoing = false;
                // btnSiteAreas_Click(sender, e);
                ReloadPage();
            }
            catch (Exception ex)
            {
                try
                {
                    MessageBox.Show(ex.Message + "\n\n\n" + ex.InnerException.Message);
                }
                catch (Exception)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Click event for edit button. Discards any changes to sublocations.
        /// 
        /// Update:
        /// Kris Howell
        /// Updated: 2022/03/24
        /// 
        /// Dsecription:
        /// Commented call to btnSiteAreas_Click() from old page structure.
        /// Replaced it with new ReloadPage() method
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelEditAreas_Click(object sender, RoutedEventArgs e)
        {
            // btnSiteAreas_Click(sender, e);
            ReloadPage();
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Populates the sublocations screen from a list of saved sublocations.
        /// </summary>
        private void populateSublocations()
        {

            txtSublocationDescription.IsReadOnly = true;
            txtSublocationName.IsReadOnly = true;
            lblLocationAreasMainName.Content = _location.Name;
            txtSublocationName.Visibility = Visibility.Hidden;
            btnDelete0.Visibility = Visibility.Hidden;
            // Purge elements from previous renders
            for (int i = 0; i < grdSublocationsRows.Children.Count; i++)
            {
                UIElement element = grdSublocationsRows.Children[i];
                if (element != lblSublocationName && element != txtSublocationDescription && element != txtSublocationName && element != btnDelete0)
                {
                    grdSublocationsRows.Children.Remove(element);
                    i--;
                    try
                    {
                        if (element is TextBox text)
                        {
                            UnregisterName(text.Name);
                        }
                        else if (element is Button btn)
                        {
                            UnregisterName(btn.Name);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            if (_sublocations.Count > 0)
            {
                lblSublocationName.Content = _sublocations[0].SublocationName;
                txtSublocationDescription.Text = _sublocations[0].SublocationDescription;

                for (int i = 1; i < _sublocations.Count; i++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(150);
                    grdSublocationsRows.RowDefinitions.Insert(i, row);

                    Label nameLabel = new Label();
                    nameLabel.Content = _sublocations[i].SublocationName;
                    nameLabel.Margin = new Thickness(30, 10, 0, 105);
                    nameLabel.FontSize = 18.0d;
                    nameLabel.FontWeight = FontWeights.Bold;


                    TextBox nameText = new TextBox();
                    nameText.Text = _sublocations[i].SublocationName;
                    nameText.Margin = new Thickness(0, 10, 100, 105);
                    nameText.IsReadOnly = true;
                    nameText.Visibility = Visibility.Hidden;
                    nameText.Name = "txtSublocationName" + i;
                    nameText.MaxLength = 100;

                    TextBox descriptionText = new TextBox();
                    descriptionText.Text = _sublocations[i].SublocationDescription;
                    descriptionText.Margin = new Thickness(30, 50, 30, 10);
                    descriptionText.IsReadOnly = true;
                    descriptionText.Name = "txtSublocationDescription" + i;
                    descriptionText.TextWrapping = TextWrapping.Wrap;

                    Button delete = new Button();
                    delete.Content = "Delete";
                    delete.Visibility = Visibility.Hidden;
                    delete.HorizontalAlignment = HorizontalAlignment.Right;
                    delete.VerticalAlignment = VerticalAlignment.Top;
                    delete.Margin = new Thickness(0, 10, 30, 0);
                    delete.Height = 30;
                    delete.Click += new RoutedEventHandler((sender, e) => btnDelete0_Click(sender, e));
                    delete.Name = "btnDelete" + i;


                    Grid.SetRow(nameLabel, i);
                    Grid.SetColumnSpan(nameLabel, 2);
                    Grid.SetRow(descriptionText, i);
                    Grid.SetColumnSpan(descriptionText, 2);
                    Grid.SetRow(nameText, i);
                    Grid.SetColumn(nameText, 1);
                    Grid.SetRow(delete, i);
                    Grid.SetColumn(delete, 1);
                    grdSublocationsRows.Children.Add(nameLabel);
                    grdSublocationsRows.Children.Add(descriptionText);
                    grdSublocationsRows.Children.Add(nameText);
                    grdSublocationsRows.Children.Add(delete);
                    RegisterName(descriptionText.Name, descriptionText);
                    RegisterName(nameText.Name, nameText);
                    RegisterName(delete.Name, delete);

                }
            }
            else
            {
                lblSublocationName.Content = "No sublocations found.";
                txtSublocationDescription.Text = "";
                btnEdit.IsEnabled = false;
            }
        }

        /// <summary>
        /// Logan Baccam
        /// Created 2022/02/28
        /// 
        /// Description:
        /// Handler to hide the add sublocation screen
        /// </summary>
        private void hideAddSublocationScreen()
        {
            txtNewSublocationName.Text = "";
            txtNewSublocationDesc.Text = "";
            grdAddsublocation.Visibility = Visibility.Collapsed;
            scrSublocations.Visibility = Visibility.Visible;
        }
    }
}
