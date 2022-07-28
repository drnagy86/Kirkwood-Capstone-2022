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
    /// Interaction logic for pgLocationDetails.xaml
    /// 
    /// Kris Howell
    /// Created: 2022/03/24
    /// 
    /// Description:
    /// Pulled from old pgViewLocationDetails page while separating functions
    /// into separate pages
    /// 
    /// Vinayak Deshpande
    /// Updated: 2022/04/15
    /// 
    /// Description:
    /// Added more managers to assist in removing events and activities and sublocations on deactivation
    /// </summary>
    public partial class pgLocationDetails : Page
    {
        ManagerProvider _managerProvider;
        ILocationManager _locationManager;
        User _user;
        DataObjects.Location _location;
        List<Reviews> _locationReviews;
        List<LocationImage> _locationImages;
        IEventManager _eventManager;
        IActivityManager _activityManager;
        ISublocationManager _sublocationManager;
        Uri _src;
        int _imageNumber = 0;

        internal pgLocationDetails(ManagerProvider managerProvider, DataObjects.Location location, User user)
        {
            _managerProvider = managerProvider;
            _user = user;
            _location = location;
            _locationManager = managerProvider.LocationManager;
            _locationReviews = _locationManager.RetrieveLocationReviews(location.LocationID);
            _locationImages = _locationManager.RetrieveLocationImagesByLocationID(location.LocationID);
            _eventManager = managerProvider.EventManager;
            _activityManager = managerProvider.ActivityManager;
            _sublocationManager = managerProvider.SublocationManager;
            InitializeComponent();
            AppData.DataPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + @"Images\LocationImages";
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// fills the text boxes, text blocks, images, and reviews for the
        /// Location Details page
        /// 
        /// Update: 
        /// Kris Howell
        /// Updated: 2022/03/24
        /// 
        /// Description:
        /// Imported method into new page for restructure into frame format
        /// 
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtLocationName.Text = _location.Name;
            txtAboutLocationName.Text = "About " + _location.Name + ":";
            txtBoxAboutLocation.Text = _location.Description;
            txtPhoneNumber.Text = _location.Phone;
            txtEmail.Text = _location.Email;
            txtAddressOne.Text = _location.Address1;
            txtAddressTwo.Text = _location.Address2;
            txtBoxPricing.Text = _location.PricingInfo;
            txtBoxReviews.Text = "";
            txtBoxReviewsSecond.Text = "";

            if (_locationReviews.Count == 0)
            {
                txtNoReviewsYet.Visibility = Visibility.Visible;
                txtBoxReviews.Visibility = Visibility.Collapsed;
                txtBoxReviewsSecond.Visibility = Visibility.Collapsed;
                btnMoreReviews.Visibility = Visibility.Collapsed;
            }
            else
            {
                try
                {
                    int avg = 0;
                    int total = 0;

                    foreach (Reviews locationReview in _locationReviews)
                    {
                        avg += locationReview.Rating;
                        total++;
                    }

                    int sum = avg / total;

                    switch (sum)
                    {
                        case 1:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            break;
                        case 2:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            break;
                        case 3:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            break;
                        case 4:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            break;
                        case 5:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gold);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.Purple);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gold);
                            break;
                        default:
                            pathStarOne.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarOne.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarTwo.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarTwo.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarThree.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarThree.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFour.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFour.Fill = new SolidColorBrush(Colors.Gray);
                            pathStarFive.Stroke = new SolidColorBrush(Colors.DarkGray);
                            pathStarFive.Fill = new SolidColorBrush(Colors.Gray);
                            txtNoReviewsYet.Visibility = Visibility.Visible;
                            break;
                    }
                }
                catch (Exception)
                {
                    return;
                }

                var fullStar = "\u2605";
                var emptyStar = "\u2606";



                switch (_locationReviews.Count)
                {
                    case 1:
                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[0].Rating > i)
                            {
                                txtBoxReviews.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviews.Text += emptyStar;
                            }
                        }
                        txtBoxReviews.Text += "     " + _locationReviews[0].FullName;
                        txtBoxReviews.Text += "\n" + _locationReviews[0].Review + "\n";
                        txtBoxReviewsSecond.Visibility = Visibility.Collapsed;
                        btnMoreReviews.Visibility = Visibility.Collapsed;
                        break;
                    case 2:
                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[0].Rating > i)
                            {
                                txtBoxReviews.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviews.Text += emptyStar;
                            }
                        }
                        txtBoxReviews.Text += "     " + _locationReviews[0].FullName;
                        txtBoxReviews.Text += "\n" + _locationReviews[0].Review + "\n";
                        btnMoreReviews.Visibility = Visibility.Collapsed;
                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[1].Rating > i)
                            {
                                txtBoxReviewsSecond.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviewsSecond.Text += emptyStar;
                            }
                        }
                        txtBoxReviewsSecond.Text += "     " + _locationReviews[1].FullName;
                        txtBoxReviewsSecond.Text += "\n" + _locationReviews[1].Review + "\n";
                        break;
                    default:
                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[0].Rating > i)
                            {
                                txtBoxReviews.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviews.Text += emptyStar;
                            }
                        }
                        txtBoxReviews.Text += "     " + _locationReviews[0].FullName;
                        txtBoxReviews.Text += "\n" + _locationReviews[0].Review + "\n";

                        for (int i = 0; i < 5; i++)
                        {
                            if (_locationReviews[1].Rating > i)
                            {
                                txtBoxReviewsSecond.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviewsSecond.Text += emptyStar;
                            }
                        }
                        txtBoxReviewsSecond.Text += "     " + _locationReviews[1].FullName;
                        txtBoxReviewsSecond.Text += "\n" + _locationReviews[1].Review + "\n";
                        break;
                }
            }


            //if (_locationImages.Count == 0)
            //{
            //    imgLocationImage.Visibility = Visibility.Collapsed;
            //    btnNext.Visibility = Visibility.Collapsed;
            //    btnBack.Visibility = Visibility.Collapsed;
            //    return;
            //}
            try
            {
                _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                BitmapImage img = new BitmapImage(_src);
                imgLocationImage.Source = img;
            }
            catch (Exception)
            {
                btnNext.Visibility = Visibility.Collapsed;
                btnBack.Visibility = Visibility.Collapsed;
                //return;
            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// When the next button is clicked the next image for locations is loaded. If its at the end
        /// of the list, goes back to the beginning
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _imageNumber++;
            if (_imageNumber >= _locationImages.Count)
            {
                _imageNumber = 0;
                try
                {
                    _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                    BitmapImage img = new BitmapImage(_src);
                    imgLocationImage.Source = img;
                }
                catch (Exception)
                {
                    return;
                }

            }
            else
            {
                try
                {
                    _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                    BitmapImage img = new BitmapImage(_src);
                    imgLocationImage.Source = img;
                }
                catch (Exception)
                {
                    return;
                }

            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// When the back button is clicked the previous image for locations is loaded. If its past the beginning
        /// of the list, goes to the end
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _imageNumber--;
            if (_imageNumber < 0)
            {
                _imageNumber = _locationImages.Count - 1;
                try
                {
                    _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                    BitmapImage img = new BitmapImage(_src);
                    imgLocationImage.Source = img;
                }
                catch (Exception)
                {
                    return;
                }

            }
            else
            {
                try
                {
                    _src = new Uri(AppData.DataPath + @"\" + _locationImages[_imageNumber].ImageName, UriKind.Absolute);
                    BitmapImage img = new BitmapImage(_src);
                    imgLocationImage.Source = img;
                }
                catch (Exception)
                {
                    return;
                }

            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Click event handler for deactivating a location
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/03/01
        /// 
        /// Description:
        /// Added _user to the page being called 
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/04/15
        /// 
        /// Description:
        /// Added logic for deactivating associated sublocations
        /// and for the unbooking events and related activiites from a deactivated
        /// location and sublocation.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this location?",
                               "Delete",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    int rowsAffected = _locationManager.DeactivateLocationByLocationID(_location.LocationID);
                    if (rowsAffected == 1)
                    {
                        MessageBox.Show("Location: " + _location.Name + " Deactivated");
                        List<Sublocation> sublocations = _sublocationManager.RetrieveSublocationsByLocationID(_location.LocationID);
                        foreach (var sub in sublocations)
                        {
                            _sublocationManager.DeactivateSublocationBySublocationID(sub.SublocationID);
                            MessageBox.Show("Sublocation: " + sub.SublocationName + " Deactivated");
                        }
                        List<EventVM> allEvents = _eventManager.RetreieveActiveEvents();
                        foreach (var ev in allEvents)
                        {
                            if (ev.LocationID == _location.LocationID)
                            {
                                _eventManager.UpdateEventLocationByEventID(ev.EventID, _location.LocationID, null);
                                MessageBox.Show("Event: " + ev.EventName + " Unbooked");
                                List<Activity> activities = _activityManager.RetrieveActivitiesByEventID(ev.EventID);
                                foreach (var act in activities)
                                {
                                    _activityManager.UpdateActivitySublocationByActivityID(act.ActivityID, act.SublocationID, null);
                                    MessageBox.Show("Activity: " + act.ActivityName + " Unbooked");
                                }
                            }
                            
                        }
                        pgViewLocations viewLocations = new pgViewLocations(_managerProvider, _user);
                        this.NavigationService.Navigate(viewLocations);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error deleting this location." + ex.Message);
                }
            }
        }

        /// Jace Pettinger
        /// Created: 2022/02/25
        /// 
        /// Description:
        /// Click event handler for selecting edit or save location biography
        /// 
        /// Kris Howell
        /// Updated: 2022/03/27
        /// 
        /// Description:
        /// Updated to redirect to new pgLocationDetails page
        /// Added EditOngoing flag to edit mode
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditSaveLocation.Content.Equals("Edit")) // edit 
            {
                ValidationHelpers.EditOngoing = true;

                btnDeleteLocation.Visibility = Visibility.Visible;
                btnCancelLocationEdit.Visibility = Visibility.Visible;


                txtBoxAboutLocation.IsReadOnly = false;
                txtPhoneNumber.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtAddressOne.IsEnabled = true;
                txtAddressTwo.IsEnabled = true;
                txtBoxPricing.IsReadOnly = false;
                txtBoxPricing.IsEnabled = true;

                btnEditSaveLocation.Content = "Save";
                txtBoxAboutLocation.Focus();
            }
            else // save
            { // validation checks
                string locationDescription = txtBoxAboutLocation.Text;
                string locationPhone = txtPhoneNumber.Text;
                string locationEmail = txtEmail.Text;
                string locationAddressOne = txtAddressOne.Text;
                string locationAddressTwo = txtAddressTwo.Text;
                string locationPricing = txtBoxPricing.Text;

                if (locationDescription != null && locationDescription.Length > 3000) // desription too long (description can be null)
                {
                    MessageBox.Show("Location description cannot excede 3,000 characters");
                }
                else if (locationPhone != null && locationPhone != ""
                    && !ValidationHelpers.IsValidPhone(locationPhone)) // phone number format validation (phone can be null)
                {
                    MessageBox.Show("Invalid phone number");
                }
                else if (locationEmail != null && locationEmail != ""
                    && !ValidationHelpers.IsValidEmailAddress(locationEmail)) // email format validation (email can be null)
                {
                    MessageBox.Show("Invalid email address");
                }
                else if (locationAddressOne == "" || locationAddressOne == null) // no address one
                {
                    MessageBox.Show("Please enter an address");
                }
                else if (locationAddressOne.Length > 100) // address one too long 
                {
                    MessageBox.Show("Address one cannot be longer than 100 characters");
                }
                else if (locationAddressTwo != null && locationAddressTwo != ""
                   && locationAddressTwo.Length > 100) // address two too long (address two can be null)
                {
                    MessageBox.Show("Address two cannot be longer than 100 characters"); //3000
                }
                else if (locationPricing.Length > 3000) // pricing too long (pricing can be null)
                {
                    MessageBox.Show("Pricing cannot be longer than 3000 characters");
                }
                else // end of validation checks
                {
                    DataObjects.Location oldLocation = _location;
                    try
                    {
                        DataObjects.Location newLocation = new DataObjects.Location()
                        {
                            Description = locationDescription,
                            Phone = locationPhone,
                            Email = locationEmail,
                            Address1 = locationAddressOne,
                            Address2 = locationAddressTwo,
                            PricingInfo = locationPricing
                        };
                        int rowsAffected = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);
                        if (rowsAffected == 1)
                        {
                            ValidationHelpers.EditOngoing = false;
                            _location = _locationManager.RetrieveLocationByLocationID(_location.LocationID);
                            pgLocationDetails page = new pgLocationDetails(_managerProvider, _location, _user);
                            NavigationService.Navigate(page);
                        }
                        else
                        {
                            MessageBox.Show("There was an error updating the location\n");
                            pgLocationDetails page = new pgLocationDetails(_managerProvider, _location, _user);
                            NavigationService.Navigate(page);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was an error updating the location\n" + ex.Message);
                        pgLocationDetails page = new pgLocationDetails(_managerProvider, _location, _user);
                        NavigationService.Navigate(page);
                    }
                }
            }
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Click event handler for canceling editing location biography
        /// 
        /// Kris Howell
        /// Updated: 2022/03/27
        /// 
        /// Description:
        /// Updated to redirect to new pgLocationDetails page
        /// Released EditOngoing flag when user chooses to discard changes and cancel edit
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btnCancelLocationEdit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Discard unsaved changes?",
                              "Cancel",
                              MessageBoxButton.YesNo,
                              MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                ValidationHelpers.EditOngoing = false;
                pgLocationDetails page = new pgLocationDetails(_managerProvider, _location, _user);
                NavigationService.Navigate(page);
            }
        }
    }
}
