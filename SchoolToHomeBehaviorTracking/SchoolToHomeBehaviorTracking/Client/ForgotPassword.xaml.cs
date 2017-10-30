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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
               ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            proxy.GenerateAccessCode(forgotEmailText.Text);
            accessCodeVerify.Visibility = System.Windows.Visibility.Visible;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login loginPage = new Login();
            loginPage.Show();
            this.Close();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void forgotEmailText_TextChanged(object sender, TextChangedEventArgs e)
        {
            accessCodeVerify.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
