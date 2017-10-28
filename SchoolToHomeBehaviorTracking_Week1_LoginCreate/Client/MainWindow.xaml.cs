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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
               ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            UserData user = new UserData();
            user.UserName = emailText.Text;
            user.Password = passwordText.Text;

            if (proxy.Login(user.UserName, user.Password))
            {
                //nohrt0@va.gov 4oENgU
                this.Hide();
                CreateAccount account = new CreateAccount();
                account.Show();
                this.Close();
            }
            else
                MessageBox.Show("Unsuccessful Login");
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
