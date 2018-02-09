using System.Windows;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// Parent for login pages
    /// </summary>
    public partial class StartScreen : Window
    {
        delegate void Delegate();

        public StartScreen()
        {
            System.Delegate delUserCloseControl = new Delegate(CloseWindow);
            System.Delegate delUserCreateControl = new Delegate(CreateAccount);
            System.Delegate delUserLoginControl = new Delegate(LoginShow);

            InitializeComponent();
            this.WindowState = WindowState.Maximized;

            loginUC.CallingCloseMethod(delUserCloseControl);
            loginUC.CallingCreateMethod(delUserCreateControl);
            createAccountUC.CallingCloseMethod(delUserCloseControl);
            createAccountUC.CallingLoginMethod(delUserLoginControl);

            login.Visibility = System.Windows.Visibility.Visible;
        }

        //close window, exit application
        public void CloseWindow()
        {
            this.Close();
        }

        //open create account user control
        public void CreateAccount()
        {
            login.Visibility = System.Windows.Visibility.Collapsed;
            createAccount.Visibility = System.Windows.Visibility.Visible;
        }

        public void LoginShow()
        {
            login.Visibility = System.Windows.Visibility.Visible;
            createAccount.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 850)
            {
                MyScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                MyScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
            if (e.NewSize.Height <= 500)
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
