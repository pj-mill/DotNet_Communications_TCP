using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicServer
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
            Console.WriteLine("SERVER");
            Console.WriteLine(divider);


            /*-----------------------------------------------------------------------
             * Vars
             * --------------------------------------------------------------------*/
            TcpListener server = null;
            TcpClient client;
            NetworkStream stream = null;
            IPAddress localhost = IPAddress.Parse("127.0.0.1");
            int port = 43000;
            int bytesToRead = 0;
            int readByteSize = 10;
            StringBuilder sb;
            bool done = false;


            /*-----------------------------------------------------------------------
             * Initialise & start TCP listener
             * --------------------------------------------------------------------*/
            try
            {
                server = new TcpListener(localhost, port);
                server.Start();
                Console.WriteLine($"Server has started on {localhost}:{port}");

                /*-----------------------------------------------------------------------
                * Listen for clients on network
                * --------------------------------------------------------------------*/
                while (!done)
                {
                    Console.WriteLine("Waiting for a connection...");

                    // Accept requests (blocks code execution until a client joins)
                    using (client = server.AcceptTcpClient())
                    {
                        // Client joined
                        Console.WriteLine("\nConnection accepted.");

                        // Get a stream object for reading and writing
                        using (stream = client.GetStream())
                        {
                            sb = new StringBuilder();

                            // Read messages in 10 byte chunks
                            do
                            {
                                byte[] chunks = new byte[readByteSize];
                                bytesToRead = stream.Read(chunks, 0, chunks.Length);
                                sb.Append(Encoding.UTF8.GetString(chunks));
                            }
                            while (bytesToRead != 0);

                            // Output the entire messsage when done.
                            string msg = sb.ToString().Trim();
                            Console.WriteLine(msg);

                            // Close connection if instructed to.
                            if (msg.Contains("close"))
                            {
                                done = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                /*-----------------------------------------------------------------------
                 * Close Tcp Listener
                 * --------------------------------------------------------------------*/
                server.Stop();
                Console.WriteLine("Server closed...");
                Console.ReadKey();
            }
        }
    }
}
