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
using Bookkeeper.Models;
using Bookkeeper.Controllers;

namespace Bookkeeper.Utils
{
    public class EntryAdapter : BaseAdapter<Entry>
    {
        BookkeeperManager bm;
        Activity context;
        List<Entry> entries;

        public EntryAdapter(Activity context)
        {
            bm = new BookkeeperManager();
            this.context = context;
            entries = bm.GetEntries();
        }

        public override Entry this[int position]
        {
            get
            {
                return entries.ElementAt(position);
            }
        }

        public override int Count
        {
            get
            {
                return entries.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return new JavaObjectWrapper() { obj = entries[position] };
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.EntryItem, null);
            view.FindViewById<TextView>(Resource.Id.textView).Text = entries.ElementAt(position).ToString();
            view.Click += delegate
            {
                Intent intent = new Intent(context, typeof(NewEntryActivity));
                intent.PutExtra("entryId", (int)entries.ElementAt(position).Id);
                context.StartActivity(intent);
            };
            return view;
        }
    }
}