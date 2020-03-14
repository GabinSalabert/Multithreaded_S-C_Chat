using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Side
{
    public partial class Server_status : Form
    {

        static readonly object lock_obj = new object();

        //All clients connecting to the server will be set in the dictionnary. ID + Client
        static Dictionary<int, Client> list_clients = new Dictionary<int, Client>();
        Channels server_channels;
       //List<String> lastConnected;

        public Server_status()
        {

            InitializeComponent();

            //Set the server's channel list with all channels created until today.
            server_channels = new Channels();
            if (File.Exists("AllChannels.bin"))
            {
                server_channels = server_channels.deserialize();
            }
            else
            {
                server_channels.listChannels.Add(new Channel("General"));
            }


            Connection(3000);


        }

        public void Connection(int port)
        {
            int count = 1;

            //Initialize server sockets and listener waiting for clients connexion
            IPAddress.Parse("127.0.0.1");
            TcpListener ServerSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            ServerSocket.Start();


            //Waiting for a connexion, and then redirect the client automatically in the server's client list.
            while (true)
            {
                Client client = new Client(ServerSocket.AcceptTcpClient());
                //lastConnected = new List<String>();
                //lastConnected.Add(client.name);

                //Mutual exclusion to not access the same instance at the same time
                lock (lock_obj) list_clients.Add(count, client);


                //Serialize on the stream every channel allowed and connected clients.
                List<string> string_server_channels = Convert_to_string();
                List<string> string_server_clients = Convert_to_string2();

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(client.tcpclient.GetStream(), string_server_channels);
                bf.Serialize(client.tcpclient.GetStream(), string_server_clients);

                //Multithreaded app to split every connected clients in 1 thread.
                Thread t = new Thread(Server_Controller);
                t.Start(count);
                count++;
            }

        }

        //Set a List of Strings with the server channels list.
        public List<string> Convert_to_string()
        {
            List<string> stringlist = new List<string>();
            foreach (Channel c in server_channels.listChannels)
            {
                stringlist.Add(c.name);
            }
            return stringlist;
        }

        //Set a List of Strings with the server clients list.
        public List<string> Convert_to_string2()
        {
            List<string> stringlist = new List<string>();
            foreach (Client c in list_clients.Values)
            {
                stringlist.Add(c.name);
                stringlist = stringlist.Distinct().ToList();
            }
            return stringlist;
        }


        //Read on the client's stream the action wanna do and then execute according to it.
        public void Server_Controller(object o)
        {
            int id = (int)o;
            TcpClient client;

            //Not access the same client at the same time.
            lock (lock_obj) client = list_clients[id].tcpclient;


            StreamWriter sW = new StreamWriter(client.GetStream());


            while (true)
            {
                NetworkStream stream = client.GetStream();

                byte[] msg = new byte[1024]; ;

                //Check if there is something to do or not.
                int action_count = stream.Read(msg, 0, msg.Length);
                if (action_count == 0)
                {
                    break;
                }

                string action = Encoding.ASCII.GetString(msg, 0, action_count);


                //Set split parameters to get the actions array.
                string[] spliter = { "," };
                int count = 2;
                string[] actions = action.Split(spliter, count, StringSplitOptions.RemoveEmptyEntries);


                //Check what's the purpose of the action and then replay by doing the according operation.

                //Got a connexion, then add the client to other clients interface.
                if (Equals(actions[0].Trim(), "new") == true)
                {

                    list_clients[id].name = actions[1];
                    addnew(actions[1], client);
                }

                else
                {
                    byte[] buffer = new byte[1024];
                    int byte_count = stream.Read(buffer, 0, buffer.Length);

                    if (byte_count == 0)
                    {
                        break;
                    }

                    //Send all the channel chat to the client.
                    else if (Equals(action.Trim(), "get_chan_msg,") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                        foreach (Channel c in list_clients[id].active_channels.listChannels)
                        {
                            if (c.name == data)
                            {
                                send_channel_text(c.channel_text, client); 
                            }
                        }

                    }

                    //Send all the dm tchat text to the client.
                    else if (Equals(action.Trim(), "get_dm_msg,") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                        foreach (Direct p in list_clients[id].dms.listDms)
                        {
                            foreach (Client c in p.dm_between)
                            {
                                if (c.name == data)
                                {
                                    Console.WriteLine(p.text);
                                }
                                else send_Direct_text(p.text, c.tcpclient);
                            }
                        }
                    }

                    //Send a message to all clients connected to the channel.
                    else if (Equals(action.Trim(), "msg") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);


                        string[] splited = data.Split(spliter, count, StringSplitOptions.RemoveEmptyEntries);
                        string channel = splited[0];
                        data = splited[1];
                        foreach (Channel c in list_clients[id].active_channels.listChannels)
                        {
                            if (Equals(channel.Trim(), c.name))
                            {
                                DateTime timestamp = DateTime.Now;
                                c.channel_text += Environment.NewLine + timestamp.ToLongTimeString() + "\t" + data ;
                            }
                        }
                        broadcast(data, client, channel);
                    }

                    //Send a dm to someone, even if he's not currently on the dm interface.
                    else if (Equals(action.Trim(), "dmsg") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);


                        string[] splited = data.Split(spliter, count, StringSplitOptions.RemoveEmptyEntries);
                        string who = splited[0];
                        data = splited[1];
                        foreach (Direct p in list_clients[id].dms.listDms)
                        {
                            foreach (Client c in p.dm_between)
                            {
                                if (Equals(who.Trim(), c.name))
                                {
                                    DateTime timestamp = DateTime.Now;
                                    p.text += Environment.NewLine + timestamp.ToLongTimeString() + "\t" + data;
                                }
                                else
                                {
                                    Directsend(data, c);
                                }
                            }
                        }
                    }

                    //Send a message to everyone when someone connects to a Channel and add it to his current channels.
                    else if (Equals(action.Trim(), "conn") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                        Client c = list_clients[id];
                        foreach (Channel channel in server_channels.listChannels)
                        {
                            if (Equals(channel.name, data.Trim()) == true)
                            {
                                c.active_channels.listChannels.Add(channel);
                                string newconn = c.name + " is now connected";
                                channel.channel_text += c.name + " is now connected" + Environment.NewLine;
                                broadcast(newconn, client, data.Trim());
                            }
                        }
                    }

                    //Send a message to the client in dm saying someone wants to speak and add it to his dm list.
                    else if (Equals(action.Trim(), "pconn") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                        Client c = list_clients[id];

                        foreach (Client clients in list_clients.Values)
                        {
                            if (Equals(clients.name, data.Trim()) == true)
                            {
                                Direct Directconn = new Direct(c, clients);
                                c.dms.listDms.Add(Directconn);
                                clients.dms.listDms.Add(Directconn);
                                string newconn = c.name + " wants to speak with you";
                                Directconn.text += c.name + "wants to speak with you";
                                Directsend(newconn, c);
                            }
                        }
                    }

                    //Update list of channels because someone created one new.
                    else if (Equals(action.Trim(), "newCh") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                        server_channels.listChannels.Add(new Channel(data.Trim()));
                        server_channels.serialize(server_channels);
                        foreach (Channel channel in server_channels.listChannels)
                        {
                            if (Equals(channel.name, data.Trim()) == true)
                            {
                                foreach(Client c in list_clients.Values)
                                {
                                    update_channels(channel.name, c.tcpclient);
                                }
                            }
                        }
                    }
                }
            }
            lock (lock_obj) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        //Send to all connected clients who are currently in this channel.
        public static void broadcast(string data, TcpClient nosend, string channel)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferActions = Encoding.ASCII.GetBytes(channel);
            lock (lock_obj)
            {
                foreach (Client c in list_clients.Values)
                {
                    TcpClient cou = c.tcpclient;
                    if (cou != nosend)
                    {
                        foreach (Channel chan in c.active_channels.listChannels)
                        {
                            if (chan.name == channel.Trim())
                            {
                                NetworkStream stream = cou.GetStream();
                                stream.Write(bufferActions, 0, bufferActions.Length);
                                stream.Write(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
            }
        }

        //Send to the client who's not the sender in the DM between. 
        public static void Directsend(string data, Client nosend)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferActions = Encoding.ASCII.GetBytes(nosend.name);

            lock (lock_obj)
            {
               foreach (Direct pr in nosend.dms.listDms)
                {
                 foreach (Client cl in pr.dm_between)
                 {
                  if (cl.name != nosend.name.Trim())
                    {
                        NetworkStream stream = cl.tcpclient.GetStream();
                        stream.Write(bufferActions, 0, bufferActions.Length);
                        stream.Write(buffer, 0, buffer.Length);
                     }
                   }
                } 
            }
        }

        //Send all the channel text.
        public static void send_channel_text(string data, TcpClient send)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferActions = Encoding.ASCII.GetBytes("get_chan_msg");

            lock (lock_obj)
            {
                NetworkStream stream = send.GetStream();
                stream.Write(bufferActions, 0, bufferActions.Length);
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        //Update channels list (choice)
        public static void update_channels(string data, TcpClient send)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferActions = Encoding.ASCII.GetBytes("update_ch");

            lock (lock_obj)
            {
                NetworkStream stream = send.GetStream();
                stream.Write(bufferActions, 0, bufferActions.Length);
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        //Send all the Dm text.
        public static void send_Direct_text(string data, TcpClient send)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferActions = Encoding.ASCII.GetBytes("get_dm_msg");

            lock (lock_obj)
            {
                NetworkStream stream = send.GetStream();
                stream.Write(bufferActions, 0, bufferActions.Length);
                stream.Write(buffer, 0, buffer.Length);
                System.Threading.Thread.Sleep(500);
            }
        }

        //notify every clients that someone has connected and then can send a dm to him.
        public static void addnew(string data, TcpClient nosend)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferActions = Encoding.ASCII.GetBytes("new");

            lock (lock_obj)
            {
                foreach (Client c in list_clients.Values)
                {
                    TcpClient allC = c.tcpclient;
                    if (allC != nosend)
                    {
                        NetworkStream stream = allC.GetStream();
                        stream.Write(bufferActions, 0, bufferActions.Length);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}
