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
    /// Interaction logic for ListStudents.xaml
    /// </summary>
    public partial class ListStudents : INotifyPropertyChanged
    {
        private Delegate _delHandleClickMethod;

        public Delegate CallingDeleteStudentMethod
        {
            set { _delHandleClickMethod = value; }
        }

        private ObservableCollection<string> _studentNames;

        public ObservableCollection<string> StudentNames
        {
            get { return _studentNames; }
            set
            {
                _studentNames = value;
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
            OnPropertyChanged("StudentNames");
        }

        public ListStudents()
        {
            InitializeComponent();
            this.DataContext = this;

            RefreshList();
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _delHandleClickMethod.DynamicInvoke(studentList.SelectedItem.ToString());
        }

        //get list of students from db and set list source
        public void RefreshList()
        {
            studentList.ItemsSource = null;

            _studentNames = new ObservableCollection<string>();

            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            List<string> list = proxy.ListStudents(Email.EmailAddress);
            _studentNames.CollectionChanged += List_Changed;

            foreach (var s in list)
            {
                _studentNames.Add(s);
            }
            studentList.ItemsSource = _studentNames;
        }
    }
}
