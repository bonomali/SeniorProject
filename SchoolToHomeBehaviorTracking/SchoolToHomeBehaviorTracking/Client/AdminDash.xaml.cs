using System;

namespace SchoolToHomeBehaviorTracking_Client
{
    /// <summary>
    /// Interaction logic for AdminDash.xaml
    /// </summary>
    public partial class AdminDash 
    {
        private System.Delegate _delCreateMethod;
        private System.Delegate _delLogoutMethod;
        private string _email = null;

        public Delegate CallingCreateMethod
        {
            set { _delCreateMethod = value; }
        }

        public Delegate CallingLogoutMethod
        {
            set { _delLogoutMethod = value; }
        }

        public AdminDash(string email)
        {
            _email = email;
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
