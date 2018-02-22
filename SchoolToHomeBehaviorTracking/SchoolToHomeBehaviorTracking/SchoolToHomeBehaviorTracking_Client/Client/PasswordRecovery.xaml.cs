using System.Windows;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for PasswordRecovery.xaml
    /// Parent for resetting and changing password
    /// </summary>
    public partial class PasswordRecovery : Window
    {
        delegate void Delegate();

        public PasswordRecovery()
        {
            InitializeComponent();

            System.Delegate delUserCloseControl = new Delegate(CloseWindow);
            System.Delegate delUserChangePasswordControl = new Delegate(ChangePasswordWindow);

            resetPassword.Visibility = System.Windows.Visibility.Visible;
            resetPasswordUC.CodeSent = false;

            resetPasswordUC.CallingCloseMethod(delUserCloseControl);
            resetPasswordUC.CallingChangePasswordMethod(delUserChangePasswordControl);
            changePasswordUC.CallingCloseMethod(delUserCloseControl);
        }

        public void CloseWindow()
        {
            this.Close();
        }

        public void ChangePasswordWindow()
        {
            resetPassword.Visibility = System.Windows.Visibility.Collapsed;
            changePassword.Visibility = System.Windows.Visibility.Visible;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 700)
            {
                MyScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                MyScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
            if (e.NewSize.Height < 650)
            {
                MyScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                MyScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
        }
    }
}
