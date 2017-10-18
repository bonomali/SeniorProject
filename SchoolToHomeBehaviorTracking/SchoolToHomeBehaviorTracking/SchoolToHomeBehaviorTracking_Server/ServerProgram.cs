using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SchoolToHomeBehaviorTracking_Server
{
    class ServerProgram
    {
        static void Main(string[] args)
        {
            using(ServiceHost host = new ServiceHost(typeof(WCFService)))
            {
                host.Open();
                Console.WriteLine("Server is open");
                Console.WriteLine("<Press enter to close server>");
                Console.ReadLine();
            }
        }
    }
}
