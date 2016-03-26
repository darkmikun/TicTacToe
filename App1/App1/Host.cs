using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace App1
{
    public class Host : Activity
    {
        private int localPort;
        private Thread ServerThread;
        TcpListener listener;
        int gameID;


        Socket ClientSock;

        public Player player1 = new Player();
        public Player player2 = new Player();

        public void Create(int port, Player pl, int gaid)
        {
            gameID = gaid;
            player1 = pl;
            localPort = port;
            //ServerThread = new Thread(new ThreadStart(ServStart));
            //ServerThread.Start();
            ServStart();
        }

        public void Close()
        {
            listener.Stop();
            ServerThread.Abort();
            return;
        }

        public void SandDate(string data)
        {
            ClientSock.Send(Encoding.ASCII.GetBytes(data));
        }

        private void ServStart()
        {
            string myHost = System.Net.Dns.GetHostName();
            string data;
            byte[] cldata = new byte[1024];
            listener = new TcpListener(System.Net.Dns.GetHostByName(myHost).AddressList[0], localPort);
            listener.Start();

            try
            {
                ClientSock = listener.AcceptSocket();
            }
            catch
            {
                ServerThread.Abort();
                return;
            }

            int i = 0;

            if (ClientSock.Connected)
            {
                try
                {
                    i = ClientSock.Receive(cldata);
                }
                catch
                { }

                try
                {
                    if (i > 0)
                    {
                        data = Encoding.ASCII.GetString(cldata).Trim();
                        string[] split = data.Split(' ');
                        player2.Login = split[0];
                        player2.Name = split[1];
                        player2.ip = split[2];
                        player2.Password = "";

                      //  TextView tv = FindViewById<TextView>(Resource.Id.connectedplayertv);
                       // tv.Text += "\n"+player2.Login;

                        data = player1.Name;

                        ClientSock.Send(Encoding.ASCII.GetBytes(data));

                        waitplayercode.btn.Click += delegate
                        {
                            ClientSock.Send(Encoding.ASCII.GetBytes("StartGame"));

                            StartActivity(typeof(TicTacToeGameCode));
                        };
                    }
                }
                catch(Exception ex)
                {
                    AlertDialog.Builder a = new AlertDialog.Builder(this);
                    a.SetMessage(ex.ToString());
                    a.Show();
                    ClientSock.Close();
                    listener.Stop();
                    //ServerThread.Abort();
                }

            }
        }

    }
}