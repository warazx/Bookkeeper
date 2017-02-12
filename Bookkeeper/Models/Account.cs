using Bookkeeper.Utils;
using SQLite;

namespace Bookkeeper.Models
{
    public class Account : Java.Lang.Object
    {
        [PrimaryKey]
        public int Number { get; set; }
        public string Name { get; set; }        
        public AccountType AccountType { get; set; }

        /// <summary>
        /// Used to create a default Account object.
        /// </summary>
        public Account() { }

        /// <summary>
        /// Used to create a specified Account object.
        /// </summary>
        public Account(string name, int number, AccountType type)
        {
            Name = name;
            Number = number;
            AccountType = type;
        }

        public override string ToString()
        {
            return Name + " (" + Number + ")";
        }
    }
}