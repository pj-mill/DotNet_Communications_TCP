using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BasicClient
{
    class Program
    {
        static void Main(string[] args)
        {
            /*-----------------------------------------------------------------------
             * Title
             * --------------------------------------------------------------------*/
            string divider = new string('=', 100);
            Console.WriteLine(divider);
            Console.WriteLine("CLIENT");
            Console.WriteLine(divider);


            /*-----------------------------------------------------------------------
             * Vars
             * --------------------------------------------------------------------*/
            int port = 43000;
            TcpClient client;
            byte count = 0;


            /*-----------------------------------------------------------------------
             * Send 3 messages (last one to close server)
             * --------------------------------------------------------------------*/
            try
            {
                while (count < 3)
                {
                    // Pause for 1 second.
                    Thread.Sleep(1000);

                    // Create a new client
                    using (client = new TcpClient("localhost", port))
                    {
                        string msg = (count < 2) ? $"This is message {count + 1}" : "close";

                        // Create a new stream to send message
                        using (NetworkStream stream = client.GetStream())
                        {
                            using (BufferedStream writer = new BufferedStream(stream))
                            {
                                byte[] messageBytesToSend = Encoding.UTF8.GetBytes(msg);
                                writer.Write(messageBytesToSend, 0, messageBytesToSend.Length);
                                Console.WriteLine("Message sent...");
                            }
                        }
                    }

                    // Inc. counter
                    count++;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to connect to server");
            }


            /*-----------------------------------------------------------------------
             * Finish up
             * --------------------------------------------------------------------*/
            Console.WriteLine("Client finished...");
            Console.ReadKey();
        }
    }
}
