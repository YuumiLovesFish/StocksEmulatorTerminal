using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLF.StocksEmulator.Services.Exceptions
{
    public class UserIsNullException : Exception
    {
        public UserIsNullException(string message) : base(message)
        {
        }
    }
}
