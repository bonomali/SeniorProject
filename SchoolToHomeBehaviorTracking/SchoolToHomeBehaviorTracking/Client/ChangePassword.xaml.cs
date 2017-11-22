using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : INotifyPropertyChanged
    {
        private string _returnPage;
        private Delegate _delCloseMethod;

        public void CallingCloseMethod(Delegate del)
        {
            _delCloseMethod = del;
        }

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

        public ChangePassword()
        {
            InitializeComponent();
            this.DataContext = this;
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
                newPasswordText.Clear();
                reenterPasswordText.Clear();
                invalidPasswordText.Visibility = System.Windows.Visibility.Visible;
                passwordValid = false;
            }
            //if password is valid, be sure password fields match or display error message
            else
            {
                passwordValid = true;

                if (!proxy.ValidateMatchingPasswords(newPasswordText.Password.ToString(), reenterPasswordText.Password.ToString()))
                {
                    newPasswordText.Clear();
                    reenterPasswordText.Clear();
                    unmatchedPasswordText.Visibility = System.Windows.Visibility.Visible;
                    passwordValid = false;
                }
                else
                    passwordValid = true;
            }

            //if valid password, change password for user
            if (passwordValid == true)
            {
                proxy.UpdatePassword(Email.EmailAddress, newPasswordText.Password.ToString());
                if (_returnPage == "Exit")
                    _delCloseMethod.DynamicInvoke();
                else
                {
                    _delCloseMethod.DynamicInvoke();
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (_returnPage == "Exit")
                _delCloseMethod.DynamicInvoke();
            else
            {
                _delCloseMethod.DynamicInvoke();
            }
        }
    }
}
