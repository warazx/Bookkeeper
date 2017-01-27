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

            rBtnGroup.CheckedChange += RBtnGroup_CheckedChange;
            addBtn.Click += AddBtn_Click;
            dateBtn.Click += DateBtn_Click;
            totalText.TextChanged += TotalText_TextChanged;
            taxSpin.ItemSelected += TaxSpin_ItemSelected;
        }

        private void initValues()
        {
            incomeRBtn.Checked = entry.IsIncome;
            expenseRBtn.Checked = !entry.IsIncome;
            dateBtn.Text = entry.Date.ToShortDateString();
            descriptionText.Text = entry.Description;
            //typeSpin.SetSelection(taxSpin.Adapter
        }

        private void TaxSpin_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ChangeTotalExMomsText();
        }

        private void TotalText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ChangeTotalExMomsText();
        }

        private void ChangeTotalExMomsText()
        {
            try
            {
                int index = taxSpin.SelectedItemPosition;
                double tax = bm.GetTaxRate(index+1).Rate;
                double exMomsTotal = int.Parse(totalText.Text) / (1 + tax);
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
                bm.GetAccounts(AccountType.Income)[typeSpin.SelectedItemPosition].Number :
                bm.GetAccounts(AccountType.Expense)[typeSpin.SelectedItemPosition].Number);            
            int accountID = bm.GetAccounts(AccountType.Money)[accountSpin.SelectedItemPosition].Number;
            int total = int.Parse(totalText.Text);
            int taxRate = bm.GetTaxRate(taxSpin.SelectedItemPosition).Id;

            Entry newEntry = new Entry(isIncome, date, description, typeID, accountID, total, taxRate);
            bm.addEntry(newEntry);
        }

        private void RBtnGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            if(incomeRBtn.Checked)
            {
                typeSpin.Adapter = GetArrayAdapter(bm.GetAccounts(AccountType.Income));
            }
            if (expenseRBtn.Checked)
            {
                typeSpin.Adapter = GetArrayAdapter(bm.GetAccounts(AccountType.Expense));
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