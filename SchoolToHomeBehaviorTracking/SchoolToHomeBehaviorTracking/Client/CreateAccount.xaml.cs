using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : UserControl
    {
        private string _email;
        private Delegate _delCloseMethod;
        private Delegate _delLoginMethod;

        public void CallingCloseMethod(Delegate del)
        {
            _delCloseMethod = del;
        }

        public void CallingLoginMethod(Delegate del)
        {
            _delLoginMethod = del;
        }

        public string email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                    _email = value;
            }
        }

        public CreateAccount()
        {
            DataContext = this;
            InitializeComponent();
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
            if (!proxy.ValidateEmail(_email))
            {
                newPasswordText.Clear();
                reenterPasswordText.Clear();
                invalidEmailText.Visibility = System.Windows.Visibility.Visible;
                emailValid = false;
            }
            else
                emailValid = true;

            //validate password meets requirements or display error message
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
                    unmatchingPasswordText.Visibility = System.Windows.Visibility.Visible;
                    passwordValid = false;
                }
                else
                    passwordValid = true;
            }

            //if valid email and password, create account
            if (emailValid == true && passwordValid == true)
            {
                if (proxy.CreateUser(_email, newPasswordText.Password.ToString()))
                {
                    Email.EmailAddress = _email;
                    Dash dashPage = new Dash();
                    dashPage.Show();
                    _delCloseMethod.DynamicInvoke();
                }
                else
                {
                    newPasswordText.Clear();
                    reenterPasswordText.Clear();
                    duplicateEmailText.Visibility = System.Windows.Visibility.Visible;
                }
            }            
        }

        //return to login page
        private void cancelAccountButton_Click(object sender, RoutedEventArgs e)
        {
            _delLoginMethod.DynamicInvoke();
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
