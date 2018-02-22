using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for TeacherDash.xaml
    /// Teacher dashboard for teacher user
    /// </summary>
    public partial class TeacherDash : INotifyPropertyChanged
    {
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

        private static string ADDSTUDENT = "Add A Student";
        private static string REMOVESTUDENT = "Drop Student";
        private static string EDITSTUDENT = "Edit Student";
        private static string ADDFORM = "Add Tracking Form";
        private static string REMOVEFORM = "Delete Tracking Form";
        private static string COMPLETEFORMS = "Complete Tracking Forms";
        private static string UPDATEEMAIL = "Update Email";
        private static string UPDATEUSERNAME = "Update Username";
        private static string PARENTFORM = "View Home Tracking Form";
        private static string PROGRESSREPORTFORM = "Progress Report Form";
        private static string HOMETRACKINGFORM = "Home Tracking Form";
        private static string FORMHISTORY = "Form Archive";
        private static string GRAPHS = "Graphs";

        private System.Delegate _delCreateMethod;
        private System.Delegate _delLogoutMethod;
        private System.Delegate _delReloadTabsMethod;
        private System.Delegate _delShowAddParentAccountMethod;

        delegate void DelHandleStudentClickUserControlMethod(string studentName);
        delegate void DelRefreshListUserControlMethod();
        delegate void DelHandleFormClickedUserControlMethod(string formName);
        delegate void DelHandleFormCancelUserControlMethod();
        delegate void DelHandleStudentFormCompleteUserControlMethod();
        delegate void DelHandleExitStudentUserControlMethod();
        delegate void DelHandleArchiveFormClickedUserControlMethod(string formID);
        delegate void DelHandleArchiveBackButtonClickedUserControlMethod(string studentName);
        delegate void DelHandleDisplayFormBackButtonClickedUserControlMethod(string formName);
        delegate void DelHandleGraphSelectionBackButtonClickedUserControlMethod();

        static ChannelFactory<IMetaInterface> channelFactory = new
            ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");
        static IMetaInterface proxy = channelFactory.CreateChannel();

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

        public Delegate CallingReloadTabsMethod
        {
            set { _delReloadTabsMethod = value; }
        }

        public Delegate CallingShowAddParentAccountMethod
        {
            set { _delShowAddParentAccountMethod = value; }
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
            DelHandleExitStudentUserControlMethod delExitStudentControl = new DelHandleExitStudentUserControlMethod(ExitStudentMenu);
            DelHandleArchiveFormClickedUserControlMethod delArchiveFormClickedControl = new DelHandleArchiveFormClickedUserControlMethod(ArchiveFormClicked);
            DelHandleArchiveBackButtonClickedUserControlMethod delArchiveBackButtonClickedControl = new DelHandleArchiveBackButtonClickedUserControlMethod(HandleStudentClick);
            DelHandleDisplayFormBackButtonClickedUserControlMethod delDisplayFormBackButtonClickedControl = new DelHandleDisplayFormBackButtonClickedUserControlMethod(HandleFormClick);
            DelHandleGraphSelectionBackButtonClickedUserControlMethod delGraphSelectionBackButtonClickedControl = new DelHandleGraphSelectionBackButtonClickedUserControlMethod(ListStudents);

            editStudentUC.CallingRefreshListMethod = delUserRefreshListControl;
            editStudentUC.CallingExitMethod = delExitStudentControl;
            addStudentFormUC.CallingExitMethod = delExitStudentControl;
            listStudentsUC.CallingDeleteStudentMethod = delUserStudentHandleClickControl;
            listFormsUC.CallingFormClickedMethod = delUserFormClickedControl;
            trackingFormsUC.CallingExitPreviewForm = delUserFormCanceledControl;
            trackingFormsUC.CallingExitCompleteStudentForm = delStudentFormCompleteControl;
            formArchiveUC.CallingFormClickedMethod = delUserFormClickedControl;
            formArchiveUC.CallingArchiveFormClickedMethod = delArchiveFormClickedControl;
            formArchiveUC.CallingBackButtonClickedMethod = delArchiveBackButtonClickedControl;
            parentSubmitFormUC.CallingBackButtonClickMethod = delDisplayFormBackButtonClickedControl;
            teacherSubmitFormUC.CallingBackButtonClickMethod = delDisplayFormBackButtonClickedControl;
            studentGraphsUC.CallingBackButtonClickMethod = delGraphSelectionBackButtonClickedControl;

            LastAccess = "Last Login: " + proxy.GetTeacherAccessDate(Email.EmailAddress);
            UserName = "Welcome " + proxy.GetTeacherUserName(Email.EmailAddress);

            trackStudent_Click(this, null);
        }

        private void newAccountButton_Click(object sender, RoutedEventArgs e)
        {
            _delShowAddParentAccountMethod.DynamicInvoke();
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
            MenuHeader = ADDSTUDENT;
            addStudentForm.Visibility = System.Windows.Visibility.Visible;
        }

        private void dropStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = REMOVESTUDENT;
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void editStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = EDITSTUDENT;
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void addForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = ADDFORM;
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void deleteForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = REMOVEFORM;
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void trackStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = COMPLETEFORMS;
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void graphs_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = GRAPHS;
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void formHistory_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = FORMHISTORY;
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        private void editUserName_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            ClearTextFields();
            MenuHeader = UPDATEUSERNAME;
            ShowUpdateUserNameForm();
        }

        private void editEmail_Click(object sender, RoutedEventArgs e)
        {
            ClearTextFields();
            ClearForms();
            ClearMessages();
            MenuHeader = UPDATEEMAIL;
            ShowUpdateEmailForm();
        }

        private void editPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecovery passwordPage = new PasswordRecovery();
            passwordPage.ChangePasswordWindow();
            passwordPage.Show();
        }

        public void ClearTextFields()
        {
            TextBox1Text = null;
            TextBox2Text = null;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            ClearMessages();

            if (MenuHeader == UPDATEUSERNAME)
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
            else if (MenuHeader == UPDATEEMAIL)
            {
                ErrorMsg = "Invalid/Mismatched Email Addresses";

                if (proxy.ValidateEmail(TextBox1Text))
                {
                    //check that emails match
                    if (TextBox1Text == TextBox2Text)
                    {
                        if (proxy.UpdateEmail(Email.EmailAddress, TextBox1Text))
                        {
                            Email.EmailAddress = TextBox1Text; //update static email value

                            SuccessMsg = "Email Updated For All Accounts";
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
            Text1Text = "     New Email: ";
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
            listFormsUC.noForms.Visibility = System.Windows.Visibility.Collapsed;
            listFormsUC.noOtherForms.Visibility = System.Windows.Visibility.Collapsed;
            parentSubmitFormUC.noParentSubmittedForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.previewWarning.Visibility = System.Windows.Visibility.Collapsed;

            if (!proxy.ExistingParentAccount(Email.EmailAddress))
                t_newAccountButton.Visibility = System.Windows.Visibility.Visible;
            else
                t_newAccountButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        //Handle events where student name is clicked from list
        public void HandleStudentClick(string studentName)
        {
            _studentName = studentName;
            if (MenuHeader == REMOVESTUDENT)
            {
                DeleteStudentVerify verify = new DeleteStudentVerify(this, studentName);
                verify.Show();
            }
            else if (MenuHeader == EDITSTUDENT)
            {
                ClearForms();
                ClearMessages();
                editStudentUC.StudentFullName = studentName;
                editStudentForm.Visibility = System.Windows.Visibility.Visible;
                editStudentUC.GetStudentInfo();
            }
            else if (MenuHeader == ADDFORM)
            {
                ClearForms();
                ClearMessages();
                RefreshTeacherFormsList();
                trackingFormsUC.StudentName = studentName;
                trackingFormsUC.previewWarning.Visibility = System.Windows.Visibility.Visible;
                listFormsUC.FormListHeader = "Student: " + studentName;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MenuHeader == COMPLETEFORMS)
            {
                ClearForms();
                ClearMessages();
                listFormsUC.StudentName = studentName;
                trackingFormsUC.StudentName = studentName;
                RefreshStudentDailyFormsList();
                listFormsUC.FormListHeader = "Student: " + studentName;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MenuHeader == REMOVEFORM)
            {
                ClearForms();
                ClearMessages();
                listFormsUC.StudentName = studentName;
                RefreshStudentFormsList();
                listFormsUC.FormListHeader = "Student: " + studentName;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MenuHeader == PARENTFORM)
            {
                ClearForms();
                parentSubmitFormUC.backButton.Visibility = System.Windows.Visibility.Collapsed;
                ParseHomeTracking();
            }
            else if (MenuHeader == FORMHISTORY)
            {
                ClearForms();
                ClearMessages();

                listFormsUC.StudentName = studentName;
                formArchiveUC.StudentName = studentName;
                RefreshAllFormsList();
                listFormsUC.FormListHeader = "Student: " + studentName;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MenuHeader == GRAPHS)
            {
                ClearForms();
                studentGraphsUC.StudentName = studentName;
                studentGraphs.Visibility = System.Windows.Visibility.Visible;
                studentGraphsUC.DisplayStudentFormList();
            }
        }

        //Handle events where form name is clicked from list
        public void HandleFormClick(string name)
        {
            ClearForms();
            trackingFormsUC.ClearFields();

            if (MenuHeader == ADDFORM || MenuHeader == COMPLETEFORMS)
            {
                DisplayForm(name);
            }
            else if (MenuHeader == REMOVEFORM)
            {
                listFormsUC.RemoveForm();
                RefreshStudentFormsList();
                ClearForms();
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MenuHeader == FORMHISTORY)
            {
                ClearForms();
                RefreshStudentformArchive(name);
                formArchiveUC.FormListHeader = "Select a form to view for: " + _studentName;
                formArchiveUC.StudentName = _studentName;
                formArchive.Visibility = System.Windows.Visibility.Visible;
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
            _delReloadTabsMethod.DynamicInvoke();
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

        public void RefreshAllFormsList()
        {
            listFormsUC.RefreshAllForms();
        }

        public void RefreshStudentformArchive(string name)
        {
            formArchiveUC.RefreshStudentformArchive(name);
        }

        public void RefreshStudentDailyFormsList()
        {
            listFormsUC.RefreshStudentDailyForms();
        }

        //return to list of all forms
        public void ExitPreviewForm()
        {
            ClearForms();

            RefreshTeacherFormsList();
            listForms.Visibility = System.Windows.Visibility.Visible;
        }

        //return to list of students
        public void ListStudents()
        {
            ClearForms();
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        //return to list of student forms
        public void ExitCompleteStudentForm()
        {
            ClearForms();

            if (MenuHeader == ADDFORM)
                RefreshTeacherFormsList();
            else
                RefreshStudentDailyFormsList();
            listForms.Visibility = System.Windows.Visibility.Visible;
        }

        //display parent home tracking form
        private void parentForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = PARENTFORM;

            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        //return to list of students
        private void ExitStudentMenu()
        {
            ClearForms();

            MenuHeader = COMPLETEFORMS;
            RefreshStudentList();
            listStudents.Visibility = System.Windows.Visibility.Visible;
        }

        //display blank teacher form 
        private void DisplayForm(string name)
        {
            ClearForms();

            if (MenuHeader == ADDFORM)
                trackingFormsUC.SubmitButtonContent = "Add";
            else
                trackingFormsUC.SubmitButtonContent = "Submit";

            trackingFormsUC.FormName = name;
            trackingForms.Visibility = System.Windows.Visibility.Visible;

            if (_studentName != null)
            {
                string lname = _studentName.Split(',')[0];
                string fname = _studentName.Split(' ')[1];

                //get behavior description field of tracking form
                trackingFormsUC.BehaviorDescription = proxy.GetStudentFormDescription(fname, lname, name);
            }

            //display correct portions of form depending on form type
            trackingFormsUC.DisplayForm(name);

            //if a custom form is visible
            if (trackingFormsUC.CustomBehaviorForm.Visibility == System.Windows.Visibility.Visible)
            {
                if (MenuHeader == ADDFORM)
                    trackingFormsUC.addCustomForm.Visibility = System.Windows.Visibility.Visible;
                else
                    trackingFormsUC.displayCustomForm.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //parse parent submitted home tracking form and display
        private void ParseHomeTracking()
        {
            if (_studentName != null)
            {
                string lname = _studentName.Split(',')[0];
                string fname = _studentName.Split(' ')[1];

                StudentFormData form = new StudentFormData();
                form = proxy.GetCompletedDailyForm(HOMETRACKINGFORM, fname, lname);

                if (form == null || form.Data == null)
                {
                    parentSubmitFormUC.noParentSubmittedForm.Visibility = System.Windows.Visibility.Visible;
                    parentSubmitFormUC.formScroller.Visibility = System.Windows.Visibility.Collapsed;
                    parentSubmitForm.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    parentSubmitFormUC.ViewParentForm(form);
                    parentSubmitForm.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        //handle event where form is clicked on in Archive
        public void ArchiveFormClicked(string formID)
        {
            ClearForms();

            StudentFormData form = proxy.GetTeacherViewableFormByID(formID);
            if (form != null)
            {
                teacherSubmitFormUC.FormName = form.FormName;
                parentSubmitFormUC.FormName = form.FormName;

                if (form.FormName == PROGRESSREPORTFORM)
                {
                    teacherSubmitFormUC.ViewProgressReportForm(form);
                    teacherSubmitForm.Visibility = System.Windows.Visibility.Visible;
                }
                else if (form.FormName == HOMETRACKINGFORM)
                {
                    parentSubmitFormUC.ViewParentForm(form);
                    parentSubmitForm.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    teacherSubmitFormUC.ViewTeacherForm(form);
                    teacherSubmitForm.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        //reset visibilty settings
        public void ClearForms()
        {
            addStudentForm.Visibility = System.Windows.Visibility.Collapsed;
            editStudentForm.Visibility = System.Windows.Visibility.Collapsed;
            listStudents.Visibility = System.Windows.Visibility.Collapsed;
            listForms.Visibility = System.Windows.Visibility.Collapsed;
            Panel1.Visibility = System.Windows.Visibility.Collapsed;
            Panel2.Visibility = System.Windows.Visibility.Collapsed;
            submitButton.Visibility = System.Windows.Visibility.Collapsed;

            trackingForms.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.ClearForms();

            teacherSubmitForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.ClearForms();

            parentSubmitForm.Visibility = System.Windows.Visibility.Collapsed;
            parentSubmitFormUC.formScroller.Visibility = System.Windows.Visibility.Visible;
            parentSubmitFormUC.backButton.Visibility = System.Windows.Visibility.Visible;

            formArchive.Visibility = System.Windows.Visibility.Collapsed;

            studentGraphs.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
