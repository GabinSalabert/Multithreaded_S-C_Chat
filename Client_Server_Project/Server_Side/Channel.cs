using System;
using System.Windows.Forms;

namespace Server_Side
{
    [Serializable]
    public class Channel
    {
        public string name;
        public string channel_text;

        public Channel(string name)
        {
            this.name = name;
            this.channel_text = "Welcome, this is the "+name+" channel" + Environment.NewLine;
        }

    }
}
