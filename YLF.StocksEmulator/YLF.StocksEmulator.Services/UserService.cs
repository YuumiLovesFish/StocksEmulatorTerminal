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
        private User _user;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public void ExistAndSaveProgess()
        {
            _userRepo.SaveUserFile(_user);
        }

        public void Login(string userName)
        {
            if (_userRepo.IfUserExist(userName))
            {
               _user = _userRepo.GetUser(userName);
            }
            CreateNewUser(userName);
        }

        public void TopUpAccount(decimal amount)
            => _user.AddMoney(amount);
        

        private void CreateNewUser(string userName)
        {
            decimal startingBalance = 10000m;
            List<OwnedStock> ownedStocks = new List<OwnedStock>();
            Portfolio portfolio = new Portfolio(startingBalance, ownedStocks);
            _user = new User(userName, portfolio);
            
        }

    }
}
