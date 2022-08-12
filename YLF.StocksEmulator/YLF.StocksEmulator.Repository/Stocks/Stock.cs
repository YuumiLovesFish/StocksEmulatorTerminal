using System;
using System.Collections.Generic;
using System.Text;

namespace YLF.StocksEmulator.Repository.Stocks
{ 
    public class Stock
    {
        public Stock(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

       
    }
}
