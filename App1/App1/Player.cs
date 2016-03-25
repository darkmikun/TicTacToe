using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;

namespace App1
{
    public class Player : User
    {
        //public string playerIP { get; set; }

        public string data; 
        byte[] remdata = { };
        public TcpClient Client = new TcpClient();
        public string ip;
        public int port;

        Socket Sock = null;

        public bool connectionResult = false;

        public Player()
        {
            ip = "";
            port = 0;
        }
        
        public string ReseiveData()
        {
            try
            {
                byte[] receive = new byte[1024];

                Sock.Receive(receive);

                return Encoding.ASCII.GetString(receive).Trim();
            }
            catch(Exception ex)
            {
                return "NODATA";
            }
        }

        public void Close()
        {
            Sock.Close();
            Client.Close();
        }
        
        public Player(User us,string i, int por )
        {
            Name = us.Name;
            Login = us.Login;
            Password = us.Password;

            ip = i;

            port = por;

            

            try
            {
                Client.Connect(ip, port);
                connectionResult = true;
            }
            catch(Exception ex)
            {
                
                return;
            }
            if (connectionResult)
            {


                Socket Sock = Client.Client;

                data = Login + " " + Name + " " + ip;
                Sock.Send(Encoding.ASCII.GetBytes(data));

                byte[] receive = new byte[1024];

                Sock.Receive(receive);

                data= Encoding.ASCII.GetString(receive).Trim();

                Toast.MakeText(Application.Context,"Connected to " + data,ToastLength.Short).Show();
                

                
            }
            
            //Sock.Receive(remdata);

            
        }
    }
}