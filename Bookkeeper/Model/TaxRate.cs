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
    class TaxRate
    {
        public double Rate { get; set; }

        public TaxRate(double rate)
        {
            Rate = rate;
        }

        public override string ToString()
        {
            return "" + Rate * 100 + "%";
        }
    }
}