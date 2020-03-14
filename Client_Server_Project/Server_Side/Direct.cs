using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side
{
    public class Direct
    {
        public String text;
        public List<Client> dm_between;

        public Direct(Client a, Client b)
        {
            this.dm_between = new List<Client>();
            this.dm_between.Add(a);
            this.dm_between.Add(b);
        }
        
    }
}
