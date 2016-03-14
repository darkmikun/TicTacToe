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
    [Activity(Label = "Activity3")]
    public class changeprofilecode : Activity
    {
        User loguser = signincode.LoginedUser;

        TextView[] tv = new TextView[3];

        Button btn;

        EditText[] et = new EditText[3];

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Changeprofile);

            tv[0] = FindViewById<TextView>(Resource.Id.changelogintv);
            tv[1] = FindViewById<TextView>(Resource.Id.changepasswordtv);
            tv[2] = FindViewById<TextView>(Resource.Id.changenametv);

            et[0] = FindViewById<EditText>(Resource.Id.changeloginedit);
            et[1] = FindViewById<EditText>(Resource.Id.changepasedit);
            et[2] = FindViewById<EditText>(Resource.Id.changenameedit);

            btn = FindViewById<Button>(Resource.Id.saveChangesbtn);

            et[0].Text = loguser.Login;
            et[1].Text = loguser.Password;
            et[2].Text = loguser.Name;

            btn.Click += delegate
              {
                  SaveChangesBTN_Click();
              };
        }

        public void SaveChangesBTN_Click()
        {
            string log = et[0].Text;
            string pas = et[1].Text;
            string nam = et[2].Text;


        }
    }
}