using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for DisplayTeacherForm.xaml
    /// Display teacher submitted form
    /// </summary>
    public partial class DisplayTeacherForm : INotifyPropertyChanged
    {
        private string _formName;
        private string _studNameText;
        private string _teacherNameText;
        private string _studGradeText;
        private string _commentsSectionText;
        private string _dateCompletedText;
        private string _completedByNameText;
        private string _gross;
        private string _net;
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

        private static string FOLLOWDIRECTIONSFORM = "Follow Directions";
        private static string COMPLETEASSIGNMENTSFORM = "Completing Assignments";
        private static string TALKINGBACKFORM = "Arguing/Talking Back";
        private static string TALKINGOUTOFTURNFORM = "Talking Out of Turn";
        private static string INATTENTIVENESSFORM = "Inattentiveness/Lack of Participation";
        private static string INCIDENTFORM = "Incident Form";
        private static string SEPERATOR = ":?:";
        private static string INTERVENTIONFORM = "Intervention Form";

        private Delegate _handleBackButtonClickMethod;

        public Delegate CallingBackButtonClickMethod
        {
            set { _handleBackButtonClickMethod = value; }
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

        public string DateCompletedText
        {
            get { return _dateCompletedText; }
            set
            {
                _dateCompletedText = value;
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

        public string CommentsSectionText
        {
            get { return _commentsSectionText; }
            set
            {
                _commentsSectionText = value;
                OnPropertyChanged();
            }
        }

        public int BehaviorScaleRating
        {
            get { return _behaviorScaleRating; }
            set
            {
                _behaviorScaleRating = value;
                if (_behaviorScaleRating == 1) { behaviorScale1.IsChecked = true; }
                if (_behaviorScaleRating == 2) { behaviorScale2.IsChecked = true; }
                if (_behaviorScaleRating == 3) { behaviorScale3.IsChecked = true; }
                if (_behaviorScaleRating == 4) { behaviorScale4.IsChecked = true; }
                if (_behaviorScaleRating == 5) { behaviorScale5.IsChecked = true; }
                if (_behaviorScaleRating == 6) { behaviorScale6.IsChecked = true; }
                if (_behaviorScaleRating == 7) { behaviorScale7.IsChecked = true; }
                if (_behaviorScaleRating == 8) { behaviorScale8.IsChecked = true; }
                if (_behaviorScaleRating == 9) { behaviorScale9.IsChecked = true; }
                if (_behaviorScaleRating == 10) { behaviorScale10.IsChecked = true; }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DisplayTeacherForm()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void backButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _handleBackButtonClickMethod.DynamicInvoke(FormName);
        }

        public void ViewProgressReportForm(StudentFormData form)
        {
            formScroller.ScrollToTop();
            string[] delimiter = new string[] { SEPERATOR };

            string[] data = (form.Data).Split(delimiter, StringSplitOptions.None);
            int counter = data.Length;

            if (counter != 0)
            {
                StudentNameText = data[0];
                counter--;
            }
            if (counter != 0)
            {
                DateCompletedText = data[1];
                counter--;
            }
            if (counter != 0)
            {
                ProgressReportText = data[2];
                counter--;
            }
            if (counter != 0)
            {
                CommentsSectionText = data[3];
                counter--;
            }

            if (form.Shared)
                sharedCheckBox.IsChecked = true;

            sharedCheckBox.Visibility = System.Windows.Visibility.Visible;
            StudentInfo.Visibility = System.Windows.Visibility.Collapsed;
            BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
            ProgressReportForm.Visibility = System.Windows.Visibility.Visible;
        }

        public void ViewTeacherForm(StudentFormData form)
        {
            formScroller.ScrollToTop();

            string[] delimiter = new string[] { SEPERATOR };

            string[] data = (form.Data).Split(delimiter, StringSplitOptions.None);
            int counter = data.Length;

            //student info
            if (counter != 0)
            {
                StudentNameText = data[0];
                counter--;
            }
            if (counter != 0)
            {
                StudentGradeText = data[1];
                counter--;
            }
            if (counter != 0)
            {
                TeacherNameText = data[2];
                counter--;
            }
            if (counter != 0)
            {
                CompletedByNameText = data[3];
                counter--;
            }
            if (counter != 0)
            {
                DateCompletedText = data[4];
                counter--;
            }
            if (counter != 0)
            {
                try
                {
                    BehaviorScaleRating = Convert.ToInt32(data[5]);
                    counter--;
                }
                catch { }
            }

            //parse data for specific form
            if (form.FormName == COMPLETEASSIGNMENTSFORM)
            {
                CompletingAssignmentsForm.Visibility = System.Windows.Visibility.Visible;
                ParseCompleteAssignments(data, counter);
            }
            else if (form.FormName == TALKINGOUTOFTURNFORM)
            {
                TalkingOutOfTurnForm.Visibility = System.Windows.Visibility.Visible;
                ParseTalkingOutOfTurn(data, counter);
            }
            else if (form.FormName == INCIDENTFORM)
            {
                IncidentForm.Visibility = System.Windows.Visibility.Visible;
                ParseIncident(data, counter);
            }
            else if (form.FormName == INTERVENTIONFORM)
            {
                InterventionForm.Visibility = System.Windows.Visibility.Visible;
                ParseIntervention(data, counter);
            }
            else if (form.FormName == FOLLOWDIRECTIONSFORM || form.FormName == TALKINGBACKFORM
                        || form.FormName == INATTENTIVENESSFORM)
            {
                if (form.FormName == FOLLOWDIRECTIONSFORM)
                { followDirectionsForm.Visibility = System.Windows.Visibility.Visible; }
                if (form.FormName == TALKINGBACKFORM)
                { ArguingTalkingBackForm.Visibility = System.Windows.Visibility.Visible; }
                if (form.FormName == INATTENTIVENESSFORM)
                { InattentivenessForm.Visibility = System.Windows.Visibility.Visible; }

                ParseOtherForms(data, counter);
            }
            else
            {
                displayCustomForm.Visibility = System.Windows.Visibility.Visible;
                CustomBehaviorForm.Visibility = System.Windows.Visibility.Visible;
                ParseCustom(data, form);
            }

            FormName = form.FormName;
            sharedCheckBox.Visibility = System.Windows.Visibility.Collapsed;
        }

        //parse custom form
        public void ParseCustom(string[] data, StudentFormData _form)
        {
            BehaviorDescription = _form.FormDescription;

            string[] delimiter = new string[] { SEPERATOR };

            data = (_form.Data).Split(delimiter, StringSplitOptions.None);
            int counter = data.Length;

            if (counter != 0)
            {
                FormName = data[0];
                counter--;
            }
            if (counter != 0)
            {
                BehaviorDescription = data[1];
                counter--;
            }
            if (counter != 0)
            {
                StudentNameText = data[2];
                counter--;
            }
            if (counter != 0)
                StudentGradeText = data[3];
            if (counter != 0)
            {
                TeacherNameText = data[4];
                counter--;
            }
            if (counter != 0)
            {
                CompletedByNameText = data[5];
                counter--;
            }
            if (counter != 0)
            {
                DateCompletedText = data[6];
                counter--;
            }
            if (counter != 0)
            {
                try
                {
                    BehaviorScaleRating = Convert.ToInt32(data[7]);
                    counter--;
                }
                catch { }
            }
            if (counter != 0)
            {
                Gross = data[8];
                counter--;
            }
            if (counter != 0)
            {
                Net = data[9];
                counter--;
            }
            if (counter != 0)
                CommentsSectionText = data[10];
        }

        //parse intervention form
        public void ParseIntervention(string[] data, int counter)
        {
            if (counter != 0)
            {
                InterventionStartDate = data[6];
                counter--;
            }
            if (counter != 0)
            {
                InterventionEndDate = data[7];
                counter--;
            }
            if (counter != 0)
            {
                InterventionPeopleInvolved = data[8];
                counter--;
            }
            if (counter != 0)
            {
                AddressedIssue = data[9];
                counter--;
            }
            if (counter != 0)
            {
                InterventionFreqTime = data[10];
                counter--;
            }
            if (counter != 0)
            {
                InterventionDescription = data[11];
                counter--;
            }
            if (counter != 0)
                CommentsSectionText = data[12];

            BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
        }

        //parse incident form
        public void ParseIncident(string[] data, int counter)
        {
            if (counter != 0)
            {
                IncidentDate = data[6];
                counter--;
            }
            if (counter != 0)
            {
                IncidentTime = data[7];
                counter--;
            }
            if (counter != 0)
            {
                IncidentPeopleInvolved = data[8];
                counter--;
            }
            if (counter != 0)
            {
                IncidentDescription = data[9];
                counter--;
            }
            if (counter != 0)
            {
                IncidentHandledDescription = data[10];
                counter--;
            }
            if (counter != 0)
            {
                InterventionObservedResults = data[11];
                counter--;
            }
            if (counter != 0)
            {
                InterventionModifications = data[12];
                counter--;
            }
            if (counter != 0)
                CommentsSectionText = data[13];

            BehaviorScale.Visibility = System.Windows.Visibility.Collapsed;
        }

        //parse talking out of turn form
        public void ParseTalkingOutOfTurn(string[] data, int counter)
        {
            if (counter != 0)
            {
                Gross = data[6];
                counter--;
            }
            if (counter != 0)
                CommentsSectionText = data[7];
        }

        //parse complete assignments form
        public void ParseCompleteAssignments(string[] data, int counter)
        {
            if (counter != 0)
            {
                Gross = data[6];
                counter--;
            }
            if (counter != 0)
            {
                Net = data[7];
                counter--;
            }
            if (counter != 0)
            {
                Gross1 = data[8];
                counter--;
            }
            if (counter != 0)
            {
                Net1 = data[9];
                counter--;
            }
            if (counter != 0)
                CommentsSectionText = data[10];
        }

        //parse and display arguing/talking back, following directions, inattentive forms
        public void ParseOtherForms(string[] data, int counter)
        {
            if (counter != 0)
            {
                Gross = data[6];
                counter--;
            }
            if (counter != 0)
            {
                Net = data[7];
                counter--;
            }
            if (counter != 0)
                CommentsSectionText = data[8];
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
            displayCustomForm.Visibility = System.Windows.Visibility.Collapsed;
            CustomBehaviorForm.Visibility = System.Windows.Visibility.Collapsed;
            IncidentForm.Visibility = System.Windows.Visibility.Collapsed;
            InterventionForm.Visibility = System.Windows.Visibility.Collapsed;
            ProgressReportForm.Visibility = System.Windows.Visibility.Collapsed;
            sharedCheckBox.IsChecked = false;
        }
    }
}
