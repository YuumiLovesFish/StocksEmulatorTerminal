using System;
using System.Collections.Generic;
using System.Text;
using YLF.StocksEmulator.Repository.Users;

namespace YLF.StocksEmulator.Services
{
    public interface IUserService
    {
        User User { get; }
        void Login(string userName);
        void ExitAndSaveProgess();
        void TopUpAccount(decimal amount);
    }
}
