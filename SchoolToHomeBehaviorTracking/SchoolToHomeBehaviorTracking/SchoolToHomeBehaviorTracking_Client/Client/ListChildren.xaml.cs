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
    /// Interaction logic for ListChildren.xaml
    /// List of children for parent account
    /// </summary>
    public partial class ListChildren : INotifyPropertyChanged
    {
        private ObservableCollection<string> _childrenNames;
        private Delegate _delHandleClickMethod;

        static ChannelFactory<IMetaInterface> channelFactory = new
        ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");

        static IMetaInterface proxy = channelFactory.CreateChannel();

        public Delegate CallingClickChildMethod
        {
            set { _delHandleClickMethod = value; }
        }

        public ObservableCollection<string> ChildrenNames
        {
            get { return _childrenNames; }
            set
            {
                _childrenNames = value;
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
            OnPropertyChanged("ChildrenNames");
        }

        public ListChildren()
        {
            InitializeComponent();
            this.DataContext = this;

            RefreshList();
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (childrenList.SelectedItem != null)
                _delHandleClickMethod.DynamicInvoke(childrenList.SelectedItem.ToString());
        }

        //get list of children from db and set list source
        public void RefreshList()
        {
            listChildren.ScrollToTop();

            childrenList.ItemsSource = null;

            _childrenNames = new ObservableCollection<string>();

            List<string> list = proxy.ListChildren(Email.EmailAddress);
            _childrenNames.CollectionChanged += List_Changed;

            foreach (var s in list)
            {
                _childrenNames.Add(s);
            }
            childrenList.ItemsSource = _childrenNames;
        }
    }
}