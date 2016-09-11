using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ReceiveRespondClient
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
            Console.WriteLine("RECEIVE RESPOND CLIENT");
            Console.WriteLine(divider);


            /*-----------------------------------------------------------------------
             * Vars
             * --------------------------------------------------------------------*/
            int port = 43000;
            TcpClient client;
            byte count = 0;
            byte[] dataToSend;
            byte[] responseBytes = new byte[256]; // Buffer for reading data
            int responseSize;
            var msgo = "Create a TcpClient. Note, for this client to work you need to have a TcpServer connected to the same address as specified by the server, port combination. Translate the passed message into ASCII and store it as a Byte array. Get a client stream for reading and writing. Stream stream = client.GetStream(); Send the message to the connected TcpServer.";


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
                        // Create a new stream to send message
                        using (NetworkStream stream = client.GetStream())
                        {
                            string msg = (count < 2) ? $"{count + 1} - {msgo}" : "close";
                            dataToSend = Encoding.UTF8.GetBytes(msg);

                            stream.Write(dataToSend, 0, dataToSend.Length);
                            Console.WriteLine("Message sent...");

                            // String to store the response
                            string responseData = String.Empty;

                            // Read the first batch of the TcpServer response bytes.
                            responseSize = stream.Read(responseBytes, 0, responseBytes.Length);
                            responseData = Encoding.UTF8.GetString(responseBytes, 0, responseSize);
                            Console.WriteLine($"Received: {responseData}");
                        }
                    }
                    // Inc. counter
                    count++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            /*-----------------------------------------------------------------------
             * Finish up
             * --------------------------------------------------------------------*/
            Console.WriteLine("Client finished...");
            Console.ReadKey();

        }


    }
}
