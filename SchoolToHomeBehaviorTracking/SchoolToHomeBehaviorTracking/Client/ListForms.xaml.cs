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
    /// </summary>
    public partial class ListForms : INotifyPropertyChanged
    {
        private Delegate _delHandleFormClickedMethod;
        private string _formType;
        private string _studentName;
        private string _formListHeader;

        public Delegate CallingDeleteFormClickedMethod
        {
            set { _delHandleFormClickedMethod = value; }
        }

        private ObservableCollection<string> _forms;
        private ObservableCollection<string> _otherForms;

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

            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            string cat1 = "Behavior";
            string cat2 = "Other";

            List<string> list = proxy.GetTeacherForms(cat1);
            _forms.CollectionChanged += List_Changed;

            foreach (var f in list)
            {
                _forms.Add(f);
            }
            formList.ItemsSource = _forms;

            list.Clear();

            list = proxy.GetTeacherForms(cat2);
            _otherForms.CollectionChanged += List_Changed;

            foreach (var f in list)
            {
                _otherForms.Add(f);
            }
            otherFormsList.ItemsSource = _otherForms;

            behaviorForms.Visibility = System.Windows.Visibility.Visible;
            otherForms.Visibility = System.Windows.Visibility.Visible;
        }

        //get list of parent forms from db and set list source
        public void RefreshParentFormsList()
        {
            formList.ItemsSource = null;

            _forms = new ObservableCollection<string>();

            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            List<string> list = proxy.GetParentForms();
            _forms.CollectionChanged += List_Changed;

            foreach (var f in list)
            {
                _forms.Add(f);
            }
            formList.ItemsSource = _forms;
        }

        //get list of forms for student from db and set list source
        public void RefreshStudentForms()
        {
            formList.ItemsSource = null;
            otherFormsList.ItemsSource = null;

            _forms = new ObservableCollection<string>();
            _otherForms = new ObservableCollection<string>();

            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            string cat1 = "Behavior";
            string cat2 = "Other";

            if (_studentName != null)
            {
                string fname = _studentName.Split(' ')[1];
                string lname = _studentName.Split(',')[0];

                List<string> list = proxy.GetStudentForms(fname, lname, cat1);
                _forms.CollectionChanged += List_Changed;

                foreach (var f in list)
                {
                    _forms.Add(f);
                }
                formList.ItemsSource = _forms;

                list.Clear();

                list = proxy.GetStudentForms(fname, lname, cat2);
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

        //Get list of daily tracking forms
        public void RefreshStudentDailyForms()
        {
            formList.ItemsSource = null;
            otherFormsList.ItemsSource = null;

            _forms = new ObservableCollection<string>();
            _otherForms = new ObservableCollection<string>();

            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (_studentName != null)
            {
                string fname = _studentName.Split(' ')[1];
                string lname = _studentName.Split(',')[0];
                string cat2 = "Other";

                try
                {
                    List<string> list = proxy.GetStudentDailyForms(fname, lname);
                    _forms.CollectionChanged += List_Changed;
                    foreach (var f in list)
                    {
                        _forms.Add(f);
                    }
                    formList.ItemsSource = _forms;

                    list.Clear();

                    list = proxy.GetStudentForms(fname, lname, cat2);
                    _otherForms.CollectionChanged += List_Changed;

                    foreach (var f in list)
                    {
                        _otherForms.Add(f);
                    }
                    otherFormsList.ItemsSource = _otherForms;
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                    {
                        string err = e.InnerException.Message;
                    }
                }
            }
            behaviorForms.Visibility = System.Windows.Visibility.Visible;
            otherForms.Visibility = System.Windows.Visibility.Visible;
        }

        //remove a tracking form for student
        public void RemoveForm()
        {
            ChannelFactory<IWCFService> channelFactory = new
           ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (_studentName != null)
            {
                string fname = _studentName.Split(' ')[1];
                string lname = _studentName.Split(',')[0];
                bool success = false;

                if (formList.SelectedItem != null)
                    success = proxy.RemoveForm(formList.SelectedItem.ToString(), fname, lname);
                else if (otherFormsList.SelectedItem != null)
                    success = proxy.RemoveForm(otherFormsList.SelectedItem.ToString(), fname, lname);

                if (success)
                {
                    MessagePopUp mess = new MessagePopUp("Form deleted");
                    mess.Show();
                }
                else
                {
                    MessagePopUp mess = new MessagePopUp("Error deleting form");
                    mess.Show();
                }
            }
        }

        private void formList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            behaviorForms.Visibility = System.Windows.Visibility.Collapsed;
            otherForms.Visibility = System.Windows.Visibility.Collapsed;
            _delHandleFormClickedMethod.DynamicInvoke(formList.SelectedItem.ToString());
        }

        private void otherFormsList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            behaviorForms.Visibility = System.Windows.Visibility.Collapsed;
            otherForms.Visibility = System.Windows.Visibility.Collapsed;
            _delHandleFormClickedMethod.DynamicInvoke(otherFormsList.SelectedItem.ToString());
        }
    }
}
