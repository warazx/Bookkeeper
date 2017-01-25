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

namespace Bookkeeper.Model
{
    class Account
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public Account(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }
}