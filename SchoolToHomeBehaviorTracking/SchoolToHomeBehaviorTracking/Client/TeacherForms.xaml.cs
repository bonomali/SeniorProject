using SchoolToHomeBehaviorTracking_Client;
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
using static System.Windows.Forms.Control;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for Forms.xaml
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
        private string _totalGiven;
        private string _totalFollowed;
        private string _commentsSectionText;
        private string _submitButtonContent;
        private string _totalInClassAssigned;
        private string _totalInClassCompleted;
        private string _totalHomeworkAssigned;
        private string _totalHomeworkCompleted;
        private string _totalArguments;
        private string _argumentativeTimes;
        private string _totalTalkOutOfTurn;
        private string _totalInattentive;
        private string _inattentiveTimes;
        private string _customBehaviorDescription;
        private string _totalIncidents;
        private string _incidentTimes;
        private string _incdientPeopleInvolved;
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
        private Delegate _delExitPreviewFormMethod;
        private Delegate _delExitCompleteStudentFormMethod;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Delegate CallingExitPreviewForm
        {
            set { _delExitPreviewFormMethod = value; }
        }

        public Delegate CallingExitCompleteStudentForm
        {
            set { _delExitCompleteStudentFormMethod = value; }
        }

        public IEnumerable<Control> Controls { get; private set; }

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
            get { return _incdientPeopleInvolved; }
            set
            {
                _incdientPeopleInvolved = value;
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

        public string TotalIncidents
        {
            get { return _totalIncidents; }
            set
            {
                _totalIncidents = value;
                OnPropertyChanged();
            }
        }

        public string IncidentTimes
        {
            get { return _incidentTimes; }
            set
            {
                _incidentTimes = value;
                OnPropertyChanged();
            }
        }

        public string TotalInattentive
        {
            get { return _totalInattentive; }
            set
            {
                _totalInattentive = value;
                OnPropertyChanged();
            }
        }

        public string InattentiveTimes
        {
            get { return _inattentiveTimes; }
            set
            {
                _inattentiveTimes = value;
                OnPropertyChanged();
            }
        }

        public string TotalTalkOutOfTurn
        {
            get { return _totalTalkOutOfTurn; }
            set
            {
                _totalTalkOutOfTurn = value;
                OnPropertyChanged();
            }
        }

        public string TotalArguments
        {
            get { return _totalArguments; }
            set
            {
                _totalArguments = value;
                OnPropertyChanged();
            }
        }

        public string ArgumentativeTimes
        {
            get { return _argumentativeTimes; }
            set
            {
                _argumentativeTimes = value;
                OnPropertyChanged();
            }
        }

        public string TotalInClassAssigned
        {
            get { return _totalInClassAssigned; }
            set
            {
                _totalInClassAssigned = value;
                OnPropertyChanged();
            }
        }

        public string TotalInClassCompleted
        {
            get { return _totalInClassCompleted; }
            set
            {
                _totalInClassCompleted = value;
                OnPropertyChanged();
            }
        }

        public string TotalHomeworkAssigned
        {
            get { return _totalHomeworkAssigned; }
            set
            {
                _totalHomeworkAssigned = value;
                OnPropertyChanged();
            }
        }

        public string TotalHomeworkCompleted
        {
            get { return _totalHomeworkCompleted; }
            set
            {
                _totalHomeworkCompleted = value;
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

        public string TotalGiven
        {
            get { return _totalGiven; }
            set
            {
                _totalGiven = value;
                OnPropertyChanged();
            }
        }

        public string TotalFollowed
        {
            get { return _totalFollowed; }
            set
            {
                _totalFollowed = value;
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

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (SubmitButtonContent == "Add")
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
            else if(SubmitButtonContent == "Submit")
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

                if ((bool)sharedCheckBox.IsChecked)
                    form.Shared = true;
                else
                    form.Shared = false;

                //custom behavior form
                if (customFormName.Visibility == System.Windows.Visibility.Visible)
                    form.Data = FormName + ":?:" + BehaviorDescription + ":?:" + StudentNameText + ":?:" + StudentGradeText +
                        ":?:" + TeacherNameText + ":?:" + CompletedByNameText + ":?:" + DateCompletedText + ":?:" +
                        _behaviorScaleRating + ":?:" + TotalIncidents + ":?:" + IncidentTimes + ":?:" + CommentsSectionText;
                //progress report form
                else if (ProgressReportForm.Visibility == System.Windows.Visibility.Visible)
                {
                    _behaviorScaleRating = 1;
                    form.Data = StudentName + ":?:" + DateCompletedText + ":?:" + ProgressReportText + ":?:" + CommentsSectionText;
                }
                else
                {
                    //student information
                    form.Data = StudentNameText + ":?:" + StudentGradeText +
                        ":?:" + TeacherNameText + ":?:" + CompletedByNameText + ":?:" + DateCompletedText + ":?:" +
                        _behaviorScaleRating;

                    //follow directions form
                    if (followDirectionsForm.Visibility == System.Windows.Visibility.Visible)
                        form.Data += ":?:" + TotalGiven + ":?:" + TotalFollowed + ":?:" + CommentsSectionText;
                    //completing assignments form
                    else if (CompletingAssignmentsForm.Visibility == System.Windows.Visibility.Visible)
                        form.Data += ":?:" + TotalInClassAssigned + ":?:" + TotalInClassCompleted + ":?:" +
                            TotalHomeworkAssigned + ":?:" + TotalHomeworkCompleted + ":?:" + CommentsSectionText;
                    //arguing/talking back form
                    else if (ArguingTalkingBackForm.Visibility == System.Windows.Visibility.Visible)
                        form.Data += ":?:" + TotalArguments + ":?:" + ArgumentativeTimes + ":?:" + CommentsSectionText;
                    //talking out of turn form
                    else if (TalkingOutOfTurnForm.Visibility == System.Windows.Visibility.Visible)
                        form.Data += ":?:" + TotalTalkOutOfTurn + ":?:" + CommentsSectionText;
                    //inattentiveness form
                    else if (InattentivenessForm.Visibility == System.Windows.Visibility.Visible)
                        form.Data += ":?:" + TotalInattentive + ":?:" + InattentiveTimes + ":?:" + CommentsSectionText;
                    //incident form
                    else if (IncidentForm.Visibility == System.Windows.Visibility.Visible)
                    {
                        _behaviorScaleRating = 1;
                        form.Data += ":?:" + IncidentDate + ":?:" + IncidentTime + ":?:" + IncidentPeopleInvolved + ":?:" +
                            IncidentDescription + ":?:" + IncidentHandledDescription + ":?:" + CommentsSectionText;
                    }
                    //intervention form
                    else if (InterventionForm.Visibility == System.Windows.Visibility.Visible)
                    {
                        _behaviorScaleRating = 1;
                        form.Data += ":?:" + InterventionStartDate + ":?:" + InterventionEndDate + ":?:" + InterventionPeopleInvolved
                            + ":?:" + AddressedIssue + ":?:" + InterventionFreqTime + ":?:" + InterventionDescription + ":?:" +
                            InterventionObservedResults + ":?:" + InterventionModifications + ":?:" + CommentsSectionText;
                    }
                }
                if(StudentNameText == null || DateCompletedText == null ||
                    StudentNameText.Length < 1 || DateCompletedText.Length < 1 || _behaviorScaleRating < 1 ||
                    !proxy.SaveStudentForm(form))
                {
                    if (_behaviorScaleRating == 0)
                    {
                        MessagePopUp mess = new MessagePopUp("Invalid Form: Student Name, Date Completed, and Behavior Scale Ranking Required");
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
            TotalIncidents = null;
            IncidentTimes = null;
            TotalInattentive = null;
            InattentiveTimes = null;
            TotalTalkOutOfTurn = null;
            TotalArguments = null;
            ArgumentativeTimes = null;
            TotalInClassAssigned = null;
            TotalInClassCompleted = null;
            TotalHomeworkAssigned = null;
            TotalHomeworkCompleted = null;
            StudentNameText = null;
            StudentGradeText = null;
            TeacherNameText = null;
            CompletedByNameText = null;
            DateCompletedText = null;
            TotalGiven = null;
            TotalFollowed = null;
            CommentsSectionText = null;
        
            behaviorScale11.IsChecked = true;
            sharedCheckBox.IsChecked = false;
        }
    }
}
