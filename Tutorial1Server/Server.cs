using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace Tutorial1Server
{
    internal class Server
    {

        static void Main(string[] args)
        {


            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(0.1);
            Console.WriteLine("Data Intermediary Server Initiated");
            
            //This is the actual host service system
            ServiceHost host;
            
            
            //This represents a tcp/ip binding in the Windows network stack
            NetTcpBinding tcp = new NetTcpBinding();
            
            
            //Bind server to the implementation of DataServer
            host = new ServiceHost(typeof(DataServer));
            
            
            //Present the publicly accessible interface to the client. 0.0.0.0 tells .net to
            //accept on any interface. :8100 means this will use port 8100. DataService is a name for the
           // actual service, this can be any string.

             host.AddServiceEndpoint(typeof(DataServerInterface), tcp,
            "net.tcp://0.0.0.0:8100/DataService");
            
            
            //And open the host for business!
            host.Open();
            Console.WriteLine("System Online Waiting for Execution");
            Console.ReadLine();
            
            
            //Closing the Server
            host.Close();


            var timer = new System.Threading.Timer((e) =>
            {
                Console.WriteLine("printing every"+periodTimeSpan);
            }, null, startTimeSpan, periodTimeSpan);



        }
       
}
}
