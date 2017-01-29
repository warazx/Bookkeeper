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

namespace Bookkeeper.Controllers
{
    [Activity(Label = "CreateReportsActivity")]
    public class CreateReportsActivity : Activity
    {
        Button taxReportBtn;
        Button accountReportBtn;
        TextView reportTextView;
        BookkeeperManager bm;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.createreports);
            taxReportBtn = FindViewById<Button>(Resource.Id.taxReportBtn);
            accountReportBtn = FindViewById<Button>(Resource.Id.accountsReportBtn);
            reportTextView = FindViewById<TextView>(Resource.Id.reportTextView);
            bm = new BookkeeperManager();

            taxReportBtn.Click += TaxReportBtn_Click;
            accountReportBtn.Click += AccountReportBtn_Click;
        }

        private void AccountReportBtn_Click(object sender, EventArgs e)
        {
            reportTextView.Text = bm.GetAccountReport();
        }

        private void TaxReportBtn_Click(object sender, EventArgs e)
        {
            reportTextView.Text = bm.GetTaxReport();
        }
    }
}