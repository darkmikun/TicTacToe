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
    [Activity(Label = "TicTacToeGameCode")]
    public class TicTacToeGameCode : Activity
    {
        public ImageView[] iv = new ImageView[3];
        public ImageView[] field = new ImageView[9];

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TicTacToe);

            iv[0] = FindViewById<ImageView>(Resource.Id.iv1);
            iv[1] = FindViewById<ImageView>(Resource.Id.iv2);
            iv[2] = FindViewById<ImageView>(Resource.Id.iv3);

            field[0] = FindViewById<ImageView>(Resource.Id.iv4);
            field[1] = FindViewById<ImageView>(Resource.Id.iv5);
            field[2] = FindViewById<ImageView>(Resource.Id.iv6);
            field[3] = FindViewById<ImageView>(Resource.Id.iv7);
            field[4] = FindViewById<ImageView>(Resource.Id.iv8);
            field[5] = FindViewById<ImageView>(Resource.Id.iv9);
            field[6] = FindViewById<ImageView>(Resource.Id.iv10);
            field[7] = FindViewById<ImageView>(Resource.Id.iv11);
            field[8] = FindViewById<ImageView>(Resource.Id.iv12);

            iv[0].SetImageResource(Resource.Drawable.x);
            iv[1].SetImageResource(Resource.Drawable.x);
            iv[2].SetImageResource(Resource.Drawable.o);

            bool myQueue;
            int queuenumb;

            if(waitplayercode.isHost)
            {
                myQueue = true;
                queuenumb = 0;
            }
            else
            {
                myQueue = false;
                queuenumb = 2;
            }
            m1:

            if (!myQueue)
            {
                string data = MainActivity.pl.ReseiveData();

                fillField(Convert.ToInt32(data),ref queuenumb,ref myQueue);
            }
            for (int i = 0; i < 9; i++)
            {
                field[i].Click += delegate
                {
                    waitplayercode.imhost.SandDate(Convert.ToString(i));
                };
            }
        }
        
        public void fillField(int id,ref int qn,ref bool q)
        {
            field[id].SetImageDrawable(iv[1].Drawable);

            iv[1].SetImageDrawable(iv[qn].Drawable);

            if(qn==0)
            {
                qn = 2;
            }
            else
            {
                qn = 0;
            }

            q = !q;
        }
    }
}