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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for TeacherDash.xaml
    /// </summary>
    public partial class TeacherDash : UserControl
    {
        private System.Delegate _delCreateMethod;
        private System.Delegate _delLogoutMethod;
        private string _email = null;

        public Delegate CallingCreateMethod
        {
            set { _delCreateMethod = value; }
        }

        public Delegate CallingLogoutMethod
        {
            set { _delLogoutMethod = value; }
        }

        public TeacherDash(string email)
        {
            _email = email;
            InitializeComponent();

            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (proxy.ExistingParentAccount(_email))
                t_newAccountButton.Visibility = System.Windows.Visibility.Hidden;

            loginText.Text += proxy.GetTeacherAccessDate(_email);
            userNameText.Text += proxy.GetTeacherUserName(_email);
        }

        private void newAccountButton_Click(object sender, RoutedEventArgs e)
        {
            _delCreateMethod.DynamicInvoke();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            _delLogoutMethod.DynamicInvoke();
        }
    }
}