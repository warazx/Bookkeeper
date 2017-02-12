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
    [Activity(Label = "Skapa rapporter")]
    public class CreateReportsActivity : Activity
    {
        Button taxReportBtn;
        Button accountReportBtn;
        TextView reportTextView;
        BookkeeperManager bm;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateReports);
            taxReportBtn = FindViewById<Button>(Resource.Id.taxReportBtn);
            accountReportBtn = FindViewById<Button>(Resource.Id.accountsReportBtn);
            reportTextView = FindViewById<TextView>(Resource.Id.reportTextView);
            bm = BookkeeperManager.Instance;

            taxReportBtn.Click += TaxReportBtnOnClick;
            accountReportBtn.Click += AccountReportBtnOnClick;
        }

        private void AccountReportBtnOnClick(object sender, EventArgs e)
        {
            reportTextView.Text = bm.GetAccountReport();
        }

        private void TaxReportBtnOnClick(object sender, EventArgs e)
        {
            reportTextView.Text = bm.GetTaxReport();
        }
    }
}