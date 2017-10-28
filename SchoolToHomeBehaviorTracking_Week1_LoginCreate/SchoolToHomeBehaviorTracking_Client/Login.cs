using SchoolToHomeBehaviorTracking_Client;
using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolToHomeBehaviorTracking_Client
{
    public partial class Login : System.Windows.Forms.Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChannelFactory<IWCFService> channelFactory = new
               ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            if (proxy.Login(textBox1.Text, textBox2.Text))
            {
                this.Hide();
                CreateAccount form = new CreateAccount();
                form.ShowDialog(); // Shows Form2
                this.Close();
            }
            else
                Console.WriteLine("Unsuccessful Login");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
