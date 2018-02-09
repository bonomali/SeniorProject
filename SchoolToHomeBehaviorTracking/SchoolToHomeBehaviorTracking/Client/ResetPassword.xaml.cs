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
    /// Interaction logic for ResetPassword.xaml
    /// Forgot password page
    /// </summary>
    public partial class ResetPassword : UserControl
    {
        private string _email;
        private string _code;

        private Delegate _delCloseMethod;
        private Delegate _delChangePasswordMethod;

        static ChannelFactory<IWCFService> channelFactory = new
        ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

        static IWCFService proxy = channelFactory.CreateChannel();

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
            proxy.ResetPassword(_email);
            accessCodeVerify.Visibility = System.Windows.Visibility.Visible;
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
                _delChangePasswordMethod.DynamicInvoke();
        }

        private void forgotEmailText_TextChanged(object sender, TextChangedEventArgs e)
        {
            accessCodeVerify.Visibility = System.Windows.Visibility.Hidden;
        }

        private void codeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            incorrectAccessCode.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}

