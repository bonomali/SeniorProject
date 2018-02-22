using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for ParentForms.xaml
    /// Parent home tracking forms
    /// </summary>
    public partial class ParentForms : INotifyPropertyChanged
    {
        private string _formName;
        private string _childName;
        private string _childNameText;
        private string _completedByText;
        private string _dateCompletedText;
        private string _wakeTime;
        private string _asleepTime;
        private string _breakfast;
        private string _lunch;
        private string _dinner;
        private string _snacks;
        private string _mood;
        private string _consequences;
        private string _commentSection;
        private int _behaviorScaleRating;

        private static string FORMNAME = "Home Tracking Form";
        private static string SEPERATOR = ":?:";
        private static string DATEFORMAT = "MM/dd/yyyy";

        private Delegate _delExitCompleteChildFormMethod;

        static ChannelFactory<IMetaInterface> channelFactory = new
        ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");

        static IMetaInterface proxy = channelFactory.CreateChannel();

        public Delegate CallingExitCompletedForm
        {
            get { return _delExitCompleteChildFormMethod; }
            set { _delExitCompleteChildFormMethod = value; }
        }

        public string ChildName
        {
            get { return _childName; }
            set { _childName = value; }
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

        public string ChildNameText
        {
            get { return _childNameText; }
            set
            {
                _childNameText = value;
                OnPropertyChanged();
            }
        }

        public string CompletedByNameText
        {
            get { return _completedByText; }
            set
            {
                _completedByText = value;
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

        public string WakeTime
        {
            get { return _wakeTime; }
            set
            {
                _wakeTime = value;
                OnPropertyChanged();
            }
        }

        public string AsleepTime
        {
            get { return _asleepTime; }
            set
            {
                _asleepTime = value;
                OnPropertyChanged();
            }
        }

        public string Breakfast
        {
            get { return _breakfast; }
            set
            {
                _breakfast = value;
                OnPropertyChanged();
            }
        }

        public string Lunch
        {
            get { return _lunch; }
            set
            {
                _lunch = value;
                OnPropertyChanged();
            }
        }

        public string Dinner
        {
            get { return _dinner; }
            set
            {
                _dinner = value;
                OnPropertyChanged();
            }
        }

        public string Snacks
        {
            get { return _snacks; }
            set
            {
                _snacks = value;
                OnPropertyChanged();
            }
        }

        public string Mood
        {
            get { return _mood; }
            set
            {
                _mood = value;
                OnPropertyChanged();
            }
        }

        public string Consequences
        {
            get { return _consequences; }
            set
            {
                _consequences = value;
                OnPropertyChanged();
            }
        }
        
        public string CommentSectionText
        {
            get { return _commentSection; }
            set
            {
                _commentSection = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ParentForms()
        {
            InitializeComponent();
            this.DataContext = this;

            FormName = "Home Tracking Form";
        }

        private void submitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StudentFormData form = new StudentFormData();
            string wakePeriod = null;
            string sleepPeriod = null;

            //check that time in correct format
            try
            {
                wakePeriod = WakeTime + " " + wakeTimePeriod.Text;
                DateTime time = Convert.ToDateTime(wakePeriod);
                wakePeriod = time.ToString("hh:mm tt");
                sleepPeriod = AsleepTime + sleepTimePeriod.Text;
                time = Convert.ToDateTime(sleepPeriod);
                sleepPeriod = time.ToString("hh:mm tt");
            }
            catch
            {
                invalidTimeMess.Visibility = System.Windows.Visibility.Visible;
                return;
            }

            if (ChildName != null)
            {
                form.StudentFName = ChildName.Split(' ')[1];
                form.StudentLName = ChildName.Split(',')[0];
            }

            form.FormName = FORMNAME;
            form.FormDate = DateCompletedText;
            form.EndDate = null;
            form.BehaviorRating = _behaviorScaleRating;

            if ((bool)sharedCheckBox.IsChecked)
                form.Shared = true;
            else
                form.Shared = false;

            
            form.Data = FORMNAME + SEPERATOR + ChildNameText + SEPERATOR + CompletedByNameText + SEPERATOR + DateCompletedText +
                SEPERATOR + _behaviorScaleRating + SEPERATOR + wakePeriod + SEPERATOR + sleepPeriod + SEPERATOR + Breakfast +
                SEPERATOR + Lunch + SEPERATOR + Dinner + SEPERATOR + Snacks + SEPERATOR + Mood + SEPERATOR + Consequences +
                SEPERATOR + CommentSectionText;

            //check if form is invalid
            if (ChildNameText == null || DateCompletedText == null || ChildNameText.Length < 1 ||
                    DateCompletedText.Length < 1 || _behaviorScaleRating < 1 || !proxy.SaveStudentForm(form))
            {
                MessagePopUp mess = new MessagePopUp("Invalid Form: Child Name, Date Completed, and Behavior Scale Rating Required");
                mess.Show();   
            }
            else
            {
                MessagePopUp mess = new MessagePopUp("Form Successfully Saved");
                mess.Show();
                ClearFields();
                _delExitCompleteChildFormMethod.DynamicInvoke();
            }
        }

        private void cancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ClearFields();
            _delExitCompleteChildFormMethod.DynamicInvoke();
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

        public void ClearFields()
        {
            ChildNameText = null;
            CompletedByNameText = null;
            DateCompletedText = DateTime.Now.ToString(DATEFORMAT);
            WakeTime = null;
            AsleepTime = null;
            Breakfast = null;
            Lunch = null;
            Dinner = null;
            Snacks = null;
            Mood = null;
            Consequences = null;
            CommentSectionText = null;
          
            behaviorScale11.IsChecked = true;
            sharedCheckBox.IsChecked = false;

            invalidTimeMess.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
