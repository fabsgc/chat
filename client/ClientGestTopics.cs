using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using chat.net;
using chat.chat;

namespace chat.client
{
    class ClientGestTopics : TCPClient, TopicsManager
    {
        private Chatter chatter;

        private string topic;

        public string Topic
        {
            get
            {
                return topic;
            }

            set
            {
                topic = value;
            }
        }

        internal Chatter Chatter
        {
            get
            {
                return chatter;
            }

            set
            {
                chatter = value;
            }
        }

        public string createTopic(string topic)
        {
            Message message = new Message(Message.Header.CREATE_TOPIC, topic);
            try
            {
                sendMessage(message);
                return topic;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }


        public Chatroom joinTopic(string topic)
        {
            Message message = new Message(Message.Header.JOIN_TOPIC, topic);
            try
            {
                sendMessage(message);
                Message answer = getMessage();
                int port = Int32.Parse(answer.getData().First());
                ClientChatRoom chatroom = new ClientChatRoom();
                chatroom.setServer(getAddress(), port);
                chatroom.connect();
                Thread t = new Thread(new ThreadStart(chatroom.run));
                t.Start();
                return chatroom;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public List<string> listTopics()
        {
            Console.WriteLine("listTopics ClientGestTopics1");
            Message message = new Message(Message.Header.LIST_TOPICS);
            Console.WriteLine("listTopics ClientGestTopics2");
            List<string> topics = new List<string>();
            Console.WriteLine("listTopics ClientGestTopics3");
            sendMessage(message);
            Console.WriteLine("listTopics ClientGestTopics4");
            Message answer = getMessage();
            Console.WriteLine("listTopics ClientGestTopics5");
            topics = answer.getData();
            Console.WriteLine("listTopics ClientGestTopics6");
            return topics;
        }
    }
}
