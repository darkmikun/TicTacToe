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
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
          //  Button button = FindViewById<Button>(Resource.Id.MyButton);

			ImageView[] iv = {FindViewById<ImageView>(Resource.Id.imageView4),
				FindViewById<ImageView>(Resource.Id.imageView5),
				FindViewById<ImageView>(Resource.Id.imageView6),
				FindViewById<ImageView>(Resource.Id.imageView7),
				FindViewById<ImageView>(Resource.Id.imageView8),
				FindViewById<ImageView>(Resource.Id.imageView9),
				FindViewById<ImageView>(Resource.Id.imageView10),
				FindViewById<ImageView>(Resource.Id.imageView11),
				FindViewById<ImageView>(Resource.Id.imageView12)};

			SqlConnection con = new SqlConnection ();
			con.ConnectionString = @"Server=tcp:qwertyuiop.database.windows.net,1433;Database=trytry;User ID=darkstar@qwertyuiop;Password=Mikun_4366644;Encrypt=False;Trusted_Connection=False;";

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
        }
    }
}

