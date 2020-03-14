using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client_Side
{
    [Serializable]
    public class Channels
    {
        public List<Channel> channels;
        public Channels()
        {
            channels = new List<Channel>();
        }

        public Channel Channel
        {
            get => default;
            set
            {
            }
        }

        public void serialize(Clients Channels)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream("ClientChannelsDB.bin", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, Channels);
            stream.Close();
        }

        public Channels deserialize()
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream("ClientChannelsDB.bin", FileMode.Open, FileAccess.Read);
            Channels c = new Channels();
            c = (Channels)formatter.Deserialize(stream);
            stream.Close();
            return c;
        }
    }
}
