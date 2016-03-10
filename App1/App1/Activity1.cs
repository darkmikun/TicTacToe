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
using System.Net.NetworkInformation;

namespace App1
{
    [Activity(Label = "blabla", MainLauncher = true)]
    public class Activity1 : Activity
    {
        static string constring = @"Server=tcp:qwertyuiop.database.windows.net,1433;Database=trytry;User ID=darkstar@qwertyuiop;Password=Mikun_4366644;Encrypt=False;Trusted_Connection=False;";

        SqlConnection con = new SqlConnection(constring);

        EditText[] et = new EditText[3];

        Button[] btn = new Button[3];

        TextView tv;

        CheckBox cb;

        ViewStates invis = Android.Views.ViewStates.Invisible;

        ViewStates vis = Android.Views.ViewStates.Visible;

        public static User LoginedUser;

        static string myHost = System.Net.Dns.GetHostName();
        string myIP = System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout1);
            // Create your application here

            //SqlCommand clear = new SqlCommand();
            //clear.CommandText = "truncate table Users";
            //clear.Connection = con;
            //con.Open();

            //clear.ExecuteNonQuery();
            //con.Close();

            SqlCommand loginWhileRemember = new SqlCommand();

            loginWhileRemember.CommandText = "select * from Users where IP=@ip";

            loginWhileRemember.Connection = con;

            loginWhileRemember.Parameters.Add(new SqlParameter("ip", myIP));


            string log = "";
            con.Open();
            SqlDataReader readWhileRemember = null;
            readWhileRemember = loginWhileRemember.ExecuteReader();

            int k = 0;
            List<int> rem = new List<int>();

            while (readWhileRemember.Read())
            {
                k++;
                rem.Add(readWhileRemember.GetInt32(7));
                log = readWhileRemember.GetString(1);
            }

            con.Close();

            if (k == 1 && rem[0] == 1)
            {
                CreateUser(log);
                StartActivity(typeof(MainActivity));
                this.Finish();
            }
            else
            {
                et[0] = FindViewById<EditText>(Resource.Id.LoginEdit);
                et[1] = FindViewById<EditText>(Resource.Id.PasswordEdit);
                et[2] = FindViewById<EditText>(Resource.Id.nameEdit);

                btn[0] = FindViewById<Button>(Resource.Id.btnEnter);
                btn[1] = FindViewById<Button>(Resource.Id.btnRegister);
                btn[2] = FindViewById<Button>(Resource.Id.SignUpbutton);

                tv = FindViewById<TextView>(Resource.Id.NameText);

                cb = FindViewById<CheckBox>(Resource.Id.RememberMeCheck);

                et[2].Visibility = invis;
                tv.Visibility = invis;
                btn[2].Visibility = invis;

                btn[0].Click += delegate
                  {
                      SigInBTN_Click();
                  };

                btn[1].Click += delegate
                {
                    RegistrationBTN_Click();
                };

                btn[2].Click += delegate
                 {
                     SigUpBTN_Click();
                 };
            }
        }

        public void RegistrationBTN_Click()
        {
            et[2].Visibility = vis;
            tv.Visibility = vis;
            btn[2].Visibility = vis;

            btn[0].Visibility = invis;
            btn[1].Visibility = invis;
        }

        public void SigUpBTN_Click()
        {
            string login = et[0].Text;
            string password = et[1].Text;
            string name = et[2].Text;

            int remember = Convert.ToInt32(cb.Checked);

            if (login == "" || password == "" || name == "")
            {
                AlertDialog.Builder a = new AlertDialog.Builder(this);
                a.SetMessage("One of the blocks blank . Please fill in all blocks");
                a.Show();
            }
            else
            {
                SqlDataReader chheck = check(login);

                int k = 0;

                while (chheck.Read())
                {
                    k++;
                    break;
                }

                con.Close();

                if (k == 0)
                {
                    SqlCommand register = new SqlCommand();

                    register.CommandText = "insert into Users (Login,Password,Name,IP,Online,Waiting,Remember) values (@login,@password,@name,@ip,@online,@waiting,@remember)";

                    register.Connection = con;

                    register.Parameters.Add(new SqlParameter("login", login));
                    register.Parameters.Add(new SqlParameter("password", password));
                    register.Parameters.Add(new SqlParameter("name", name));
                    register.Parameters.Add(new SqlParameter("ip", myIP));
                    register.Parameters.Add(new SqlParameter("online", 1));
                    register.Parameters.Add(new SqlParameter("waiting", 1));
                    register.Parameters.Add(new SqlParameter("remember", remember));

                    try
                    {
                        con.Open();
                        register.ExecuteNonQuery();
                        con.Close();

                        AlertDialog.Builder a = new AlertDialog.Builder(this);
                        a.SetMessage("Registration was succesfull");
                        a.Show();

                        CreateUser(login);
                        StartActivity(typeof(MainActivity));
                        this.Finish();
                    }
                    catch (Exception ex)
                    {
                        AlertDialog.Builder a = new AlertDialog.Builder(this);
                        a.SetMessage("Registration was not completed" + "\n\n" + ex.Message);
                        a.Show();
                    }
                }
                else
                {
                    AlertDialog.Builder a = new AlertDialog.Builder(this);
                    a.SetMessage("This login is already in use. Please try again");
                    a.Show();
                    con.Close();
                }
            }
        }

        public void SigInBTN_Click()
        {
            string login = et[0].Text;
            string password = et[1].Text;

            int remember = Convert.ToInt32(cb.Checked);

            if (login == "" || password == "")
            {
                AlertDialog.Builder a = new AlertDialog.Builder(this);
                a.SetMessage("One of the blocks blank . Please fill in all blocks");
                a.Show();
            }
            else
            {
                SqlDataReader chheck = check(login);

                int k = 0;

                while (chheck.Read())
                {
                    k++;

                    if (chheck.GetString(2) == password)
                    {
                        con.Close();

                        if (cb.Checked)
                        {
                            SqlCommand updateRemember = new SqlCommand();

                            updateRemember.CommandText = "update Users set Remember=1 where Login=@login";

                            updateRemember.Parameters.Add(new SqlParameter("login", login));

                            updateRemember.Connection = con;

                            con.Open();
                            updateRemember.ExecuteNonQuery();
                            con.Close();
                        }

                        CreateUser(login);
                        StartActivity(typeof(MainActivity));
                        this.Finish();
                        break;
                    }
                    else
                    {
                        AlertDialog.Builder a = new AlertDialog.Builder(this);
                        a.SetMessage("Incorrect password. Pleasetry again");
                        a.Show();
                        con.Close();
                        break;
                    }
                }
                if (k == 0)
                {
                    AlertDialog.Builder a = new AlertDialog.Builder(this);
                    a.SetMessage("There is nosuch user with this Login. Please try again");
                    a.Show();
                    con.Close();
                }
            }
        }

        public SqlDataReader check(string login)
        {
            SqlCommand isIn = new SqlCommand();

            isIn.CommandText = "select * from users where Login=@login";

            isIn.Parameters.Add(new SqlParameter("login", login));

            isIn.Connection = con;

            con.Open();
            return isIn.ExecuteReader();
        }

        public void CreateUser(string login)
        {
            SqlCommand selectuser = new SqlCommand("select Login,Password,Name from Users where Login=@login", con);

            selectuser.Parameters.Add(new SqlParameter("login", login));

            string pas = "";
            string n = "";

            con.Open();
            SqlDataReader readselecteduser = selectuser.ExecuteReader();
            while(readselecteduser.Read())
            {
                pas = readselecteduser.GetString(1);
                n = readselecteduser.GetString(2);
                break;
            }
            con.Close();

            LoginedUser = new User(login, pas, n);
        }
    }
}