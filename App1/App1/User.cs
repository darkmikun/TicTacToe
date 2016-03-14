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
    public class User
    {
        private string login;

        private string password;

        private string name;

        public User()
        { }

        public User(string log, string pas, string n)
        {
            Login = log;
            Password = pas;
            Name = n;
        }

        public User(User us)
        {
            Login = us.login;
            Password = us.Password;
            Name = us.Name;
        }

        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public void Clear()
        {
            Login = "";
            Password = "";
            Name = "";
        }
    }
}