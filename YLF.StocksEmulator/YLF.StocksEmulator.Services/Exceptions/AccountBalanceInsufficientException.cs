using System;
using System.Collections.Generic;
using System.Text;

namespace YLF.StocksEmulator.Services.Exceptions
{
    public class AccountBalanceInsufficientException : Exception
    {
        public AccountBalanceInsufficientException(string message) : base(message)
        {
            
        }

    }
}
