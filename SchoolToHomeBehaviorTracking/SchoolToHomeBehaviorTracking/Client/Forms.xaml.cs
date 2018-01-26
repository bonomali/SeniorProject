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

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for Forms.xaml
    /// </summary>
    public partial class Forms : INotifyPropertyChanged
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
        private Delegate _delExitPreviewFormMethod;
        private int _behaviorScaleRating;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Delegate CallingExitPreviewForm
        {
            set { _delExitPreviewFormMethod = value; }
        }

        public Forms()
        {
            InitializeComponent();
            this.DataContext = this;
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

        public string StudNameText
        {
            get { return _studNameText; }
            set
            {
                _studNameText = value;
                OnPropertyChanged();
            }
        }

        public string StudGradeText
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

        public string DateCompletedNameText
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
                    if (proxy.AddFormToStudent(FormName, fname, lname))
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
                _delExitPreviewFormMethod.DynamicInvoke();
            }
            
        }

        private void ClearFields()
        {
            StudNameText = "";
            TeacherNameText = "";
            StudGradeText = "";
            DateCompletedNameText = "";
            TotalGiven = "";
            TotalFollowed = "";
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

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            _delExitPreviewFormMethod.DynamicInvoke();
        }
    }
}
