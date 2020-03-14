using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Side
{
    [Serializable]

    public class Directs
    {
        public List<Direct> direct_list;

        public Directs()
        {
            this.direct_list = new List<Direct>();
        }

        public Direct Direct
        {
            get => default;
            set
            {
            }
        }
    }
}
