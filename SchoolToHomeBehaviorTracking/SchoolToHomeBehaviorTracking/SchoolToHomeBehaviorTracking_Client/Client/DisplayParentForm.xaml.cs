using SchoolToHomeBehaviorTracking_Interface;
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
        private static string SEPERATOR = ":?:";

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

            FormName = "Home Tracking Form";
        }

        private void backButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _handleBackButtonClickMethod.DynamicInvoke(FormName);
        }

        public void ViewParentForm(StudentFormData form)
        {
            formScroller.ScrollToTop();

            string[] delimiter = new string[] { SEPERATOR };

            string[] data = (form.Data).Split(delimiter, StringSplitOptions.None);
            int counter = data.Length;

            if (counter != 0)
            {
                FormName = data[0];
                counter--;
            }
            if (counter != 0)
            {
                ChildNameText = data[1];
                counter--;
            }
            if (counter != 0)
            {
                CompletedByNameText = data[2];
                counter--;
            }
            if (counter != 0)
            {
                DateCompletedText = data[3];
                counter--;
            }
            if (counter != 0)
            {
                BehaviorScaleRating = Convert.ToInt32(data[4]);
                counter--;
            }
            if (counter != 0)
            {
                WakeTime = data[5];
                counter--;
            }
            if (counter != 0)
            {
                AsleepTime = data[6];
                counter--;
            }
            if (counter != 0)
            {
                Breakfast = data[7];
                counter--;
            }
            if (counter != 0)
            {
                Lunch = data[8];
                counter--;
            }
            if (counter != 0)
            {
                Dinner = data[9];
                counter--;
            }
            if (counter != 0)
            {
                Snacks = data[10];
                counter--;
            }
            if (counter != 0)
            {
                Mood = data[11];
                counter--;
            }
            if (counter != 0)
            {
                Consequences = data[12];
                counter--;
            }
            if (counter != 0)
            {
                CommentSectionText = data[13];
                counter--;
            }

            if(form.Shared)
                sharedCheckBox.IsChecked = true;

            FormName = form.FormName;
        }
    }
}
