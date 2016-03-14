using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Data.Sql;
using System.Data.SqlClient;


namespace App1
{
    [Activity(Label = "App1", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;
        public static string constring = @"Server=tcp:qwertyuiop.database.windows.net,1433;Database=trytry;User ID=darkstar@qwertyuiop;Password=Mikun_4366644;Encrypt=False;Trusted_Connection=False;";

        ImageView[] iv = new ImageView[9];

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Menu);
            
            iv[0] = FindViewById<ImageView>(Resource.Id.imageView4);
            iv[1] = FindViewById<ImageView>(Resource.Id.imageView5);
            iv[2] = FindViewById<ImageView>(Resource.Id.imageView6);
            iv[3] = FindViewById<ImageView>(Resource.Id.imageView7);
            iv[4] = FindViewById<ImageView>(Resource.Id.imageView8);
            iv[5] = FindViewById<ImageView>(Resource.Id.imageView9);
            iv[6] = FindViewById<ImageView>(Resource.Id.imageView10);
            iv[7] = FindViewById<ImageView>(Resource.Id.imageView11);
            iv[8] = FindViewById<ImageView>(Resource.Id.imageView12);

            Button btn = FindViewById<Button>(Resource.Id.button1);
            
            string myHost = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();
            btn.Click += delegate {
                AlertDialog.Builder a = new AlertDialog.Builder(this);
                a.SetMessage(myIP);
                a.Show();
            };

            SqlConnection con = new SqlConnection (constring);

			try
			{
				con.Open();
                AlertDialog.Builder a = new AlertDialog.Builder(this);
				a.SetMessage(con.ConnectionString);
				a.Show ();
				con.Close();
			}
			catch(Exception ex) 
			{
				AlertDialog.Builder a = new AlertDialog.Builder (this);
				a.SetMessage(ex.ToString ());
				a.Show ();
			}


            SqlCommand clear = new SqlCommand();
            clear.CommandText = "truncate table pictures";
            clear.Connection = con;
            con.Open();

            clear.ExecuteNonQuery();
            con.Close();


            SqlDependency.Start(constring);



            iv[0].Click += delegate
            {
                //iv[0].SetImageResource(Resource.Drawable.allisvarybad);

                SqlCommand ins = new SqlCommand();
                ins.CommandText = "insert into pictures (picturenumb) values (0)";
                ins.Connection = con;

                

                SqlDependency sqd = new SqlDependency(ins);


                try
                {
                    

                    con.Open();
                    ins.ExecuteNonQuery();
                    sqd.OnChange += new OnChangeEventHandler(OnDependencyChange);
                    con.Close();
                }
                catch (Exception ex)
                {
                    AlertDialog.Builder a = new AlertDialog.Builder(this);
                    a.SetMessage(ex.ToString());
                    a.Show();
                }
            };


            iv[1].SetImageResource(Resource.Drawable.profile);
            iv[1].Click += delegate
              {
                  StartActivity(typeof(profilecode));
                  this.Finish();
              };
            iv[2].SetImageResource(Resource.Drawable.startgame);
            iv[2].Click += delegate
              {
                  StartActivity(typeof(waitplayercode));
                  
              };
            iv[3].SetImageResource(Resource.Drawable.findgame);
            iv[3].Click += delegate
              {
                  FindGame();
              };
        }

        public void FindGame()
        {
            SqlConnection con = new SqlConnection(constring);

            SqlCommand findHost = new SqlCommand("select top 1 * from Games",con);

            SqlDataReader readHost;

            con.Open();
            readHost = findHost.ExecuteReader();
            while(readHost.Read())
            {
                Player pl = new Player(signincode.LoginedUser, readHost.GetString(2), readHost.GetInt32(3));
                break;
            }
        }

        void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            // Handle the event (for example, invalidate this cache entry).

            SqlConnection con = new SqlConnection(constring);

            using (SqlCommand sel = new SqlCommand("select * from pictures", con))
            {
                using (SqlDataReader read = sel.ExecuteReader())
                {
                    while (read.Read())
                    {
                        iv[read.GetInt32(1)].SetImageResource(Resource.Drawable.allisvarybad);
                    }
                }
            }
        }
    }
}

