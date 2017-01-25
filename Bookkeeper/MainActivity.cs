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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            newEntryBtn = FindViewById<Button>(Resource.Id.newEntryBtn);

            newEntryBtn.Click += NewEntryBtn_Click;
        }

        private void NewEntryBtn_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewEntryActivity));
            StartActivity(intent);
        }
    }
}

