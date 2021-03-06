﻿using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows.Input;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for MuliColumnFormsList.xaml
    /// Multi column list for displaying form Archive
    /// </summary>
    public partial class MuliColumnFormsList : INotifyPropertyChanged
    {
        private string _studentName;
        private string _formListHeader;
        private string _formName;

        private List<StudentFormData> _forms;

        private Delegate _delHandleFormClickedMethod;
        private Delegate _delHandleArchiveFormClickedMethod;
        private Delegate _delHandleBackButtonClickedMethod;

        static ChannelFactory<IMetaInterface> channelFactory = new
        ChannelFactory<IMetaInterface>("SchoolToHomeServiceEndpoint");

        static IMetaInterface proxy = channelFactory.CreateChannel();

        public Delegate CallingFormClickedMethod
        {
            set { _delHandleFormClickedMethod = value; }
        }

        public Delegate CallingArchiveFormClickedMethod
        {
            set { _delHandleArchiveFormClickedMethod = value; }
        }

        public Delegate CallingBackButtonClickedMethod
        {
            set { _delHandleBackButtonClickedMethod = value; }
        }

        public List<StudentFormData> formArchive
        {
            get { return _forms; }
            set
            {
                _forms = value;
                OnPropertyChanged();
            }
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MuliColumnFormsList()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        //get list of forms for student by type for teacher viewing
        public void RefreshStudentformArchive(string formName)
        {
            forms.ScrollToTop();

            _formName = formName;
            formArc.ItemsSource = null;

            _forms = new List<StudentFormData>();

            if (_studentName != null)
            {
                string fname = _studentName.Split(' ')[1];
                string lname = _studentName.Split(',')[0];

                //get all forms by type
                List<StudentFormData> list = proxy.GetStudentFormsListByType(formName, fname, lname);

                foreach (var f in list)
                {
                    _forms.Add(new StudentFormData() { FormName = f.FormName, FormDate = f.FormDate, EndDate = f.EndDate, FormID = f.FormID });
                }
                formArc.ItemsSource = _forms;

                if (_forms.Count == 0)
                { 
                    noFormsMsg.Visibility = System.Windows.Visibility.Visible;
                    formHeader.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    noFormsMsg.Visibility = System.Windows.Visibility.Collapsed;
                    formHeader.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        //get list of forms for child by type for parent viewing
        public void RefreshChildformArchive(string formName)
        {
            forms.ScrollToTop();

            _formName = formName;
            formArc.ItemsSource = null;

            _forms = new List<StudentFormData>();

            if (_studentName != null)
            {
                string fname = _studentName.Split(' ')[1];
                string lname = _studentName.Split(',')[0];

                //get all forms by type
                List<StudentFormData> list = proxy.GetChildFormsListByType(formName, fname, lname);

                foreach (var f in list)
                {
                    _forms.Add(new StudentFormData() { FormName = f.FormName, FormDate = f.FormDate, FormID = f.FormID });
                }
                formArc.ItemsSource = _forms;

                if (_forms.Count == 0)
                {
                    noFormsMsg.Visibility = System.Windows.Visibility.Visible;
                    formHeader.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    noFormsMsg.Visibility = System.Windows.Visibility.Collapsed;
                    formHeader.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void formArchive_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var row = (StudentFormData)formArc.SelectedItem;
            if (row != null)
                _delHandleArchiveFormClickedMethod.DynamicInvoke(row.FormID);
        }

        private void backButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _delHandleBackButtonClickedMethod.DynamicInvoke(StudentName);
        }
    }
}
