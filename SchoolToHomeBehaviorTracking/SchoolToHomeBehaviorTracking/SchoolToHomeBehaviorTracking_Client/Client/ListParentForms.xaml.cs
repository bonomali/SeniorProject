using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for ListParentForms.xaml
    /// </summary>
    public partial class ListParentForms : INotifyPropertyChanged
    {
        private string _formType;
        private string _childName;
        private string _formListHeader;
        private static string PROGRESSREPORTFORM = "Progress Report Form";
        private static string HOMETRACKINGFORM = "Home Tracking Form";

        private ObservableCollection<string> _forms;

        private Delegate _delHandleFormClickedMethod;

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

        public string ChildName
        {
            get { return _childName; }
            set { _childName = value; }
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
        }
        public ListParentForms()
        {
            InitializeComponent();
            this.DataContext = this;

            RefreshParentViewableForms();
        }

        //get list of names of forms parent may view
        public void RefreshParentViewableForms()
        {
            forms.ScrollToTop();
            formList.ItemsSource = null;

            _forms = new ObservableCollection<string>();

            _forms.Add(PROGRESSREPORTFORM);
            _forms.Add(HOMETRACKINGFORM);
            _forms.CollectionChanged += List_Changed;

            formList.ItemsSource = _forms;
            trackingForms.Visibility = System.Windows.Visibility.Visible;
        }

        private void formList_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (formList.SelectedItem != null)
            {
                trackingForms.Visibility = System.Windows.Visibility.Collapsed;
                _delHandleFormClickedMethod.DynamicInvoke(formList.SelectedItem.ToString());
            }
        }
    }
}
