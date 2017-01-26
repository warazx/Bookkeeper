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
    [Activity(Label = "ShowAllEntriesActivity")]
    public class ShowAllEntriesActivity : Activity
    {
        ListView entryListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShowAllEntries);
            entryListView = FindViewById<ListView>(Resource.Id.list);
            var adapter = new EntryAdapter(this);
            entryListView.Adapter = adapter;
        }
    }
}