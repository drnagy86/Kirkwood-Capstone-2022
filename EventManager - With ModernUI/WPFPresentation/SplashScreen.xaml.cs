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
using System.Windows.Shapes;

using DataObjects;
using LogicLayerInterfaces;
using LogicLayer;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// 
    /// Update:
    /// Austin Timmerman
    /// Updated: 2022/02/27
    /// 
    /// Description:
    /// Added the ManagerProvider instance variable
    /// </summary>
    public partial class SplashScreen : Window
    {
        private IUserManager _userManager;
        ManagerProvider _managerProvider = new ManagerProvider();

        public SplashScreen()
        {
            InitializeComponent();
            this._userManager = _managerProvider.UserManager;
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/2/4
        /// 
        /// Description:
        /// Click event for the "Login" button. Verifies the username and password and then attempts to log the user in.
        /// 
        /// </summary>
        /// <param name="sender">The "Login" button</param>
        /// <param name="e">Arguments to go along with the event</param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = this.txtEmail.Text;
            var password = this.pwdPassword.Password;
            if (!email.IsValidEmailAddress())
            {
                MessageBox.Show("Bad email address.");
                return;
            }
            else
            {
                try
                {
                    User user = this._userManager.LoginUser(email, password);
                    if (user != null)
                    {

                        string instructions = "On first login, all new users must choose a password to continue.";
                        if (password == "newuser")
                        {
                            // force change password
                            var updateWindow = new UpdatePasswordWindow(this._userManager, user, instructions, true);

                            bool? result = updateWindow.ShowDialog();
                            if (result == true)
                            {
                                MainWindow window = new MainWindow(user, this._userManager);
                                window.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("You did not update your password. You will be returned to the previous screen.");
                            }
                        }
                        else
                        {
                            MainWindow window = new MainWindow(user, this._userManager);
                            window.Show();
                            this.Close();
                        }
                    }


                }
                catch (Exception ex)
                {
                    string message = "Failed to log in.\n\n";
                    message += ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += "\n\n" + ex.InnerException.Message;
                    }

                    MessageBox.Show(message, "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.pwdPassword.Password = "";
                    this.txtEmail.Select(0, Int32.MaxValue);
                    this.txtEmail.Focus();
                }
            }
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/2/4
        /// 
        /// Description:
        /// Click event for the "Register Here" button. Opens the User Creation screen. If the user is created, logs them in.
        /// 
        /// </summary>
        /// <param name="sender">The "Register Here" button</param>
        /// <param name="e">Arguments passed as part of the event</param>
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterUser(this._userManager);

            bool? result = registerWindow.ShowDialog();
            if (result == true)
            {
                try
                {

                    User user = this._userManager.LoginUser(registerWindow.txtEmail.Text, registerWindow.pwdPassword.Password);
                    if (user != null)
                    {
                        MainWindow window = new MainWindow(user, this._userManager);
                        window.Show();
                        this.Close();
                    }
                    else
                    {
                        throw new ApplicationException("Failed to create new user.");
                    }
                }
                catch (Exception ex)
                {
                    string message = "Failed to log in.\n\n";
                    message += ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += "\n\n" + ex.InnerException.Message;
                    }

                    MessageBox.Show(message, "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
