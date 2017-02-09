using System;
using System.Collections.Generic;
using SQLite;
using Bookkeeper.Models;
using Bookkeeper.Utils;
using System.Linq;

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
            }
        }

        public void addEntry(Entry e)
        {
            using (var db = new SQLiteConnection(fullPath))
            {
                db.Insert(e);
            }            
        }

        public List<Account> GetAccounts(AccountType type)
        {
            List<Account> returnList;
            using (var db = new SQLiteConnection(fullPath))
            {
                returnList = db.Table<Account>().Where(a => a.AccountType.Equals(type)).ToList();
            }
            return returnList;
        }

        public List<Account> GetAccounts()
        {
            List<Account> returnList;
            using (var db = new SQLiteConnection(fullPath))
            {
                returnList = db.Table<Account>().ToList();
            }
            return returnList;
        }

        public List<TaxRate> GetTaxRates()
        {
            List<TaxRate> returnList;
            using (var db = new SQLiteConnection(fullPath))
            {
                returnList = db.Table<TaxRate>().ToList();
            }
            return returnList;
        }

        public TaxRate GetTaxRate(int id)
        {
            TaxRate taxRate;
            using (var db = new SQLiteConnection(fullPath))
            {
                taxRate = db.Get<TaxRate>(id);
            }
            return taxRate;
        }

        public List<Entry> GetEntries()
        {
            List<Entry> returnList;
            using (var db = new SQLiteConnection(fullPath))
            {
                returnList = db.Table<Entry>().OrderBy(e => e.Date).ToList();
            }
            return returnList;
        }

        public Account GetAccount(int id)
        {
            Account account;
            using (var db = new SQLiteConnection(fullPath))
            {
                account = db.Table<Account>().Where(a => a.Number.Equals(id)).First();
            }
            return account;
        }

        public Entry GetEntry(int id)
        {
            Entry entry;
            using (var db = new SQLiteConnection(fullPath))
            {
                entry = db.Table<Entry>().Where(e => e.Id.Equals(id)).First();
            }
            return entry;
        }

        public void UpdateEntry(Entry entry)
        {
            using (var db = new SQLiteConnection(fullPath))
            {
                db.Update(entry);
            }
        }

        public string GetTaxReport()
        {
            List<Entry> entries = GetEntries();
            List<TaxRate> taxrates = GetTaxRates();
            double totalTax = 0;
            string toReturn = "";
            foreach (Entry entry in entries)
            {
                double taxRate = taxrates.Where(tr => tr.Id.Equals(entry.TaxRateID)).FirstOrDefault().Rate;
                double entryTax = (entry.Total - (entry.Total / (1 + taxRate)));
                if (entry.IsIncome)
                {
                    totalTax += entryTax;
                    toReturn += string.Format("Entry {0}: Moms: {1}\n", entry.Id, Math.Round(entryTax, 2));
                }
                else
                {
                    totalTax -= entryTax;
                    toReturn += string.Format("Entry {0}: Moms: -{1}\n", entry.Id, Math.Round(entryTax, 2));
                }
            }
            toReturn += string.Format("Rapport: Slutlig skatt = {0}", Math.Round(totalTax, 2));
            return toReturn;
        }

        public string GetAccountReport()
        {
            List<Account> accounts = GetAccounts();
            List<Entry> entries = GetEntries();
            string fullString = "";
            string accountString = "";
            foreach(Account account in accounts)
            {
                accountString += string.Format("*** {0} - ({1}) ***\n", account.Name, account.Number);
                var relatedEntries = entries.Where(e => e.TypeID.Equals(account.Number) || e.AccountID.Equals(account.Number));
                int accountTotal = 0;
                foreach(Entry entry in relatedEntries)
                {
                    if(account.AccountType == AccountType.Income)
                    {
                        accountString += string.Format("Entry {0}: Total: {1}\n", entry.Id, entry.Total);
                        accountTotal += entry.Total;
                    }
                    else if(account.AccountType == AccountType.Expense)
                    {
                        accountString += string.Format("Entry {0}: Total: -{1}\n", entry.Id, entry.Total);
                        accountTotal -= entry.Total;
                    }
                    else if(account.AccountType == AccountType.Money)
                    {
                        if (entry.IsIncome)
                        {
                            accountString += string.Format("Entry {0}: Total: {1}\n", entry.Id, entry.Total);
                            accountTotal += entry.Total;
                        }
                        else
                        {
                            accountString += string.Format("Entry {0}: Total: -{1}\n", entry.Id, entry.Total);
                            accountTotal -= entry.Total;
                        }
                    }
                }
                accountString += string.Format("*** Total: {0} ***\n\n", accountTotal);
                fullString += accountString;
                accountString = "";
            }
            return fullString;
        }
    }
}