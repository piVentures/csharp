using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// Namespace for the client application
namespace Sockets.Client
{
    class Program
    {
        /*
         * ObjectState:
         * Holds all information related to a single client socket operation.
         */
        public class ObjectState
        {
            // Client socket
            public Socket WorkSocket = null;

            // Buffer for receiving data
            public byte[] Buffer = new byte[1024];

            // Used to store received data piece by piece
            public StringBuilder Sb = new StringBuilder();
        }

        /*
         * AsyncSocketClient:
         * Handles connecting, sending, and receiving data asynchronously.
         */
        public class AsyncSocketClient
        {
            // Signals when a connection is completed
            private static ManualResetEvent connectDone =
                new ManualResetEvent(false);

            // Signals when data has been sent
            private static ManualResetEvent sendDone =
                new ManualResetEvent(false);

            // Signals when response has been received
            private static ManualResetEvent receiveDone =
                new ManualResetEvent(false);

            // Stores server response
            private static string response = string.Empty;

            /*
             * StartClient:
             * Entry method for the client.
             */
            public static void StartClient()
            {
                try
                {
                    // Resolve server IP address
                    IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipAddress = ipHost.AddressList[0];

                    // Define server endpoint (same port as server)
                    IPEndPoint remoteEndPoint =
                        new IPEndPoint(ipAddress, 11000);

                    // Create TCP socket
                    Socket client = new Socket(
                        ipAddress.AddressFamily,
                        SocketType.Stream,
                        ProtocolType.Tcp
                    );

                    // Begin connecting to the server
                    client.BeginConnect(
                        remoteEndPoint,
                        new AsyncCallback(ConnectCallback),
                        client
                    );

                    // Wait until connection completes
                    connectDone.WaitOne();

                    // Message to send (must end with <EOF>)
                    string message = "Hello from client<EOF>";

                    // Send message
                    Send(client, message);

                    // Wait until message is sent
                    sendDone.WaitOne();

                    // Receive response
                    Receive(client);

                    // Wait until response is received
                    receiveDone.WaitOne();

                    Console.WriteLine(
                        $"Response received from server: {response}"
                    );

                    // Close the socket
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            /*
             * ConnectCallback:
             * Called when the client successfully connects to the server.
             */
            private static void ConnectCallback(IAsyncResult ar)
            {
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection
                client.EndConnect(ar);

                Console.WriteLine(
                    $"Connected to {client.RemoteEndPoint}"
                );

                // Signal that connection is complete
                connectDone.Set();
            }

            /*
             * Receive:
             * Begins receiving data from the server.
             */
            private static void Receive(Socket client)
            {
                ObjectState state = new ObjectState();
                state.WorkSocket = client;

                client.BeginReceive(
                    state.Buffer,
                    0,
                    state.Buffer.Length,
                    0,
                    new AsyncCallback(ReceiveCallback),
                    state
                );
            }

            /*
             * ReceiveCallback:
             * Called whenever data is received from the server.
             */
            private static void ReceiveCallback(IAsyncResult ar)
            {
                ObjectState state = (ObjectState)ar.AsyncState;
                Socket client = state.WorkSocket;

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // Convert received bytes to string
                    state.Sb.Append(
                        Encoding.ASCII.GetString(
                            state.Buffer,
                            0,
                            bytesRead
                        )
                    );

                    response = state.Sb.ToString();

                    // Check for end-of-message marker
                    if (response.IndexOf("<EOF>") > -1)
                    {
                        // Signal that response is complete
                        receiveDone.Set();
                    }
                    else
                    {
                        // Continue receiving data
                        client.BeginReceive(
                            state.Buffer,
                            0,
                            state.Buffer.Length,
                            0,
                            new AsyncCallback(ReceiveCallback),
                            state
                        );
                    }
                }
            }

            /*
             * Send:
             * Sends data to the server asynchronously.
             */
            private static void Send(Socket client, string data)
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                client.BeginSend(
                    byteData,
                    0,
                    byteData.Length,
                    0,
                    new AsyncCallback(SendCallback),
                    client
                );
            }

            /*
             * SendCallback:
             * Called when data has been sent to the server.
             */
            private static void SendCallback(IAsyncResult ar)
            {
                Socket client = (Socket)ar.AsyncState;

                int bytesSent = client.EndSend(ar);
                Console.WriteLine(
                    $"Sent {bytesSent} bytes to server."
                );

                // Signal that sending is complete
                sendDone.Set();
            }
        }

        /*
         * Program entry point
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Starting asynchronous TCP client...");
            AsyncSocketClient.StartClient();
            Console.WriteLine("Client finished execution.");
            Console.ReadLine();
        }
    }
}
