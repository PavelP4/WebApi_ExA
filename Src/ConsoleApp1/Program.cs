using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace ConsoleApp1
{
    class Program
    {
        private const int buffer_size = 100;

        static void Main(string[] args)
        {
            string URI = "http://myhost:5500/api/V2/auto/getauto";
            //string URI = "http://localhost:55001/api/V2/auto/getauto";

            //ShowRequestResult(URI);
            //ShowRequestResult_WebClient(URI);

            RunClient();
            Console.WriteLine("Press ENTER to Close");

            Console.ReadLine();
        }

        static async void RunClient()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://myhost:5500/api/V2/push", HttpCompletionOption.ResponseHeadersRead);

            using (Stream stream = await response.Content.ReadAsStreamAsync())
            {
                byte[] buffer = new byte[512];
                int bytesRead = 0;
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string content = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(content);
                }
            }
        }


        public static async Task ShowRequestResult(string URI)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");//, gzip;q=0.2
                

                //string result = await client.GetStringAsync(URI);                
                //Console.WriteLine("Result = {0}", result);
                
                HttpResponseMessage response = await client.GetAsync(URI);
                
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Result ({1}) = {0}", result, result.Length);
                
                foreach(var item in response.Content.Headers.ContentEncoding)
                {
                    Console.WriteLine("ContentEncoding = {0}", item);

                    if (item.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        using (MemoryStream ms = new MemoryStream()) //System.Text.Encoding.UTF8.GetBytes(result)
                        {
                            await response.Content.CopyToAsync(ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            //using (StreamReader str = new StreamReader(ms))
                            //{
                            //    Console.WriteLine("str = {0}", str.ReadToEnd());   
                            //}
                                using (MemoryStream msD = new MemoryStream())
                                using (GZipStream zipStream = new GZipStream(ms, CompressionMode.Decompress))
                                {

                                    //Console.WriteLine("CanSeek = {0}", zipStream.CanSeek);
                                    byte[] decompressedBuffer = new byte[ms.Length + buffer_size];

                                    try
                                    {
                                        //int totalCount = ReadAllBytesFromStream(zipStream, decompressedBuffer);
                                        //Console.WriteLine("Decompressed {0} bytes", totalCount);
                                        //Console.WriteLine("decompressedBuffer: {0} ", System.Text.Encoding.UTF8.GetString(decompressedBuffer));

                                        zipStream.CopyTo(msD);
                                        Console.WriteLine("decompressedBuffer: {0} ", System.Text.Encoding.UTF8.GetString(msD.ToArray()));

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error: {0}", ex.Message);
                                    }                                                

                               
                                }
                         }                        
                    }
                }



                //using (FileStream fs = File.Create(@"D:\encodedStream.txt"))
                //{
                //    await response.Content.CopyToAsync(fs);

                //}
            }
        }

        public static int ReadAllBytesFromStream(Stream stream, byte[] buffer)
        {
            // Use this method is used to read all bytes from a stream.
            int offset = 0;
            int totalCount = 0;
            while (true)
            {
                int bytesRead = stream.Read(buffer, offset, buffer_size);
                if (bytesRead == 0)
                {
                    break;
                }
                offset += bytesRead;
                totalCount += bytesRead;
            }
            return totalCount;
        }

        public static void ShowRequestResult_WebClient(string URI)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Clear();
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Accept-Encoding", "gzip, deflate;q=0.8");
                var result = client.DownloadString(URI);
                
                Console.WriteLine("Result (WebClient) = {0}", result);
            }
        }
    }
}
