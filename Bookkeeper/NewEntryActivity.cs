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
        Entry entry;
        BookkeeperManager bm;
        RadioGroup rBtnGroup;
        RadioButton incomeRBtn;
        RadioButton expenseRBtn;
        Button dateBtn;
        EditText descriptionText;
        Spinner typeSpin;
        Spinner accountSpin;
        EditText totalText;
        Spinner taxSpin;
        Button addBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewEntry);
            bindLayout();

            typeSpin.Adapter = GetArrayAdapter(bm.IncomeAccounts);
            accountSpin.Adapter = GetArrayAdapter(bm.MoneyAccounts);
            taxSpin.Adapter = GetArrayAdapter(bm.TaxRates);
            entry = new Entry();
            rBtnGroup.CheckedChange += RBtnGroup_CheckedChange;
            addBtn.Click += AddBtn_Click;
            dateBtn.Click += DateBtn_Click;
        }

        private void DateBtn_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                dateBtn.Text = time.ToLongDateString();
                entry.Date = time;
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            bool isIncome = incomeRBtn.Checked;
            DateTime date = entry.Date;
            string description = descriptionText.Text;
            int typeID = (isIncome ?
                bm.IncomeAccounts[typeSpin.SelectedItemPosition].Number :
                bm.ExpenseAccounts[typeSpin.SelectedItemPosition].Number);            
            int accountID = bm.MoneyAccounts[accountSpin.SelectedItemPosition].Number;
            int total = Int32.Parse(totalText.Text);
            double taxRate = bm.TaxRates[taxSpin.SelectedItemPosition].Rate;

            Entry newEntry = new Entry(isIncome, date, description, typeID, accountID, total, taxRate);
            bm.addEntry(newEntry);
        }

        private void RBtnGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            if(incomeRBtn.Checked)
            {
                typeSpin.Adapter = GetArrayAdapter(bm.IncomeAccounts);
            }
            if (expenseRBtn.Checked)
            {
                typeSpin.Adapter = GetArrayAdapter(bm.ExpenseAccounts);
            }
        }

        private ArrayAdapter<T> GetArrayAdapter<T>(List<T> items)
        {
            return new ArrayAdapter<T>(this, Resource.Layout.SpinnerItem, Resource.Id.SpinnerItemText, items);
        }

        private void bindLayout()
        {
            bm = new BookkeeperManager();
            rBtnGroup = FindViewById<RadioGroup>(Resource.Id.RBtnGroup);
            incomeRBtn = FindViewById<RadioButton>(Resource.Id.newEntryRBtnIncome);
            expenseRBtn = FindViewById<RadioButton>(Resource.Id.newEntryRBtnExpense);
            dateBtn = FindViewById<Button>(Resource.Id.newEntryDateBtn);
            descriptionText = FindViewById<EditText>(Resource.Id.NewEntryDescription);
            typeSpin = FindViewById<Spinner>(Resource.Id.NewEntrySpinType);
            accountSpin = FindViewById<Spinner>(Resource.Id.NewEntrySpinAccount);
            totalText = FindViewById<EditText>(Resource.Id.NewEntryTotal);
            taxSpin = FindViewById<Spinner>(Resource.Id.NewEntrySpinTax);
            addBtn = FindViewById<Button>(Resource.Id.newEntryAddEntryBtn);
        }
    }
}