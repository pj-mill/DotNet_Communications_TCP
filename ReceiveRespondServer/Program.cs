using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ReceiveRespondServer
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
            Console.WriteLine("RECEIVE RESPOND SERVER");
            Console.WriteLine(divider);


            /*-----------------------------------------------------------------------
             * Vars
             * --------------------------------------------------------------------*/
            TcpListener server = null;
            TcpClient client = null;
            NetworkStream stream = null;
            IPAddress localhost = IPAddress.Parse("127.0.0.1");
            int port = 43000;
            int bytesToRead = 256;
            int numberOfBytesRead = 0;
            byte[] receivedBytes = new byte[bytesToRead]; // Buffer for reading data
            byte[] response;
            StringBuilder sb;
            bool done = false;


            /*-----------------------------------------------------------------------
             * Initialise & start TCP listener
             * --------------------------------------------------------------------*/
            try
            {
                //Start listening
                server = new TcpListener(localhost, port);
                server.Start();
                Console.WriteLine($"Server has started on {localhost}:{port}");

                // Enter the listening loop.
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
                            receivedBytes = new byte[bytesToRead];

                            // Read Loop
                            while ((numberOfBytesRead = stream.Read(receivedBytes, 0, receivedBytes.Length)) != 0)
                            {
                                // Process message
                                var msg = Encoding.UTF8.GetString(receivedBytes, 0, numberOfBytesRead);
                                sb.Append(msg);

                                // Respond (when message fully received)
                                if (numberOfBytesRead < bytesToRead)
                                {
                                    if (msg.Equals("close"))
                                    {
                                        response = Encoding.UTF8.GetBytes("Good Bye from Server !!!");
                                        done = true;
                                    }
                                    else
                                    {
                                        response = Encoding.UTF8.GetBytes("Hey Thanks");
                                    }
                                    stream.Write(response, 0, response.Length);
                                }
                            }
                            // Output message
                            Console.WriteLine(sb.ToString());
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
            }

            Console.WriteLine("Server closed...");
            Console.ReadKey();
        }
    }
}
