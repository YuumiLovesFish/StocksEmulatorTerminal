using System;
using System.Collections.Generic;
using System.Text;
using YLF.StocksEmulator.Repository.Stocks;

namespace YLF.StocksEmulator.Repository.Users
{
    public class Portfolio
    {
        public Portfolio(decimal accountBalance, List<OwnedStock> myStocks)
        {
            AccountBalance = accountBalance;
            MyStocks = myStocks;
        }

        public decimal AccountBalance { get; set; }
        public List<OwnedStock> MyStocks { get; set; }
    }
}
