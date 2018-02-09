using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for AddStudentForm.xaml
    /// Add a student to teacher account
    /// </summary>
    public partial class AddStudentForm : INotifyPropertyChanged
    {
        private string _studFirstNameText;
        private string _studLastNameText;
        private string _studBirthDate;
        private string _studGradeText;
        private string _par1NameText;
        private string _par1PhoneText;
        private string _par1AddressText;
        private string _par2NameText;
        private string _par2PhoneText;
        private string _par2AddressText;
        private Delegate _delExitMethod;

        static ChannelFactory<IWCFService> channelFactory = new
              ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

        static IWCFService proxy = channelFactory.CreateChannel();

        public Delegate CallingExitMethod
        {
            set { _delExitMethod = value; }
        }

        public string StudFirstNameText
        {
            get { return _studFirstNameText; }
            set
            {
                _studFirstNameText = value;
                OnPropertyChanged();
            }
        }

        public string StudLastNameText
        {
            get { return _studLastNameText; }
            set
            {
                _studLastNameText = value;
                OnPropertyChanged();
            }
        }

        public string StudBirthDate
        {
            get { return _studBirthDate; }
            set
            {
                _studBirthDate = value;
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

        public string Par1NameText
        {
            get { return _par1NameText; }
            set
            {
                _par1NameText = value;
                OnPropertyChanged();
            }
        }

        public string Par1PhoneText
        {
            get { return _par1PhoneText; }
            set
            {
                _par1PhoneText = value;
                OnPropertyChanged();
            }
        }

        public string Par1AddressText
        {
            get { return _par1AddressText; }
            set
            {
                _par1AddressText = value;
                OnPropertyChanged();
            }
        }

        public string Par2NameText
        {
            get { return _par2NameText; }
            set
            {
                _par2NameText = value;
                OnPropertyChanged();
            }
        }

        public string Par2PhoneText
        {
            get { return _par2PhoneText; }
            set
            {
                _par2PhoneText = value;
                OnPropertyChanged();
            }
        }

        public string Par2AddressText
        {
            get { return _par2AddressText; }
            set
            {
                _par2AddressText = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddStudentForm()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudFirstNameText == "" || StudFirstNameText == null ||
                StudLastNameText == "" || StudLastNameText == null)
            {
                MessagePopUp pop = new MessagePopUp("Must Enter Student's First and Last Name");
                pop.Show();
            }
            else
            {
                StudentData student = new StudentData();

                student.FirstName = StudFirstNameText;
                student.LastName = StudLastNameText;
                student.BirthDate = StudBirthDate;
                student.Grade = StudGradeText;
                student.Parent1Name = Par1NameText;
                student.Parent1Phone = Par1PhoneText;
                student.Parent1Address = Par1AddressText;
                student.Parent2Name = Par2NameText;
                student.Parent2Phone = Par2PhoneText;
                student.Parent2Address = Par2AddressText;

                //add student, get teacher code for student
                int code = proxy.AddStudent(Email.EmailAddress, student);
                if (code != -1)
                {
                    MessagePopUp pop = new MessagePopUp(StudFirstNameText + " " + StudLastNameText + "'s Teacher Code is: " + code);
                    pop.Show();
                    ClearFields();
                }
                else
                {
                    MessagePopUp pop = new MessagePopUp("Duplicate/Invalid Student");
                    pop.Show();
                }
            }
        }

        private void ClearFields()
        {
            StudFirstNameText = null;
            StudLastNameText = null;
            StudBirthDate = null;
            StudGradeText = null;
            Par1NameText = null;
            Par1PhoneText = null;
            Par1AddressText = null;
            Par2NameText = null;
            Par2PhoneText = null;
            Par2AddressText = null;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            _delExitMethod.DynamicInvoke();
        }
    }
}
