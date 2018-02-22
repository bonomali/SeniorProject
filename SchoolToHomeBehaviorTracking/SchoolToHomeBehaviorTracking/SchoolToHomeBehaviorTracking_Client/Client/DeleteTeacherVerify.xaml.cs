using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for DeleteTeacherVerify.xaml
    /// Confirm and handle the deleting of a teacher account
    /// </summary>
    public partial class DeleteTeacherVerify : Window
    {
        AdminDash _dash = null;
        string _deleteName;

        public string DeleteName
        {
            get { return _deleteName; }
            set { _deleteName = value; }
        }

        public DeleteTeacherVerify(AdminDash dash, string name)
        {
            _dash = dash;
            InitializeComponent();
            this.DataContext = this;
            DeleteName = name;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            _dash.RemoveTeacher();
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
