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
    /// Interaction logic for TeacherDash.xaml
    /// </summary>
    public partial class TeacherDash : INotifyPropertyChanged
    {
        delegate void DelHandleClickUserControlMethod(string studentName);
        delegate void DelRefreshListUserControlMethod();
        private System.Delegate _delCreateMethod;
        private System.Delegate _delLogoutMethod;
        private string _lastAccess;
        private string _userName;
        private string _menuHeader;
        private string _text1Text;
        private string _text2Text;
        private string _textBox1Text;
        private string _textBox2Text;
        private string _invalidMsg;
        private string _successMsg;
        private string _submitButtonContent;
        private string _studentName;

        public string LastAccess
        {
            get { return _lastAccess; }
            set
            {
                _lastAccess = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
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

        public string ErrorMsg
        {
            get { return _invalidMsg; }
            set
            {
                _invalidMsg = value;
                OnPropertyChanged();
            }
        }

        public string SuccessMsg
        {
            get { return _successMsg; }
            set
            {
                _successMsg = value;
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

        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
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

        public TeacherDash()
        {
            InitializeComponent();
            this.DataContext = this;
            DelHandleClickUserControlMethod delUserHandleClickControl = new DelHandleClickUserControlMethod(HandleStudentClick);
            DelRefreshListUserControlMethod delUserRefreshListControl = new DelRefreshListUserControlMethod(RefreshList);
            editStudentUC.CallingRefreshListMethod = delUserRefreshListControl;
            listStudentsUC.CallingDeleteStudentMethod = delUserHandleClickControl;

            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (proxy.ExistingParentAccount(Email.EmailAddress))
                t_newAccountButton.Visibility = System.Windows.Visibility.Hidden;

            LastAccess = "Last Login: " + proxy.GetAdminAccessDate(Email.EmailAddress);
            UserName = "Welcome " + proxy.GetTeacherUserName(Email.EmailAddress);
        }

        private void newAccountButton_Click(object sender, RoutedEventArgs e)
        {
            _delCreateMethod.DynamicInvoke();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            _delLogoutMethod.DynamicInvoke();
        }

        private void addStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = "Add Student";
            addStudentForm.Visibility = System.Windows.Visibility.Visible;
        }

        private void dropStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = "Remove Student";
            RefreshList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void editStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = "Edit Student";
            RefreshList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void addForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
        }

        private void deleteForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
        }

        private void trackStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
        }

        private void viewProgress_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
        }

        private void editUserName_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            ClearTextFields();
            MenuHeader = "Update Username";
            ShowUpdateUserNameForm();
        }

        private void editEmail_Click(object sender, RoutedEventArgs e)
        {
            ClearTextFields();
            ClearForms();
            ClearMessages();
            MenuHeader = "Update Email";
            ShowUpdateEmailForm();
        }

        private void editPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecovery passwordPage = new PasswordRecovery();
            passwordPage.ChangePasswordWindow();
            passwordPage.Show();
        }
        
        public void ClearForms()
        {
            addStudentForm.Visibility = System.Windows.Visibility.Collapsed;
            editStudentForm.Visibility = System.Windows.Visibility.Collapsed;
            listStudents.Visibility = System.Windows.Visibility.Collapsed;
            Panel1.Visibility = System.Windows.Visibility.Collapsed;
            Panel2.Visibility = System.Windows.Visibility.Collapsed;
            invalidEntryMsg.Visibility = System.Windows.Visibility.Collapsed;
            successMsg.Visibility = System.Windows.Visibility.Collapsed;
            submitButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void ClearTextFields()
        {
            TextBox1Text = "";
            TextBox2Text = "";
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            ClearMessages();

            if (MenuHeader == "Update Username")
            {
                if (proxy.UpdateTeacherUserName(Email.EmailAddress, TextBox1Text))
                {
                    SuccessMsg = "Username Updated";
                    successMsg.Visibility = System.Windows.Visibility.Visible;
                    UserName = "Welcome " + proxy.GetTeacherUserName(Email.EmailAddress);
                }
                else
                {
                    ErrorMsg = "Error Updating Username";
                    invalidEntryMsg.Visibility = System.Windows.Visibility.Visible;
                }
                    
            }
            else if (MenuHeader == "Update Email")
            {
                ErrorMsg = "Invalid/Mismatched Email Addresses";

                if (proxy.ValidateEmail(TextBox1Text))
                {
                    if (TextBox1Text == TextBox2Text)
                    {
                        if (proxy.UpdateEmail(Email.EmailAddress, TextBox1Text))
                        {
                            Email.EmailAddress = TextBox1Text;

                            SuccessMsg = "Email Updated";
                            successMsg.Visibility = System.Windows.Visibility.Visible;
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
        }

        public void ShowUpdateEmailForm()
        {
            Panel1.Visibility = System.Windows.Visibility.Visible;
            Panel2.Visibility = System.Windows.Visibility.Visible;
            submitButton.Visibility = System.Windows.Visibility.Visible;
            Text1Text = "New Email: ";
            Text2Text = "Reenter Email: ";
            SubmitButtonContent = "Update";
        }

        public void ShowUpdateUserNameForm()
        {
            Panel1.Visibility = System.Windows.Visibility.Visible;
            submitButton.Visibility = System.Windows.Visibility.Visible;
            Text1Text = "New Username: ";
            SubmitButtonContent = "Update";
        }

        public void ClearMessages()
        {
            invalidEntryMsg.Visibility = System.Windows.Visibility.Collapsed;
            successMsg.Visibility = System.Windows.Visibility.Collapsed;
        }

        //Handle events where student name is clicked from list
        public void HandleStudentClick(string studentName)
        {
            if(MenuHeader == "Remove Student")
            {
                DeleteStudentVerify verify = new DeleteStudentVerify(this, studentName);
                verify.Show();
            }
            else if(MenuHeader == "Edit Student")
            {
                ClearForms();
                ClearMessages();
                editStudentUC.StudentFullName = studentName;
                editStudentForm.Visibility = System.Windows.Visibility.Visible;
                editStudentUC.GetStudentInfo();
            }
        }
        //Delete a student
        public void DeleteStudent(string studentName)
        {
           ChannelFactory<IWCFService> channelFactory = new
           ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            string lname = studentName.Split(',')[0];
            string fname = studentName.Split(' ')[1];

            proxy.DeleteStudent(Email.EmailAddress, fname, lname);
            RefreshList();
        }

        public void RefreshList()
        {
            listStudentsUC.RefreshList();
        }
    }
}
