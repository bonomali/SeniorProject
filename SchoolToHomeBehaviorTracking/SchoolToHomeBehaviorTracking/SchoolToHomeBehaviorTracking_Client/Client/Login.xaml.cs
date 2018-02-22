using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// Login to application
    /// </summary>
    public partial class Login : UserControl
    {
        private string _email;

        private Delegate _delCloseMethod;
        private Delegate _delCreateMethod;

        static ChannelFactory<IMetaInterface> channelFactory = new
        ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");
        static IMetaInterface proxy = channelFactory.CreateChannel();

        public void CallingCloseMethod(Delegate del)
        {
            _delCloseMethod = del;
        }

        public void CallingCreateMethod(Delegate del)
        {
            _delCreateMethod = del;
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
      
        public Login()
        {
            DataContext = this;
            InitializeComponent();
        }

        //submit button to login to application
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            //check login credentials
            //if valid: go to accounts page, if invalid display error message
            try
            {
                if (proxy.Login(_email, passwordText.Password.ToString()))
                {
                    Email.EmailAddress = _email; //set static email
                    Dash dashPage = new Dash();
                    dashPage.Show();
                    _delCloseMethod.DynamicInvoke();
                }
                else
                {
                    passwordText.Clear();
                    invalidText.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        //exit application
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            _delCloseMethod.DynamicInvoke();
        }

        //open window to create a new account
        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            _delCreateMethod.DynamicInvoke();
        }

        private void emailText_TextChanged(object sender, TextChangedEventArgs e)
        {
            invalidText.Visibility = System.Windows.Visibility.Hidden;
        }

        private void passwordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            invalidText.Visibility = System.Windows.Visibility.Hidden;
        }

        //open window to reset password
        private void forgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecovery restPage = new PasswordRecovery();
            restPage.Show();
        }
    }
}
