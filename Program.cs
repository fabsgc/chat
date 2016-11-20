using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using chat.client;
using chat.net;
using chat.server;
using chat.test;

namespace chat
{
    class Program
    {
        public static void Main()
        {
            //Server.Main(); // Launched by 2nd project
            Client.Main();
        }
    }
}
