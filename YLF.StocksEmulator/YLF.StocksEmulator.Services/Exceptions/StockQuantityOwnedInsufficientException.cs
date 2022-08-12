using System;
using System.Collections.Generic;
using System.Text;

namespace YLF.StocksEmulator.Services.Exceptions
{
    class StockQuantityOwnedInsufficientException : Exception
    {
        public StockQuantityOwnedInsufficientException(string message) : base(message)
        {
        }
    }
}
