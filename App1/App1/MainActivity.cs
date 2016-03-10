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
        string constring = @"Server=tcp:qwertyuiop.database.windows.net,1433;Database=trytry;User ID=darkstar@qwertyuiop;Password=Mikun_4366644;Encrypt=False;Trusted_Connection=False;";

        ImageView[] iv = new ImageView[9];

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //  Button button = FindViewById<Button>(Resource.Id.MyButton);

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
            //Button btn = FindViewById<Button>(Resource.Id.but);
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

