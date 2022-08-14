using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YLF.StocksEmulator.Repository.Stocks;

namespace YLF.StocksEmulator.Repository.Users
{
    public class User
    {
        public User(string name, Portfolio myPortfolio)
        {
            Name = name;
            MyPortfolio = myPortfolio;
        }

        public override string ToString()
        {
            return Name.ToUpper().ToString();
        }


        public string Name { get; private set; }
        public Portfolio MyPortfolio { get; private set; }

        public void AddMoney(decimal amount)
        {
            if (amount < 0)
            {
                return;
            }
            this.MyPortfolio.AccountBalance += amount;

        }

        public void AddStock(string stockName, int quantity)
        {
            if (string.IsNullOrEmpty(stockName)|| quantity < 0)
                return;

            var ownedStock = this.MyPortfolio.MyStocks.SingleOrDefault(s => s.Name == stockName);
            if (ownedStock == null)
            {
                OwnedStock newStock = new OwnedStock(stockName, quantity);


                this.MyPortfolio.MyStocks.Add(newStock);
            }
            else
            {
                ownedStock.QuantityHold += quantity;
            }

        }
        public void RemoveStock(string stockName, int quantity)
        {
            if (string.IsNullOrEmpty(stockName) || quantity < 0)
                return;

            var ownedStock = this.MyPortfolio.MyStocks.SingleOrDefault(s => s.Name == stockName);
            if (ownedStock != null)
            {
                ownedStock.QuantityHold -= quantity;
            }
        }
    }
}
