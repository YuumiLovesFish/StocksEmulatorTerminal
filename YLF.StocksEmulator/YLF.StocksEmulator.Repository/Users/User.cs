using System;
using System.Collections.Generic;
using System.Text;

namespace YLF.StocksEmulator.Repository.Users
{
    public class User
    {
        public User(string name, Portfolio myPortfolio)
        {
            Name = name;
            MyPortfolio = myPortfolio;
        }

        public string Name { get; private set; }
        public Portfolio MyPortfolio { get; private set; }
    }
}
