using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Side
{
    [Serializable]
    public class Channel
    {
        public string name;

        public Channel(string name)
        {
            this.name = name;
        }
    }
}
