using SchoolToHomeBehaviorTracking_Interface;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for Dash.xaml
    /// Parent dashboard containing tabs for user account dashboards
    /// </summary>
    public partial class Dash : Window
    {
        delegate void DelCreateUserControlMethod();
        delegate void DelLogoutUserControlMethod();
        delegate void DelHideCreateControlMethod();
        delegate void DelReloadAccountsControlMethod();

        static ChannelFactory<IWCFService> channelFactory = new
        ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

        static IWCFService proxy = channelFactory.CreateChannel();

        public Dash()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            ShowTabs();
        }

        //determine which tabs to show
        public void ShowTabs()
        {
            bool parentAccounts = false;
            bool adminAccounts = false;
            bool teacherAccounts = false;

            DelCreateUserControlMethod delUserCreateControl = new DelCreateUserControlMethod(CreateAccount);
            DelLogoutUserControlMethod delUserLogoutControl = new DelLogoutUserControlMethod(Logout);
            DelHideCreateControlMethod delUserHideCreateControl = new DelHideCreateControlMethod(ShowTabs);
            DelReloadAccountsControlMethod delUserReloadAccountsControl = new DelReloadAccountsControlMethod(ReloadAccounts);
            createAccountUC.HideCreateAccountMethod = delUserHideCreateControl;
            createAccountUC.LogoutMethod = delUserLogoutControl;

            createAccount.Visibility = System.Windows.Visibility.Collapsed;

            //initialize correct tabs for user accounts
            if (proxy.ExistingParentAccount(Email.EmailAddress))
            {
                ParentDash pDash = new ParentDash();
                pDash.CallingCreateMethod = delUserCreateControl;
                pDash.CallingLogoutMethod = delUserLogoutControl;
                this.parentTab.Content = pDash;
                this.parentTab.Visibility = System.Windows.Visibility.Visible;
                parentAccounts = true;
            }
            if (proxy.ExistingTeacherAccount(Email.EmailAddress))
            {
                TeacherDash tDash = new TeacherDash();
                tDash.CallingCreateMethod = delUserCreateControl;
                tDash.CallingLogoutMethod = delUserLogoutControl;
                tDash.CallingReloadTabsMethod = delUserReloadAccountsControl;
                this.teacherTab.Content = tDash;
                this.teacherTab.Visibility = System.Windows.Visibility.Visible;
                teacherAccounts = true;
            }
            if (proxy.ExistingAdminAccount(Email.EmailAddress))
            {
                AdminDash aDash = new AdminDash();
                aDash.CallingCreateMethod = delUserCreateControl;
                aDash.CallingLogoutMethod = delUserLogoutControl;
                aDash.CallingReloadTabsMethod = delUserReloadAccountsControl;
                this.adminTab.Content = aDash;
                this.adminTab.Visibility = System.Windows.Visibility.Visible;
                adminAccounts = true;
            }
            //go to dashboard if user already has accounts
            if (parentAccounts || teacherAccounts || adminAccounts)
            {

                //determine which tab has focus
                if (TabFocus.Focus == "admin")
                    this.adminTab.Focus();
                else if (TabFocus.Focus == "teacher")
                    this.teacherTab.Focus();
                else if (TabFocus.Focus == "parent")
                    this.parentTab.Focus();
                else if (adminAccounts)
                    this.adminTab.Focus();
                else if (teacherAccounts)
                    this.teacherTab.Focus();
                else if (parentAccounts)
                    this.parentTab.Focus();
                this.Show();
            }
            //if not exisiting accounts, go to create accounts window
            else
                CreateAccount();
        }

        //create a new account
        public void CreateAccount()
        {
            tabControl.SelectedItem = createAccount;
            if(!ExistingParentAccount() && !ExistingTeacherAccount())
                createAccountUC.cancelButton.Visibility = System.Windows.Visibility.Collapsed;
            else
                createAccountUC.cancelButton.Visibility = System.Windows.Visibility.Visible;
            createAccount.Visibility = System.Windows.Visibility.Visible;
        }

        //check if user already has parent account
        //return true if existing account, false if no parent account
        public bool ExistingParentAccount()
        {
            if (proxy.ExistingParentAccount(Email.EmailAddress))
                return true;
            return false;
        }

        //check if user already has teacher account
        //return true if existing account, false if no teacher account
        public bool ExistingTeacherAccount()
        {
            if (proxy.ExistingTeacherAccount(Email.EmailAddress))
                return true;
            return false;
        }
        
        //logout user, return to login screen
        public void Logout()
        {
            Email.EmailAddress = null;
            TabFocus.Focus = null;
            this.Hide();
            StartScreen loginPage = new StartScreen();
            loginPage.Show();
            this.Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (adminTab.IsSelected)
            {
                TabFocus.Focus = "admin";
                proxy.UpdateAdminLastAccess(Email.EmailAddress);
            }
            else if (teacherTab.IsSelected)
            {
                TabFocus.Focus = "teacher";
                proxy.UpdateTeacherLastAccess(Email.EmailAddress);
            }
            else if (parentTab.IsSelected)
            {
                TabFocus.Focus = "parent";
                proxy.UpdateParentLastAccess(Email.EmailAddress);
            }
            tabControl.Background = Brushes.LightGray;
        }

        private void ReloadAccounts()
        {
            //show correct tabs for user accounts
            if (proxy.ExistingParentAccount(Email.EmailAddress))
                this.parentTab.Visibility = System.Windows.Visibility.Visible;
            else
                this.parentTab.Visibility = System.Windows.Visibility.Collapsed;

            if (proxy.ExistingTeacherAccount(Email.EmailAddress))
                this.teacherTab.Visibility = System.Windows.Visibility.Visible;
            else
                this.teacherTab.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 925)
            {
                MyScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                MyScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
            if (e.NewSize.Height < 850)
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