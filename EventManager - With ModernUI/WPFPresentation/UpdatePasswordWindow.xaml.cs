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
using LogicLayer;
using DataAccessFakes;
using DataAccessInterfaces;
using LogicLayerInterfaces;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        IUserManager _userManager;
        User _user;
        bool _newUser;


        public UpdatePasswordWindow(IUserManager userManager, User user, string instructions, bool newUser = false)
        {
            this._userManager = userManager;
            this._user = user;
            this._newUser = newUser;
            InitializeComponent();
            this.pwdOldPassword.Focus();
            this.txtInstructions.Text = instructions;
            if (this._newUser)
            {
                this.newUserUpdate();
            }
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Method to set up newuser inputs.
        /// 
        /// </summary>
        private void newUserUpdate()
        {
            this.pwdOldPassword.Password = "newuser";
            this.pwdOldPassword.IsEnabled = false;
            this.pwdNewPassword.Focus();
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Click event for "Submit" button. Validates password, then attempts to set it as the new password.
        /// 
        /// </summary>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(!pwdNewPassword.Password.IsValidPassword())
            {
                MessageBox.Show("Your new password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
                pwdNewPassword.Password = "";
                this.pwdConfirmPassword.Password = "";
                this.pwdNewPassword.Focus();
                return;
            }
            if(pwdNewPassword.Password != pwdConfirmPassword.Password )
            {
                MessageBox.Show("New password and Retype password must match");
                pwdNewPassword.Password = "";
                this.pwdConfirmPassword.Password = "";
                this.pwdNewPassword.Focus();
                return;
            }
            try
            {
                string oldPassword = this.pwdOldPassword.Password;
                string newPassword = this.pwdNewPassword.Password;

                if (this._userManager.UpdatePasswordHash(this._user.EmailAddress, oldPassword, newPassword))
                {
                    MessageBox.Show("Password successfully updated");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Update failed.\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/2/03
        /// 
        /// Description:
        /// Click event for "Cancel" button. Sets DialogueResult to false to close window and indicate failure in updating password.
        /// 
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
