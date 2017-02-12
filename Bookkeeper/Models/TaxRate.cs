using Bookkeeper.Utils;
using SQLite;

namespace Bookkeeper.Models
{
    public class TaxRate : Java.Lang.Object
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Rate { get; set; }

        /// <summary>
        /// Used to create a default TaxRate object.
        /// </summary>
        public TaxRate() { }

        /// <summary>
        /// Used to create a specified TaxRate object.
        /// </summary>
        public TaxRate(double rate)
        {
            Rate = rate;
        }

        public override string ToString()
        {
            return "" + Rate * 100 + "%";
        }
    }
}