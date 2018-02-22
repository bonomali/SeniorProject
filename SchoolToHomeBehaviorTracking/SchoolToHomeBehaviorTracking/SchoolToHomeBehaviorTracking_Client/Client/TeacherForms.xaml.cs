using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for Forms.xaml
    /// Teacher forms for tracking
    /// </summary>
    public partial class TeacherForms : INotifyPropertyChanged
    {
        private string _formName;
        private string _name;
        private string _studNameText;
        private string _teacherNameText;
        private string _studGradeText;
        private string _completedByNameText;
        private string _dateCompletedText;
        private string _gross;
        private string _net;
        private string _commentsSectionText;
        private string _submitButtonContent;
        private string _gross1;
        private string _net1;
        private string _customBehaviorDescription;
        private string _incidentPeopleInvolved;
        private string _incidentDate;
        private string _incidentTime;
        private string _incidentDescription;
        private string _incidentHandledDescription;
        private string _interventionStartDate;
        private string _interventionEndDate;
        private string _interventionPeopleInvolved;
        private string _addressInterventionIssue;
        private string _interventionFreqTime;
        private string _interventionDescription;
        private string _interventionModifications;
        private string _interventionObservedResults;
        private string _progressReportText;
        private int _behaviorScaleRating;

        private static string ADDFORM = "Add";
        private static string COMPLETEFORM = "Submit";
        private static string SEPERATOR = ":?:";
        private static string PROGRESSREPORTFORM = "Progress Report Form";
        private static string FOLLOWDIRECTIONSFORM = "Follow Directions";
        private static string COMPLETEASSIGNMENTSFORM = "Completing Assignments";
        private static string TALKINGBACKFORM = "Arguing/Talking Back";
        private static string TALKINGOUTOFTURNFORM = "Talking Out of Turn";
        private static string INATTENTIVENESSFORM = "Inattentiveness/Lack of Participation";
        private static string INTERVENTIONFORM = "Intervention Form";
        private static string INCIDENTFORM = "Incident Form";
        private static string DATEFORMAT = "MM/dd/yyyy";

        private Delegate _delExitPreviewFormMethod;
        private Delegate _delExitCompleteStudentFormMethod;

        static ChannelFactory<IMetaInterface> channelFactory = new
        ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");
        static IMetaInterface proxy = channelFactory.CreateChannel();

        public Delegate CallingExitPreviewForm
        {
            set { _delExitPreviewFormMethod = value; }
        }

        public Delegate CallingExitCompleteStudentForm
        {
            set { _delExitCompleteStudentFormMethod = value; }
        }

        public TeacherForms()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string ProgressReportText
        {
            get { return _progressReportText; }
            set
            {
                _progressReportText = value;
                OnPropertyChanged();
            }
        }

        public string InterventionDescription
        {
            get { return _interventionDescription; }
            set
            {
                _interventionDescription = value;
                OnPropertyChanged();
            }
        }

        public string InterventionObservedResults
        {
            get { return _interventionObservedResults; }
            set
            {
                _interventionObservedResults = value;
                OnPropertyChanged();
            }
        }

        public string InterventionFreqTime
        {
            get { return _interventionFreqTime; }
            set
            {
                _interventionFreqTime = value;
                OnPropertyChanged();
            }
        }

        public string InterventionModifications
        {
            get { return _interventionModifications; }
            set
            {
                _interventionModifications = value;
                OnPropertyChanged();
            }
        }

        public string AddressedIssue
        {
            get { return _addressInterventionIssue; }
            set
            {
                _addressInterventionIssue = value;
                OnPropertyChanged();
            }
        }

        public string InterventionStartDate
        {
            get { return _interventionStartDate; }
            set
            {
                _interventionStartDate = value;
                OnPropertyChanged();
            }
        }
        public string InterventionEndDate
        {
            get { return _interventionEndDate; }
            set
            {
                _interventionEndDate = value;
                OnPropertyChanged();
            }
        }

        public string InterventionPeopleInvolved
        {
            get { return _interventionPeopleInvolved; }
            set
            {
                _interventionPeopleInvolved = value;
                OnPropertyChanged();
            }
        }

        public string IncidentDescription
        {
            get { return _incidentDescription; }
            set
            {
                _incidentDescription = value;
                OnPropertyChanged();
            }
        }
        public string IncidentHandledDescription
        {
            get { return _incidentHandledDescription; }
            set
            {
                _incidentHandledDescription = value;
                OnPropertyChanged();
            }
        }

        public string IncidentPeopleInvolved
        {
            get { return _incidentPeopleInvolved; }
            set
            {
                _incidentPeopleInvolved = value;
                OnPropertyChanged();
            }
        }

        public string IncidentDate
        {
            get { return _incidentDate; }
            set
            {
                _incidentDate = value;
                OnPropertyChanged();
            }
        }

        public string IncidentTime
        {
            get { return _incidentTime; }
            set
            {
                _incidentTime = value;
                OnPropertyChanged();
            }
        }

        public string BehaviorDescription
        {
            get { return _customBehaviorDescription; }
            set
            {
                _customBehaviorDescription = value;
                OnPropertyChanged();
            }
        }

        public string Gross
        {
            get { return _gross; }
            set
            {
                _gross = value;
                OnPropertyChanged();
            }
        }

        public string Net
        {
            get { return _net; }
            set
            {
                _net = value;
                OnPropertyChanged();
            }
        }

        public string Gross1
        {
            get { return _gross1; }
            set
            {
                _gross1 = value;
                OnPropertyChanged();
            }
        }

        public string Net1
        {
            get { return _net1; }
            set
            {
                _net1 = value;
                OnPropertyChanged();
            }
        }

        public string FormName
        {
            get { return _formName; }
            set
            {
                _formName = value;
                OnPropertyChanged();
            }
        }

        public string StudentName
        {
            get { return _name; }
            set { _name = value; }
        }

        public string StudentNameText
        {
            get { return _studNameText; }
            set
            {
                _studNameText = value;
                OnPropertyChanged();
            }
        }

        public string StudentGradeText
        {
            get { return _studGradeText; }
            set
            {
                _studGradeText = value;
                OnPropertyChanged();
            }
        }

        public string TeacherNameText
        {
            get { return _teacherNameText; }
            set
            {
                _teacherNameText = value;
                OnPropertyChanged();
            }
        }

        public string CompletedByNameText
        {
            get { return _completedByNameText; }
            set
            {
                _completedByNameText = value;
                OnPropertyChanged();
            }
        }

        public string DateCompletedText
        {
            get { return _dateCompletedText; }
            set
            {
                _dateCompletedText = value;
                OnPropertyChanged();
            }
        }

        public string CommentsSectionText
        {
            get { return _commentsSectionText; }
            set
            {
                _commentsSectionText = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubmitButtonContent == ADDFORM)
            {
                if (_name != null)
                {
                    string lname = _name.Split(',')[0];
                    string fname = _name.Split(' ')[1];

                    if (proxy.AddFormToStudent(FormName, BehaviorDescription, fname, lname))
                    {
                        MessagePopUp mess = new MessagePopUp("Form successfully added");
                        mess.Show();
                    }
                    else
                    {
                        MessagePopUp mess = new MessagePopUp("Error adding form/Duplicate form");
                        mess.Show();
                    }
                }
                ClearFields();
                _delExitPreviewFormMethod.DynamicInvoke();
            }
            else if(SubmitButtonContent == COMPLETEFORM)
            {
                StudentFormData form = new StudentFormData();

                if (StudentName != null)
                {
                    form.StudentFName = StudentName.Split(' ')[1];
                    form.StudentLName = StudentName.Split(',')[0];
                } 

                form.FormName = FormName;
                form.FormDate = DateCompletedText;
                form.EndDate = InterventionEndDate;
                form.BehaviorRating = _behaviorScaleRating;

                if ((bool)sharedCheckBox.IsChecked)
                    form.Shared = true;
                else
                    form.Shared = false;

                //custom behavior form
                if (displayCustomForm.Visibility == System.Windows.Visibility.Visible)
                    form.Data = FormName + SEPERATOR + BehaviorDescription + SEPERATOR + StudentNameText + SEPERATOR + 
                        StudentGradeText + SEPERATOR + TeacherNameText + SEPERATOR + CompletedByNameText + SEPERATOR 
                        + DateCompletedText + SEPERATOR + _behaviorScaleRating + SEPERATOR + Gross + SEPERATOR 
                        + Net + SEPERATOR + CommentsSectionText;
                
                //progress report form
                else if (ProgressReportForm.Visibility == System.Windows.Visibility.Visible)
                {
                    _behaviorScaleRating = 1;
                    form.Data = StudentNameText + SEPERATOR + DateCompletedText + SEPERATOR + ProgressReportText + SEPERATOR 
                        + CommentsSectionText;
                }

                else
                {
                    //student information
                    form.Data = StudentNameText + SEPERATOR + StudentGradeText + SEPERATOR + TeacherNameText +
                        SEPERATOR + CompletedByNameText + SEPERATOR + DateCompletedText + SEPERATOR + _behaviorScaleRating;

                    //completing assignments
                    if (CompletingAssignmentsForm.Visibility == System.Windows.Visibility.Visible)
                        form.Data += SEPERATOR + Gross + SEPERATOR + Net + SEPERATOR +
                            Gross1 + SEPERATOR + Net1 + SEPERATOR + CommentsSectionText;
                                        
                    //incident form
                    else if (IncidentForm.Visibility == System.Windows.Visibility.Visible)
                    {
                        _behaviorScaleRating = 1;
                        form.Data += SEPERATOR + IncidentDate + SEPERATOR + IncidentTime + SEPERATOR + 
                            IncidentPeopleInvolved + SEPERATOR + IncidentDescription + SEPERATOR + 
                            IncidentHandledDescription + SEPERATOR + CommentsSectionText;
                    }
                    
                    //intervention form
                    else if (InterventionForm.Visibility == System.Windows.Visibility.Visible)
                    {
                        _behaviorScaleRating = 1;
                        form.Data += SEPERATOR + InterventionStartDate + SEPERATOR + InterventionEndDate + SEPERATOR 
                            + InterventionPeopleInvolved + SEPERATOR + AddressedIssue + SEPERATOR + InterventionFreqTime 
                            + SEPERATOR + InterventionDescription + SEPERATOR + InterventionObservedResults + SEPERATOR 
                            + InterventionModifications + SEPERATOR + CommentsSectionText;
                    }

                    //talking out of turn
                    else if(TalkingOutOfTurnForm.Visibility == System.Windows.Visibility.Visible)
                        form.Data += SEPERATOR + Gross + SEPERATOR + Net + SEPERATOR + CommentsSectionText;

                    //arguing/talking back, following directions, inattentive forms
                    else
                        form.Data += SEPERATOR + Gross + SEPERATOR + Net + SEPERATOR + CommentsSectionText;
                }

                //check if form is invalid
                if (StudentNameText == null || DateCompletedText == null || StudentNameText.Length < 1 || 
                        DateCompletedText.Length < 1 || _behaviorScaleRating < 1 || !proxy.SaveStudentForm(form))
                {
                    if (_behaviorScaleRating == 0)
                    {
                        MessagePopUp mess = new MessagePopUp("Invalid Form: Student Name, Date Completed, and Behavior Scale Rating Required");
                        mess.Show();
                    }
                    else
                    {
                        MessagePopUp mess = new MessagePopUp("Invalid Form: Student Name and Date Completed Required");
                        mess.Show();
                    }
                }
                else
                {
                    MessagePopUp mess = new MessagePopUp("Form Successfully Saved");
                    mess.Show();
                    ClearFields();
                    _delExitCompleteStudentFormMethod.DynamicInvoke();
                }
            }
        }

        public void DisplayForm(string name)
        {
            form.ScrollToTop();

            if (name == FOLLOWDIRECTIONSFORM)
                followDirectionsForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == COMPLETEASSIGNMENTSFORM)
                CompletingAssignmentsForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == TALKINGBACKFORM)
                ArguingTalkingBackForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == TALKINGOUTOFTURNFORM)
                TalkingOutOfTurnForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == INATTENTIVENESSFORM)
                InattentivenessForm.Visibility = System.Windows.Visibility.Visible;
            else if (name == INCIDENTFORM)
            {
                BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
                IncidentForm.Visibility = System.Windows.Visibility.Visible;
            }
            else if (name == INTERVENTIONFORM)
            {
                BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
                InterventionForm.Visibility = System.Windows.Visibility.Visible;
            }
            else if (name == PROGRESSREPORTFORM)
            {
                StudentInfo.Visibility = System.Windows.Visibility.Collapsed;
                BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
                ProgressReportForm.Visibility = System.Windows.Visibility.Visible;
            }
            else   //custom form
            {
                CustomBehaviorForm.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void behaviorScale1_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 1;
        }

        private void behaviorScale2_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 2;
        }

        private void behaviorScale3_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 3;
        }

        private void behaviorScale4_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 4;
        }

        private void behaviorScale5_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 5;
        }

        private void behaviorScale6_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 6;
        }

        private void behaviorScale7_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 7;
        }

        private void behaviorScale8_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 8;
        }

        private void behaviorScale9_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 9;
        }

        private void behaviorScale10_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 10;
        }

        //reset behavior scale
        private void behaviorScale11_Checked(object sender, RoutedEventArgs e)
        {
            _behaviorScaleRating = 0;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            _delExitCompleteStudentFormMethod.DynamicInvoke();
        }

        public void ClearFields()
        {
            ProgressReportText = null;
            InterventionDescription = null;
            InterventionObservedResults = null;
            InterventionFreqTime = null;
            InterventionModifications = null;
            AddressedIssue = null;
            InterventionStartDate = null;
            InterventionEndDate = null;
            InterventionPeopleInvolved = null;
            IncidentDescription = null;
            IncidentHandledDescription = null;
            IncidentPeopleInvolved = null;
            IncidentDate = null;
            IncidentTime = null;
            BehaviorDescription = null;
            StudentNameText = null;
            StudentGradeText = null;
            TeacherNameText = null;
            CompletedByNameText = null;
            DateCompletedText = DateTime.Now.ToString(DATEFORMAT);
            Gross = null;
            Net = null;
            Gross1 = null;
            Net1 = null;
            CommentsSectionText = null;
        
            behaviorScale11.IsChecked = true;
            sharedCheckBox.IsChecked = false;
        }

        public void ClearForms()
        {
            BehaviorScale.Visibility = System.Windows.Visibility.Visible;
            StudentInfo.Visibility = System.Windows.Visibility.Visible;
            followDirectionsForm.Visibility = System.Windows.Visibility.Collapsed;
            CompletingAssignmentsForm.Visibility = System.Windows.Visibility.Collapsed;
            InattentivenessForm.Visibility = System.Windows.Visibility.Collapsed;
            ArguingTalkingBackForm.Visibility = System.Windows.Visibility.Collapsed;
            TalkingOutOfTurnForm.Visibility = System.Windows.Visibility.Collapsed;
            addCustomForm.Visibility = System.Windows.Visibility.Collapsed;
            displayCustomForm.Visibility = System.Windows.Visibility.Collapsed;
            CustomBehaviorForm.Visibility = System.Windows.Visibility.Collapsed;
            IncidentForm.Visibility = System.Windows.Visibility.Collapsed;
            InterventionForm.Visibility = System.Windows.Visibility.Collapsed;
            ProgressReportForm.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
