using Bookkeeper.Utils;
using SQLite;

namespace Bookkeeper.Models
{
    public class Account
    {
        [PrimaryKey]
        public int Number { get; set; }
        public string Name { get; set; }        

        public AccountType AccountType { get; set; }

        public Account() { }

        public Account(string name, int number, AccountType type)
        {
            Name = name;
            Number = number;
            AccountType = type;
        }

        public override string ToString()
        {
            return "" + Number;
        }
    }
}