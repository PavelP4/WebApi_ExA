using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MvcApp1.Controllers.V2
{
    public class TestController : ApiController
    {
        private const int delay = 500;

        public string GetString(int id)
        {      
            using (StreamReader reader = File.OpenText(@"d:\Temp\testFile.log"))
            {
                Thread.Sleep(delay);

                return reader.ReadToEnd();
            }
        }

        public async Task<string> GetString()
        {
            return await ReadFileAsync();
        }

        private async Task<string> ReadFileAsync()
        {
            using (StreamReader reader = File.OpenText(@"d:\Temp\testFile.log"))
            {
                await Task.Delay(delay);

                return await reader.ReadToEndAsync();
            }        
        }

    }
}
