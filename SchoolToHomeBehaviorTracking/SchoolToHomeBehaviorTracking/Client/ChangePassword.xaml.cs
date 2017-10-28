using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        string email = null;

        public ChangePassword(string e)
        {
            email = e;
            InitializeComponent();
        }
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void newPasswordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            invalidPasswordText.Visibility = System.Windows.Visibility.Hidden;
            unmatchedPasswordText.Visibility = System.Windows.Visibility.Hidden;
        }

        private void reenterPasswordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            invalidPasswordText.Visibility = System.Windows.Visibility.Hidden;
            unmatchedPasswordText.Visibility = System.Windows.Visibility.Hidden;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            bool passwordValid = false;

            ChannelFactory<IWCFService> channelFactory = new
                  ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            //validate new password meets requirements or display error message
            if (!proxy.ValidatePassword(newPasswordText.Password.ToString()))
            {
                invalidPasswordText.Visibility = System.Windows.Visibility.Visible;
                passwordValid = false;
            }
            //if password is valid, be sure password fields match or display error message
            else
            {
                passwordValid = true;

                if (!proxy.ValidateMatchingPasswords(newPasswordText.Password.ToString(), reenterPasswordText.Password.ToString()))
                {
                    unmatchedPasswordText.Visibility = System.Windows.Visibility.Visible;
                    passwordValid = false;
                }
                else
                    passwordValid = true;
            }

            //if valid password, change password for user
            if (passwordValid == true)
            {
                proxy.UpdatePassword(email, newPasswordText.Password.ToString());
                this.Hide();
                Login loginPage = new Login();
                loginPage.Show();
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login loginPage = new Login();
            loginPage.Show();
            this.Close();
        }
    }
}
