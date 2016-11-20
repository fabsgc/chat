using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace chat.net
{
    class TCPClient : MessageConnection
    {
        private int _port; // todo: get port from socket?
        private TcpClient socket;
        private IPAddress _adr;

        public void setServer(IPAddress adr, int port)
        {
            _port = port;
            _adr = adr;
        }

        public void connect()
        {
            socket = new TcpClient("127.0.0.1", _port);
        }

        public void close()
        {
            //socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        /* Pour l'envoi et la réception de données, on est censé sérialiser l'objet Message
         * Surtout que dans ton code, à la réception, tu mets le header et la liste des messages dans l'attribut data */

        /*public Message getMessage()
        {
            try
            {
                NetworkStream strm = new NetworkStream(socket);
                IFormatter formatter = new BinaryFormatter();
                Message message;

                try
                {
                    message = (Message)formatter.Deserialize(strm);
                    return message;
                }
                catch(SerializationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }*/

        /*public void sendMessage(Message message)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                NetworkStream strm = new NetworkStream(socket);
                formatter.Serialize(strm, message);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
        }*/

        public int getPort()
        {
            return _port;
        }

        public IPAddress getAddress()
        {
            return _adr;
        }

        public Message getMessage()
        {
            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            Byte[] data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            NetworkStream stream = socket.GetStream();
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            //Console.WriteLine("Received: {0}", responseData);

            Message msgReceived = new Message(Message.Header.DEBUG, responseData);

            return msgReceived;
        }

        public void sendMessage(Message m)
        {
            //Console.WriteLine("sendmessage");

            //string message = m.getData().First();

            string message = "test message";

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = socket.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);

            //Console.WriteLine("Sent: {0}", message);
        }
    }
}