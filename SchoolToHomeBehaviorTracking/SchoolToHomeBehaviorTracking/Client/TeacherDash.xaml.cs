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

        private static string ADDSTUDENT = "Add Student";
        private static string REMOVESTUDENT = "Remove Student";
        private static string EDITSTUDENT = "Edit Student";
        private static string ADDFORM = "Add Form To Student";
        private static string REMOVEFORM = "Remove Form From Student";
        private static string COMPLETEFORMS = "Complete Student Forms";
        private static string UPDATEEMAIL = "Update Email";
        private static string UPDATEUSERNAME = "Update Username";
        private static string PARENTFORM = "View Home Tracking Form";
        private static string PROGRESSREPORTFORM = "Progress Report Form";
        private static string FOLLOWDIRECTIONSFORM = "Follow Directions";
        private static string COMPLETEASSIGNMENTSFORM = "Completing Assignments";
        private static string TALKINGBACKFORM = "Arguing/Talking Back";
        private static string TALKINGOUTOFTURNFORM = "Talking Out of Turn";
        private static string INATTENTIVENESSFORM = "Inattentiveness/Lack of Participation";
        private static string INTERVENTIONFORM = "Intervention Form";
        private static string INCIDENTFORM = "Incident Form";
        private static string HOMETRACKINGFORM = "Home Tracking Form";
        private static string FORMHISTORY = "Form Archieve";
        private static string GRAPHS = "Graphs";
        private static string SEPERATOR = ":?:";

        private System.Delegate _delCreateMethod;
        private System.Delegate _delLogoutMethod;
        private System.Delegate _delReloadTabsMethod;

        delegate void DelHandleStudentClickUserControlMethod(string studentName);
        delegate void DelRefreshListUserControlMethod();
        delegate void DelHandleFormClickedUserControlMethod(string formName);
        delegate void DelHandleFormCancelUserControlMethod();
        delegate void DelHandleStudentFormCompleteUserControlMethod();
        delegate void DelHandleExitStudentUserControlMethod();
        delegate void DelHandleArchieveFormClickedUserControlMethod(string formID);
        delegate void DelHandleArchieveBackButtonClickedUserControlMethod(string studentName);
        delegate void DelHandleDisplayFormBackButtonClickedUserControlMethod(string formName);

        static ChannelFactory<IWCFService> channelFactory = new
         ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

        static IWCFService proxy = channelFactory.CreateChannel();

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
            DelHandleArchieveFormClickedUserControlMethod delArchieveFormClickedControl = new DelHandleArchieveFormClickedUserControlMethod(ArchieveFormClicked);
            DelHandleArchieveBackButtonClickedUserControlMethod delArchieveBackButtonClickedControl = new DelHandleArchieveBackButtonClickedUserControlMethod(HandleStudentClick);
            DelHandleDisplayFormBackButtonClickedUserControlMethod delDisplayFormBackButtonClickedControl = new DelHandleDisplayFormBackButtonClickedUserControlMethod(HandleFormClick);

            editStudentUC.CallingRefreshListMethod = delUserRefreshListControl;
            editStudentUC.CallingExitMethod = delExitStudentControl;
            addStudentFormUC.CallingExitMethod = delExitStudentControl;
            listStudentsUC.CallingDeleteStudentMethod = delUserStudentHandleClickControl;
            listFormsUC.CallingFormClickedMethod = delUserFormClickedControl;
            trackingFormsUC.CallingExitPreviewForm = delUserFormCanceledControl;
            trackingFormsUC.CallingExitCompleteStudentForm = delStudentFormCompleteControl;
            formArchieveUC.CallingFormClickedMethod = delUserFormClickedControl;
            formArchieveUC.CallingArchieveFormClickedMethod = delArchieveFormClickedControl;
            formArchieveUC.CallingBackButtonClickedMethod = delArchieveBackButtonClickedControl;
            parentSubmitFormUC.CallingBackButtonClickMethod = delDisplayFormBackButtonClickedControl;
            teacherSubmitFormUC.CallingBackButtonClickMethod = delDisplayFormBackButtonClickedControl;

            LastAccess = "Last Login: " + proxy.GetTeacherAccessDate(Email.EmailAddress);
            UserName = "Welcome " + proxy.GetTeacherUserName(Email.EmailAddress);

            trackStudent_Click(this, null);
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
                listFormsUC.FormListHeader = "Select a form to add to student: " + studentName;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MenuHeader == COMPLETEFORMS)
            {
                ClearForms();
                ClearMessages();
                listFormsUC.StudentName = studentName;
                trackingFormsUC.StudentName = studentName;
                RefreshStudentDailyFormsList();
                listFormsUC.FormListHeader = "Select a form to complete: " + studentName;
                listForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MenuHeader == REMOVEFORM)
            {
                ClearForms();
                ClearMessages();
                listFormsUC.StudentName = studentName;
                RefreshStudentFormsList();
                listFormsUC.FormListHeader = "Select a form to remove from student: " + studentName;
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
                formArchieveUC.StudentName = studentName;
                RefreshAllFormsList();
                listFormsUC.FormListHeader = "Select a form: " + studentName;
                listForms.Visibility = System.Windows.Visibility.Visible;
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
                RefreshStudentFormArchieve(name);
                formArchieveUC.FormListHeader = "Select a form to view for: " + _studentName;
                formArchieveUC.StudentName = _studentName;
                formArchieve.Visibility = System.Windows.Visibility.Visible;
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

        public void RefreshStudentFormArchieve(string name)
        {
            formArchieveUC.RefreshStudentFormArchieve(name);
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

            if (name == FOLLOWDIRECTIONSFORM)
                trackingFormsUC.followDirectionsForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == COMPLETEASSIGNMENTSFORM)
                trackingFormsUC.CompletingAssignmentsForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == TALKINGBACKFORM)
                trackingFormsUC.ArguingTalkingBackForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == TALKINGOUTOFTURNFORM)
                trackingFormsUC.TalkingOutOfTurnForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == INATTENTIVENESSFORM)
                trackingFormsUC.InattentivenessForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == INCIDENTFORM)
            {
                trackingFormsUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
                trackingFormsUC.IncidentForm.Visibility = System.Windows.Visibility.Visible;
            }
            else if (name == INTERVENTIONFORM)
            {
                trackingFormsUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
                trackingFormsUC.InterventionForm.Visibility = System.Windows.Visibility.Visible;
            }
            else if (name == PROGRESSREPORTFORM)
            {
                trackingFormsUC.StudentInfo.Visibility = System.Windows.Visibility.Collapsed;
                trackingFormsUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
                trackingFormsUC.ProgressReportForm.Visibility = System.Windows.Visibility.Visible;
            }
            else   //custom form
            {
                if (MenuHeader == ADDFORM)
                    trackingFormsUC.addCustomForm.Visibility = System.Windows.Visibility.Visible;
                else
                    trackingFormsUC.displayCustomForm.Visibility = System.Windows.Visibility.Visible;

                trackingFormsUC.CustomBehaviorForm.Visibility = System.Windows.Visibility.Visible;
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
                form = proxy.GetDailyForm(HOMETRACKINGFORM, fname, lname);

                if (form == null || form.Data == null)
                {
                    parentSubmitFormUC.noParentSubmittedForm.Visibility = System.Windows.Visibility.Visible;
                    parentSubmitFormUC.form.Visibility = System.Windows.Visibility.Collapsed;
                    parentSubmitForm.Visibility = System.Windows.Visibility.Visible;
                }
                else
                    ParseHomeTrackingFormData(form);
            }
        }

        //parse home tracking form
        public void ParseHomeTrackingFormData(StudentFormData form)
        {
            string[] delimiter = new string[] { SEPERATOR };

            string[] data = (form.Data).Split(delimiter, StringSplitOptions.None);
            int counter = data.Length;

            if (counter != 0)
            {
                parentSubmitFormUC.FormName = data[0];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.ChildNameText = data[1];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.CompletedByNameText = data[2];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.DateCompletedText = data[3];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.BehaviorScaleRating = Convert.ToInt32(data[4]);
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.WakeTime = data[5];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.AsleepTime = data[6];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.Breakfast = data[7];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.Lunch = data[8];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.Dinner = data[9];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.Snacks = data[10];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.Mood = data[11];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.Consequences = data[12];
                counter--;
            }
            if (counter != 0)
            {
                parentSubmitFormUC.CommentSectionText = data[13];
                counter--;
            }
            parentSubmitFormUC.sharedCheckBox.IsChecked = true;

            parentSubmitForm.Visibility = System.Windows.Visibility.Visible;
        }

        //handle event where form is clicked on in archieve
        public void ArchieveFormClicked(string formID)
        {
            ClearForms();

            StudentFormData form = proxy.GetTeacherViewableFormByID(formID);
            if (form != null)
            {
                teacherSubmitFormUC.FormName = form.FormName;
                parentSubmitFormUC.FormName = form.FormName;

                if (form.FormName == PROGRESSREPORTFORM)
                    ParseProgressReport(form);
                else if (form.FormName == HOMETRACKINGFORM)
                    ParseHomeTrackingFormData(form);
                else
                    ParseTeacherForm(form);
            }
        }

        //parse a behavior form submitted by teacher
        private void ParseTeacherForm(StudentFormData form)
        {
            string[] delimiter = new string[] { SEPERATOR };

            string[] data = (form.Data).Split(delimiter, StringSplitOptions.None);
            int counter = data.Length;

            //student info
            if (counter != 0)
            {
                teacherSubmitFormUC.StudentNameText = data[0];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.StudentGradeText = data[1];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.TeacherNameText = data[2];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.CompletedByNameText = data[3];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.DateCompletedText = data[4];
                counter--;
            }
            if (counter != 0)
            {
                try
                {
                    teacherSubmitFormUC.BehaviorScaleRating = Convert.ToInt32(data[5]);
                    counter--;
                }
                catch { }
            }

            //parse data for specific form
            if (form.FormName == COMPLETEASSIGNMENTSFORM)
            {
                teacherSubmitFormUC.CompletingAssignmentsForm.Visibility = System.Windows.Visibility.Visible;
                ParseCompleteAssignments(data, counter);
            }
            else if (form.FormName == TALKINGOUTOFTURNFORM)
            {
                teacherSubmitFormUC.TalkingOutOfTurnForm.Visibility = System.Windows.Visibility.Visible;
                ParseTalkingOutOfTurn(data, counter);
            }
            else if (form.FormName == INCIDENTFORM)
            {
                teacherSubmitFormUC.IncidentForm.Visibility = System.Windows.Visibility.Visible;
                ParseIncident(data, counter);
            }
            else if (form.FormName == INTERVENTIONFORM)
            {
                teacherSubmitFormUC.InterventionForm.Visibility = System.Windows.Visibility.Visible;
                ParseIntervention(data, counter);
            }
            else if (form.FormName == FOLLOWDIRECTIONSFORM || form.FormName == TALKINGBACKFORM
                        || form.FormName == INATTENTIVENESSFORM)
            {
                if (form.FormName == FOLLOWDIRECTIONSFORM)
                { teacherSubmitFormUC.followDirectionsForm.Visibility = System.Windows.Visibility.Visible; }
                if (form.FormName == TALKINGBACKFORM)
                { teacherSubmitFormUC.ArguingTalkingBackForm.Visibility = System.Windows.Visibility.Visible; }
                if (form.FormName == INATTENTIVENESSFORM)
                { teacherSubmitFormUC.InattentivenessForm.Visibility = System.Windows.Visibility.Visible; }

                ParseOtherForms(data, counter);
            }
            else
            {
                teacherSubmitFormUC.displayCustomForm.Visibility = System.Windows.Visibility.Visible;
                teacherSubmitFormUC.CustomBehaviorForm.Visibility = System.Windows.Visibility.Visible;
                ParseCustom(data, form);
            }

            teacherSubmitFormUC.sharedCheckBox.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitForm.Visibility = System.Windows.Visibility.Visible;
        }

        //parse custom form
        public void ParseCustom(string[] data, StudentFormData form)
        {
            if (_studentName != null)
            {
                string lname = _studentName.Split(',')[0];
                string fname = _studentName.Split(' ')[1];

                //get behavior description field of tracking form
                teacherSubmitFormUC.BehaviorDescription = proxy.GetStudentFormDescription(fname, lname, form.FormName);
            }

            string[] delimiter = new string[] { SEPERATOR };

            data = (form.Data).Split(delimiter, StringSplitOptions.None);
            int counter = data.Length;

            if (counter != 0)
            {
                teacherSubmitFormUC.FormName = data[0];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.BehaviorDescription = data[1];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.StudentNameText = data[2];
                counter--;
            }
            if (counter != 0)
                teacherSubmitFormUC.StudentGradeText = data[3];
            if (counter != 0)
            {
                teacherSubmitFormUC.TeacherNameText = data[4];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.CompletedByNameText = data[5];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.DateCompletedText = data[6];
                counter--;
            }
            if(counter != 0)
            {
                try
                {
                    teacherSubmitFormUC.BehaviorScaleRating = Convert.ToInt32(data[7]);
                    counter--;
                }
                catch { }
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.Gross = data[8];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.Net = data[9];
                counter--;
            }
            if (counter != 0)
                teacherSubmitFormUC.CommentsSectionText = data[10];
        }

        //parse incident form
        public void ParseIntervention(string[] data, int counter)
        {
            if (counter != 0)
            {
                teacherSubmitFormUC.InterventionStartDate = data[6];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.InterventionEndDate = data[7];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.InterventionPeopleInvolved = data[8];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.AddressedIssue = data[9];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.InterventionFreqTime = data[10];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.InterventionDescription = data[11];
                counter--;
            }
            if (counter != 0)
                teacherSubmitFormUC.CommentsSectionText = data[12];

            teacherSubmitFormUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
        }

        //parse incident form
        public void ParseIncident(string[] data, int counter)
        {
            if (counter != 0)
            {
                teacherSubmitFormUC.IncidentDate = data[6];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.IncidentTime = data[7];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.IncidentPeopleInvolved = data[8];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.IncidentDescription = data[9];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.IncidentHandledDescription = data[10];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.InterventionObservedResults = data[11];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.InterventionModifications = data[12];
                counter--;
            }
            if (counter != 0)
                teacherSubmitFormUC.CommentsSectionText = data[13];

            teacherSubmitFormUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
        }

        //parse talking out of turn form
        public void ParseTalkingOutOfTurn(string[] data, int counter)
        {
            if (counter != 0)
            {
                teacherSubmitFormUC.Gross = data[6];
                counter--;
            }
            if (counter != 0)
                teacherSubmitFormUC.CommentsSectionText = data[7];
        }

        //parse complete assignments form
        public void ParseCompleteAssignments(string[] data, int counter)
        {
            if (counter != 0)
            {
                teacherSubmitFormUC.Gross = data[6];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.Net = data[7];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.Gross1 = data[8];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.Net1 = data[9];
                counter--;
            }
            if (counter != 0)
                teacherSubmitFormUC.CommentsSectionText = data[10];
        }

        //parse and display arguing/talking back, following directions, inattentive forms
        public void ParseOtherForms(string[] data, int counter)
        {
            if (counter != 0)
            {
                teacherSubmitFormUC.Gross = data[6];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.Net = data[7];
                counter--;
            }
            if (counter != 0)
                teacherSubmitFormUC.CommentsSectionText = data[8];
        }

        //display progress report
        public void ParseProgressReport(StudentFormData form)
        {
            string[] delimiter = new string[] { SEPERATOR };

            string[] data = (form.Data).Split(delimiter, StringSplitOptions.None);
            int counter = data.Length;

            if (counter != 0)
            {
                teacherSubmitFormUC.StudentNameText = data[0];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.DateCompletedText = data[1];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.ProgressReportText = data[2];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.CommentsSectionText = data[3];
                counter--;
            }

            if(form.Shared)
                teacherSubmitFormUC.sharedCheckBox.IsChecked = true;

            teacherSubmitFormUC.sharedCheckBox.Visibility = System.Windows.Visibility.Visible;
            teacherSubmitFormUC.StudentInfo.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.ProgressReportForm.Visibility = System.Windows.Visibility.Visible;
            teacherSubmitForm.Visibility = System.Windows.Visibility.Visible;
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
            trackingFormsUC.BehaviorScale.Visibility = System.Windows.Visibility.Visible;
            trackingFormsUC.StudentInfo.Visibility = System.Windows.Visibility.Visible;
            trackingFormsUC.followDirectionsForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.CompletingAssignmentsForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.InattentivenessForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.ArguingTalkingBackForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.TalkingOutOfTurnForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.addCustomForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.displayCustomForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.CustomBehaviorForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.IncidentForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.InterventionForm.Visibility = System.Windows.Visibility.Collapsed;
            trackingFormsUC.ProgressReportForm.Visibility = System.Windows.Visibility.Collapsed;

            teacherSubmitForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.BehaviorScale.Visibility = System.Windows.Visibility.Visible;
            teacherSubmitFormUC.StudentInfo.Visibility = System.Windows.Visibility.Visible;
            teacherSubmitFormUC.followDirectionsForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.CompletingAssignmentsForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.InattentivenessForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.ArguingTalkingBackForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.TalkingOutOfTurnForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.addCustomForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.displayCustomForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.CustomBehaviorForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.IncidentForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.InterventionForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.ProgressReportForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.sharedCheckBox.IsChecked = false;

            parentSubmitForm.Visibility = System.Windows.Visibility.Collapsed;
            parentSubmitFormUC.form.Visibility = System.Windows.Visibility.Visible;
            parentSubmitFormUC.backButton.Visibility = System.Windows.Visibility.Visible;

            formArchieve.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
