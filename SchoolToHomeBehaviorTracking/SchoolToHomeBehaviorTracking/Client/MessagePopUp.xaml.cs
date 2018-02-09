using System.Windows;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for MessagePopUP.xaml
    /// Pop up window displaying customized message
    /// </summary>
    public partial class MessagePopUp : Window
    {
        private string _msg;

        public string Message
        {
            get { return _msg; }
            set { _msg = value; }
        }

        public MessagePopUp(string msg)
        {
            _msg = msg;
            InitializeComponent();
            this.DataContext = this;
        }

        private void okayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
