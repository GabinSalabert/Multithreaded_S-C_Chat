using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side
{
    public class Client
    {
        public TcpClient tcpclient;
        public Directs dms;
        public Channels active_channels;
        public String name;
        public Client(TcpClient client)
        {
            this.active_channels = new Channels();
            this.dms = new Directs();
            this.tcpclient = client;
        }
    }
}
