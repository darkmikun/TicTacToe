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

namespace App1
{
    [Activity(Label = "Activity2")]
    public class profilecode : Activity
    {
        public static bool isLogOut = false;

        User loguser = signincode.LoginedUser;

        TextView[] tv = new TextView[3];

        Button[] btn = new Button[2];

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.profile);

            tv[0] = FindViewById<TextView>(Resource.Id.ProfileLogin);
            tv[1] = FindViewById<TextView>(Resource.Id.Profilepassword);
            tv[2] = FindViewById<TextView>(Resource.Id.Profilename);

            btn[0] = FindViewById<Button>(Resource.Id.ChangeProfilebtn);
            btn[1] = FindViewById<Button>(Resource.Id.ExitProfilebtn);

            tv[0].Text += loguser.Login;
            tv[1].Text += loguser.Password;
            tv[2].Text += loguser.Name;

            btn[1].Click += delegate
              {
                  LogOutBTn_Click();
              };

            btn[0].Click += delegate
              {
                  ChangeProfileBTN_Click();
              };
        }

        public void LogOutBTn_Click()
        {
            signincode.LoginedUser.Clear();
            isLogOut = true;
            StartActivity(typeof(signincode));
            this.Finish();
        }

        public void ChangeProfileBTN_Click()
        {

        }
    }
}