using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLF.StocksEmulator.Repository.Exceptions
{
    public class StockNotFoundException : Exception
    {
        public StockNotFoundException(string message) : base(message)
        {

        }
    }
}
