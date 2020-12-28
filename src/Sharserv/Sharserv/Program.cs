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
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var host = Dns.GetHostAddresses("localhost");
            var ip = host[0];
            var endpoint = new IPEndPoint(ip, 3000);

            //ThreadPool.SetMinThreads(600, 600);
            //ThreadPool.SetMaxThreads(700, 700);
            s.Bind(endpoint);
            s.Listen(100);
            while (true)
            {

                var handler = s.Accept();
                Console.WriteLine("Pending works: " + ThreadPool.PendingWorkItemCount);
                Console.WriteLine("Threads: " + ThreadPool.ThreadCount);
                ThreadPool.QueueUserWorkItem(state =>
                {
                    var buffer = new byte[handler.ReceiveBufferSize];

                    handler.Receive(buffer);
                    var da = Encoding.ASCII.GetString(buffer);
                    var rp = "HTTP/1.1 200 OK\nContent-Type: text/plain\nContent-Length: 12\n\nHello world!";
                    var enc = Encoding.ASCII.GetBytes(rp);
                    Thread.Sleep(1000);
                    var response = handler.Send(enc);
                    handler.Close();
                });

                
                    
            }
        }
    }
}