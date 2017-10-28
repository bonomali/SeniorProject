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
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
            newPasswordText.MaxLength = 15;
            newPasswordText.PasswordChar = '*';
            reenterPasswordText.MaxLength = 15;
            reenterPasswordText.PasswordChar = '*';
        }

        //validate email and password to create user account
        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            bool emailValid = false;
            bool passwordValid = false;

            ChannelFactory<IWCFService> channelFactory = new
                  ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            //validate email or display error message
            if (!proxy.ValidateEmail(newEmailText.Text))
            {
                invalidEmailText.Visibility = System.Windows.Visibility.Visible;
                emailValid = false;
            }
            else
                emailValid = true;

            //validate password meets requirements or display error message
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
                    unmatchingPasswordText.Visibility = System.Windows.Visibility.Visible;
                    passwordValid = false;
                }
                else
                    passwordValid = true;
            }

            //if valid email and password, create account
            if (emailValid == true && passwordValid == true)
            {
                if (proxy.CreateUser(newEmailText.Text, newPasswordText.Password.ToString()))
                {
                    this.Hide();
                    Accounts accountPage = new Accounts();
                    accountPage.Show();
                    this.Close();
                }
                else
                    duplicateEmailText.Visibility = System.Windows.Visibility.Visible;
            }            
        }

        //return to login page
        private void cancelAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Login loginPage = new Login();
            this.Hide();
            loginPage.Show();
            this.Close();
        }

        private void newEmailText_TextChanged(object sender, TextChangedEventArgs e)
        {
            invalidEmailText.Visibility = System.Windows.Visibility.Hidden;
            duplicateEmailText.Visibility = System.Windows.Visibility.Hidden;
        }

        private void reenterPasswordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            invalidPasswordText.Visibility = System.Windows.Visibility.Hidden;
            unmatchingPasswordText.Visibility = System.Windows.Visibility.Hidden;
        }

        private void newPasswordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            invalidPasswordText.Visibility = System.Windows.Visibility.Hidden;
            unmatchingPasswordText.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
