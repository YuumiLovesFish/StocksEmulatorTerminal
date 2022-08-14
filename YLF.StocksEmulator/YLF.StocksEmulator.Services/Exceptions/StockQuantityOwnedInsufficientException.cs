using System;
using System.Collections.Generic;
using System.Text;

namespace YLF.StocksEmulator.Services.Exceptions
{
    public class StockQuantityInvalidtException : Exception
    {
        public StockQuantityInvalidtException(string message) : base(message)
        {
        }
    }
}
