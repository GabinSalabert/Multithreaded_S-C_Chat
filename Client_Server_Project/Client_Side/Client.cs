using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Client_Side
{
    [Serializable]
    public class Client
    {
        public string name;
        public string password;
        public Channel currentCh;
        public Direct currentDm;
        public Channels clientsCh;
        public Directs clientsDm;
        IPAddress ip;

        public Client(string name, string password)
        {
            clientsCh = new Channels();
            clientsCh.channels.Add(new Channel("General"));
            clientsDm = new Directs();
            currentDm = new Direct("Start");
            this.name = name;
            this.password = password;

            this.ip = IPAddress.Parse("127.0.0.1");

        }

        public Direct Direct
        {
            get => default;
            set
            {
            }
        }

        public Channel Channel
        {
            get => default;
            set
            {
            }
        }

        public Channels Channels
        {
            get => default;
            set
            {
            }
        }

        public Directs Directs
        {
            get => default;
            set
            {
            }
        }

        public Direct Direct1
        {
            get => default;
            set
            {
            }
        }

        public IPAddress GetIp()
        {
            return this.ip;
        }


    }
}