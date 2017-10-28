using SchoolToHomeBehaviorTracking_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolToHomeBehaviorTracking_Client
{
    class ClientProgram
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());

            ChannelFactory<IWCFService> channelFactory = new
                ChannelFactory<IWCFService>("SchoolToHomeServiceEndpoint");

            IWCFService proxy = channelFactory.CreateChannel();

            //calls the server
            List<string> students = proxy.ListStudents();

            foreach (var s in students)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("Select Student");
            string lname = Console.ReadLine();

            StudentData student = proxy.GetStudent(lname);

            Console.WriteLine(student.FirstName);
            Console.WriteLine(student.LastName);
            Console.WriteLine(student.BirthDate);
            Console.WriteLine(student.Grade);
            Console.ReadLine();
        }
    }
}
