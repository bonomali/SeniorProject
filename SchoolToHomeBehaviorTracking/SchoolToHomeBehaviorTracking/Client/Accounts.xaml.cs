using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : INotifyPropertyChanged
    {
        private string _pUserName;
        private string _tUserName;
        private string _accessCode;
        private string _teacherCode;
        private Delegate _delHideCreateAccountMethod;
        private Delegate _delLogoutMethod;

        public Delegate HideCreateAccountMethod
        {
            set { _delHideCreateAccountMethod = value; }
        }

        public Delegate LogoutMethod
        {
            set { _delLogoutMethod = value; }
        }

        public string pUserName
        {
            get { return _pUserName; }
            set
            {
                _pUserName = value;
                OnPropertyChanged();
            }
        }

        public string tUserName
        {
            get { return _tUserName; }
            set
            {
                _tUserName = value;
                OnPropertyChanged();
            }
        }

        public string accessCode
        {
            get { return _accessCode; }
            set
            {
                _accessCode = value;
                OnPropertyChanged();
            }
        }
        public string teacherCode
        {
            get { return _teacherCode; }
            set
            {
                _teacherCode = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Accounts()
        {
            ChannelFactory<IWCFService> channelFactory = new
              ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            DataContext = this;
            InitializeComponent();

            if (proxy.ExistingTeacherAccount(Email.EmailAddress))
                teacher.Visibility = System.Windows.Visibility.Collapsed;
            if (proxy.ExistingParentAccount(Email.EmailAddress))
                parent.Visibility = System.Windows.Visibility.Collapsed;
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
                        if (proxy.AddTeacherAccount(Email.EmailAddress, code, _tUserName))
                        {
                            tUserName = "";
                            accessCode = "";
                            proxy.UpdateTeacherLastAccess(Email.EmailAddress);
                            TabFocus.Focus = "teacher";
                            _delHideCreateAccountMethod.DynamicInvoke();
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
                        if (proxy.AddParentAccount(Email.EmailAddress, _pUserName))
                        {
                            pUserName = "";
                            teacherCode = "";
                            proxy.UpdateParentLastAccess(Email.EmailAddress);
                            TabFocus.Focus = "parent";
                            _delHideCreateAccountMethod.DynamicInvoke();
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
                        if (proxy.AddStudentToParentAccount(Email.EmailAddress, code))
                        {
                            teacherCode = "";
                            _delHideCreateAccountMethod.DynamicInvoke();
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
            _delHideCreateAccountMethod.DynamicInvoke();
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
            _delLogoutMethod.DynamicInvoke();
        }
    }
}
