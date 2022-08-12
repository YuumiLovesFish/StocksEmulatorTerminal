﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YLF.StocksEmulator.Repository.Stocks
{
    public class CurrentStock : Stock
    {
        public CurrentStock(string name):base(name)
        {
        }

        public decimal Price { get; private set; }
        public decimal PriceChanges { get; private set; }
    }
}
