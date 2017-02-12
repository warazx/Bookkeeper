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

        /// <summary>
        /// Used to create a default Entry object.
        /// </summary>
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

        /// <summary>
        /// Used to create a specified Entry object.
        /// </summary>
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
            return "" + Date.ToString("yyyy-MM-dd") + " - " + Description + " : " + Total + " kr"; 
        }
    }
}