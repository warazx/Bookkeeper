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
using Bookkeeper.Utils;

namespace Bookkeeper.Controllers
{
    [Activity(Label = "ShowAllEntriesActivity")]
    public class ShowAllEntriesActivity : Activity
    {
        ListView entryListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShowAllEntries);
            initLst();
        }

        private void initLst()
        {
            entryListView = FindViewById<ListView>(Resource.Id.list);
            var adapter = new EntryAdapter(this);
            entryListView.Adapter = adapter;
        }

        protected override void OnResume()
        {
            base.OnResume();
            initLst();
        }
    }
}