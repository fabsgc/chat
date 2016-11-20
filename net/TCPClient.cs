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
            Console.WriteLine("connected to server");
        }

        public void close()
        {
            //socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

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
            try
            {
                NetworkStream strm = socket.GetStream();
                IFormatter formatter = new BinaryFormatter();
                Message returnMsg = (Message) formatter.Deserialize(strm);
                return returnMsg;
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("on ne devrait pas etre la");
            return null;
        }

        public void sendMessage(Message m)
        {
            //Console.WriteLine("sendmessage");

            //string message = m.getData().First();

            //string message = "test message";

            

            // Translate the passed message into ASCII and store it as a Byte array.
            //Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = socket.GetStream();
            BinaryFormatter binaryFmt = new BinaryFormatter();
            binaryFmt.Serialize(stream, m);

            // Send the message to the connected TcpServer. 
            //stream.Write(data, 0, data.Length);

            //Console.WriteLine("Sent: {0}", message);
        }
    }
}