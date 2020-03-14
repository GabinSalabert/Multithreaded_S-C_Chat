using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Side
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            password.PasswordChar = '\u25CF';
        }

       
        public Client Log()
        {
            Client use = null;
            Clients dbClients = new Clients();
            Clients c = dbClients.deserialize();
            string user_name = username.Text;
            string pass_word = password.Text;
            use = c.checkClients(user_name, pass_word, status_info);
            return use;

        }


        private void to_register_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void btn_login_Click_1(object sender, EventArgs e)
        {
            Client current = Log();
            if (current != null)
            {
                try
                {
                    Chat_status chat = new Chat_status(current);
                    chat.Show();
                    status_info.Text = "All right.";
                }

                catch (SocketException)
                {
                    status_info.Text = "Server is not running.";
                }
            }
        }
    }
}
