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
using Bookkeeper.Model;

namespace Bookkeeper
{
    [Activity(Label = "New Entry")]
    public class NewEntryActivity : Activity
    {
        BookkeeperManager bm;
        RadioButton incomeRBtn;
        RadioButton expenseRBtn;
        EditText dateText;
        EditText descriptionText;
        Spinner typeSpin;
        Spinner accountSpin;
        EditText totalText;
        EditText taxText;
        Button addBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewEntry);
            bindLayout();

            typeSpin.Adapter = GetArrayAdapter(bm.ExpenseAccounts);
            accountSpin.Adapter = GetArrayAdapter(bm.MoneyAccounts); 
        }

        private ArrayAdapter<Account> GetArrayAdapter(List<Account> accounts)
        {
            return new ArrayAdapter<Account>(this, Resource.Layout.SpinnerItem, Resource.Id.SpinnerItemText, accounts);
        }

        private void bindLayout()
        {
            bm = new BookkeeperManager();
            incomeRBtn = FindViewById<RadioButton>(Resource.Id.newEntryRBtnIncome);
            expenseRBtn = FindViewById<RadioButton>(Resource.Id.newEntryRBtnExpense);
            dateText = FindViewById<EditText>(Resource.Id.newEntryDate);
            descriptionText = FindViewById<EditText>(Resource.Id.NewEntryDescription);
            typeSpin = FindViewById<Spinner>(Resource.Id.NewEntrySpinType);
            accountSpin = FindViewById<Spinner>(Resource.Id.NewEntrySpinAccount);
            totalText = FindViewById<EditText>(Resource.Id.NewEntryTotal);
            taxText = FindViewById<EditText>(Resource.Id.NewEntryTax);
            addBtn = FindViewById<Button>(Resource.Id.newEntryAddEntryBtn);
        }
    }
}