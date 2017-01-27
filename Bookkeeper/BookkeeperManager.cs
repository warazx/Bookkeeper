using System;
using System.Collections.Generic;
using SQLite;
using Bookkeeper.Models;
using Bookkeeper.Utils;

namespace Bookkeeper
{
    public class BookkeeperManager
    {
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string fullPath = path + "/database.db";

        private static BookkeeperManager instance;

        public static BookkeeperManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookkeeperManager();
                }
                return instance;
            }
        }

        public BookkeeperManager()
        {
            using (var db = new SQLiteConnection(fullPath))
            {
                db.CreateTable<Entry>();
                db.CreateTable<Account>();
                db.CreateTable<TaxRate>();

                if(db.Table<Account>().Count() == 0)
                {
                    db.Insert(new Account("Computer", 585, AccountType.Expense));
                    db.Insert(new Account("Supplies", 631, AccountType.Expense));
                    db.Insert(new Account("Labour & Welfare", 597, AccountType.Expense));

                    db.Insert(new Account("Rental", 400, AccountType.Income));
                    db.Insert(new Account("Interest", 420, AccountType.Income));
                    db.Insert(new Account("Sales", 440, AccountType.Income));

                    db.Insert(new Account("Assets", 211, AccountType.Money));
                    db.Insert(new Account("Founds", 224, AccountType.Money));
                    db.Insert(new Account("Project", 245, AccountType.Money));
                }
                if(db.Table<TaxRate>().Count() == 0)
                {
                    db.Insert(new TaxRate(0.06));
                    db.Insert(new TaxRate(0.12));
                    db.Insert(new TaxRate(0.20));
                    db.Insert(new TaxRate(0.25));
                }
                db.Close();
            }
        }

        public void addEntry(Entry e)
        {
            using (var db = new SQLiteConnection(fullPath))
            {
                db.Insert(e);
                db.Close();
            }            
        }

        public List<Account> GetAccounts(AccountType type)
        {
            List<Account> returnList = new List<Account>();
            using (var db = new SQLiteConnection(fullPath))
            {
                var list = db.Table<Account>().Where(a => a.AccountType.Equals(type));
                returnList = ConvertToList(list);
                db.Close();
            }
            return returnList;
        }

        public List<TaxRate> GetTaxRates()
        {
            List<TaxRate> returnList = new List<TaxRate>();
            using (var db = new SQLiteConnection(fullPath))
            {
                var list = db.Table<TaxRate>();
                returnList = ConvertToList(list);
                db.Close();
            }
            return returnList;
        }

        private List<T> ConvertToList<T>(IEnumerable<T> list)
        {
            List<T> returnList = new List<T>();
            foreach (var v in list)
            {
                returnList.Add(v);
            }
            return returnList;
        }

        public TaxRate GetTaxRate(int id)
        {
            TaxRate taxRate = new TaxRate();
            using (var db = new SQLiteConnection(fullPath))
            {
                taxRate = db.Get<TaxRate>(id);
                db.Close();
            }
            return taxRate;
        }

        public List<Entry> GetEntries()
        {
            List<Entry> returnList = new List<Entry>();
            using (var db = new SQLiteConnection(fullPath))
            {
                var list = db.Table<Entry>();
                returnList = ConvertToList(list);
                db.Close();
            }
            return returnList;
        }

        public Account GetAccount(int id)
        {
            Account account;
            using (var db = new SQLiteConnection(fullPath))
            {
                account = db.Table<Account>().Where(a => a.Number.Equals(id)).First();
                db.Close();
            }
            return account;
        }
    }
}