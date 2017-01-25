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
        public string Date { get; set; }
        public string Description { get; set; }
        public int TypeID { get; set; }
        public int AccountID { get; set; }
        public int Total { get; set; }
        public double TaxRate { get; set; }

        public Entry()
        {
            IsIncome = true;
            Date = "";
            Description = "";
            TypeID = 0;
            AccountID = 0;
            Total = 0;
            TaxRate = 0;
        }

        public Entry(bool isIncome, string date, string description, int typeID, int accountID, int total, double taxRate)
        {
            IsIncome = isIncome;
            Date = date;
            Description = description;
            TypeID = typeID;
            AccountID = accountID;
            Total = total;
            TaxRate = taxRate;
        }
    }
}