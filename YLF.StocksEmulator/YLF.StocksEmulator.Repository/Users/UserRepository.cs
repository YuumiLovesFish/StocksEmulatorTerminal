using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using YLF.StocksEmulator.Repository.Stocks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YLF.StocksEmulator.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        public User GetUser(string userName)
        {
            User user = null;
            string path = CreateFilePath(userName);
            string json = File.ReadAllText(path);
            user = JsonSerializer.Deserialize<User>(json);
            return user;
        }

        public void SaveUserFile(User user)
        {
            string path = CreateFilePath(user.Name);
            string json = JsonSerializer.Serialize<User>(user);
            File.WriteAllText(path, json);
        }
        
        public bool IfUserExist(string fileName)
        {
            var filePath = CreateFilePath(fileName);
            return File.Exists(filePath);
        }

        private string CreateFilePath(string fileName)
        {
            fileName = fileName.ToLower();

            string filePath = $"{fileName}.json";
            return filePath;
        }
    }
}
