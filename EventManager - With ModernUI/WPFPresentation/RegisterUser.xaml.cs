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

using LogicLayerInterfaces;
using DataObjects;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window
    {
        IUserManager _userManager;
        public RegisterUser(IUserManager userManager)
        {
            InitializeComponent();
            this._userManager = userManager;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/2/03
        /// 
        /// Description:
        /// Click event for "Submit" button. Verifies all fields are valid before creating the profile, setting the password, and setting DialogueResult true. 
        /// 
        /// </summary>
        /// <param name="sender">The Submit button</param>
        /// <param name="e">Arguments related to the event</param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(!this.txtEmail.Text.IsValidEmailAddress())
            {
                MessageBox.Show("The email address entered is not valid.");
                this.txtEmail.Focus();
                return;
            }
            if(!this.pwdPassword.Password.IsValidPassword())
            {
                MessageBox.Show("Valid passwords must contain at least one capital letter, one lowercase letter, one number, and one special character.");
                this.pwdPassword.Focus();
                this.pwdConfirmPassword.Password = "";
                this.pwdPassword.Password = "";
                return;
            }
            if(!this.pwdConfirmPassword.Password.Equals(this.pwdPassword.Password))
            {
                MessageBox.Show("Passwords did not match.");
                this.pwdConfirmPassword.Focus();
                this.pwdConfirmPassword.Password = "";
                return;
            }
            if (!this.txtGivenName.Text.IsValidName())
            {
                MessageBox.Show("You must enter a given name.");
                this.txtGivenName.Focus();
                return;
            }
            if (!this.txtFamilyName.Text.IsValidName())
            {
                MessageBox.Show("You must enter a family name.");
                this.txtFamilyName.Focus();
                return;
            }
            if (!this.txtCity.Text.IsValidCityName())
            {
                MessageBox.Show("You must enter a city.");
                this.txtCity.Focus();
                return;
            }
            if (!this.cboState.Text.IsValidStateName())
            {
                MessageBox.Show("You must select a state.");
                this.cboState.Focus();
                return;
            }
            if (!this.txtZipCode.Text.IsValidZipCode())
            {
                MessageBox.Show("You must enter a valid zip code.");
                this.txtZipCode.Focus();
                return;
            }
            try
            {
                User user = new User()
                {
                    GivenName = this.txtGivenName.Text,
                    FamilyName = this.txtFamilyName.Text,
                    EmailAddress = this.txtEmail.Text,
                    State = ((ComboBoxItem)cboState.SelectedItem).Tag.ToString(),
                    City = this.txtCity.Text,
                    Zip = Int32.Parse(this.txtZipCode.Text.Replace("-", "")) // Already has to be numeric to get this far.
                };
                this._userManager.CreateUser(user);
                this._userManager.UpdatePasswordHash(user.EmailAddress, "P@ssw0rd", this.pwdPassword.Password);
                this.DialogResult = true;
            } catch (Exception ex)
            {
                MessageBox.Show("An error occurred while creating account.\n\n" + ex.Message );
            }
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/2/03
        /// 
        /// Description:
        /// Click event for "Cancel" button. Sets DialogueResult to false to close window and indicate failure in registering account password.
        /// 
        /// </summary>
        /// <param name="sender">The Cancel button</param>
        /// <param name="e">Arguments related to the event</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
