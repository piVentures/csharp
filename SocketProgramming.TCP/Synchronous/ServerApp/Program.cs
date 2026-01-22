using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    class Program
    {
        // ================= SYNCHRONOUS SOCKET SERVER =================
        public static void StartServer()
        {
            // Buffer for incoming data from client
            byte[] bytes = new byte[1024];

            // Get the local machine host information
            string hostName = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(hostName);

            // Select the first available IP address
            IPAddress ipAddress = ipHost.AddressList[0];

            // Create a local endpoint (IP + Port)
            // Port must match the client’s remote port
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP socket
            Socket listener = new Socket(
                ipAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            try
            {
                // ---------------- BIND ----------------
                // Associates the socket with the local IP and port
                listener.Bind(localEndPoint);

                // ---------------- LISTEN ----------------
                // Starts listening for incoming connections
                // 10 is the maximum pending connections queue
                listener.Listen(10);

                Console.WriteLine(
                    $"Server listening on {localEndPoint}"
                );

                // ---------------- ACCEPT ----------------
                // This call BLOCKS until a client connects
                Socket handler = listener.Accept();

                Console.WriteLine(
                    $"Client connected: {handler.RemoteEndPoint}"
                );

                // ---------------- RECEIVE DATA ----------------
                // Read data sent by client
                int bytesReceived = handler.Receive(bytes);

                string data = Encoding.ASCII.GetString(
                    bytes, 0, bytesReceived
                );

                Console.WriteLine($"Received: {data}");

                // ---------------- SEND RESPONSE ----------------
                // Echo the same data back to the client
                byte[] response = Encoding.ASCII.GetBytes(data);
                handler.Send(response);

                // ---------------- CLOSE CONNECTION ----------------
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

                listener.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Server error: {e.Message}");
            }
        }

        // ---------------- PROGRAM ENTRY POINT ----------------
        static void Main(string[] args)
        {
            Console.WriteLine("Starting synchronous TCP server...");
            StartServer();

            Console.ReadKey();
        }
    }
}
