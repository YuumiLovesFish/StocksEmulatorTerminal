using System;
using System.Collections.Generic;
using System.Text;
using YLF.StocksEmulator.Repository.Users;

namespace YLF.StocksEmulator.Services
{
    public interface IUserService
    {
        void Login(string userName);
        void ExistAndSaveProgess();
        void TopUpAccount(decimal amount);
        
    }
}
