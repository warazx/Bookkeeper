using System;
using SQLite;

namespace Bookkeeper.Models
{
    public class Entry
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public bool IsIncome { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int TypeID { get; set; }
        public int AccountID { get; set; }
        public int Total { get; set; }
        public int TaxRateID { get; set; }

        public Entry()
        {
            IsIncome = true;
            Date = DateTime.Now;
            Description = "";
            TypeID = 0;
            AccountID = 0;
            Total = 0;
            TaxRateID = 0;
        }

        public Entry(bool isIncome, DateTime date, string description, int typeID, int accountID, int total, int taxRateID)
        {
            IsIncome = isIncome;
            Date = date;
            Description = description;
            TypeID = typeID;
            AccountID = accountID;
            Total = total;
            TaxRateID = taxRateID;
        }

        public override string ToString()
        {
            return "" + Id + ": " + Total + "kr till: " + AccountID;
        }
    }
}