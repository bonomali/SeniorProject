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
        private static string FORMHISTORY = "Form Archive";
        private static string GRAPHS = "Graphs";

        private System.Delegate _delCreateMethod;
        private System.Delegate _delLogoutMethod;
        private System.Delegate _delShowAddTeacherAccountMethod;
        private System.Delegate _delCollapseAccountsMethod;

        delegate void DelHandleChildClickUserControlMethod(string childName);
        delegate void DelHandleChildFormCompleteUserControlMethod();
        delegate void DelHandleFormClickedUserControlMethod(string formName);
        delegate void DelHandleArchiveFormClickedUserControlMethod(string formName);
        delegate void DelHandleArchiveBackButtonClickedUserControlMethod(string childName);
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

        public Delegate CallingShowAddTeacherAccountMethod
        {
            set { _delShowAddTeacherAccountMethod = value; }
        }

        public Delegate CallingCollapseAccountsMethod
        {
            set { _delCollapseAccountsMethod = value; }
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
            DelHandleArchiveFormClickedUserControlMethod delUserHandleArchiveFormClickedControl = new DelHandleArchiveFormClickedUserControlMethod(ArchiveFormClicked);
            DelHandleArchiveBackButtonClickedUserControlMethod delArchiveBackButtonMethod = new DelHandleArchiveBackButtonClickedUserControlMethod(HandleChildClick);
            DelHandleDisplayFormBackButtonClickedUserControlMethod delDisplayFormBackButtonClickedControl = new DelHandleDisplayFormBackButtonClickedUserControlMethod(HandleFormClicked);
            DelHandleGraphSelectionBackButtonClickedUserControlMethod delGraphBackButtonClickedControl = new DelHandleGraphSelectionBackButtonClickedUserControlMethod(ChildrenList);

            listChildrenUC.CallingClickChildMethod = delUserHandleChildClickedControl;
            homeTrackingFormsUC.CallingExitCompletedForm = delUserChildFormCompleteControl;
            listParentFormsUC.CallingFormClickedMethod = delUserHandleFormClickedControl;
            parentformArchiveUC.CallingArchiveFormClickedMethod = delUserHandleArchiveFormClickedControl;
            parentformArchiveUC.CallingBackButtonClickedMethod = delArchiveBackButtonMethod;
            parentSubmitFormUC.CallingBackButtonClickMethod = delDisplayFormBackButtonClickedControl;
            teacherSubmitFormUC.CallingBackButtonClickMethod = delDisplayFormBackButtonClickedControl;
            childGraphsUC.CallingBackButtonClickMethod = delGraphBackButtonClickedControl;

            LastAccess = "Last Login: " + proxy.GetParentAccessDate(Email.EmailAddress);
            UserName = "Welcome " + proxy.GetParentUserName(Email.EmailAddress);

            trackChild_Click(this, null);
        }

        private void newAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (!proxy.ExistingTeacherAccount(Email.EmailAddress))
                _delShowAddTeacherAccountMethod.DynamicInvoke();
            else
                _delCollapseAccountsMethod.DynamicInvoke();
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
            RefreshChildrenList();
            listChildren.Visibility = System.Windows.Visibility.Visible;
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

            homeTrackingFormsUC.form.ScrollToTop();
            homeTrackingForms.Visibility = System.Windows.Visibility.Collapsed;

            listChildren.Visibility = System.Windows.Visibility.Collapsed;
            listParentForms.Visibility = System.Windows.Visibility.Collapsed;
            parentformArchive.Visibility = System.Windows.Visibility.Collapsed;
            childGraphs.Visibility = System.Windows.Visibility.Collapsed;

            teacherSubmitForm.Visibility = System.Windows.Visibility.Collapsed;
            teacherSubmitFormUC.formScroller.Visibility = System.Windows.Visibility.Visible;
            teacherSubmitFormUC.StudentInfo.Visibility = System.Windows.Visibility.Visible;
            teacherSubmitFormUC.BehaviorScale.Visibility = System.Windows.Visibility.Visible;

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

        //return to list of children
        public void ChildrenList()
        {
            ClearForms();
            RefreshChildrenList();
            listChildren.Visibility = System.Windows.Visibility.Visible;
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
                teacherSubmitFormUC.FormName = "Daily Progress Report";
                ParseProgressReport();
            }
            else if (MenuHeader == FORMHISTORY)
            {
                ClearForms();
                ClearMessages();
                listParentFormsUC.FormListHeader = "Select a form to view for: " + childName;
                listParentFormsUC.ChildName = childName;
                parentformArchiveUC.StudentName = childName;
                RefreshParentViewableForms();
                listParentForms.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MenuHeader == GRAPHS)
            {
                ClearForms();
                childGraphsUC.StudentName = childName;
                childGraphs.Visibility = System.Windows.Visibility.Visible;
                childGraphsUC.DisplayChildFormList();
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
                RefreshChildformArchive(formName);
                parentformArchiveUC.FormListHeader = "Select a form to view for: " + ChildName;
                parentformArchive.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //handle event where form is clicked on in Archive
        public void ArchiveFormClicked(string formID)
        {
            ClearForms();

            StudentFormData form = proxy.GetParentViewableFormByID(formID);
            if(form != null)
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

        public void RefreshChildformArchive(string name)
        {
            parentformArchiveUC.RefreshChildformArchive(name);
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
                form = proxy.GetCompletedDailyForm(PROGRESSREPORTFORM, fname, lname);

                if (form == null || form.Data == null)
                {
                    teacherSubmitFormUC.noTeacherReport.Visibility = System.Windows.Visibility.Visible;
                    teacherSubmitFormUC.formScroller.Visibility = System.Windows.Visibility.Collapsed;
                    teacherSubmitForm.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    teacherSubmitFormUC.ViewProgressReportForm(form);
                    teacherSubmitForm.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
    }
}