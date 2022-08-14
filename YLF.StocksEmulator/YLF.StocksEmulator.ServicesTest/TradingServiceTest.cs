using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLF.StocksEmulator.Repository.Stocks;
using YLF.StocksEmulator.Repository.Users;
using YLF.StocksEmulator.Services;
using FluentAssertions;
using YLF.StocksEmulator.Repository.Exceptions;
using YLF.StocksEmulator.Services.Exceptions;

namespace YLF.StocksEmulator.ServicesTest
{
    public class TradingServiceTest
    {
       
        private User _user;
        private int _originalStockOwnCount = 1;
        private CurrentStock _currentStock;

        [SetUp]
        public void SetUp()
        {
            string stockName = "aaa";
            _originalStockOwnCount = 1;
            OwnedStock ownedStock = new OwnedStock(stockName, _originalStockOwnCount);
            List<OwnedStock> listOfOwnedStock = new List<OwnedStock>();
            listOfOwnedStock.Add(ownedStock);
            decimal accountBalance = 100m;
            Portfolio portfolio = new Portfolio(accountBalance, listOfOwnedStock);
            string name = "poppy";
            _user = new User(name, portfolio);

            string currentStockName = "aaa";
            decimal currentStockPrice = 100m;
            decimal currentStockChange = 10m;
            _currentStock = new CurrentStock(currentStockName, currentStockPrice, currentStockChange);
        }
        [Test]
        public void BuyStockAsync_UserIsNull_ThrowUserIsNullException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);

            User user = null;
            string stockName= string.Empty;
            int quantity = 0;
            //act
            //assert
            Assert.ThrowsAsync<UserIsNullException>(() => tradingService.BuyStockAsync(user, stockName, quantity));

        }
        [Test]
        public void BuyStockAsync_StockNameIsNull_ThrowStockNotFoundException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = null;
            int quantity = 0;
            //act
            //assert
            Assert.ThrowsAsync<StockNotFoundException>(() => tradingService.BuyStockAsync(_user, stockName, quantity));
        }
        [Test]
        public void BuyStockAsync_StockNameIsEmpty_ThrowStockNotFoundException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = string.Empty;
            int quantity = 0;
            //act
            //assert
            Assert.ThrowsAsync<StockNotFoundException>(() => tradingService.BuyStockAsync(_user, stockName, quantity));
        }
        [Test]
        public void BuyStockAsync_QuantitySmallerthan1_ThrowStockQuantityInvalidtException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName ="aaa";
            int quantity = 0;
            //act
            //assert
            Assert.ThrowsAsync<StockQuantityInvalidtException>(() => tradingService.BuyStockAsync(_user, stockName, quantity));
        }
        [Test]
        public void BuyStockAsync_InvalidStockName_ThrowStockNotFoundException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            currentStockRepositoryMock.Setup(f=>f.GetStockAsync(It.IsAny<string>())).ThrowsAsync(new StockNotFoundException("It does not exist."));
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = "aaa";
            int quantity = 1;
            //act
            //assert
            Assert.ThrowsAsync<StockNotFoundException>(() => tradingService.BuyStockAsync(_user, stockName, quantity));
        }

        [Test]
        public async Task BuyStockAsync_CorrectInput_ReturnCurrentStock()
        {
            //arrange
       
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            currentStockRepositoryMock.Setup(f => f.GetStockAsync(It.IsAny<string>())).ReturnsAsync(_currentStock);
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = "aaa";
            int quantity = 1;
            var userStartingBalance = _user.MyPortfolio.AccountBalance;

            //act
           var stockBought=  await tradingService.BuyStockAsync(_user, stockName, quantity);
            //assert
            stockBought.Should().NotBeNull();
           var leftBalance= userStartingBalance - stockBought.Price;
            _user.MyPortfolio.AccountBalance.Should().Be(leftBalance);
            
            _user.MyPortfolio.MyStocks.SingleOrDefault(s=>s.Name==stockBought.Name)?.QuantityHold.Should().Be(_originalStockOwnCount+quantity);
            
        }
        [Test]
        public void BuyStockAsync_NotEnoughMoneyInAccount_ThrowAccountBalanceInsufficientException()
        {
            //arrange

            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            currentStockRepositoryMock.Setup(f => f.GetStockAsync(It.IsAny<string>())).ReturnsAsync(_currentStock);
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = "aaa";
            int quantity = 100;

            //act
            //assert
            Assert.ThrowsAsync<AccountBalanceInsufficientException>(() => tradingService.BuyStockAsync(_user, stockName, quantity));
        }

        ///////////////////////////////
        [Test]
        public void SellStockAsync_UserIsNull_ThrowUserIsNullException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);

            User user = null;
            string stockName = string.Empty;
            int quantity = 0;
            //act
            //assert
            Assert.ThrowsAsync<UserIsNullException>(() => tradingService.SellStcokAsync(user, stockName, quantity));

        }
        [Test]
        public void SellStcokAsync_StockNameIsNull_ThrowStockNotFoundException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = null;
            int quantity = 0;
            //act
            //assert
            Assert.ThrowsAsync<StockNotFoundException>(() => tradingService.SellStcokAsync(_user, stockName, quantity));
        }
        [Test]
        public void SellStcokAsync_StockNameIsEmpty_ThrowStockNotFoundException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = string.Empty;
            int quantity = 0;
            //act
            //assert
            Assert.ThrowsAsync<StockNotFoundException>(() => tradingService.SellStcokAsync(_user, stockName, quantity));
        }
        [Test]
        public void SellStcokAsync_QuantitySmallerthan1_ThrowStockQuantityInvalidtException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = "aaa";
            int quantity = 0;
            //act
            //assert
            Assert.ThrowsAsync<StockQuantityInvalidtException>(() => tradingService.SellStcokAsync(_user, stockName, quantity));
        }
        [Test]
        public void SellStcokAsync_InvalidStockName_ThrowStockNotFoundException()
        {
            //arrange
            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            currentStockRepositoryMock.Setup(f => f.GetStockAsync(It.IsAny<string>())).ThrowsAsync(new StockNotFoundException("It does not exist."));
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = "aaa";
            int quantity = 0;
            //act
            //assert
            Assert.ThrowsAsync<StockNotFoundException>(() => tradingService.SellStcokAsync(_user, stockName, quantity));
        }
        [Test]
        public async Task SellStcokAsync_CorrectInput_ReturnCurrentStock()
        {
            //arrange

            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            currentStockRepositoryMock.Setup(f => f.GetStockAsync(It.IsAny<string>())).ReturnsAsync(_currentStock);
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = "aaa";
            int quantity = 1;
            var userStartingBalance = _user.MyPortfolio.AccountBalance;

            //act
            var stockBought = await tradingService.SellStcokAsync(_user, stockName, quantity);
            //assert
            stockBought.Should().NotBeNull();
            var leftBalance = userStartingBalance + stockBought.Price;
            _user.MyPortfolio.AccountBalance.Should().Be(leftBalance);

            _user.MyPortfolio.MyStocks.SingleOrDefault(s => s.Name == stockBought.Name)?.QuantityHold.Should().Be(_originalStockOwnCount - quantity);

        }
        [Test]
        public void SellStcokAsync_NotStockInAccount_ThrowStockQuantityInvalidtException()
        {
            //arrange

            Mock<ICurrentStockRepository> currentStockRepositoryMock = new Mock<ICurrentStockRepository>();
            currentStockRepositoryMock.Setup(f => f.GetStockAsync(It.IsAny<string>())).ReturnsAsync(_currentStock);
            TradingService tradingService = new TradingService(currentStockRepositoryMock.Object);
            string? stockName = "aaa";
            int quantity = 100;

            //act
            //assert
            Assert.ThrowsAsync<StockQuantityInvalidtException>(() => tradingService.SellStcokAsync(_user, stockName, quantity));
        }
    }
}
