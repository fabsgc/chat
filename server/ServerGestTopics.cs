using chat.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace chat.server
{
    [Serializable]
    public class ServerGestTopics : TCPServer
    {
        private TCPGestTopics tcpTopicsManager;

        public override void gereClient(int port)
        {
            Console.WriteLine("gereClient ServerGestTopics IN");

            tcpTopicsManager = new TCPGestTopics();

            try
            {
                Message inputMessage;

                /*int i;

                String data = null;

                Byte[] bytes = new Byte[256];

                // Listen to the client
                NetworkStream stream = commSocket.GetStream();

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }*/

                while ((inputMessage = getMessage()) != null)
                {
                    Console.WriteLine("while ((inputMessage = getMessage()) != null)");
                    switch (inputMessage.Head)
                    {
                        case Message.Header.LIST_TOPICS:
                            {
                                List<string> topics = tcpTopicsManager.listTopics();
                                Message outputMessage = new Message(Message.Header.LIST_TOPICS, topics);
                                sendMessage(outputMessage);
                            }
                            break;

                        case Message.Header.CREATE_TOPIC:
                            tcpTopicsManager.createTopic(inputMessage.Data.First());
                            break;

                        case Message.Header.JOIN_TOPIC:
                            {
                                string topicToJoin = inputMessage.Data.First();
                            
                                Message outputMessage = new Message(Message.Header.JOIN_TOPIC, port.ToString());
                                sendMessage(outputMessage);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
