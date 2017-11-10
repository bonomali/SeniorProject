using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AdminDash.xaml
    /// </summary>
    public partial class AdminDash : INotifyPropertyChanged
    {
        private System.Delegate _delCreateMethod;
        private System.Delegate _delLogoutMethod;
        private string _email = null;
        private string _lastAccess;
        private string _firstNameText;
        private string _lastNameText;
        private string _submitButtonContent;
        private string _menuHeader;
        private string _accessCode;
        private string _text1Text;
        private string _text2Text;

        public string LastAccess
        {
            get { return _lastAccess; }
            set
            {
                _lastAccess = value;
                OnPropertyChanged();
            }
        }

        public string FirstNameText
        {
            get { return _firstNameText; }
            set
            {
                _firstNameText = value;
                OnPropertyChanged();
            }
        }

        public string LastNameText
        {
            get { return _lastNameText; }
            set
            {
                _lastNameText = value;
                OnPropertyChanged();
            }
        }

        public string SubmitButtonContent
        {
            get { return _submitButtonContent; }
            set
            {
                _submitButtonContent = value;
                OnPropertyChanged();
            }
        }

        public string MenuHeader
        {
            get { return _menuHeader; }
            set
            {
                _menuHeader = value;
                OnPropertyChanged();
            }
        }

        public string AccessCode
        {
            get { return _accessCode; }
            set {
                    _accessCode = value;
                    OnPropertyChanged();
                }
        }

        public string Text1Text
        {
            get { return _text1Text; }
            set
            {
                _text1Text = value;
                OnPropertyChanged();
            }
        }

        public string Text2Text
        {
            get { return _text2Text; }
            set
            {
                _text2Text = value;
                OnPropertyChanged();
            }
        }

        public Delegate CallingCreateMethod
        {
            set { _delCreateMethod = value; }
        }

        public Delegate CallingLogoutMethod
        {
            set { _delLogoutMethod = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AdminDash(string email)
        {
            _email = email;
            InitializeComponent();
            this.DataContext = this;

            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (proxy.ExistingParentAccount(_email) && proxy.ExistingTeacherAccount(_email))
                a_newAccountButton.Visibility = System.Windows.Visibility.Hidden;

            LastAccess = "Last Login: " + proxy.GetAdminAccessDate(_email);
            MenuHeader = "Add Teacher";
            SubmitButtonContent = "Add";
            Text1Text = "First Name: ";
            Text2Text = "Last Name: ";
        }

        private void newAccountButton_Click(object sender, RoutedEventArgs e)
        {
            _delCreateMethod.DynamicInvoke();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            _delLogoutMethod.DynamicInvoke();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();
            ClearTextBoxes();
            SubmitButtonContent = "Add";
            MenuHeader = "Add Teacher";
            Text1Text = "First Name: ";
            Text2Text = "Last Name: ";
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();
            ClearTextBoxes();
            SubmitButtonContent = "Remove";
            MenuHeader = "Remove Teacher";
            Text1Text = "First Name: ";
            Text2Text = "Last Name: ";
        }

        private void lookup_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();
            ClearTextBoxes();
            SubmitButtonContent = "Search";
            MenuHeader = "Code Lookup";
            Text1Text = "First Name: ";
            Text2Text = "Last Name: ";
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            ClearMessages();

            if (SubmitButtonContent == "Add")
            {
                int code = proxy.AddTeacher(FirstNameText, LastNameText);
                if (code != -1)
                {
                    accessCode.Visibility = System.Windows.Visibility.Visible;
                    AccessCode = "Access Code: " + code.ToString();
                }
                else
                    invalidTeacherName.Visibility = System.Windows.Visibility.Visible;
            }
            else if (SubmitButtonContent == "Remove")
            {
                DeleteTeacherVerify verify = new DeleteTeacherVerify(this, FirstNameText + " " + LastNameText);
                verify.Show();
            }
            else if(Text1Text == "New Email: ")
            {
                if (FirstNameText == LastNameText)
                {
                    if (proxy.ValidateEmail(FirstNameText))
                    {
                        if (proxy.UpdateEmail(_email, FirstNameText))
                            validUpdateInfo.Visibility = System.Windows.Visibility.Visible;
                        else
                            invalidUpdateInfo.Visibility = System.Windows.Visibility.Visible;
                    }
                     else
                        invalidUpdateInfo.Visibility = System.Windows.Visibility.Visible;
                }
                else
                    invalidUpdateInfo.Visibility = System.Windows.Visibility.Visible;
            }
            else if(SubmitButtonContent == "Search")
            {
                int code = proxy.AccessCodeLookup(FirstNameText + " " + LastNameText);
                accessCode.Visibility = System.Windows.Visibility.Visible;

                if (code != -1)
                    AccessCode = "Access Code: " + code.ToString();
                else
                    AccessCode = "No Access Code Exists";
                    
            }
        }

        private void editEmail_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();
            ClearTextBoxes();
            Text1Text = "New Email: ";
            Text2Text = "Reenter Email: ";
            SubmitButtonContent = "Update";
            MenuHeader = "Update Email";
        }

        private void editPassword_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();
            ClearTextBoxes();
            ChangePassword passwordPage = new ChangePassword(_email);
            passwordPage.ReturnPage = "Exit";
            passwordPage.Show();
        }

        private void teacherFNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearMessages();
        }

        private void teacherLNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearMessages();
        }

        //clear message to user
        private void ClearMessages()
        {
            invalidTeacherName.Visibility = System.Windows.Visibility.Collapsed;
            accessCode.Visibility = System.Windows.Visibility.Collapsed;
            invalidUpdateInfo.Visibility = System.Windows.Visibility.Collapsed;
            validUpdateInfo.Visibility = System.Windows.Visibility.Collapsed;
        }

        //clear text boxes
        private void ClearTextBoxes()
        {
            FirstNameText = "";
            LastNameText = "";
        }

        public void RemoveTeacher()
        {
            ChannelFactory<IWCFService> channelFactory = new
           ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (proxy.RemoveTeacher(FirstNameText, LastNameText))
            {
                accessCode.Visibility = System.Windows.Visibility.Visible;
                AccessCode = "Teacher Removed";
            }
            else
                invalidTeacherName.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
