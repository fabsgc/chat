using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using chat.chat;
using chat.client;

namespace chat.test
{
    class Client
    {
        public static void Main()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

            ClientGestTopics gt = new ClientGestTopics();
            gt.setServer(ipAddress, 2300);
            gt.connect();
            Chatter bob = new TextChatter("Bob");
            Chatter joe = new TextChatter("Joe");
            gt.createTopic("java");
            gt.createTopic("UML");
            List<string> topics = gt.listTopics();
            gt.createTopic("jeux");
            gt.listTopics();
            /*Chatroom cr = gt.joinTopic("jeux");
            cr.join(bob);
            cr.post("Je suis seul ou quoi ?", bob);
            cr.join(joe);
            cr.post("Tiens, salut Joe !", bob);
            cr.post("Toi aussi tu chat sur les forums de jeux pendant les TP, Bob ? ", joe);*/
        }
    }
}
