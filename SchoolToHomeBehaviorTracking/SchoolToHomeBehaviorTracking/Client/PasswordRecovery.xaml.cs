using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for PasswordRecovery.xaml
    /// </summary>
    public partial class PasswordRecovery : Window
    {
        delegate void Delegate();

        public PasswordRecovery()
        {
            System.Delegate delUserCloseControl = new Delegate(CloseWindow);
            System.Delegate delUserChangePasswordControl = new Delegate(ChangePasswordWindow);

            InitializeComponent();

            resetPassword.Visibility = System.Windows.Visibility.Visible;
            resetPasswordUC.CallingCloseMethod(delUserCloseControl);
            resetPasswordUC.CallingChangePasswordMethod(delUserChangePasswordControl);
            changePasswordUC.CallingCloseMethod(delUserCloseControl);

            InitializeComponent();

        }

        public void CloseWindow()
        {
            this.Close();
        }

        public void ChangePasswordWindow()
        {
            resetPassword.Visibility = System.Windows.Visibility.Collapsed;
            changePassword.Visibility = System.Windows.Visibility.Visible;
            changePasswordUC.ReturnPage = "Cancel";
        }
    }
}
