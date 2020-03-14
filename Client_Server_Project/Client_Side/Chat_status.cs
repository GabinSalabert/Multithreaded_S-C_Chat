using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Side
{
    public partial class Chat_status : Form
    {
        NetworkStream currStream;
        TcpClient client;
        Thread thread;
        Client current;
        List<string> string_server_channels;
        List<string> string_server_clients;
        bool add = false;

        public Chat_status()
        {
            InitializeComponent();
        }

        public Chat_status(Client current)
        {
           
            InitializeComponent();

            info.Text = current.name;
            msg_area.Enabled = false;

            InitCheck();                

            this.current = current;

            this.client = Initialize(current);

            LauchingConnection(this.client);
            
        }

        private void LauchingConnection(TcpClient client)
        {
            //Lambda used to pass parameters. Here, We set the thread which will listen the client's stream continuously.
            Thread thread = new Thread(o => Client_Controller((TcpClient)o));
            this.thread = thread;

            thread.Start(client);
        }

        //Return the TCP Client connected to the server.
        private TcpClient Initialize(Client current)
        {
            TcpClient client;
            current.currentCh = current.clientsCh.channels[0];

            client = new TcpClient();

            // Connecting the TCP Client to the server : port 3000 / IP Address 127.0.0.1
            try
            {
                client.Connect(current.GetIp(), 3000);
                GetServerChannelsClients(client);
                NetworkStream currStream = client.GetStream();
                this.currStream = currStream;
                SendMessage("new", current.name.ToUpper(), currStream);
            }
            catch (SocketException)
            {
                Console.WriteLine("Channel do not exist");
            }

            return client;
        }


        //get all the channels existing and clients connected and add them to the choices list.
        public void GetServerChannelsClients(TcpClient client)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string_server_channels = (List<string>)formatter.Deserialize(client.GetStream());
            string_server_clients = (List<string>)formatter.Deserialize(client.GetStream());
            foreach (String nameCh in string_server_channels)
            {
                channels_to_select.Items.Add(nameCh);
            }
            if (string_server_clients.Count > 1)
            {
                foreach (String nameCl in string_server_clients)
                {
                    if (nameCl != null)
                    {
                        client_to_connect.Items.Add(nameCl);
                    }
                }
            }
        }

        //Non stop checking what is said on the stream.
        void Client_Controller(TcpClient client)
        {
            NetworkStream currStream = client.GetStream();

            byte[] dataFromServer = new byte[1024];
            int byte_count;

            string streamStr = "";

            //Reset each time an action is done.
            int status = 0;

            while ((byte_count = currStream.Read(dataFromServer, 0, dataFromServer.Length)) > 0)
            {
                Thread.Sleep(2000);

                string data = Encoding.ASCII.GetString(dataFromServer, 0, byte_count);

                if (status == 0)
                {
                    streamStr = data;
                    status++;
                }
                else if (status == 1)
                {
                    Console.WriteLine("--------------------------- " + current.name + " --------------------------");
                    Console.WriteLine("FULL STREAM = " + streamStr);
                    string action_to_do = streamStr.Trim();
                    Console.WriteLine("ACTION TO DO = " + action_to_do);


                    if (Equals(action_to_do, "new") == true)
                    {
                        AppendNewDm(data.Trim());
                        status = 0;
                    }

                    else if ((Equals(action_to_do, "get_chan_msg") == true) || (Equals(action_to_do, "get_dm_msg") == true))
                    {
                        Console.WriteLine("text to display =" + data.Trim());
                        SetText(data.Trim(), false);
                        status = 0;
                    }

                    else if ((Equals(action_to_do, "update_ch") == true))
                    {
                        AppendNewChannel(data.Trim(), false);
                        status = 0;
                    }

                    else
                    {
                        streamStr = streamStr.Trim();
                        Console.WriteLine("STREAM STR = " + streamStr);
                        //Channel selected, verify if the channel correspond and display message else add new dm in the list.
                        if (acces_channel.Checked)
                        {
                            if (Equals(streamStr, current.currentCh.name) == true)
                            {
                                this.AppendText(data, false);
                            }
                            else
                            {
                                bool notachannel = true;
                                foreach (String names in string_server_channels)
                                {
                                    if (names == streamStr)
                                    {
                                        AppendNotif("chan");
                                        notachannel = false;
                                    }
                                }
                                if (notachannel == true)
                                {
                                    List<Direct> tmp = current.clientsDm.direct_list;
                                    if (current.clientsDm.direct_list.Count == 0) AddClient(streamStr, false);
                                    else
                                    {
                                        foreach (Direct p in tmp)
                                        {
                                            if (streamStr != p.name && streamStr != current.name.ToUpper())
                                            {
                                                add = true;
                                            }
                                            else
                                            {
                                                add = false;
                                                AppendNotif("dm");
                                            }
                                        }
                                        if (add == true)
                                        {
                                            AddClient(streamStr, false);
                                        }
                                    }
                                }

                            }
                        }

                        //DM selected, verify if the dm correspond and display message else add new dm in the list.
                        else if (acces_dm.Checked)
                        {
                            Console.WriteLine("DMMMMMMM");
                            if (Equals(streamStr.Trim(), current.currentDm.name) == true)
                            {
                                this.AppendText(data, false);
                            }
                            else
                            {
                                bool notachannel = true;
                                foreach (String names in string_server_channels)
                                {
                                    if (names == streamStr)
                                    {
                                        AppendNotif("chan");
                                        notachannel = false;
                                    }
                                }
                                if (notachannel == true)
                                {
                                    List<Direct> tmp2 = current.clientsDm.direct_list;
                                    try
                                    {
                                        if (current.clientsDm.direct_list.Count == 0)
                                        {
                                            Console.WriteLine("fezfzefez");
                                            AddClient(streamStr, false);
                                        }
                                        else
                                        {
                                            Console.WriteLine("ON RENTRE LA");
                                        }
                                        /*else
                                        {
                                            foreach (Direct p in tmp2)
                                            {
                                                if (streamStr != p.name && streamStr != current.name.ToUpper())
                                                {
                                                    add = true;
                                                    Console.WriteLine("putain");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("merde");
                                                    add = false;
                                                    AppendNotif("dm");
                                                }
                                            }
                                            if (add == true)
                                            {
                                                AddClient(streamStr, false);
                                            }
                                        }*/
                                    }
                                    catch(InvalidOperationException exception)
                                    {
                                        Console.WriteLine("PROBLEME : " + exception);
                                    }
                                }
                            }
                        }
                        status = 0;
                    }
                }
               
            }
      
        }

        //---------------------- Make different actions on the components of the client interface - NotifyPropertyChanged ----------------------

        //display messages in the chat
        public void AppendText(string str, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { AppendText(str); }));
            }
            else
            {
                DateTime timestamp = DateTime.Now;
                msg_area.AppendText(timestamp.ToLongTimeString() + "\t" + str + Environment.NewLine);
            }
        }

        //Once connected, add the dm to the dms list and remove it from the choices.
        public void AddClient(string str, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { AddClient(str); }));
            }
            else
            {
                dms_list.Items.Add(str);
                current.clientsDm.direct_list.Add(new Direct(str));
                client_to_connect.Items.Remove(str);
                dm_notif.Visible = true;
            }
        }

        public void AppendNotif(String st, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () {
                        if (st == "dm") {
                            AppendNotif("dm");
                        }
                        else
                        {
                            AppendNotif("chan");
                        }
                    }));
            }
            else
            {
                if (st == "dm") dm_notif.Visible = true;
                else chan_notif.Visible = true;
            }
        }

        //Set the text in the chat according to what's selected on the interface.
        public void SetText(string str, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { SetText(str); }));
            }
            else
            {
                msg_area.Clear();
                msg_area.AppendText(str + Environment.NewLine + Environment.NewLine);
                dm_notif.Visible = false;
                chan_notif.Visible = false;
            }
        }
        
        //Add new clients in the dms choice list if someone connects.
        public void AppendNewDm(string str, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { AppendNewDm(str); }));
            }
            else
            {
                client_to_connect.Items.Add(str);
            }
        }

        //Add new channel created in the channel choice list.
        public void AppendNewChannel(string str, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { AppendNewChannel(str); }));
            }
            else
            {
                channels_to_select.Items.Add(str);
            }
        }



        //--------------------------------- Send messages and set a current status ------------------------------------------


        //Specifically used to send messages 
        private void send_Click(object sender, EventArgs e)
        {
            if (send_message.Text == "EXIT")
            {
                client.Client.Shutdown(SocketShutdown.Send);
                thread.Join();
                currStream.Close();
                client.Close();
                Console.WriteLine("Disconnecting...");
            }

            else
            {
                if (acces_channel.Checked) {
                    string action = "msg";
                    byte[] action_buffer = Encoding.ASCII.GetBytes(action);
                    string current_channel_name = current.currentCh.name;
                    string print = current.name.ToUpper() + " : " + send_message.Text + "\n";
                    byte[] buffer = Encoding.ASCII.GetBytes(current_channel_name + "," + print);
                    currStream.Write(action_buffer, 0, action_buffer.Length);
                    currStream.Write(buffer, 0, buffer.Length);
                    this.AppendText(print, false);
                }

                else if (acces_dm.Checked)
                {
                    string action = "dmsg";
                    byte[] action_buffer = Encoding.ASCII.GetBytes(action);
                    string current_private_name = current.currentDm.name;
                    string display = current.name.ToUpper() + " : " + send_message.Text + "\n";
                    Console.WriteLine("ENVOI A = " + current_private_name);
                    byte[] buffer = Encoding.ASCII.GetBytes(current_private_name + "," + display);
                    currStream.Write(action_buffer, 0, action_buffer.Length);
                    currStream.Write(buffer, 0, buffer.Length);
                    this.AppendText(display, false);
                }
            }
        }

        //Only used like the prev method but to notify something, when something has to change on the interface. 
        private void SendMessage(string action, string data, NetworkStream currStream)
        {
            byte[] action_buffer = Encoding.ASCII.GetBytes(action+",");

            byte[] buffer = Encoding.ASCII.GetBytes(data);


            currStream.Write(action_buffer, 0, action_buffer.Length);
            currStream.Write(buffer, 0, buffer.Length);
        }

        //Set the selected dm client as the client's current dm automatically.
        private void dms_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            dms_list.Enabled = true;
            channels_list.Enabled = false;
            string current_private = (String)dms_list.SelectedItem;
            Console.WriteLine(current_private);
            foreach (Direct p in current.clientsDm.direct_list)
            {
                if (current_private == p.name)
                {
                    current.currentDm = p;
                    SendMessage("get_dm_msg", current.currentDm.name, currStream);
                }
            }


        }

        //Set the selected channel as the client's current channel automatically.
        private void channels_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            dms_list.Enabled = false;
            channels_list.Enabled = true;
            string current_channel = (String)channels_list.SelectedItem;
            Console.WriteLine(current_channel);
            foreach(Channel c in current.clientsCh.channels)
            {
                if (current_channel == c.name)
                {
                    current.currentCh = c;
                    SendMessage("get_chan_msg", current.currentCh.name, currStream);
                }
            }            
        }


        //Write the action to do in the stream, it will be got by server and analysed - CONNECT BUTTON HERE
        private void Connect_new_channel_dm_Click(object sender, EventArgs e)
        {
            NetworkStream currStream = client.GetStream();
            if (check_channel.Checked) 
            {
                string action = "conn";
                byte[] action_buffer = Encoding.ASCII.GetBytes(action);
                currStream.Write(action_buffer, 0, action_buffer.Length);

                string new_connexion_channel = (String)channels_to_select.SelectedItem;
                byte[] new_channel = Encoding.ASCII.GetBytes(new_connexion_channel);
                currStream.Write(new_channel, 0, new_channel.Length);

                channels_list.Items.Add(new_connexion_channel);
                current.clientsCh.channels.Add(new Channel(new_connexion_channel));
                channels_to_select.Items.Remove(new_connexion_channel);
            }

            else if (check_dm.Checked)
            {
                string action = "pconn";
                byte[] action_buffer = Encoding.ASCII.GetBytes(action);
                currStream.Write(action_buffer, 0, action_buffer.Length);

                string new_connexion_dm = (String)client_to_connect.SelectedItem;
                byte[] new_private = Encoding.ASCII.GetBytes(new_connexion_dm);
                currStream.Write(new_private, 0, new_private.Length);

                dms_list.Items.Add(new_connexion_dm);
                current.clientsDm.direct_list.Add(new Direct(new_connexion_dm));
                client_to_connect.Items.Remove(new_connexion_dm);
                //dm_notif.Visible = true;
            }
        }

        private void newCh_Click(object sender, EventArgs e)
        {
            NewChannel nCh = new NewChannel(currStream, this);
            nCh.Show();
        }



        //--------------- AUTOMATIC VERIFICATIONS FOR CHECKBOXES ---------------


        private void channels_list_Click(object sender, EventArgs e)
        {
            dms_list.Enabled = false;
            channels_list.Enabled = true;
        }


        private void dms_list_Click(object sender, EventArgs e)
        {
            dms_list.Enabled = true;
            channels_list.Enabled = false;
        }

        private void check_dm_CheckedChanged(object sender, EventArgs e)
        {
            if (check_dm.Checked == true)
            {
                channels_to_select.Enabled = false;
                client_to_connect.Enabled = true;
            }
        }


        private void check_channel_CheckedChanged(object sender, EventArgs e)
        {
            if (check_channel.Checked == true)
            {
                client_to_connect.Enabled = false;
                channels_to_select.Enabled = true;
            }
        }

        private void InitCheck()
        {
            check_channel.Checked = true;
            acces_channel.Checked = true;
        }

        private void acces_channel_CheckedChanged(object sender, EventArgs e)
        {
            if (acces_channel.Checked)
            {
                acces_dm.Checked = false;
                channels_list.Enabled = true;
                dms_list.Enabled = false;
            }
        }

        private void acces_private_CheckedChanged(object sender, EventArgs e)
        {
            if (acces_dm.Checked)
            {
                acces_channel.Checked = false;
                dms_list.Enabled = true;
                channels_list.Enabled = false;
            }
        }
    }
}
