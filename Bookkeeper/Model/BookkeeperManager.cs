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
    class BookkeeperManager
    {
        public List<Entry> Entries { get; set; }
        public List<Account> ExpenseAccounts { get; set; }
        public List<Account> IncomeAccounts { get; set; }
        public List<Account> MoneyAccounts { get; set; }        public List<TaxRate> TaxRates { get; set; }

        public BookkeeperManager()
        {
            ExpenseAccounts = new List<Account> { new Account("Computer", 585),
                                                  new Account("Supplies", 631),
                                                  new Account("Labour & Welfare", 597) };

            IncomeAccounts = new List<Account> { new Account("Rental", 400),
                                                 new Account("Interest", 420),
                                                 new Account("Sales", 440) };

            MoneyAccounts = new List<Account> { new Account("Assets", 211),
                                                new Account("Founds", 224),
                                                new Account("Project", 245)};
        }
    }
}