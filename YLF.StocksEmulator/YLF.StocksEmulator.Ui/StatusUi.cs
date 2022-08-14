using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using YLF.StocksEmulator.Repository.Users;
using YLF.StocksEmulator.Services;

namespace YLF.StocksEmulator.Ui
{
    public class StatusUi
    {
        private readonly ITradingService _tradingService;
        private readonly IUserService _userService;

        public StatusUi(ITradingService tradingService, IUserService userService)
        {
            _tradingService = tradingService;
            _userService = userService;
        }
        public void Login()
        {
            Console.WriteLine("Hey!");
            Console.WriteLine("Do you mind telling me your name :)");
            Console.WriteLine("[only letters a to z; between 1 and 20]");
            Console.Write(Environment.NewLine);
            string name = Console.ReadLine().Trim();
            while(!new Regex("[a-z]{1,20}").IsMatch(name))
            {
              
                Console.WriteLine("Please enter a name that is between 1 and 20 characters");
                name = Console.ReadLine();
            }
            Console.WriteLine($"Welcome to Stock Emulator, {name}");
            Console.WriteLine($"{name}, please press Enter to see your account.");
            Console.ReadLine();


            _userService.Login(name);
        }
       
       
        public async Task RenderUserAccountAndStocksInfoAsync()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 120));
            Console.Write(Environment.NewLine);
            Console.WriteLine($"\t\t\t\t{_userService.User}\t\tAccount Balance:{Math.Round(_userService.User.MyPortfolio.AccountBalance, 2): 0.00}");
            Console.Write(Environment.NewLine);
            Console.WriteLine(new string('=', 120));
            await RenderAllStocksAsync();
        }
        private async Task RenderAllStocksAsync()
        {
            
            var stockList = await _tradingService.CheckAllStocksAsync();
            foreach (var stock in stockList)
            {
                int quantityOwned = _userService.User.MyPortfolio.MyStocks.SingleOrDefault(s => s.Name == stock.Name)?.QuantityHold ?? 0;
                int lengthString = $"{stock.Name}: {Math.Round(stock.Price, 2):0.00}, {stock.PriceChanges:0.00}%".Length;
                int spaceCount = 40 - lengthString;
                
                Console.WriteLine($"\t\t\t{stock.Name}: {Math.Round(stock.Price, 2):0.00}, {stock.PriceChanges:0.00}% {new string(' ', spaceCount)}|| Quantity Owned: {quantityOwned}");
            }
            Console.WriteLine(new string('=', 120));
           
        }
    }
}
