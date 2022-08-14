using System;
using System.Collections.Generic;
using System.Text;

namespace YLF.StocksEmulator.Repository.Stocks
{
    public class OwnedStock : Stock
    {
        public OwnedStock(string name, int quantityHold) :base(name)
        {
            QuantityHold = quantityHold;
        }

        public int QuantityHold { get; internal set; }
    }
}
