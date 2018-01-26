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
        static ChannelFactory<IWCFService> channelFactory = new
           ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

        static IWCFService proxy = channelFactory.CreateChannel();

        delegate void DelHandleStudentClickUserControlMethod(string studentName);
        delegate void DelRefreshListUserControlMethod();
        delegate void DelHandleFormClickedUserControlMethod(string formName);
        delegate void DelHandleFormCancelUserControlMethod();
        delegate void DelHandleStudentFormCompleteUserControlMethod();

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

            DelHandleStudentClickUserControlMethod delUserStudentHandleClickControl = new DelHandleStudentClickUserControlMethod(HandleStudentClick);
            DelRefreshListUserControlMethod delUserRefreshListControl = new DelRefreshListUserControlMethod(RefreshStudentList);
            DelHandleFormClickedUserControlMethod delUserFormClickedControl = new DelHandleFormClickedUserControlMethod(HandleFormClick);
            DelHandleFormCancelUserControlMethod delUserFormCanceledControl = new DelHandleFormCancelUserControlMethod(ExitPreviewForm);
            DelHandleStudentFormCompleteUserControlMethod delStudentFormCompleteControl = new DelHandleStudentFormCompleteUserControlMethod(ExitCompleteStudentForm);
            editStudentUC.CallingRefreshListMethod = delUserRefreshListControl;
            listStudentsUC.CallingDeleteStudentMethod = delUserStudentHandleClickControl;
            listFormsUC.CallingDeleteFormClickedMethod = delUserFormClickedControl;
            trackingFormsUC.CallingExitPreviewForm = delUserFormCanceledControl;
            trackingFormsUC.CallingExitCompleteStudentForm = delStudentFormCompleteControl;

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
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void editStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = "Edit Student";
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void addForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = "Add Form To Student";
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void deleteForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = "Remove Form";
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void trackStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = "Complete Student Forms";
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
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
            listForms.Visibility = System.Windows.Visibility.Collapsed;
            Panel1.Visibility = System.Windows.Visibility.Collapsed;
            Panel2.Visibility = System.Windows.Visibility.Collapsed;
            invalidEntryMsg.Visibility = System.Windows.Visibility.Collapsed;
            successMsg.Visibility = System.Windows.Visibility.Collapsed;
            submitButton.Visibility = System.Windows.Visibility.Collapsed;
            trackingForms.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.BehaviorScale.Visibility = System.Windows.Visibility.Visible;
            trackingFormsUC.StudentInfo.Visibility = System.Windows.Visibility.Visible;
            trackingFormsUC.followDirectionsForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.CompletingAssignmentsForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.InattentivenessForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.ArguingTalkingBackForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.TalkingOutOfTurnForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.customFormName.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.CustomBehaviorForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.IncidentForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.InterventionForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.ProgressReportForm.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void ClearTextFields()
        {
            TextBox1Text = "";
            TextBox2Text = "";
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
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
            _studentName = studentName;
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
            else if(MenuHeader == "Add Form To Student")
            {
                ClearForms();
                ClearMessages();
                RefreshTeacherFormsList();
                trackingFormsUC.StudentName = studentName;
                listFormsUC.FormListHeader = "Select a form to add to student: " + studentName;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if(MenuHeader == "Complete Student Forms")
            {
                ClearForms();
                ClearMessages();
                listFormsUC.StudentName = studentName;
                trackingFormsUC.StudentName = studentName;
                RefreshStudentDailyFormsList();
                listFormsUC.FormListHeader = "Select a form to complete: " + studentName;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if(MenuHeader == "Remove Form")
            {
                ClearForms();
                ClearMessages();
                listFormsUC.StudentName = studentName;
                RefreshStudentFormsList();
                listFormsUC.FormListHeader = "Select a form to remove from student: " + studentName;
                listFormsUC.Visibility = System.Windows.Visibility.Visible;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //Handle events where form name is clicked from list
        public void HandleFormClick(string name)
        {
            ClearForms();
            ClearMessages();
            trackingFormsUC.ClearFields();

            if (MenuHeader == "Add Form To Student" || MenuHeader == "Complete Student Forms")
            {
                if (MenuHeader == "Add Form To Student")
                    trackingFormsUC.SubmitButtonContent = "Add";
                else
                    trackingFormsUC.SubmitButtonContent = "Submit";
                   
                trackingFormsUC.FormName = name;
                trackingForms.Visibility = System.Windows.Visibility.Visible;

                if (_studentName != null)
                {
                    string lname = _studentName.Split(',')[0];
                    string fname = _studentName.Split(' ')[1];

                    trackingFormsUC.BehaviorDescription = proxy.GetStudentFormDescription(fname, lname, name);
                }

                if (name == "Follow Directions")
                    trackingFormsUC.followDirectionsForm.Visibility = System.Windows.Visibility.Visible;
                else if (name == "Completing Assignments")
                    trackingFormsUC.CompletingAssignmentsForm.Visibility = System.Windows.Visibility.Visible;
                else if (name == "Arguing/Talking Back")
                    trackingFormsUC.ArguingTalkingBackForm.Visibility = System.Windows.Visibility.Visible;
                else if (name == "Talking Out of Turn")
                    trackingFormsUC.TalkingOutOfTurnForm.Visibility = System.Windows.Visibility.Visible;
                else if (name == "Inattentiveness/Lack of Participation")
                    trackingFormsUC.InattentivenessForm.Visibility = System.Windows.Visibility.Visible;
                else if (name == "Incident Form")
                {
                    trackingFormsUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
                    trackingFormsUC.IncidentForm.Visibility = System.Windows.Visibility.Visible;
                }
                else if (name == "Intervention Form")
                {
                    trackingFormsUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
                    trackingFormsUC.InterventionForm.Visibility = System.Windows.Visibility.Visible;
                }
                else if (name == "Progress Report Form")
                {
                    trackingFormsUC.StudentInfo.Visibility = System.Windows.Visibility.Collapsed;
                    trackingFormsUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
                    trackingFormsUC.ProgressReportForm.Visibility = System.Windows.Visibility.Visible;
                }
                else   //custom form
                {
                    trackingFormsUC.customFormName.Visibility = System.Windows.Visibility.Visible;
                    trackingFormsUC.CustomBehaviorForm.Visibility = System.Windows.Visibility.Visible;
                }
             }
            else if(MenuHeader == "Remove Form")
            {
                listFormsUC.RemoveForm();
                RefreshStudentFormsList();
                ClearForms();
                listFormsUC.Visibility = System.Windows.Visibility.Visible;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //Delete a student
        public void DeleteStudent(string studentName)
        {
            if (studentName != null)
            {
                string lname = studentName.Split(',')[0];
                string fname = studentName.Split(' ')[1];

                proxy.DeleteStudent(Email.EmailAddress, fname, lname);
            }
            RefreshStudentList();
        }

        public void RefreshStudentList()
        {
            listStudentsUC.RefreshList();
        }

        public void RefreshTeacherFormsList()
        {
            listFormsUC.RefreshTeacherFormsList();
        }

        public void RefreshStudentFormsList()
        {
            listFormsUC.RefreshStudentForms();
        }

        public void RefreshParentFormsList()
        {
            listFormsUC.RefreshParentFormsList();
        }

        public void RefreshStudentDailyFormsList()
        {
            listFormsUC.RefreshStudentDailyForms();
        }
        public void ExitPreviewForm()
        {
            ClearForms();
            ClearMessages();
            
            RefreshTeacherFormsList();
            listForms.Visibility = System.Windows.Visibility.Visible;
            listFormsUC.Visibility = System.Windows.Visibility.Visible;
        }

        public void ExitCompleteStudentForm()
        {
            ClearForms();

            RefreshStudentDailyFormsList();
            listForms.Visibility = System.Windows.Visibility.Visible;
            listFormsUC.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
