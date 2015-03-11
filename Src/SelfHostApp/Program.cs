using MvcApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;

namespace SelfHostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new HttpSelfHostConfiguration("http://myhost:4400");

            WebApiConfig.Register(configuration);
            
            //
            // in cmd.exe is running as administrator it need to be invoked
            // netsh http add urlacl url=http://+:4400/ user=<user account>
            //
            RunHost(configuration);

            
        }

        private static void RunHost(HttpSelfHostConfiguration configuration) //async Task 
        {
            using (HttpSelfHostServer server = new HttpSelfHostServer(configuration))
            {
                server.OpenAsync().Wait();
               
                Console.WriteLine("Press Enter to terminate the server...");
                Console.ReadLine();
            }
        }
    }
}
