using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for AdminDash.xaml
    /// Handle menu options for administrator dashbboard 
    /// </summary>
    public partial class AdminDash : INotifyPropertyChanged
    {
        private string _lastAccess;
        private string _textBox1Text;
        private string _textBox2Text;
        private string _submitButtonContent;
        private string _menuHeader;
        private string confirmMessage;
        private string _text1Text;
        private string _text2Text;
        private string _errorMsg;

        private static string ADDTEACHER = "Add";
        private static string REMOVETEACHER = "Remove";
        private static string LOOKUPTEACHER = "Search";
        private static string UPDATEEMAIL = "Update";
        
        private System.Delegate _delCreateMethod;
        private System.Delegate _delLogoutMethod;
        private System.Delegate _delReloadTabsMethod;
        private System.Delegate _delShowAddAccountsMethod;

        static ChannelFactory<IMetaInterface> accountsChannelFactory = new
             ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");
        static IMetaInterface proxy = accountsChannelFactory.CreateChannel();

        //create new user account
        public Delegate CallingCreateMethod
        {
            set { _delCreateMethod = value; }
        }

        public Delegate CallingLogoutMethod
        {
            set { _delLogoutMethod = value; }
        }

        public Delegate CallingReloadTabsMethod
        {
            set { _delReloadTabsMethod = value; }
        }

        public Delegate CallingShowAddAccountsMethod
        {
           set { _delShowAddAccountsMethod = value; }
        }

        public string LastAccess
        {
            get { return _lastAccess; }
            set
            {
                _lastAccess = value;
                OnPropertyChanged();
            }
        }

        public string TextBox1Text
        {
            get { return _textBox1Text; }
            set
            {
                _textBox1Text = value;
                OnPropertyChanged();
            }
        }

        public string TextBox2Text
        {
            get { return _textBox2Text; }
            set
            {
                _textBox2Text = value;
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

        public string ConfirmMessage
        {
            get { return confirmMessage; }
            set {
                    confirmMessage = value;
                    OnPropertyChanged();
                }
        }
        
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AdminDash()
        {
            InitializeComponent();
            this.DataContext = this;

            LastAccess = "Last Login: " + proxy.GetAdminAccessDate(Email.EmailAddress);
            MenuHeader = "Add A Teacher";
            SubmitButtonContent = ADDTEACHER;
            Text1Text = "First Name: ";
            Text2Text = "Last Name: ";
            ErrorMsg = "Invalid/Duplicate Name";
        }

        private void newAccountButton_Click(object sender, RoutedEventArgs e)
        {
            _delShowAddAccountsMethod.DynamicInvoke();
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
            SubmitButtonContent = ADDTEACHER;
            MenuHeader = "Add A Teacher";
            Text1Text = "First Name: ";
            Text2Text = "Last Name: ";
            ErrorMsg = "Invalid/Duplicate Name";
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();
            ClearTextBoxes();
            SubmitButtonContent = REMOVETEACHER;
            MenuHeader = "Remove A Teacher";
            Text1Text = "First Name: ";
            Text2Text = "Last Name: ";
            ErrorMsg = "Teacher doesn't exist";
        }

        private void lookup_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();
            ClearTextBoxes();
            SubmitButtonContent = LOOKUPTEACHER;
            MenuHeader = "Teacher Code Lookup";
            Text1Text = "First Name: ";
            Text2Text = "Last Name: ";
            ErrorMsg = "Teacher doesn't exist";
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();

            if (SubmitButtonContent == ADDTEACHER)
            {
                //add teacher, get access code for teacher
                int code = proxy.AddTeacher(TextBox1Text, TextBox2Text);
                if (code != -1)
                {
                    confirmMsg.Visibility = System.Windows.Visibility.Visible;
                    ConfirmMessage = "Teacher Access Code: " + code.ToString();
                }
                else
                    invalidEntryMsg.Visibility = System.Windows.Visibility.Visible;
            }
            else if (SubmitButtonContent == REMOVETEACHER)
            {
                DeleteTeacherVerify verify = new DeleteTeacherVerify(this, TextBox1Text + " " + TextBox2Text);
                verify.Show();
            }
            else if(SubmitButtonContent == UPDATEEMAIL)
            {
                //check that emails match
                if (TextBox1Text == TextBox2Text)
                {
                    if (proxy.ValidateEmail(TextBox1Text))
                    {
                        if (proxy.UpdateEmail(Email.EmailAddress, TextBox1Text))
                        {
                            Email.EmailAddress = TextBox1Text; //update static email value
                            validUpdateInfo.Visibility = System.Windows.Visibility.Visible;
                            ClearTextBoxes();
                        }
                        else
                            invalidEntryMsg.Visibility = System.Windows.Visibility.Visible;
                    }
                     else
                        invalidEntryMsg.Visibility = System.Windows.Visibility.Visible;
                }
                else
                    invalidEntryMsg.Visibility = System.Windows.Visibility.Visible;
            }
            else if(SubmitButtonContent == LOOKUPTEACHER)
            {
                //get access code of with teacher with matching first and last name
                int code = proxy.AccessCodeLookup(TextBox1Text + " " + TextBox2Text);

                if (code != -1)
                {
                    confirmMsg.Visibility = System.Windows.Visibility.Visible;
                    ConfirmMessage = "Access Code: " + code.ToString();
                }
                else
                    invalidEntryMsg.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void editEmail_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();
            ClearTextBoxes();
            Text1Text = "     New Email: ";
            Text2Text = "Reenter Email: ";
            SubmitButtonContent = UPDATEEMAIL;
            MenuHeader = "Update Account Email";
            ErrorMsg = "Invalid/Mismatched Email Entries";
        }

        private void editPassword_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();
            ClearTextBoxes();
            PasswordRecovery passwordPage = new PasswordRecovery();
            passwordPage.ChangePasswordWindow();
            passwordPage.Show();
        }

        //clear message to user
        private void ClearMessages()
        {
            invalidEntryMsg.Visibility = System.Windows.Visibility.Collapsed;
            confirmMsg.Visibility = System.Windows.Visibility.Collapsed;
            validUpdateInfo.Visibility = System.Windows.Visibility.Collapsed;

            if (!proxy.ExistingParentAccount(Email.EmailAddress) || !proxy.ExistingTeacherAccount(Email.EmailAddress))
                a_newAccountButton.Visibility = System.Windows.Visibility.Visible;
            else
                a_newAccountButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        //clear text boxes
        private void ClearTextBoxes()
        {
            TextBox1Text = null;
            TextBox2Text = null;
        }

        public void RemoveTeacher()
        {
            if (proxy.RemoveTeacher(TextBox1Text, TextBox2Text))
            {
                ClearTextBoxes();
                confirmMsg.Visibility = System.Windows.Visibility.Visible;
                ConfirmMessage = "Teacher Removed";
                _delReloadTabsMethod.DynamicInvoke();
            }
            else
                invalidEntryMsg.Visibility = System.Windows.Visibility.Visible;
        }

        private void textBoxText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ClearMessages();
        }
    }
}
