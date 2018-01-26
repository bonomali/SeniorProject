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
    /// Interaction logic for EditStudentForm.xaml
    /// </summary>
    public partial class EditStudentForm : UserControl
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
        private string _teacherCode;
        private string _studentFullName;
        private Delegate _delRefreshListMethod;

        public Delegate CallingRefreshListMethod
        {
            set { _delRefreshListMethod = value; }
        }

        public EditStudentForm()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public string TeacherCode
        {
            get { return _teacherCode; }
            set
            {
                _teacherCode = value;
                OnPropertyChanged();
            }
        }

        public string StudentFullName
        {
            get { return _studentFullName; }
            set { _studentFullName = value; }
        }
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
            ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            StudentData student = new StudentData();
            student.FirstName = studFirstNameText.Text;
            student.LastName = studLastNameText.Text;
            student.BirthDate = studBirthDateText.Text;
            student.Grade = studGradeText.Text;
            student.Parent1Name = par1NameText.Text;
            student.Parent1Phone = par1PhoneText.Text;
            student.Parent1Address = par1AddressText.Text;
            student.Parent2Name = par2NameText.Text;
            student.Parent2Phone = par2PhoneText.Text;
            student.Parent2Address = par2AddressText.Text;
            if(_teacherCode != null)
                student.TeacherCode = Convert.ToInt32(_teacherCode.Split(' ')[2]);

            if (_studentFullName != null)
            {
                string lname = _studentFullName.Split(',')[0];
                string fname = _studentFullName.Split(' ')[1];

                if (proxy.UpdateStudent(Email.EmailAddress, fname, lname, student))
                {
                    MessagePopUp pop = new MessagePopUp(_studFirstNameText + " " + _studLastNameText + " Successfully Updated");
                    pop.Show();
                    _delRefreshListMethod.DynamicInvoke();
                }
                else
                {
                    MessagePopUp pop = new MessagePopUp("Duplicate/Invalid Student");
                    pop.Show();
                }
            }
        }
        public void GetStudentInfo()
        {
            if (_studentFullName != null)
            {
                string lname = _studentFullName.Split(',')[0];
                string fname = _studentFullName.Split(' ')[1];

                ChannelFactory<IWCFService> channelFactory = new
                   ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

                IWCFService proxy = channelFactory.CreateChannel();

                StudentData stud = proxy.GetStudent(fname, lname);

                studFirstNameText.Text = stud.FirstName;
                studLastNameText.Text = stud.LastName;
                studBirthDateText.Text = stud.BirthDate;
                studGradeText.Text = stud.Grade;
                par1NameText.Text = stud.Parent1Name;
                par1PhoneText.Text = stud.Parent1Phone;
                par1AddressText.Text = stud.Parent1Address;
                par2NameText.Text = stud.Parent2Name;
                par2PhoneText.Text = stud.Parent2Phone;
                par2AddressText.Text = stud.Parent2Address;
                teacherCode.Text = "Teacher Code: " + stud.TeacherCode.ToString();
            }
        }
    }
}

