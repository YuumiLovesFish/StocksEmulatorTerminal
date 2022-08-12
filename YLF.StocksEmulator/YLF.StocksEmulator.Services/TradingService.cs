using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLF.StocksEmulator.Repository.Stocks;
using YLF.StocksEmulator.Repository.Users;
using YLF.StocksEmulator.Services.Exceptions;

namespace YLF.StocksEmulator.Services
{
    public class TradingService : ITradingService
    {
        private ICurrentStockRepository _currentStockRepository;

        public TradingService(ICurrentStockRepository currentStockRepository)
        {
            _currentStockRepository = currentStockRepository;
        }
        private static decimal GetTotalPrice(decimal stockPrice, int quantity)
        {
            return stockPrice * quantity;
        }

        public async Task BuyStockAsync(User user, string stockName, int quantity)
        {
            var currentStock = await _currentStockRepository.GetStockAsync(stockName);
            if (user.MyPortfolio.AccountBalance >= GetTotalPrice(currentStock.Price ,quantity))
            {
                user.AddStock(currentStock.Name, quantity);
                user.MyPortfolio.AccountBalance -= GetTotalPrice(currentStock.Price, quantity);
            }
            else
            {
                throw new AccountBalanceInsufficientException($"To completed the transcation you need to have at least {GetTotalPrice(currentStock.Price, quantity)} GBP in the account.");
            }

           
        }

        public async Task SellStcokAsync(User user, string stockName, int quantity)
        {
            var currentStock = await _currentStockRepository.GetStockAsync(stockName);
            var ownedStock = user.MyPortfolio.MyStocks.SingleOrDefault(s => s.Name == currentStock.Name);

            if (ownedStock == null)
            {
                throw new StockQuantityOwnedInsufficientException($"The account owned 0 shares of {currentStock.Name}, so the transcation is canceled. ");
            }
            if (ownedStock.QuantityHold < quantity)
            {
                throw new StockQuantityOwnedInsufficientException($"The account owned {ownedStock.QuantityHold} shares of {ownedStock.Name}, so the transcation is canceled. ");
            }

            user.RemoveStock(stockName, quantity);
            user.AddMoney(GetTotalPrice(currentStock.Price, quantity));


        }

        public async Task<List<CurrentStock>> CheckAllStocks()
            => await _currentStockRepository.GetAllStocksAsync();
    }
}
