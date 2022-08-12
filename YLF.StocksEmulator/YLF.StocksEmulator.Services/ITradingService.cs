using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YLF.StocksEmulator.Repository.Stocks;
using YLF.StocksEmulator.Repository.Users;

namespace YLF.StocksEmulator.Services
{
    public interface ITradingService
    {

       Task BuyStockAsync(User user, string stockName, int quantity);
       Task SellStcokAsync(User user, string stockName, int quantity);
       Task<List<CurrentStock>> CheckAllStocks();

    }
}
