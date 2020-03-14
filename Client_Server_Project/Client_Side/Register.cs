using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Side
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            password.PasswordChar = '\u25CF';
            passwconfirm.PasswordChar = '\u25CF';
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            string name = username.Text;
            string passw = password.Text;
            string passwconfirm = this.passwconfirm.Text;

            if (passw == passwconfirm)
            {
                Client client = new Client(name, passw);
                Clients clients = new Clients();

                if (File.Exists("ClientsDB.bin"))
                {
                    Clients clientsDb = clients.deserialize();
                    clientsDb.listClients.Add(client);
                    clientsDb.serialize(clientsDb);
                    this.Dispose();
                }
                else
                {
                    Clients newDb = new Clients();
                    newDb.listClients = new List<Client>();
                    newDb.listClients.Add(client);
                    newDb.serialize(newDb);
                    this.Dispose();
                }
            }

        }
    }
}
