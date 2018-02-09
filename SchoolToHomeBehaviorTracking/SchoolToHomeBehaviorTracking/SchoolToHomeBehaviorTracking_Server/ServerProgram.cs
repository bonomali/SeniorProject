using System;
using System.ServiceModel;

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
