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
using LogicLayerInterfaces;
using DataObjects;
using LogicLayer;
using DataAccessFakes;
using Microsoft.Win32;

namespace WPFPresentation.Location
{
    public partial class pgParkingLot : Page
    {        
        DataObjects.Location _location = null;
        User _user = null;
        ManagerProvider _managerProvider = null;
        Dictionary<ParkingLotVM, BitmapImage> parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
        bool _isAddingNewLot = false;
        bool _isEditingLot = false;
        string _originalImagePath = "";
        string _oldFileName = "";
        string _newFileName = "";
        bool _canEditDelete = false;

        ParkingLotVM parkingLotBeforeEdit = null;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Page constructor for ParkingLot
        ///
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Added check to see if the current user can edit/delete the lot
        /// 
        /// </summary>
        /// <param name="managerProvider">The manager provider object</param>
        /// <param name="location">Location of Parking Lot</param>
        /// <param name="user">Current user</param>
        internal pgParkingLot(ManagerProvider managerProvider, DataObjects.Location location, User user)
        {
            _managerProvider = managerProvider;
            _location = location;
            _user = user;
            try
            {
                _canEditDelete = _managerProvider.ParkingLotManager.UserCanEditParkingLot(_user.UserID);
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was a problem checking to see if you can edit and delete parking lots.\n" + ex.Message, "Edit/Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            InitializeComponent();
            setupParkingLotImageDictionary();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Helper method for creating the Dictionary that holds the ParkingLot object and the image associated
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated 2022/03/06
        /// 
        /// Description:
        /// Now displaces a blank picture when there is no picture to return 
        /// </summary>
        private void setupParkingLotImageDictionary()
        {
            try
            {
                List<ParkingLotVM> parkingLotVMs = _managerProvider.ParkingLotManager.RetrieveParkingLotByLocationID(_location.LocationID);
                txtLocationName.Text = _location.Name;

                foreach (ParkingLotVM parkingLot in parkingLotVMs)
                {
                    if (parkingLot.ImageName == null || parkingLot.ImageName == "")
                    {
                        parkingLotAndImage.Add(parkingLot, new BitmapImage());
                    }
                    else
                    {
                        BitmapImage image = _managerProvider.ImageHelper.ReturnBitMapImage(parkingLot.ImageName);
                        parkingLotAndImage.Add(parkingLot, image);
                    }
                }

                txtBlockErrors.Text = "";
                icParkingLots.ItemsSource = parkingLotAndImage;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem finding the parking lots for this location.\n" + ex.Message, "Parking Lot Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Click event handler for creating a new parking lot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddParkingLot_Click(object sender, RoutedEventArgs e)
        {
            // set up ParkingLotVM
            ParkingLotVM newParkingLot = new ParkingLotVM()
            {
                LocationID = _location.LocationID,
                Name = "",
                Description = "",
                ImageName = null,
                Active = true,
                LocationName = _location.Name
            };

            if (parkingLotAndImage.Count == 0)
            {
                parkingLotAndImage.Add(newParkingLot, new BitmapImage());
                _isAddingNewLot = true;
            }
            else if (_isAddingNewLot)
            {
                MessageBox.Show("Already adding a new parking lot.", "Already Adding", MessageBoxButton.OK, MessageBoxImage.Information);                
            }
            else
            {
                _isAddingNewLot = true;
                parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
                parkingLotAndImage.Add(newParkingLot, new BitmapImage());
                setupParkingLotImageDictionary();
            }

            icParkingLots.ItemsSource = parkingLotAndImage;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Initialization handler for text boxes in the icParkingLot items control.
        /// Changes the text box from read only true to read only false.
        /// 
        /// Update:
        /// Mike Cahow
        /// 2022/03/11
        /// 
        /// Adding an edit mode check to initialize textboxes for editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Initialized(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text == "" || (_isEditingLot))
            {
                textBox.IsReadOnly = false;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// Initialization handler for buttons "Save" and "Cancel" in the icParkingLot items control.
        /// Changes the buttons so that they are visible.
        /// 
        /// Description:
        /// Inserts a parking lot object into the database
        /// 
        /// Update:
        /// Mike Cahow
        /// 2022/03/11
        /// 
        /// Adding an edit mode check to help initialize buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Initialized(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            //if (button.Tag.ToString() == "" || button.Tag.ToString() == "System.Windows.Media.Imaging.BitmapImage")
            if (_isAddingNewLot && button.Tag.ToString() == "" || (_isEditingLot && parkingLotBeforeEdit.Name == (string)button.Tag))
            {
                button.Visibility = Visibility.Visible;

                if(_isEditingLot && button.Content.ToString() == "Add Picture")
                {
                    if(parkingLotBeforeEdit.ImageName != "")
                    {
                        button.Content = "Change Picture";
                        button.Width = 150;
                    }
                }
            }

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// Description:
        /// Save button click event handler. Creates a new parking lot object and updates the list
        /// 
        /// Update:
        /// Mike Cahow
        /// 2022/03/11
        /// 
        /// Added separate logic for edit mode
        /// </summary>        
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isEditingLot)
            {
                Button button = (Button)sender;

                // set the old values
                ParkingLotVM oldParkingLot = parkingLotBeforeEdit;

                // set new values
                ParkingLotVM newParkingLot = parkingLotAndImage.First(npl => npl.Key.LotID == oldParkingLot.LotID).Key;

                
                if (_originalImagePath != "" || _originalImagePath != null)
                {
                    try
                    {
                        _newFileName = _managerProvider.ImageHelper.SaveImageReturnsNewImageName(_oldFileName, _originalImagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Problem saving this image.\n" + ex.Message, "Image Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                // try saving edit to database
                try
                {
                    if(newParkingLot.ImageName == "")
                    {
                        _originalImagePath = "";
                    }
                    newParkingLot.ImageName = _newFileName;
                    _managerProvider.ParkingLotManager.EditParkingLotByLotID(oldParkingLot.LotID, oldParkingLot, newParkingLot);
                    parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
                    setupParkingLotImageDictionary();
                    _isEditingLot = false;
                    btnAddParkingLot.IsEnabled = true;
                    _newFileName = "";
                    _oldFileName = "";
                    _originalImagePath = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem updating this parking lot entry.\n" + ex.Message, "Problem Updating Parking Lot", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Save button click
                ParkingLotVM lotToCreate = parkingLotAndImage.First(l => l.Key.LotID == 0).Key;

                if (lotToCreate.Name == null || lotToCreate.Name == "")
                {
                    MessageBox.Show("Please enter a name for the parking lot in order to add a location.", "Add Lot Name", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (_originalImagePath != "")
                    {
                        try
                        {
                            _newFileName = _managerProvider.ImageHelper.SaveImageReturnsNewImageName(_oldFileName, _originalImagePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Problem saving this image.\n" + ex.Message, "Image Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    // send off to get added to db
                    try
                    {
                        lotToCreate.ImageName = _newFileName;
                        _managerProvider.ParkingLotManager.CreateParkingLot(lotToCreate);
                        parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
                        setupParkingLotImageDictionary();
                        _isAddingNewLot = false;
                        _newFileName = "";
                        _oldFileName = "";
                        _originalImagePath = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was an issue adding the parking lot information.\n" + ex.Message, "Problem Adding Parking Lot", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Cancel handler for "Cancel" in the icParkingLot items control.
        /// Cancels adding a new parking lot
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel? Any unsaved work will be lost.", "Are you sure you would like to cancel?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            switch (result)
            {                
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    _isAddingNewLot = false;
                    _isEditingLot = false;
                    parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
                    setupParkingLotImageDictionary();
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Click event handler for stagging an image to be saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddImage(object sender, RoutedEventArgs e)
        {
            // sources
            // https://wpf-tutorial.com/dialogs/the-openfiledialog/
            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (openFile.ShowDialog() == true)
            {
                _originalImagePath = openFile.FileName;
                _oldFileName = openFile.SafeFileName;
                
                // preview image would be awesome
                Button button = (Button)sender;
                button.Width = 300;
                button.Content = "Added! Save the image or hit cancel.";
                button.IsEnabled = false;
            }
        }

        private void Image_Initialized(object sender, EventArgs e)
        {
            Image image = (Image)sender;

            // if the source for the image is null and it has a tag with a value
            // show an error message
            if(image.Source != null)
            {
                if (image.Source.ToString() == "System.Windows.Media.Imaging.BitmapImage" && image.Tag != null)
                {
                    txtBlockErrors.Text = txtBlockErrors.Text.ToString() + "*Problem loading: The file \"" + image.Tag.ToString() + "\" can not be found." + "\n";
                }
            }


        }

        private void DeleteButton_Initialized(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            
            if (_canEditDelete && (int)button.Tag != 0 && !_isEditingLot)
            {
                button.Visibility = Visibility.Visible;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to permanently delete this parking lot?\nThis cannot be undone.", "Delete Parking Lot", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);

            bool lotRecordRemoved = false;            
            Button button = (Button)sender;
            int lotID = (int)button.Tag;
            string imageName = parkingLotAndImage.Keys.First(lot => lot.LotID == lotID).ImageName;

            switch (result)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    try
                    {                        
                        lotRecordRemoved = _managerProvider.ParkingLotManager.RemoveParkingLotByLotID(lotID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was a problem deleting this parking lot.\n" + ex.Message, "Problem Deleting", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }


            if (lotRecordRemoved)
            {
                MessageBox.Show("The parking lot has been deleted successfully.", "Deletion Success", MessageBoxButton.OK, MessageBoxImage.Information);
                parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
                setupParkingLotImageDictionary();

            }
        }

        private void EditButton_Initialized(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (_canEditDelete && (int)button.Tag != 0 && !_isEditingLot)
            {
                button.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Click event handler to help re-initialize textboxes for editing purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            btnAddParkingLot.IsEnabled = false;
            Button button = (Button)sender;
            int lotID = (int)button.Tag;

            parkingLotBeforeEdit = parkingLotAndImage.Keys.First(npl => npl.LotID == lotID);

            if (_isEditingLot)
            {
                MessageBox.Show("Already editing a parking lot.", "Already Editing", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _isEditingLot = true;
                parkingLotAndImage = new Dictionary<ParkingLotVM, BitmapImage>();
                setupParkingLotImageDictionary();
                
            }
        }

    }
}