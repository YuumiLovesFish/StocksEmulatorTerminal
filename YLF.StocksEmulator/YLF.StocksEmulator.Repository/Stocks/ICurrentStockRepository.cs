using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YLF.StocksEmulator.Repository.Stocks
{
    public interface ICurrentStockRepository
    {
        Task<CurrentStock> GetStock(string stockName);
        Task<List<CurrentStock>> GetAllStocks();
    }
}
