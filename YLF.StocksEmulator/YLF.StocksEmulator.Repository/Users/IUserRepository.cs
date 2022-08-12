using System;
using System.Collections.Generic;


namespace YLF.StocksEmulator.Repository.Users
{
   public interface IUserRepository
    {
       
        User GetUser(string userName);
       
        void SaveUserFile(User user);
        bool IfUserExist(string fileName);

    }
}
