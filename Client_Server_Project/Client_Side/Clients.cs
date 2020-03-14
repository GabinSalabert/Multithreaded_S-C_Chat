using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Client_Side
{
    [Serializable]
    public class Clients
    {
        public List<Client> listClients;

        public Clients()
        {
         
        }

        public Client Client
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

        public void serialize(Clients clients)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream("ClientsDB.bin", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, clients);
            stream.Close();
        }

        public Clients deserialize()
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream("ClientsDB.bin", FileMode.Open, FileAccess.Read);
            Clients c = new Clients();
            c = (Clients)formatter.Deserialize(stream);
            stream.Close();
            return c;
        }

        public Client checkClients(string username, string password, Label print)
        {
            Client current = null;
            int code = 0;
            foreach (Client clients in listClients)
            {
                if (clients.name == username)
                {
                    if (clients.password == password)
                    {
                        print.ForeColor = Color.White;
                        print.Text = "You are connected";
                        current = clients;
                        break;
                    }
                    else
                    {
                        print.ForeColor = Color.Red;
                        print.Text = "Wrong password !";
                    }
                }
                else
                {
                    print.ForeColor = Color.Red;
                    print.Text = "Please register, you're not in the DB !";
                }
            }

            return current;
        }

        public Client checkClientsCh(string username, string password)
        {
            Client none = null;
            foreach (Client clients in listClients)
            {
                if (clients.name == username && clients.password == password)
                {
                    return clients;
                }
            }
            return none;
        }


    }
}