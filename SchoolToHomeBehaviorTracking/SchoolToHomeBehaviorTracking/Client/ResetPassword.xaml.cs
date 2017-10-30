﻿using SchoolToHomeBehaviorTracking_Interface;
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
    /// </summary>
    public partial class ResetPassword : Window
    {
        private string _email;
        private string _code;

        public string email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                    _email = value;
            }
        }
        public string code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                    _code = value;
            }
        }
        public ResetPassword()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
               ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            proxy.ResetPassword(_email);
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
            ChannelFactory<IWCFService> channelFactory = new
              ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (!proxy.VerifyResetPassword(_email, _code))
                incorrectAccessCode.Visibility = System.Windows.Visibility.Visible;
            else
            {
                this.Hide();
                ChangePassword resetPage = new ChangePassword(_email);
                resetPage.ReturnPage = "Cancel";
                resetPage.Show();
                this.Close();
            }
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

