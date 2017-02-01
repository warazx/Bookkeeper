using SQLite;

namespace Bookkeeper.Models
{
    public class TaxRate
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; private set; }
        public double Rate { get; set; }        

        public TaxRate(double rate)
        {
            Rate = rate;
        }

        public TaxRate() { }

        public override string ToString()
        {
            return "" + Rate * 100 + "%";
        }
    }
}