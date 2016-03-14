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
    class Host : Activity
    {
        private int localPort;
        private Thread ServerThread;
        TcpListener listener;
        int gameID;

        public Player player1;
        public Player player2;

        public void Create(int port, Player pl, int gaid)
        {
            gameID = gaid;
            player1 = pl;
            localPort = port;
            ServerThread = new Thread(new ThreadStart(ServStart));
            ServerThread.Start();
        }

        public void Close()
        {
            listener.Stop();
            ServerThread.Abort();
            return;
        }

        private void ServStart()
        {
            Socket ClientSock;
            string data;
            byte[] cldata = new byte[1024];
            listener = new TcpListener(localPort);
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

                        FindViewById<TextView>(Resource.Id.connectedplayertv).Text += "\n"+player2.Login;

                        data = player1.Name;

                        ClientSock.Send(Encoding.ASCII.GetBytes(data));
                    }
                }
                catch
                {

                    ClientSock.Close();
                    listener.Stop();
                    ServerThread.Abort();
                }

            }
        }

    }
}