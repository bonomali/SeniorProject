using SchoolToHomeBehaviorTracking_Interface;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window 
    {
        private string _email;

        public string email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                }
            }
        }
      
        public Login()
        {
            DataContext = this;
            InitializeComponent();
        }

        //submit button to login to application
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
               ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            //check login credentials
            //if valid: go to accounts page, if invalid display error message
            if (proxy.Login(_email, passwordText.Password.ToString()))
            {
                this.Hide();
                Dash dashPage = new Dash(_email);
                this.Close();
            }
            else
                invalidText.Visibility = System.Windows.Visibility.Visible;
        }

        //exit application
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //open window to create a new account
        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CreateAccount accountPage = new CreateAccount();
            accountPage.Show();
            this.Close();
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
            this.Hide();
            ResetPassword restPage = new ResetPassword();
            restPage.Show();
            this.Close();
        }
    }
}
