using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Namespace groups related classes logically
namespace ClientApp
{
    // Entry class of the console application
    class Program
    {
        // ================= SYNCHRONOUS SOCKET CLIENT =================
        // This method handles the entire client-side TCP communication
        public static void StartClient()
        {
            // Buffer to store data received from the server
            // TCP works with bytes, not strings
            byte[] bytes = new byte[1024];

            // Get the name of the current machine (client machine)
            string hostName = Dns.GetHostName();

            // Get DNS information (IP addresses) related to this host
            IPHostEntry ipHost = Dns.GetHostEntry(hostName);

            // Print the host name (for debugging / learning)
            Console.WriteLine($"Host Name: {hostName}");

            // Select the first IP address from the list
            // Usually IPv4 or IPv6 depending on the system
            IPAddress ipAddress = ipHost.AddressList[0];

            // Create an endpoint (IP + Port) of the server
            // Port 11000 must match the server’s listening port
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP socket
            // AddressFamily → IPv4 or IPv6
            // SocketType.Stream → TCP
            // ProtocolType.Tcp → TCP protocol
            Socket sender = new Socket(
                ipAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            try
            {
                // ---------------- CONNECT ----------------
                // This is a BLOCKING (synchronous) call
                // The program waits here until connection succeeds or fails
                sender.Connect(remoteEP);

                Console.WriteLine(
                    $"Socket connected to {sender.RemoteEndPoint}"
                );

                // ---------------- SEND DATA ----------------
                // Convert string message to byte array
                // TCP sends raw bytes, not strings
                byte[] msg = Encoding.ASCII.GetBytes(
                    "This is a test<EOF>"
                );

                // Send the message to the server
                // This call blocks until data is sent
                int bytesSent = sender.Send(msg);

                // ---------------- RECEIVE DATA ----------------
                // Receive response from server
                // This blocks until data is received
                int bytesReceived = sender.Receive(bytes);

                // Convert received bytes back into string
                Console.WriteLine(
                    $"Echoed test = {Encoding.ASCII.GetString(bytes, 0, bytesReceived)}"
                );

                // ---------------- CLOSE CONNECTION ----------------
                // Shutdown sending and receiving
                sender.Shutdown(SocketShutdown.Both);

                // Release socket resources
                sender.Close();
            }
            catch (ArgumentNullException e)
            {
                // Happens if invalid arguments are passed
                Console.WriteLine($"Argument error: {e.Message}");
            }
            catch (SocketException e)
            {
                // Happens if connection fails, server unreachable, etc.
                Console.WriteLine($"Socket error: {e.Message}");
            }
            catch (Exception e)
            {
                // Any other unexpected error
                Console.WriteLine($"General error: {e.Message}");
            }
        }

        // ---------------- PROGRAM ENTRY POINT ----------------
        static void Main(string[] args)
        {
            // Start the synchronous TCP client
            StartClient();

            // Prevent console from closing immediately
            Console.ReadKey();
        }
    }
}
