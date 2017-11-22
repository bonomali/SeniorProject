using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for AddStudentPopUp.xaml
    /// </summary>
    public partial class AddStudentPopUp : Window
    {
        private string _msg;
        public AddStudentPopUp(string msg)
        {
            _msg = msg;
            InitializeComponent();
            this.DataContext = this;
        }

        public string Message
        {
            get { return _msg; }
            set { _msg = value; }
        }

        private void okayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
