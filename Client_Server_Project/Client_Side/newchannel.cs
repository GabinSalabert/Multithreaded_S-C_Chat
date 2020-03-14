using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Side
{
    public partial class NewChannel : Form
    {
        NetworkStream streamSender;
        Chat_status c;
        public NewChannel(NetworkStream nws, Chat_status cw)
        {
            InitializeComponent();
            this.streamSender = nws;
            this.c = cw;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string action = "newCh";
            byte[] action_buffer = Encoding.ASCII.GetBytes(action);
            streamSender.Write(action_buffer, 0, action_buffer.Length);

            byte[] new_private = Encoding.ASCII.GetBytes(newChName.Text);
            streamSender.Write(new_private, 0, new_private.Length);

            this.Close();
        }


    }
}
