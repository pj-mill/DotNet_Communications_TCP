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
            TcpListener server;
            Socket client;
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
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to connect server");
                return;
            }


            /*-----------------------------------------------------------------------
             * Listen for clients on network
             * --------------------------------------------------------------------*/
            while (!done)
            {
                Console.WriteLine("Waiting for a connection...");
                // Socket that manages the connection to the client (blocks code execution until a client joins)
                client = server.AcceptSocket();

                // Client joined
                Console.WriteLine("\nConnection accepted.");
                stream = new NetworkStream(client);
                sb = new StringBuilder();
                try
                {
                    // Read messages in chunks of 1024 bytes
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Tidy up
                stream.Close();
                client.Close();
            }


            /*-----------------------------------------------------------------------
             * Close Tcp Listener
             * --------------------------------------------------------------------*/
            server.Stop();
            Console.WriteLine("Server closed...");
            Console.ReadKey();
        }
    }
}
