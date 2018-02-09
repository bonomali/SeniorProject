using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for DeleteStudentVerify.xaml
    /// Confirm and handle deleting a student from teacher account
    /// </summary>
    public partial class DeleteStudentVerify : Window
    {
        TeacherDash _dash = null;
        string _deleteName;

        public string DeleteName
        {
            get { return _deleteName; }
            set { _deleteName = value; }
        }

        public DeleteStudentVerify(TeacherDash dash, string name)
        {
            _dash = dash;
            InitializeComponent();
            this.DataContext = this;
            DeleteName = name;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            _dash.DeleteStudent(DeleteName);
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
