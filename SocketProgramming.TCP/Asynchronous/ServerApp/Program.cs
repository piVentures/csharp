using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// Namespace for the server application
namespace Sockets.Server
{
    class Program
    {
        /*
         * ObjectState:
         * This class holds all information related to a single client connection.
         * Each connected client gets its own ObjectState instance.
         */
        public class ObjectState
        {
            // The socket connected to the client
            public Socket WorkSocket = null;

            // Buffer used to receive data from the client
            public byte[] Buffer = new byte[1024];

            // Buffer used to send data back to the client
            public byte[] SendBuffer =
                Encoding.ASCII.GetBytes("Hello from server<EOF>");

            // Used to store incoming data piece by piece
            public StringBuilder Sb = new StringBuilder();
        }

        /*
         * AsyncSocketListener:
         * This class contains all logic for the asynchronous TCP server.
         */
        public class AsyncSocketListener
        {
            // Used to block the main thread until a client connects
            public static ManualResetEvent allDone = new ManualResetEvent(false);

            /*
             * StartListener:
             * 1. Creates a socket
             * 2. Binds it to an IP + Port
             * 3. Listens for incoming connections
             */
            public static void StartListener()
            {
                // Get local machine IP address
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHost.AddressList[0];

                // Define endpoint (IP + Port)
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

                // Create TCP socket
                Socket listener = new Socket(
                    ipAddress.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp
                );

                try
                {
                    // Bind socket to the endpoint
                    listener.Bind(localEndPoint);

                    // Start listening (max 10 pending connections)
                    listener.Listen(10);

                    while (true)
                    {
                        // Reset event before accepting a connection
                        allDone.Reset();

                        Console.WriteLine("Waiting for a connection...");

                        // Begin accepting a client asynchronously
                        listener.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            listener
                        );

                        // Block until a client connects
                        allDone.WaitOne();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            /*
             * AcceptCallback:
             * Called when a client successfully connects.
             */
            private static void AcceptCallback(IAsyncResult ar)
            {
                // Signal that a connection was accepted
                allDone.Set();

                // Get the listening socket
                Socket listener = (Socket)ar.AsyncState;

                // Create socket for the connected client
                Socket handler = listener.EndAccept(ar);

                // Create state object for this client
                ObjectState state = new ObjectState();
                state.WorkSocket = handler;

                // Begin receiving data from the client
                handler.BeginReceive(
                    state.Buffer,
                    0,
                    state.Buffer.Length,
                    0,
                    new AsyncCallback(ReadCallback),
                    state
                );
            }

            /*
             * ReadCallback:
             * Called when data is received from the client.
             */
            private static void ReadCallback(IAsyncResult ar)
            {
                ObjectState state = (ObjectState)ar.AsyncState;
                Socket handler = state.WorkSocket;

                // Read how many bytes were received
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // Convert bytes to string and append
                    state.Sb.Append(
                        Encoding.ASCII.GetString(
                            state.Buffer,
                            0,
                            bytesRead
                        )
                    );

                    string content = state.Sb.ToString();

                    // Check for end-of-message marker
                    if (content.IndexOf("<EOF>") > -1)
                    {
                        Console.WriteLine(
                            $"Read {content.Length} bytes from client.\nData: {content}"
                        );

                        // Send response back to client
                        Send(handler, state.SendBuffer);
                    }
                    else
                    {
                        // Continue receiving more data
                        handler.BeginReceive(
                            state.Buffer,
                            0,
                            state.Buffer.Length,
                            0,
                            new AsyncCallback(ReadCallback),
                            state
                        );
                    }
                }
            }

            /*
             * Send:
             * Sends data back to the client asynchronously.
             */
            private static void Send(Socket handler, byte[] data)
            {
                handler.BeginSend(
                    data,
                    0,
                    data.Length,
                    0,
                    new AsyncCallback(SendCallback),
                    handler
                );
            }

            /*
             * SendCallback:
             * Called after data has been sent to the client.
             */
            private static void SendCallback(IAsyncResult ar)
            {
                try
                {
                    Socket handler = (Socket)ar.AsyncState;

                    int bytesSent = handler.EndSend(ar);
                    Console.WriteLine($"Sent {bytesSent} bytes to client.");

                    // Close the connection gracefully
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        /*
         * Program entry point
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Starting asynchronous TCP server...");
            AsyncSocketListener.StartListener();
        }
    }
}
