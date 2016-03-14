using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
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
    [Activity(Label = "Activity4")]
    public class waitplayercode : Activity
    {
        public TextView tv;

        Button btn;

      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.waitplayer);

            tv = FindViewById<TextView>(Resource.Id.connectedplayertv);

            btn = FindViewById<Button>(Resource.Id.startbtn);

            

            Host imhost = new Host();
            Random rnd = new Random();
            int port = rnd.Next(9999);
            Player I = new Player(signincode.LoginedUser, System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString(), port);

            SqlConnection con = new SqlConnection(MainActivity.constring);

            SqlCommand creategame = new SqlCommand("insert into Games (hostName,hostIP,hostPort) values (@name,@IP,@port)", con);
            creategame.Parameters.Add(new SqlParameter("name", I.Login));
            creategame.Parameters.Add(new SqlParameter("IP", I.ip));
            creategame.Parameters.Add(new SqlParameter("port", I.port));

            con.Open();
            creategame.ExecuteNonQuery();
            con.Close();

            SqlCommand getGameId = new SqlCommand("select Id from Games where hostName=@name", con);
            getGameId.Parameters.Add(new SqlParameter("name", I.Login));
            int gameid = 0;
            con.Open();
            SqlDataReader readId = getGameId.ExecuteReader();
            while(readId.Read())
            {
                gameid = readId.GetInt32(0);
                break;
            }

            tv.Text = "";
            tv.Text += I.Name + "\n";
            imhost.Create(rnd.Next(9999),I,gameid);
            //imhost.Close();
        }

       
    }

   
}