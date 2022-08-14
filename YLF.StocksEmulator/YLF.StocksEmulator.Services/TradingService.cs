using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLF.StocksEmulator.Repository.Stocks;
using YLF.StocksEmulator.Repository.Users;
using YLF.StocksEmulator.Services.Exceptions;
using YLF.StocksEmulator.Repository.Exceptions;

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

        public async Task<CurrentStock> BuyStockAsync(User user, string stockName, int quantity)
        {
            if (user == null)
            {
                throw new UserIsNullException("User cannot be null");
            }
            if (string.IsNullOrEmpty(stockName))
            {
                throw new StockNotFoundException($"Stock: {stockName} cannot be empty!");
            }
            if (quantity <= 0)
            {
                throw new StockQuantityInvalidtException("Quantity must be bigger than 0.");
            }
            CurrentStock currentStock;
            
            currentStock = await _currentStockRepository.GetStockAsync(stockName);
            
            
           
            if (user.MyPortfolio.AccountBalance >= GetTotalPrice(currentStock.Price ,quantity))
            {
                user.AddStock(currentStock.Name, quantity);
                user.MyPortfolio.AccountBalance -= GetTotalPrice(currentStock.Price, quantity);
                return currentStock;
            }
            else
            {
                throw new AccountBalanceInsufficientException($"To complete the transcation you need to have at least {GetTotalPrice(currentStock.Price, quantity)} in the account.");
            }

           
        }

        public async Task<CurrentStock> SellStcokAsync(User user, string stockName, int quantity)
        {
            if (user == null)
            {
                throw new UserIsNullException("User cannot be null");
            }
            if (string.IsNullOrEmpty(stockName))
            {
                throw new StockNotFoundException($"Stock: {stockName} cannot be empty!");
            }
            if (quantity <= 0)
            {
                throw new StockQuantityInvalidtException("Quantity must be bigger than 0.");
            }
            var currentStock = await _currentStockRepository.GetStockAsync(stockName);
            var ownedStock = user.MyPortfolio.MyStocks.SingleOrDefault(s => s.Name == currentStock.Name);

            if (ownedStock == null)
            {
                throw new StockNotFoundException($" Cannot find {currentStock.Name} in the portfolio.");
            }
            if (ownedStock.QuantityHold < quantity)
            {
                throw new StockQuantityInvalidtException($"The account owns {ownedStock.QuantityHold} shares of {ownedStock.Name}, which is not enough to sell {quantity} shares, the transcation cannot be completed.");
            }

            user.RemoveStock(ownedStock.Name, quantity);
            user.AddMoney(GetTotalPrice(currentStock.Price, quantity));
            return currentStock;


        }

        public async Task<List<CurrentStock>> CheckAllStocksAsync()
            => await _currentStockRepository.GetAllStocksAsync();
    }
}
