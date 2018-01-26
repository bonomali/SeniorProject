using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for MessagePopUP.xaml
    /// </summary>
    public partial class MessagePopUp : Window
    {
        private string _msg;
        public MessagePopUp(string msg)
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
