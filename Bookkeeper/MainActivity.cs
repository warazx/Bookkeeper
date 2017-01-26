using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Bookkeeper
{
    [Activity(Label = "Bookkeeper", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button newEntryBtn;
        Button showAllEntries;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            newEntryBtn = FindViewById<Button>(Resource.Id.newEntryBtn);
            showAllEntries = FindViewById<Button>(Resource.Id.showAllEntriesBtn);

            newEntryBtn.Click += NewEntryBtn_Click;
            showAllEntries.Click += ShowAllEntries_Click;
        }

        private void ShowAllEntries_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ShowAllEntriesActivity));
            StartActivity(intent);
        }

        private void NewEntryBtn_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewEntryActivity));
            StartActivity(intent);
        }
    }
}

