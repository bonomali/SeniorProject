using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// Forgot password page
    /// </summary>
    public partial class ResetPassword : UserControl
    {
        private string _email;
        private string _code;

        private Delegate _delCloseMethod;
        private Delegate _delChangePasswordMethod;

        static ChannelFactory<IMetaInterface> channelFactory = new
        ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");

        static IMetaInterface proxy = channelFactory.CreateChannel();

        public bool CodeSent { get; set; }

        public void CallingCloseMethod(Delegate del)
        {
            _delCloseMethod = del;
        }

        public void CallingChangePasswordMethod(Delegate del)
        {
            _delChangePasswordMethod = del;
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string code
        {
            get { return _code; }
            set { _code = value; }
        }

        public ResetPassword()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (CodeSent)
            {
                accessCodeResentVerify.Visibility = System.Windows.Visibility.Visible;
                CodeSent = false;
                accessCodeVerify.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                accessCodeResentVerify.Visibility = System.Windows.Visibility.Collapsed;
                CodeSent = true;
                accessCodeVerify.Visibility = System.Windows.Visibility.Visible;
            } 
            proxy.ResetPassword(_email);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            _delCloseMethod.DynamicInvoke();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!proxy.VerifyResetPassword(_email, _code))
                incorrectAccessCode.Visibility = System.Windows.Visibility.Visible;
            else
            {
                Email.EmailAddress = _email;
                _delChangePasswordMethod.DynamicInvoke();
            }
        }

        private void forgotEmailText_TextChanged(object sender, TextChangedEventArgs e)
        {
            accessCodeVerify.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void codeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            incorrectAccessCode.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}

