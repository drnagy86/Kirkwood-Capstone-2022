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
    /// Interaction logic for pgSupplierDetails.xaml
    /// </summary>
    public partial class pgSupplierDetails : Page
    {
        ManagerProvider _managerProvider;
        ISupplierManager _supplierManager;
        DataObjects.Supplier _supplier;
        private List<Reviews> _reviews;
        private List<string> _images;

        internal pgSupplierDetails(ManagerProvider managerProvider, DataObjects.Supplier supplier)
        {
            _managerProvider = managerProvider;
            _supplierManager = managerProvider.SupplierManager;
            _supplier = supplier;

            InitializeComponent();
            // CHANGE FOLDER NAME FROM LocationImages, TO SERVICE IMAGES ONCE CREATED
            AppData.DataPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + @"Images\SupplierImages";
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Page loaded event. Loads data into all elements that need it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.lblName.Content = _supplier.Name;
            this.lblType.Content = _supplier.TypeID;
            this.txtAbout.Text = _supplier.Description;

            try
            {
                _supplier.Tags = _supplierManager.RetrieveSupplierTagsBySupplierID(_supplier.SupplierID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n\n" + ex.InnerException.Message);
            }

            if (_supplier.Tags != null)
            {
                this.lblTags.Content = "Tags: " + string.Join(", ", _supplier.Tags);
            }
            else
            {
                this.lblTags.Content = "Tags: ";
            }
            this.lblEmail.Content = _supplier.Email;
            this.lblPhone.Content = _supplier.Phone;

            try
            {
                _images = _supplierManager.RetrieveSupplierImagesBySupplierID(_supplier.SupplierID);
                _reviews = _supplierManager.RetrieveSupplierReviewsBySupplierID(_supplier.SupplierID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n\n" + ex.InnerException.Message);
            }

            // Following block is lifted from pgViewLocationDetails.
            // Author: Austin Timmerman
            try
            {
                int avg = 0;
                int total = 0;

                foreach (Reviews review in _reviews)
                {
                    avg += review.Rating;
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
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }

            if (_reviews.Any())
            {
                PopulateTextBoxWithReview(this.txtFirstReview, _reviews[0]);
            }
            if (_reviews.Count > 1)
            {
                PopulateTextBoxWithReview(this.txtSecondReview, _reviews[1]);
            }
            if (_reviews.Count < 3)
            {
                this.btnMoreReviews.Visibility = Visibility.Collapsed;
            }

            try
            {
                Uri source = new Uri(AppData.DataPath + @"\" + _images[0], UriKind.Absolute);
                BitmapImage img = new BitmapImage(source);
                this.imgSupplier.Source = img;
            }
            catch (Exception)
            {
                this.btnNext.Visibility = Visibility.Collapsed;
                this.btnBack.Visibility = Visibility.Collapsed;
                this.lblPhotoHeading.Visibility = Visibility.Collapsed;
                return;
            }
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// helper method to do review formatting in text boxes. 
        /// Largely lifted and editted from pgViewLocationDetails.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="review"></param>
        private void PopulateTextBoxWithReview(TextBox box, Reviews review)
        {
            var fullStar = "\u2605";
            var emptyStar = "\u2606";

            for (int i = 0; i < 5; i++)
            {
                if (review.Rating > i)
                {
                    box.Text += fullStar;
                }
                else
                {
                    box.Text += emptyStar;
                }
            }
            box.Text += "     " + review.FullName;
            box.Text += "\n" + review.Review + "\n";
        }
    }
}
