using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows.Input;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for ListForms.xaml
    /// //List of student, teacher, and parent forms
    /// </summary>
    public partial class ListForms : INotifyPropertyChanged
    {
        private string _formType;
        private string _studentName;
        private string _formListHeader;
        private string BEHAVIOR_CATEGORY = "Behavior";
        private string OTHER_CATEGORY = "Other";
        private string ALL_CATEGORY = "All Forms";

        private ObservableCollection<string> _forms;
        private ObservableCollection<string> _otherForms;

        private Delegate _delHandleFormClickedMethod;

        static ChannelFactory<IWCFService> channelFactory = new
        ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

        static IWCFService proxy = channelFactory.CreateChannel();

        public Delegate CallingFormClickedMethod
        {
            set { _delHandleFormClickedMethod = value; }
        }

        public ObservableCollection<string> Forms
        {
            get { return _forms; }
            set
            {
                _forms = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> OtherForms
        {
            get { return _otherForms; }
            set
            {
                _otherForms = value;
                OnPropertyChanged();
            }
        }

        public string FormType
        {
            get { return _formType; }
            set { _formType = value; }
        }

        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }

        public string FormListHeader
        {
            get { return _formListHeader; }
            set
            {
                _formListHeader = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler List_Changed;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnList_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Forms");
            OnPropertyChanged("OtherForms");
        }

        public ListForms()
        {
            InitializeComponent();
            this.DataContext = this;

            RefreshTeacherFormsList();
        }

        //get list of teacher forms from db and set list source
        public void RefreshTeacherFormsList()
        {
            formList.ItemsSource = null;
            otherFormsList.ItemsSource = null;

            _forms = new ObservableCollection<string>();
            _otherForms = new ObservableCollection<string>();

            List<string> list = proxy.GetTeacherForms(BEHAVIOR_CATEGORY);
            _forms.CollectionChanged += List_Changed;

            foreach (var f in list)
            {
                _forms.Add(f);
            }
            formList.ItemsSource = _forms;

            list.Clear();

            list = proxy.GetTeacherForms(OTHER_CATEGORY);
            _otherForms.CollectionChanged += List_Changed;

            foreach (var f in list)
            {
                _otherForms.Add(f);
            }
            otherFormsList.ItemsSource = _otherForms;

            behaviorForms.Visibility = System.Windows.Visibility.Visible;
            otherForms.Visibility = System.Windows.Visibility.Visible;
        }

        //get list of forms for student from db and set list source
        public void RefreshStudentForms()
        {
            formList.ItemsSource = null;
            otherFormsList.ItemsSource = null;

            _forms = new ObservableCollection<string>();
            _otherForms = new ObservableCollection<string>();

            if (_studentName != null)
            {
                string fname = _studentName.Split(' ')[1];
                string lname = _studentName.Split(',')[0];

                List<string> list = proxy.GetStudentForms(fname, lname, BEHAVIOR_CATEGORY);
                _forms.CollectionChanged += List_Changed;

                foreach (var f in list)
                {
                    _forms.Add(f);
                }
                formList.ItemsSource = _forms;

                list.Clear();

                list = proxy.GetStudentForms(fname, lname, OTHER_CATEGORY);
                _otherForms.CollectionChanged += List_Changed;

                foreach (var f in list)
                {
                    _otherForms.Add(f);
                }
                otherFormsList.ItemsSource = _otherForms;
            }
            behaviorForms.Visibility = System.Windows.Visibility.Visible;
            otherForms.Visibility = System.Windows.Visibility.Visible;
        }

        //Get list of daily behavior tracking forms
        public void RefreshStudentDailyForms()
        {
            formList.ItemsSource = null;
            otherFormsList.ItemsSource = null;

            _forms = new ObservableCollection<string>();
            _otherForms = new ObservableCollection<string>();

            formList.ItemsSource = _forms;

            if (_studentName != null)
            {
                string fname = _studentName.Split(' ')[1];
                string lname = _studentName.Split(',')[0];

                //get list of forms that need to be completed for the day (forms that haven't already
                //been completed on that day)
                List<string> list = proxy.GetStudentDailyForms(fname, lname);
                _forms.CollectionChanged += List_Changed;

                foreach (var f in list)
                {
                    _forms.Add(f);
                }
                formList.ItemsSource = _forms;

                list.Clear();

                list = proxy.GetStudentForms(fname, lname, OTHER_CATEGORY);
                _otherForms.CollectionChanged += List_Changed;

                foreach (var f in list)
                {
                    _otherForms.Add(f);
                }
                otherFormsList.ItemsSource = _otherForms;
            }
            behaviorForms.Visibility = System.Windows.Visibility.Visible;
            otherForms.Visibility = System.Windows.Visibility.Visible;
            if (_forms.Count == 0)
                noForms.Visibility = System.Windows.Visibility.Visible;
            if (_otherForms.Count == 0)
                noOtherForms.Visibility = System.Windows.Visibility.Visible;
        }

        //Get list of all student forms (parent and teacher)
        public void RefreshAllForms()
        {
            if (_studentName != null)
            {
                string fname = _studentName.Split(' ')[1];
                string lname = _studentName.Split(',')[0];
                formList.ItemsSource = null;

                _forms = new ObservableCollection<string>();

                //get list of all forms for student
                List<string> list = proxy.GetStudentForms(fname, lname, ALL_CATEGORY);
                _forms.CollectionChanged += List_Changed;

                foreach (var f in list)
                {
                    _forms.Add(f);
                }
                formList.ItemsSource = _forms;
                otherFormsList.ItemsSource = null;
            }
            behaviorForms.Visibility = System.Windows.Visibility.Visible;
            otherForms.Visibility = System.Windows.Visibility.Collapsed;
            noOtherForms.Visibility = System.Windows.Visibility.Collapsed;
            if (_forms.Count == 0)
                noForms.Visibility = System.Windows.Visibility.Visible;
        }

        //remove a tracking form for student
        public void RemoveForm()
        {
            if (_studentName != null)
            {
                string fname = _studentName.Split(' ')[1];
                string lname = _studentName.Split(',')[0];

                if (formList.SelectedItem != null)
                    proxy.RemoveForm(formList.SelectedItem.ToString(), fname, lname);
                else if (otherFormsList.SelectedItem != null)
                    proxy.RemoveForm(otherFormsList.SelectedItem.ToString(), fname, lname);
            }
        }

        private void formList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (formList.SelectedItem != null)
            {
                behaviorForms.Visibility = System.Windows.Visibility.Collapsed;
                otherForms.Visibility = System.Windows.Visibility.Collapsed;
                _delHandleFormClickedMethod.DynamicInvoke(formList.SelectedItem.ToString());
            }
        }

        private void otherFormsList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (otherFormsList.SelectedItem != null)
            {
                behaviorForms.Visibility = System.Windows.Visibility.Collapsed;
                otherForms.Visibility = System.Windows.Visibility.Collapsed;
                _delHandleFormClickedMethod.DynamicInvoke(otherFormsList.SelectedItem.ToString());
            }
        }
    }
}
