using System;

namespace Bookkeeper.Utils
{
    public class JavaObjectWrapper : Java.Lang.Object
    {
        [SQLite.Ignore]
        public Object obj { get; set; }
    }
}