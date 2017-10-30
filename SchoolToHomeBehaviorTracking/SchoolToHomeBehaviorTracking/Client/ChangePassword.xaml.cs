using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class ChangePassword : INotifyPropertyChanged
    {
        private string _email = null;
        private string _returnPage;

        public string ReturnPage
        {
            get { return _returnPage; }
            set
            {
                _returnPage = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChangePassword(string e)
        {
            _email = e;
            InitializeComponent();
            this.DataContext = this;
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
                proxy.UpdatePassword(_email, newPasswordText.Password.ToString());
                if (_returnPage == "Exit")
                    this.Close();
                else
                {
                    this.Hide();
                    Login loginPage = new Login();
                    loginPage.Show();
                    this.Close();
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (_returnPage == "Exit")
                this.Close();
            else
            {
                this.Hide();
                Login loginPage = new Login();
                loginPage.Show();
                this.Close();
            }
        }
    }
}
