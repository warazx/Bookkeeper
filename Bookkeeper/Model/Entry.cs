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
    class Entry
    {
        public bool IsIncome { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Account { get; set; }
        public int Total { get; set; }
        public int TaxRate { get; set; }
    }
}