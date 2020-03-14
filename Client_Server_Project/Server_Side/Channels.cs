using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side
{
    [Serializable]
    public class Channels
    {
        public List<Channel> listChannels;

        public Channels()
        {
            listChannels = new List<Channel>();
        }

        public void serialize(Channels channels)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream("AllChannels.bin", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, channels);
            stream.Close();
        }

        public Channels deserialize()
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream("AllChannels.bin", FileMode.Open, FileAccess.Read);
            Channels c = new Channels();
            c = (Channels)formatter.Deserialize(stream);
            stream.Close();
            return c;
        }


    }
}
