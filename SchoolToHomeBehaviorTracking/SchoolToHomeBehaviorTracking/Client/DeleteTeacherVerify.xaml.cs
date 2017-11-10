using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for DeleteTeacherVerify.xaml
    /// </summary>
    public partial class DeleteTeacherVerify : Window
    {
        AdminDash _dash = null;
        string _name = null;
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
