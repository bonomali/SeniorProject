using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ParentDash.xaml
    /// </summary>
    public partial class ParentDash : UserControl
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

        public ParentDash(string email)
        {
            _email = email;
            InitializeComponent();
        }
    }
}
