using DataObjects;
using LogicLayerInterfaces;
using LogicLayer;
using DataAccessFakes;
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

namespace WPFPresentation
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/07
    /// 
    /// Description:
    /// The Volunteer Details page
    /// </summary>
    public partial class pgViewVolunteerDetails : Page
    {
        ManagerProvider _managerProvider = null;
        IVolunteerSkillSetManager _volunteerSkillSetManager = null;
        IUserImageManager _userImageManager = null;
        IVolunteerReviewManager _volunteerReviewManager = null;

        Volunteer _volunteer = null;
        List<VolunteerSkillSet> _volunteerSkills = null;
        List<UserImage> _userImages = null;
        List<Reviews> _volunteerReviews = null;

        Uri _src;
        int _imageNumber = 0;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// The custom constructor for the pgViewVolunteerDetails page
        /// </summary>
        /// <param name="volunteer"></param>
        /// <param name="managerProvider"></param>
        internal pgViewVolunteerDetails(Volunteer volunteer, ManagerProvider managerProvider)
        {
            _managerProvider = managerProvider;
            _volunteerSkillSetManager = managerProvider.VolunteerSkillSetManager;
            _userImageManager = managerProvider.UserImageManager;
            _volunteerReviewManager = managerProvider.VolunteerReviewManager;
            _volunteer = volunteer;

            InitializeComponent();

            AppData.DataPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + @"Images\UserImages";

            loadVolunteerDetails();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// The helper method that fills the text boxes, text blocks, images, and reviews for the
        /// Volunteer Details page
        /// </summary>
        private void loadVolunteerDetails()
        {
            txtVolunteerName.Text = _volunteer.GivenName + " " + _volunteer.FamilyName;
            txtAboutVolunnteerName.Text = "About " + _volunteer.GivenName;
            if (_volunteer.UserDescription == null || _volunteer.UserDescription == "")
            {
                txtBoxAboutVolunteer.Text = "No Description...";
                txtBoxAboutVolunteer.FontStyle = FontStyles.Italic;
            }
            else
            {
                txtBoxAboutVolunteer.Text = _volunteer.UserDescription;
                txtBoxAboutVolunteer.FontStyle = FontStyles.Normal;
            }
            txtVolunteerType.Text = _volunteer.VolunteerType;
            loadVolunteerReviews();
            loadVolunteerSkillSet();
            loadUserImages();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// The helper method that grabs the user images and places them on the page
        /// </summary>
        private void loadUserImages()
        {
            _userImages = _userImageManager.RetrieveUserImagesByUserID(_volunteer.UserID);

            try
            {
                if(_userImages != null)
                {
                    _src = new Uri(AppData.DataPath + @"\" + _userImages[_imageNumber].ImageName, UriKind.Absolute);
                    BitmapImage img = new BitmapImage(_src);
                    imgVolunteerImage.Source = img;
                    if (_userImages.Count <= 1)
                    {
                        btnBack.Visibility = Visibility.Collapsed;
                        btnNext.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    imgVolunteerImage.Visibility = Visibility.Collapsed;
                    btnBack.Visibility = Visibility.Collapsed;
                    btnNext.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                imgVolunteerImage.Visibility = Visibility.Collapsed;
                btnNext.Visibility = Visibility.Collapsed;
                btnBack.Visibility = Visibility.Collapsed;
                //return;
            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// The helper method that fills the skill set text box with the volunteer's skills (if applicable)
        /// </summary>
        private void loadVolunteerSkillSet()
        {
            _volunteerSkills = _volunteerSkillSetManager.RetrieveSkillSetByVolunteerID(_volunteer.VolunteerID);
            txtBoxSkillSet.Clear();
            if(_volunteerSkills != null)
            {
                foreach (VolunteerSkillSet skill in _volunteerSkills)
                {
                    txtBoxSkillSet.Text += skill.SkillSetID + "\n" + skill.SkillSetDescription + "\n\n";
                }
                txtBoxSkillSet.FontStyle = FontStyles.Normal;
            }
            else
            {
                txtBoxSkillSet.Text = "No Skills...";
                txtBoxSkillSet.FontStyle = FontStyles.Italic;
            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// The helper method that fills the reviews text box with the volunteer's reviews (if applicable)
        /// </summary>
        private void loadVolunteerReviews()
        {
            _volunteerReviews = _volunteerReviewManager.RetrieveVolunteerReviewsByVolunteerID(_volunteer.VolunteerID);
            txtBoxReviews.Clear();
            if (_volunteerReviews == null || _volunteerReviews.Count == 0)
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

                    foreach (Reviews volunteerReview in _volunteerReviews)
                    {
                        avg += volunteerReview.Rating;
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



                switch (_volunteerReviews.Count)
                {
                    case 1:
                        for (int i = 0; i < 5; i++)
                        {
                            if (_volunteerReviews[0].Rating > i)
                            {
                                txtBoxReviews.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviews.Text += emptyStar;
                            }
                        }
                        txtBoxReviews.Text += "     " + _volunteerReviews[0].FullName;
                        txtBoxReviews.Text += "\n" + _volunteerReviews[0].Review + "\n";
                        txtBoxReviewsSecond.Visibility = Visibility.Collapsed;
                        btnMoreReviews.Visibility = Visibility.Collapsed;
                        break;
                    case 2:
                        for (int i = 0; i < 5; i++)
                        {
                            if (_volunteerReviews[0].Rating > i)
                            {
                                txtBoxReviews.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviews.Text += emptyStar;
                            }
                        }
                        txtBoxReviews.Text += "     " + _volunteerReviews[0].FullName;
                        txtBoxReviews.Text += "\n" + _volunteerReviews[0].Review + "\n";
                        btnMoreReviews.Visibility = Visibility.Collapsed;
                        for (int i = 0; i < 5; i++)
                        {
                            if (_volunteerReviews[1].Rating > i)
                            {
                                txtBoxReviewsSecond.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviewsSecond.Text += emptyStar;
                            }
                        }
                        txtBoxReviewsSecond.Text += "     " + _volunteerReviews[1].FullName;
                        txtBoxReviewsSecond.Text += "\n" + _volunteerReviews[1].Review + "\n";
                        break;
                    default:
                        for (int i = 0; i < 5; i++)
                        {
                            if (_volunteerReviews[0].Rating > i)
                            {
                                txtBoxReviews.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviews.Text += emptyStar;
                            }
                        }
                        txtBoxReviews.Text += "     " + _volunteerReviews[0].FullName;
                        txtBoxReviews.Text += "\n" + _volunteerReviews[0].Review + "\n";

                        for (int i = 0; i < 5; i++)
                        {
                            if (_volunteerReviews[1].Rating > i)
                            {
                                txtBoxReviewsSecond.Text += fullStar;
                            }
                            else
                            {
                                txtBoxReviewsSecond.Text += emptyStar;
                            }
                        }
                        txtBoxReviewsSecond.Text += "     " + _volunteerReviews[1].FullName;
                        txtBoxReviewsSecond.Text += "\n" + _volunteerReviews[1].Review + "\n";
                        break;
                }
            }
        }
    }
}
