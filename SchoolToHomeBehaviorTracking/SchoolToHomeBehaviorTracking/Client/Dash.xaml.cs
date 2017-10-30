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
    /// Interaction logic for Dash.xaml
    /// </summary>
    public partial class Dash : Window
    {
        delegate void DelCreateUserControlMethod();
        delegate void DelLogoutUserControlMethod();
        private string _email = null;

        public Dash(string e)
        {
            DelCreateUserControlMethod delUserCreateControl = new DelCreateUserControlMethod(CreateAccount);
            DelLogoutUserControlMethod delUserLogoutControl = new DelLogoutUserControlMethod(Logout);

            _email = e;
            bool parentAccounts = false;
            bool adminAccounts = false;
            bool teacherAccounts = false;

            ChannelFactory<IWCFService> channelFactory = new
               ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            InitializeComponent();

            //initialize correct tabs for user accounts
            if (proxy.ExistingParentAccount(_email))
            {
                ParentDash pDash = new ParentDash(_email);
                pDash.CallingCreateMethod = delUserCreateControl;
                pDash.CallingLogoutMethod = delUserLogoutControl;
                this.parentTab.Content = pDash;
                this.parentTab.Visibility = System.Windows.Visibility.Visible;
                parentAccounts = true;
            }
            if (proxy.ExistingTeacherAccount(_email))
            {
                TeacherDash tDash = new TeacherDash(_email);
                tDash.CallingCreateMethod = delUserCreateControl;
                tDash.CallingLogoutMethod = delUserLogoutControl;
                this.teacherTab.Content = tDash;
                this.teacherTab.Visibility = System.Windows.Visibility.Visible;
                teacherAccounts = true;
            }
            if (proxy.ExistingAdminAccount(_email))
            {
                AdminDash aDash = new AdminDash(_email);
                aDash.CallingCreateMethod = delUserCreateControl;
                aDash.CallingLogoutMethod = delUserLogoutControl;
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
                else if(parentAccounts)
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
            this.Hide();
            Accounts accountsPage = new Accounts(_email);
            accountsPage.Show();
            this.Close();
        }

        //check if user already has parent account
        //return true if existing account, false if no parent account
        public bool ExistingParentAccount()
        {
            ChannelFactory<IWCFService> channelFactory = new
              ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (proxy.ExistingParentAccount(_email))
                return true;
            return false;
        }

        //check if user already has teacher account
        //return true if existing account, false if no teacher account
        public bool ExistingTeacherAccount()
        {
            ChannelFactory<IWCFService> channelFactory = new
              ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (proxy.ExistingTeacherAccount(_email))
                return true;
            return false;
        }
        
        //logout user, return to login screen
        public void Logout()
        {
            TabFocus.Focus = null;
            this.Hide();
            Login loginPage = new Login();
            loginPage.Show();
            this.Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
              ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (adminTab.IsSelected)
            {
                TabFocus.Focus = "admin";
                proxy.UpdateAdminLastAccess(_email);
            }
            else if (teacherTab.IsSelected)
            {
                TabFocus.Focus = "teacher";
                proxy.UpdateTeacherLastAccess(_email);
            }
            else if (parentTab.IsSelected)
            {
                TabFocus.Focus = "parent";
                proxy.UpdateParentLastAccess(_email);
            }
        }
    }
}
