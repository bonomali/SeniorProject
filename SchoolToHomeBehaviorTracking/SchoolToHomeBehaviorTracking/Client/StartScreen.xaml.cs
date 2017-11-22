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
    /// Interaction logic for StartScreen.xaml
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

            login.Visibility = System.Windows.Visibility.Visible;
            loginUC.CallingCloseMethod(delUserCloseControl);
            loginUC.CallingCreateMethod(delUserCreateControl);
            createAccountUC.CallingCloseMethod(delUserCloseControl);
            createAccountUC.CallingLoginMethod(delUserLoginControl);
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
    }
}
