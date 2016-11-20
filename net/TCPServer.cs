using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using chat.server;

namespace chat.net
{

    [Serializable]
    public abstract class TCPServer : MessageConnection
    {
        protected TcpClient commSocket;
        protected TcpListener waitSocket; // todo: serversocket in the subject
        protected int _port; // todo: get port from socket?
        protected enum Mode { treatClient, treatConnections }
        protected Mode mode;
        //private byte[] bytes;

        public void startServer(int port)
        {
            mode = Mode.treatConnections;
            _port = port;
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

            try
            {
                waitSocket = new TcpListener(ipAddress, port);
            }
            catch(NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(SocketException e)
            {
                Console.WriteLine(e.Message);
            }

            // Start the server's TcpListener
            waitSocket.Start();

            // Enter the listening loop
            this.treatConnections();
        }

        public void stopServer()
        {
            //waitSocket.Close();
        }

        private void treatConnections()
        {
            while (true)
            {
                commSocket = waitSocket.AcceptTcpClient();
                Console.WriteLine("Connection accepted.");

                var childSocketThread = new Thread(() =>
                {
                    gereClient(_port);
                });
                childSocketThread.Start();
            }
            /*
            while (true)
            {
                Console.WriteLine("treatConnections");
                try
                {
                    commSocket = waitSocket.AcceptTcpClient();
                    Console.WriteLine("accept");

                    Console.WriteLine("test1");

                    ServerGestTopics newInstance = new ServerGestTopics();
                    Console.WriteLine("test2");

                    newInstance.mode = Mode.treatClient;
                    Console.WriteLine("test3");
                    newInstance._port = _port;
                    Console.WriteLine("test4");
                    newInstance.waitSocket = waitSocket;
                    Console.WriteLine("test5");
                    newInstance.commSocket = newInstance.waitSocket.AcceptTcpClient();
                    Console.WriteLine("test6");

                    Console.WriteLine("clone OK");
                    newInstance.gereClient(_port);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }*/
        }

        public int getPort()
        {
            return _port;
        }

        /* Pour l'envoi et la réception de données, on est censé sérialiser l'objet Message
         * Surtout que dans ton code, à la réception, tu mets le header et la liste des messages dans l'attribut data */

        public Message getMessage()
        {
            /*try
            {
                NetworkStream strm = new NetworkStream(commSocket);
                IFormatter formatter = new BinaryFormatter();
                return (Message)formatter.Deserialize(strm);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }*/

            return null;
        }

        public void sendMessage(Message message)
        {
            /*try
            {
                IFormatter formatter = new BinaryFormatter();
                NetworkStream strm = new NetworkStream(commSocket);
                formatter.Serialize(strm, message);
            }
            catch(SerializationException e)
            {
                Console.WriteLine(e.Message);
            }*/


        }

        /*public Message getMessage()
        {
            // Data buffer for incoming data.
            bytes = new byte[1024];

            // Receive the response from the remote device.
            int bytesRec = commSocket.Receive(bytes);

            Message msgReceived = new Message(bytes, bytesRec);

            return msgReceived;
        }

        public void sendMessage(Message m)
        {
            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(m.toString());

            // Send the data through the socket.
            commSocket.Send(msg);
        }*/

        public abstract void gereClient(int port);
    }
}
