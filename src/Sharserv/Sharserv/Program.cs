using Sharserv.Request;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Sharserv
{
    class Program
    {
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

            s.Bind(endpoint);
            s.Listen(100);

            return s;
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
                handler.Close();
            }
            catch (Exception e)
            {

            }
            finally
            {
                handler.Close();
            }
        }
    }
}