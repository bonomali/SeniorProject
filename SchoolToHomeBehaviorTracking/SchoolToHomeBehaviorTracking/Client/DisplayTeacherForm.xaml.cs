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
    }
}
