using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for DisplayParentForm.xaml
    /// Display parent submitted forms in non-edit mode
    /// </summary>
    public partial class DisplayParentForm : INotifyPropertyChanged
    {
        private string _formName;
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

        private Delegate _handleBackButtonClickMethod;

        public Delegate CallingBackButtonClickMethod
        {
            set { _handleBackButtonClickMethod = value; }
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

        public DisplayParentForm()
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
