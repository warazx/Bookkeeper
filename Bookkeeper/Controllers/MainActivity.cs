using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Bookkeeper.Controllers;

namespace Bookkeeper.Controllers
{
    [Activity(Label = "Bookkeeper", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button newEntryBtn;
        Button showAllEntries;
        Button createReports;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            newEntryBtn = FindViewById<Button>(Resource.Id.newEntryBtn);
            showAllEntries = FindViewById<Button>(Resource.Id.showAllEntriesBtn);
            createReports = FindViewById<Button>(Resource.Id.createReportsBtn);

            newEntryBtn.Click += NewEntryBtnOnClick;
            showAllEntries.Click += ShowAllEntriesOnClick;
            createReports.Click += CreateReportsOnClick;
        }

        private void CreateReportsOnClick(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(CreateReportsActivity));
            StartActivity(intent);
        }

        private void ShowAllEntriesOnClick(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ShowAllEntriesActivity));
            StartActivity(intent);
        }

        private void NewEntryBtnOnClick(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewEntryActivity));
            StartActivity(intent);
        }
    }
}

