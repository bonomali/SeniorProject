using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for ParentDash.xaml
    /// Dashboard for parent user
    /// 
    /// </summary>
    public partial class ParentDash : INotifyPropertyChanged
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
        private string _childName;

        private static string COMPLETEFORMS = "Complete Child Forms";
        private static string UPDATEEMAIL = "Update Email";
        private static string UPDATEUSERNAME = "Update Username";
        private static string TEACHERREPORT = "View Daily Progress Report";
        private static string PROGRESSREPORTFORM = "Progress Report Form";
        private static string HOMETRACKINGFORM = "Home Tracking Form";
        private static string FORMHISTORY = "Form Archieve";
        private static string GRAPHS = "Graphs";
        private static string SEPERATOR = ":?:";

        private System.Delegate _delCreateMethod;
        private System.Delegate _delLogoutMethod;

        delegate void DelHandleChildClickUserControlMethod(string childName);
        delegate void DelHandleChildFormCompleteUserControlMethod();
        delegate void DelHandleFormClickedUserControlMethod(string formName);
        delegate void DelHandleArchieveFormClickedUserControlMethod(string formName);
        delegate void DelHandleArchieveBackButtonClickedUserControlMethod(string childName);
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

        public string ChildName
        {
            get { return _childName; }
            set
            {
                _childName = value;
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

        public ParentDash()
        {
            InitializeComponent();
            this.DataContext = this;

            DelHandleChildClickUserControlMethod delUserHandleChildClickedControl = new DelHandleChildClickUserControlMethod(HandleChildClick);
            DelHandleChildFormCompleteUserControlMethod delUserChildFormCompleteControl = new DelHandleChildFormCompleteUserControlMethod(FormComplete);
            DelHandleFormClickedUserControlMethod delUserHandleFormClickedControl = new DelHandleFormClickedUserControlMethod(HandleFormClicked);
            DelHandleArchieveFormClickedUserControlMethod delUserHandleArchieveFormClickedControl = new DelHandleArchieveFormClickedUserControlMethod(ArchieveFormClicked);
            DelHandleArchieveBackButtonClickedUserControlMethod delArchieveBackButtonMethod = new DelHandleArchieveBackButtonClickedUserControlMethod(HandleChildClick);
            DelHandleDisplayFormBackButtonClickedUserControlMethod delDisplayFormBackButtonClickedControl = new DelHandleDisplayFormBackButtonClickedUserControlMethod(HandleFormClicked);

            listChildrenUC.CallingClickChildMethod = delUserHandleChildClickedControl;
            homeTrackingFormsUC.CallingExitCompletedForm = delUserChildFormCompleteControl;
            listParentFormsUC.CallingFormClickedMethod = delUserHandleFormClickedControl;
            parentFormArchieveUC.CallingArchieveFormClickedMethod = delUserHandleArchieveFormClickedControl;
            parentFormArchieveUC.CallingBackButtonClickedMethod = delArchieveBackButtonMethod;
            parentSubmitFormUC.CallingBackButtonClickMethod = delDisplayFormBackButtonClickedControl;
            teacherSubmitFormUC.CallingBackButtonClickMethod = delDisplayFormBackButtonClickedControl;

            LastAccess = "Last Login: " + proxy.GetParentAccessDate(Email.EmailAddress);
            UserName = "Welcome " + proxy.GetParentUserName(Email.EmailAddress);

            trackChild_Click(this, null);
        }

        private void newAccountButton_Click(object sender, RoutedEventArgs e)
        {
            _delCreateMethod.DynamicInvoke();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            _delLogoutMethod.DynamicInvoke();
        }

        private void trackChild_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = COMPLETEFORMS;
            RefreshChildrenList();
            listChildren.Visibility = System.Windows.Visibility.Visible;
            listChildrenUC.Visibility = System.Windows.Visibility.Visible;
        }

        private void formHistory_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = FORMHISTORY;
            RefreshChildrenList();
            listChildren.Visibility = System.Windows.Visibility.Visible;
        }

        private void graphs_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = GRAPHS;
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

        public void ClearForms()
        {
            Panel1.Visibility = System.Windows.Visibility.Collapsed;
            Panel2.Visibility = System.Windows.Visibility.Collapsed;
            submitButton.Visibility = System.Windows.Visibility.Collapsed;
            listChildren.Visibility = System.Windows.Visibility.Collapsed;
            homeTrackingForms.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.form.Visibility = System.Windows.Visibility.Visible;
            teacherSubmitFormUC.StudentInfo.Visibility = System.Windows.Visibility.Visible;
            teacherSubmitFormUC.BehaviorScale.Visibility = System.Windows.Visibility.Visible;
            listParentForms.Visibility = System.Windows.Visibility.Collapsed;
            parentFormArchieve.Visibility = System.Windows.Visibility.Collapsed;
            parentSubmitForm.Visibility = System.Windows.Visibility.Collapsed;
            parentSubmitFormUC.sharedCheckBox.IsChecked = false;
            teacherSubmitFormUC.backButton.Visibility = System.Windows.Visibility.Visible;
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
                if (proxy.UpdateParentUserName(Email.EmailAddress, TextBox1Text))
                {
                    SuccessMsg = "Username Updated";
                    successMsg.Visibility = System.Windows.Visibility.Visible;
                    UserName = "Welcome " + proxy.GetParentUserName(Email.EmailAddress);
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
            homeTrackingFormsUC.noForms.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.noTeacherReport.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void teacherForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
            ClearMessages();
            MenuHeader = TEACHERREPORT;

            RefreshChildrenList();
            listChildren.Visibility = System.Windows.Visibility.Visible;
            listChildrenUC.Visibility = System.Windows.Visibility.Visible;
        }

        private void RefreshChildrenList()
        {
            listChildrenUC.RefreshList();
        }

        private void HandleChildClick(string childName)
        {
            ClearForms();
            homeTrackingFormsUC.ClearFields();

            _childName = childName;

            if (MenuHeader == TEACHERREPORT)
            {
                ClearForms();
                teacherSubmitFormUC.backButton.Visibility = System.Windows.Visibility.Collapsed;
                ParseProgressReport();
            }
            else if (MenuHeader == FORMHISTORY)
            {
                ClearForms();
                ClearMessages();
                listParentFormsUC.FormListHeader = "Select a form to view for: " + childName;
                listParentFormsUC.ChildName = childName;
                parentFormArchieveUC.StudentName = childName;
                RefreshParentViewableForms();
                listParentForms.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                homeTrackingFormsUC.ChildName = childName;
                RefreshDailyHomeForms();
            }
        }

        //handle event where form name is clicked from list
        public void HandleFormClicked(string formName)
        {
            if (MenuHeader == FORMHISTORY)
            {
                ClearForms();
                RefreshChildFormArchieve(formName);
                parentFormArchieveUC.FormListHeader = "Select a form to view for: " + ChildName;
                parentFormArchieve.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //handle event where form is clicked on in archieve
        public void ArchieveFormClicked(string formID)
        {
            ClearForms();

            StudentFormData form = proxy.GetParentViewableFormByID(formID);
            if(form != null)
            {
                teacherSubmitFormUC.FormName = form.FormName;
                parentSubmitFormUC.FormName = form.FormName;

                if (form.FormName == PROGRESSREPORTFORM)
                    ParseProgressReportFormData(form);
                else if (form.FormName == HOMETRACKINGFORM)
                    ParseHomeTrackingFormData(form);
            }
        }

        private void FormComplete()
        {
            ClearForms();

            RefreshChildrenList();
            listChildren.Visibility = System.Windows.Visibility.Visible;
        }

        //get list of names of forms parent may view
        public void RefreshParentViewableForms()
        {
            listParentFormsUC.RefreshParentViewableForms();
        }

        public void RefreshChildFormArchieve(string name)
        {
            parentFormArchieveUC.RefreshChildFormArchieve(name);
        }

        //get list of daily home tracking forms from db and set list source
        public void RefreshDailyHomeForms()
        {
            ClearForms();

            List<string> forms = new List<string>();

            if (_childName != null)
            {
                string fname = _childName.Split(' ')[1];
                string lname = _childName.Split(',')[0];

                forms = proxy.GetChildDailyForms(fname, lname);
            }
            if (forms.Count == 0)
            {
                homeTrackingFormsUC.noForms.Visibility = System.Windows.Visibility.Visible;
                homeTrackingFormsUC.form.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
                homeTrackingFormsUC.form.Visibility = System.Windows.Visibility.Visible;

            homeTrackingForms.Visibility = System.Windows.Visibility.Visible;
        }

        //get daily progress report from teacher
        private void ParseProgressReport()
        {
            if (_childName != null)
            {
                string lname = _childName.Split(',')[0];
                string fname = _childName.Split(' ')[1];

                StudentFormData form = new StudentFormData();
                form = proxy.GetDailyForm(PROGRESSREPORTFORM, fname, lname);

                if (form == null || form.Data == null)
                {
                    teacherSubmitFormUC.noTeacherReport.Visibility = System.Windows.Visibility.Visible;
                    teacherSubmitFormUC.form.Visibility = System.Windows.Visibility.Collapsed;
                    teacherSubmitForm.Visibility = System.Windows.Visibility.Visible;
                }
                else
                    ParseProgressReportFormData(form);
            }
        }

        private void ParseProgressReportFormData(StudentFormData form)
        {
            string[] delimiter = new string[] { SEPERATOR };

            string[] temp = (form.Data).Split(delimiter, StringSplitOptions.None);
            int counter = temp.Length;

            if (counter != 0)
            {
                teacherSubmitFormUC.StudentNameText = temp[0];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.DateCompletedText = temp[1];
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.ProgressReportText = temp[2];
                counter--;
            }
            if (counter != 0)
            {
                teacherSubmitFormUC.CommentsSectionText = temp[3];
                counter--;
            }

            teacherSubmitFormUC.sharedCheckBox.IsChecked = true;

            teacherSubmitFormUC.StudentInfo.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitForm.Visibility = System.Windows.Visibility.Visible;
        }

        private void ParseHomeTrackingFormData(StudentFormData form)
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
            if (form.Shared)
                parentSubmitFormUC.sharedCheckBox.IsChecked = true;

            parentSubmitForm.Visibility = System.Windows.Visibility.Visible;
        }
    }
}