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
using SQLite;
using Bookkeeper.Models;
using Bookkeeper.Utils;

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
        TextView totalExMoms;
        Spinner taxSpin;
        Button addBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewEntry);
            bindLayout();
            entry = new Entry();

            typeSpin.Adapter = GetArrayAdapter(bm.GetAccounts(AccountType.Income));
            accountSpin.Adapter = GetArrayAdapter(bm.GetAccounts(AccountType.Money));
            taxSpin.Adapter = GetArrayAdapter(bm.GetTaxRates());

            initValues();
            initDelegates();
        }

        private void initDelegates()
        {
            rBtnGroup.CheckedChange += RBtnGroup_CheckedChange;
            dateBtn.Click += DateBtn_Click;
            descriptionText.TextChanged += DescriptionText_TextChanged;
            typeSpin.ItemSelected += TypeSpin_ItemSelected;
            accountSpin.ItemSelected += AccountSpin_ItemSelected;                      
            totalText.TextChanged += TotalText_TextChanged;
            taxSpin.ItemSelected += TaxSpin_ItemSelected;
            addBtn.Click += AddBtn_Click;
        }

        private void AccountSpin_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string id = spinner.GetItemAtPosition(e.Position).ToString();
            entry.AccountID = int.Parse(id);
        }

        private void TypeSpin_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string id = spinner.GetItemAtPosition(e.Position).ToString();
            entry.TypeID = int.Parse(id);
        }

        private void DescriptionText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            entry.Description = descriptionText.Text;
        }

        private void initValues()
        {
            incomeRBtn.Checked = entry.IsIncome;
            expenseRBtn.Checked = !entry.IsIncome;
            dateBtn.Text = entry.Date.ToShortDateString();
            descriptionText.Text = entry.Description;
            totalText.Text = "" + entry.Total;

            int position = 0;
            if(entry.TypeID != 0)
            {
                var list = bm.GetAccounts(AccountType.Income);
                Account currentAccount = list.Where(a => a.Number.Equals(entry.TypeID)).First();
                position = list.IndexOf(currentAccount);
            }            
            typeSpin.SetSelection(position);

            position = 0;
            if(entry.AccountID != 0)
            {
                var list = bm.GetAccounts(AccountType.Money);
                Account currentAccount = list.Where(a => a.Number.Equals(entry.AccountID)).First();
                position = list.IndexOf(currentAccount);
            }
            accountSpin.SetSelection(position);

            position = 0;
            if (entry.TaxRateID != 0)
            {
                var list = bm.GetTaxRates();
                TaxRate currentTaxRate = list.Where(tr => tr.Id.Equals(entry.TaxRateID)).First();
                position = list.IndexOf(currentTaxRate);
            }
            taxSpin.SetSelection(position);
        }

        private void TaxSpin_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string id = spinner.GetItemAtPosition(e.Position).ToString();
            entry.TaxRateID = int.Parse(id);
            ChangeTotalExMomsText();
        }

        private void TotalText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if(totalText.Text.Length > 0)
            {
                entry.Total = int.Parse(totalText.Text);
            }
            ChangeTotalExMomsText();
        }

        private void ChangeTotalExMomsText()
        {
            try
            {
                double tax = bm.GetTaxRate(entry.TaxRateID).Rate;
                double exMomsTotal = entry.Total / (1 + tax);
                totalExMoms.Text = "" + Math.Round(exMomsTotal, 2);
            }
            catch (Exception)
            {
                totalExMoms.Text = Resources.GetString(Resource.String.noValue);
            }
        }

        private void DateBtn_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                dateBtn.Text = time.ToShortDateString();
                entry.Date = time;
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            bm.addEntry(entry);
        }

        private void RBtnGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            if(incomeRBtn.Checked)
            {
                typeSpin.Adapter = GetArrayAdapter(bm.GetAccounts(AccountType.Income));
                entry.IsIncome = true;
            }
            if (expenseRBtn.Checked)
            {
                typeSpin.Adapter = GetArrayAdapter(bm.GetAccounts(AccountType.Expense));
                entry.IsIncome = false;
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
            totalExMoms = FindViewById<TextView>(Resource.Id.NewEntryTotalExMoms);
        }
    }
}