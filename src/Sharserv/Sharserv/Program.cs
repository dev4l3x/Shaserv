using Sharserv.Request;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Sharserv.Files;
using Sharserv.Settings;

namespace Sharserv
{
    class Program
    {

        public readonly static string defaultSettings = "{ \n" +
            "\t\"launch_port\": 3000,\n" +
            "\t\"mime_types\": { \n" +
                "\t\t\"application/javascript\": [ \"js\", \"jsonp\" ], \n" +
                "\t\t\"application/json\": [ \"json\" ], \n" +
                "\t\t\"text/html\": [ \"html\", \"htm\" ], \n" +
                "\t\t\"text/plain\": [ \"txt\" ], \n" +
                "\t\t\"application/pdf\": [ \"pdf\" ] \n" +
            "\t} \n" +
        "}";

        static void Main(string[] args)
        {
            Socket s = StartServer();
            Console.WriteLine("Started server");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Listening for request on port 3000");
            Console.ResetColor();
            while (true)
            {
                var handler = s.Accept();
                ThreadPool.QueueUserWorkItem(state => ProccessRequest(handler));
            }

            s.Close();
        }

        static Socket StartServer()
        {
            var s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var host = Dns.GetHostAddresses("localhost");
            var ip = host[1];
            var endpoint = new IPEndPoint(ip, 3000);
            CreateFolders();
            s.Bind(endpoint);
            s.Listen(100);

            return s;
        }

        private static void CreateFolders()
        {
            if (!Directory.Exists(PathManager.GetServerDocumentsFolder()))
            {
                Directory.CreateDirectory(PathManager.GetServerDocumentsFolder());
                Directory.CreateDirectory(PathManager.GetPublicFolderPath());

                var stream = System.IO.File.Create(PathManager.GetSettingsFilePath());
                var encoded = Encoding.ASCII.GetBytes(defaultSettings);
                stream.Write(encoded);
                stream.Close();
            }
        }

        static void ProccessRequest(Socket handler) {

            try
            {
                var buffer = new byte[handler.ReceiveBufferSize];
                handler.Receive(buffer);
                var da = Encoding.ASCII.GetString(buffer);
                HttpRequest request = HttpRequest.FromString(da);
                Console.WriteLine($"{request.Method} {request.RequestedResource}");
                var response = RequestHandlerFactory.GetHandlerForRequest(request).Handle(request);
                var responseString = response.ToString();
                var enc = Encoding.ASCII.GetBytes(responseString);
                handler.Send(enc);
            }
            catch (Exception e)
            {
                var response = ExceptionFactory.GetResponse(e);
                var responseString = response.ToString();
                var encoded = Encoding.ASCII.GetBytes(responseString);
                handler.Send(encoded);
            }
            finally
            {
                handler.Close();
            }
        }
    }
}