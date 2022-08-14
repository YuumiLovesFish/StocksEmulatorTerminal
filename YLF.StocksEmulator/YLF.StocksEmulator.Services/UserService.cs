using System;
using System.Collections.Generic;
using System.Text;
using YLF.StocksEmulator.Repository.Stocks;
using YLF.StocksEmulator.Repository.Users;

namespace YLF.StocksEmulator.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public User User { get; private set; }

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public void ExitAndSaveProgess()
        {
            _userRepo.SaveUserFile(User);
        } 

        public void Login(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return;
            }
            if (!_userRepo.IfUserExist(userName))
            {
                User = CreateNewUser(userName);
            }
            else
            {
                User = _userRepo.GetUser(userName);
            }         
        }

        public void TopUpAccount(decimal amount)
            => User.AddMoney(amount);
        

        private User CreateNewUser(string userName)
        {
            decimal startingBalance = 10000m;
            List<OwnedStock> ownedStocks = new List<OwnedStock>();
            Portfolio portfolio = new Portfolio(startingBalance, ownedStocks);
            return new User(userName, portfolio);
        }

    }
}
