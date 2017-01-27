namespace Bookkeeper.Models
{
    public class Account
    {
        public enum Type
        {
            Income,
            Expense,
            Money
        };

        public string Name { get; set; }
        public int Number { get; set; }
        public Type AccountType { get; set; }

        public Account() { }

        public Account(string name, int number, Type type)
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