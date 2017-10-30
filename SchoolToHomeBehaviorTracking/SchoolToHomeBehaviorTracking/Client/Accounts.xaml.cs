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
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : Window
    {
        private string _pUserName;
        private string _tUserName;
        private string _accessCode;
        private string _teacherCode;
        private string _email = null;

        public string pUserName
        {
            get { return _pUserName; }
            set
            {
                if (_pUserName != value)
                    _pUserName = value;
            }
        }

        public string tUserName
        {
            get { return _tUserName; }
            set
            {
                if (_tUserName != value)
                    _tUserName = value;
            }
        }

        public string accessCode
        {
            get { return _accessCode; }
            set
            {
                if (_accessCode != value)
                    _accessCode = value;
            }
        }
        public string teacherCode
        {
            get { return _teacherCode; }
            set
            {
                if (_teacherCode != value)
                    _teacherCode = value;
            }
        }

        public Accounts(string e)
        {
            ChannelFactory<IWCFService> channelFactory = new
              ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            DataContext = this;
            _email = e;
            InitializeComponent();

            if (proxy.ExistingTeacherAccount(_email))
                teacher.Visibility = System.Windows.Visibility.Collapsed;
            if (proxy.ExistingParentAccount(_email))
                parent.Visibility = System.Windows.Visibility.Collapsed;
        }

        public Accounts()
        {
            InitializeComponent();
        }

        private void teacher_Checked(object sender, RoutedEventArgs e)
        {
            parentAccount.Visibility = System.Windows.Visibility.Collapsed;
            teacherAccount.Visibility = System.Windows.Visibility.Visible;
            addStud.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void parent_Checked(object sender, RoutedEventArgs e)
        {
            teacherAccount.Visibility = System.Windows.Visibility.Collapsed;
            parentAccount.Visibility = System.Windows.Visibility.Visible;
            addStud.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void accessCodeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            invalidAccessCodeText.Visibility = System.Windows.Visibility.Hidden;
        }

        private void teacherCodeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            invalidTeacherCodeText.Visibility = System.Windows.Visibility.Hidden;
        }

        private void sumbitButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
               ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if ((bool)teacher.IsChecked)
            {
                try
                {
                    int code = Convert.ToInt32(_accessCode);
                    if (proxy.ValidateTeacherAccessCode(code))
                    {
                        if (proxy.AddTeacherAccount(_email, code, _tUserName))
                        {
                            proxy.UpdateTeacherLastAccess(_email);
                            TabFocus.Focus = "teacher";
                            this.Hide();
                            Dash dashPage = new Dash(_email);
                            this.Close();
                        }
                        else
                            invalidAccessCodeText.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                        invalidAccessCodeText.Visibility = System.Windows.Visibility.Visible;
                }
                catch
                {
                    invalidAccessCodeText.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else if ((bool)parent.IsChecked)
            {
                try
                {
                    int code = Convert.ToInt32(_teacherCode);
                    if (proxy.ValidateParentTeacherCode(code))
                    {
                        if (proxy.AddParentAccount(_email, _pUserName))
                        {
                            proxy.UpdateParentLastAccess(_email);
                            TabFocus.Focus = "parent";
                            this.Hide();
                            Dash dashPage = new Dash(_email);
                            this.Close();
                        }
                        else
                            invalidTeacherCodeText.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                        invalidTeacherCodeText.Visibility = System.Windows.Visibility.Visible;
                }
                catch
                {
                    invalidTeacherCodeText.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else if ((bool)addStudent.IsChecked)
            {
                try
                {
                    int code = Convert.ToInt32(_teacherCode);
                    if (proxy.ValidateParentTeacherCode(code))
                    {
                        if (proxy.AddStudentToParentAccount(_email, code))
                        {
                            this.Hide();
                            Dash dashPage = new Dash(_email);
                            this.Close();
                        }
                        else
                            invalidAddTeacherCodeText.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                        invalidAddTeacherCodeText.Visibility = System.Windows.Visibility.Visible;
                }
                catch
                {
                    invalidAddTeacherCodeText.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Dash dashPage = new Dash(_email);
            this.Close();
        }

        private void addStudent_Checked(object sender, RoutedEventArgs e)
        {
            teacherAccount.Visibility = System.Windows.Visibility.Collapsed;
            parentAccount.Visibility = System.Windows.Visibility.Collapsed;
            addStud.Visibility = System.Windows.Visibility.Visible;
        }

        private void addTeacherCodeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            invalidAddTeacherCodeText.Visibility = System.Windows.Visibility.Hidden;
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            TabFocus.Focus = null;
            this.Hide();
            Login loginPage = new Login();
            loginPage.Show();
            this.Close();
        }
    }
}
